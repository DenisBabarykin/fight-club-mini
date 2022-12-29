using FightClub.Dto;

namespace FightClub.Core;

public interface IFightClubFacade
{
    Task<Battle?> GetBattleStateAsync();

    Task DeleteBattleAsync();

    Task StartNewBattleAsync(BattleConfig battleConfig);

    Task<RoundLog?> GetLogsAsync(int round);

    Task MakeSkirmishDecisionAsync(SkirmishDecision skirmishDecision);

    Task<PlayerCurrentGlobalState> GetPlayerCurrentGlobalStateAsync(string playerName);
}
