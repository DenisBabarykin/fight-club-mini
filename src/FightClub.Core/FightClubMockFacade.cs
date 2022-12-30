using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightClub.Dto;

namespace FightClub.Core;

public class FightClubMockFacade : IFightClubFacade
{
    private readonly SemaphoreSlim _semaphore = new(1);

    private Battle? BattleState { get; set; } = new Battle(
        new Team(new List<Player>()
        {
            new Player("Ден", "Дену", 1, 15, 12, 5, 5, 150, 121, new List<int>() { 11, 21, 31, 41}),
            new Player("Вика", "Вике", 2, 5, 5, 20, 5, 50, 45, new List<int>() { 12, 22, 32, 42 }),
            new Player("Макс", "Максу", 3, 6, 7, 6, 16, 60, 0, new List<int>())
        }),
        new Team(new List<Player>()
        {
            new Player("Света", "Свете", 4, 6, 7, 6, 16, 60, 15, new List<int>() { 10, 23, 30, 40 }),
            new Player("Маша", "Маше", 5, 15, 12, 5, 5, 150, 121, new List<int>()),
            new Player("Вова", "Вове", 6, 5, 5, 20, 5, 50, 45, new List<int>() { 11, 22, 33 })
        }),
        1,
        false
    );

    private RoundLog RoundLog { get; } = new RoundLog(1, new List<SkirmishLog>()
            {
                new SkirmishLog("Ден",
                    "Света",
                    "Ден подпаляет ресницы Свете, сильно за это извиняясь. Нанесенный урон: 12. Оставшееся здоровье: 15.",
                    "Света распускает похабные шуточки про Дена, вспоминая своего тренера по данному непотребству. Нанесенный урон: 9. Оставшееся здоровье: 121."),
                new SkirmishLog("Ден",
                    "Маша",
                    "Ден подпаляет ресницы Маше, сильно за это извиняясь. Но Маша блокирует удар! Нанесенный урон: 12. Оставшееся здоровье: 15.",
                    "Маша загоняет якорь в зад Дену, уверяя что так было нужно. Нанесенный урон: 9. Оставшееся здоровье: 121."),
                new SkirmishLog("Ден",
                    "Вова",
                    "Ден подпаляет ресницы Вове, сильно за это извиняясь. Нанесенный урон: 12. Оставшееся здоровье: 15.",
                    "Вова загоняет якорь в зад Дену, уверяя что так было нужно. Ден пытался увернуться, но тщетно. Нанесенный урон: 9. Оставшееся здоровье: 121."),

                new SkirmishLog("Вика",
                    "Света",
                    "Вика подпаляет ресницы Свете, сильно за это извиняясь. Нанесенный урон: 12. Оставшееся здоровье: 15.",
                    "Света загоняет якорь в зад Вике, уверяя что так было нужно. Нанесенный урон: 9. Оставшееся здоровье: 121."),
                new SkirmishLog("Вика",
                    "Маша",
                    "Вика подпаляет ресницы Маше, сильно за это извиняясь. Но Маша блокирует удар! Нанесенный урон: 12. Оставшееся здоровье: 15.",
                    "Маша загоняет якорь в зад Вике, уверяя что так было нужно. Нанесенный урон: 9. Оставшееся здоровье: 121."),
                new SkirmishLog("Вика",
                    "Вова",
                    "Вика подпаляет ресницы Вове, сильно за это извиняясь. Нанесенный урон: 12. Оставшееся здоровье: 15.",
                    "Вова загоняет якорь в зад Вике, уверяя что так было нужно. Но Вика уворачивается! Нанесенный урон: 9. Оставшееся здоровье: 121."),

                new SkirmishLog("Макс",
                    "Света",
                    "Макс подпаляет ресницы Свете, сильно за это извиняясь. Нанесенный урон: 12. Оставшееся здоровье: 15.",
                    "Света загоняет якорь в зад Максу, уверяя что так было нужно. Нанесенный урон: 9. Оставшееся здоровье: 121."),
                new SkirmishLog("Макс",
                    "Маша",
                    "Макс подпаляет ресницы Маше, сильно за это извиняясь. Но Маша блокирует удар! Нанесенный урон: 12. Оставшееся здоровье: 15.",
                    "Маша загоняет якорь в зад Максу, уверяя что так было нужно. Нанесенный урон: 9. Оставшееся здоровье: 121."),
                new SkirmishLog("Макс",
                    "Вова",
                    "Макс подпаляет ресницы Вове, сильно за это извиняясь. Нанесенный урон: 12. Оставшееся здоровье: 15.",
                    "Вова загоняет якорь в зад Максу, уверяя что так было нужно. Но Макс уворачивается! Нанесенный урон: 9. Оставшееся здоровье: 121."),
            });

    public Task<Battle?> GetBattleStateAsync()
    {
        if (BattleState != null && BattleState.Round > 20)
        {
            BattleState.IsFinished = true;
        }
        else if (BattleState != null)
        {
            BattleState.Round++;
        }

        return Task.FromResult(BattleState);
    }

    public async Task DeleteBattleAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            BattleState = null;
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
            BattleState = new Battle(new Team(), new Team(), 1, false);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public Task<RoundLog?> GetLogsAsync(int round)
    {
        RoundLog? result = null;
        if (BattleState != null)
        {
            RoundLog.Round = round;
            result = RoundLog;
        }
        return Task.FromResult(result);
    }

    public Task MakeSkirmishDecisionAsync(SkirmishDecision skirmishDecision)
    {
        return Task.CompletedTask;
    }

    public Task<PlayerCurrentGlobalState> GetPlayerCurrentGlobalStateAsync(string playerName)
    {
        PlayerSkirmishState? state = null;
        if (BattleState != null)
        {
            state = new PlayerSkirmishState("Макс", 1000, 29, 750, 600);
        }

        return Task.FromResult(new PlayerCurrentGlobalState(state,
            true,
            BattleState != null,
            true,
            false));
    }
}