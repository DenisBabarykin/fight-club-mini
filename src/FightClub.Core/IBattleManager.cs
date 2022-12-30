using FightClub.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightClub.Core;

public interface IBattleManager
{
    Task<Battle?> GetBattleStateAsync();

    Task DeleteBattleAsync();

    Task StartNewBattleAsync(BattleConfig battleConfig);

    Task MakeSkirmishDecisionAsync(SkirmishDecision skirmishDecision);

    Task<PlayerCurrentGlobalState> GetPlayerCurrentGlobalStateAsync(string playerName);
}