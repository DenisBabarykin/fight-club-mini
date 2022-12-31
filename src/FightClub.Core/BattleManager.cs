using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FightClub.Dto;

namespace FightClub.Core;

public class BattleManager : IBattleManager
{
    private readonly IFightEngine _fightEngine;
    private readonly ICommentator _commentator;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
    private readonly Dictionary<Player, List<Player>> _roundUpcomingDecisions = new Dictionary<Player, List<Player>>();

    private Battle? _battle;

    public BattleManager(IFightEngine fightEngine, ICommentator commentator)
    {
        _fightEngine = fightEngine;
        _commentator = commentator;
    }

    public async Task<Battle?> GetBattleStateAsync()
    {
        return await GetBattleCloneThreadSafelyAsync();
    }

    public async Task DeleteBattleAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            _battle = null;
            await _commentator.ResetAsync();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task StartNewBattleAsync(BattleConfig battleConfig)
    {
        await _semaphore.WaitAsync();
        try
        {
            _fightEngine.SetParams(battleConfig.FightEngineParams);
            _battle = InitBattle(battleConfig);
            await _commentator.ResetAsync();
            InitRound();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public Task MakeSkirmishDecisionAsync(SkirmishDecision skirmishDecision)
    {
        throw new NotImplementedException();
    }

    public async Task<PlayerCurrentGlobalState> GetPlayerCurrentGlobalStateAsync(string playerName)
    {
        await _semaphore.WaitAsync();
        try
        {
            var player = _battle?.GetPlayer(playerName);
            if (_battle == null || player == null)
                return new PlayerCurrentGlobalState(null, PlayerBattleState.GameNotStarted);

            Team team = _battle.GetPlayerTeam(player);

            if (_battle.IsFinished && team.IsAlive())
                return new PlayerCurrentGlobalState(null, PlayerBattleState.Win);

            if (_battle.IsFinished && !_battle.TeamOne.IsAlive() && !_battle.TeamTwo.IsAlive())
                return new PlayerCurrentGlobalState(null, PlayerBattleState.Draw);

            if (_battle.IsFinished)
                return new PlayerCurrentGlobalState(null, PlayerBattleState.Lose);

            if (!player.IsAlive())
                return new PlayerCurrentGlobalState(null, PlayerBattleState.Takedown);

            if (!_roundUpcomingDecisions[player].Any())
                return new PlayerCurrentGlobalState(null, PlayerBattleState.WaitingForNewRound);

            Player enemyPlayer = _roundUpcomingDecisions[player].First();

            return new PlayerCurrentGlobalState(new PlayerSkirmishState(enemyPlayer.Name,
                    player.MaxHp,
                    player.CurrentHp,
                    enemyPlayer.MaxHp,
                    enemyPlayer.CurrentHp), 
                PlayerBattleState.Fighting);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private void InitRound()
    {
        var _ = _battle ?? throw new NullReferenceException("Вызвана инициализация раунда когда битва не начата.");
        if (_battle.IsFinished)
            throw new Exception("Попытка проинициализировать раунд при завершенной игре!");

        _roundUpcomingDecisions.Clear();
        foreach (var player in _battle.TeamOne.Players.Concat(_battle.TeamTwo.Players).ToList())
        {
            _roundUpcomingDecisions[player] = new List<Player>();
        }

        foreach (var playerFromTeamOne in _battle.TeamOne.Players)
        {
            foreach (var playerFromTeamTwo in _battle.TeamTwo.Players)
            {
                if (playerFromTeamOne.IsAlive() && playerFromTeamTwo.IsAlive())
                {
                    _roundUpcomingDecisions[playerFromTeamOne].Add(playerFromTeamTwo);
                    _roundUpcomingDecisions[playerFromTeamTwo].Add(playerFromTeamOne);
                }
            }
        }

        _battle.Round++;
    }

    private async Task<Battle?> GetBattleCloneThreadSafelyAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            return _battle?.Clone();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private Battle InitBattle(BattleConfig battleConfig)
    {
        return new Battle(InitTeam(battleConfig.TeamOne), InitTeam(battleConfig.TeamTwo), 1, false);
    }

    private Team InitTeam(TeamConfig teamConfig)
    {
        return new Team(teamConfig.Players.Select(p => InitPlayer(p)).ToList());
    }

    private Player InitPlayer(PlayerConfig playerConfig)
    {
        return new Player(playerConfig.Name,
            playerConfig.DativeName,
            playerConfig.AvatarId,
            playerConfig.Strength,
            playerConfig.Endurance,
            playerConfig.Agility,
            playerConfig.Intuition,
            _fightEngine.CalcMaxHp(playerConfig.Endurance),
            _fightEngine.CalcMaxHp(playerConfig.Endurance),
            playerConfig.Items.ToList());
    }
}