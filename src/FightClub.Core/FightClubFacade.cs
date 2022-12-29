using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightClub.Dto;

namespace FightClub.Core;

public class FightClubFacade : IFightClubFacade
{
    private readonly IBattleManager _battleManager;
    private readonly ICommentator _commentator;

    public FightClubFacade(IBattleManager battleManager, ICommentator commentator)
    {
        _battleManager = battleManager;
        _commentator = commentator;
    }

    public async Task<Battle?> GetBattleStateAsync() => await _battleManager.GetBattleStateAsync();

    public async Task DeleteBattleAsync() => await _battleManager.DeleteBattleAsync();

    public async Task StartNewBattleAsync(BattleConfig battleConfig) =>
        await _battleManager.StartNewBattleAsync(battleConfig);

    public async Task<RoundLog?> GetLogsAsync(int round) => await _commentator.GetLogsAsync(round);

    public async Task MakeSkirmishDecisionAsync(SkirmishDecision skirmishDecision) =>
        await _battleManager.MakeSkirmishDecisionAsync(skirmishDecision);

    public async Task<PlayerCurrentGlobalState> GetPlayerCurrentGlobalStateAsync(string playerName) =>
        await _battleManager.GetPlayerCurrentGlobalStateAsync(playerName);
}