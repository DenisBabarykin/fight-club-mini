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
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

    private Battle? Battle { get; set; } 

    public BattleManager(IFightEngine fightEngine)
    {
        _fightEngine = fightEngine;
    }

    public  Task<Battle?> GetBattleStateAsync()
    {
        throw new NotImplementedException();
    }

    public  Task DeleteBattleAsync()
    {
        throw new NotImplementedException();
    }

    public async Task StartNewBattleAsync(BattleConfig battleConfig)
    {
        await _semaphore.WaitAsync();
        try
        {
            _fightEngine.SetParams(battleConfig.FightEngineParams);
            Battle = InitBattle(battleConfig);

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

    private static Battle InitBattle(BattleConfig battleConfig)
    {
        throw new NotImplementedException();
    }
}