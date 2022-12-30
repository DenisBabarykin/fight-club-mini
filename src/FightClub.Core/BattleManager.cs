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

    private Battle? Battle { get; set; } 

    public BattleManager(IFightEngine fightEngine, ICommentator commentator)
    {
        _fightEngine = fightEngine;
        _commentator = commentator;
    }

    public  Task<Battle?> GetBattleStateAsync()
    {
        throw new NotImplementedException();
    }

    public async Task DeleteBattleAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            Battle = null;
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
            Battle = InitBattle(battleConfig);
            await _commentator.ResetAsync();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public  Task MakeSkirmishDecisionAsync(SkirmishDecision skirmishDecision)
    {
        throw new NotImplementedException();
    }

    public  Task<PlayerCurrentGlobalState> GetPlayerCurrentGlobalStateAsync(string playerName)
    {
        throw new NotImplementedException();
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