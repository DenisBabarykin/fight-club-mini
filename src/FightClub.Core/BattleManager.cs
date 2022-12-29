using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightClub.Dto;

namespace FightClub.Core;

public class BattleManager : IBattleManager
{
    public  Task<Battle?> GetBattleStateAsync()
    {
        throw new NotImplementedException();
    }

    public  Task DeleteBattleAsync()
    {
        throw new NotImplementedException();
    }

    public  Task StartNewBattleAsync(BattleConfig battleConfig)
    {
        throw new NotImplementedException();
    }

    public  Task MakeSkirmishDecisionAsync(SkirmishDecision skirmishDecision)
    {
        throw new NotImplementedException();
    }

    public  Task<PlayerCurrentGlobalState> GetPlayerCurrentGlobalStateAsync(string playerName)
    {
        throw new NotImplementedException();
    }
}