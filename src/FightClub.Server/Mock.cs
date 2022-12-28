﻿using FightClub.Dto;

namespace FightClub.Server;

public class Mock
{
    private int _requestsCount = 0;
    private int _round = 1;

    public Battle? GetBattleTestData()
    {
        Battle? result = null;

        if (_requestsCount++ > 1)
        {
            result = new Battle(
                new Team(new List<Player>()
                {
                    new Player("Ден", "Дену", 1, 15, 12, 5, 5, 150, 121, new List<int>() { 1, 2, 3, 4, 5}),
                    new Player("Вика", "Вике", 2, 5, 5, 20, 5, 50, 45, new List<int>() { 6, 7, 8, 9, 10 }),
                    new Player("Макс", "Максу", 3, 6, 7, 6, 16, 60, 0, Enumerable.Range(11, 5).ToList())
                }),
                new Team(new List<Player>()
                {
                    new Player("Света", "Свете", 4, 6, 7, 6, 16, 60, 15, Enumerable.Range(16, 5).ToList()),
                    new Player("Маша", "Маше", 5, 15, 12, 5, 5, 150, 121, Enumerable.Range(21, 5).ToList()),
                    new Player("Вова", "Вове", 6, 5, 5, 20, 5, 50, 45, Enumerable.Range(26, 5).ToList())
                }),
                _round < 10 ? _round++ : _round,
                _round >= 10
            );
        }

        return result;
    }

    public void Reset()
    {
        _requestsCount = 0;
        _round = 1;
    }

    public RoundLog? GetLogsTestData(int round)
    {
        RoundLog? result = null;

        if (_requestsCount++ > 1 && round <= _round)
        {
            result = new RoundLog(round, new List<SkirmishLog>()
            {
                new SkirmishLog("Ден", 
                    "Света", 
                    "Ден подпаляет ресницы Свете, сильно за это извиняясь. Нанесенный урон: 12. Оставшееся здоровье: 15.", 
                    "Света загоняет якорь в зад Дену, уверяя что так было нужно. Нанесенный урон: 9. Оставшееся здоровье: 121."),
                new SkirmishLog("Ден",
                    "Маша",
                    "Ден подпаляет ресницы Маше, сильно за это извиняясь. Но Маша блокирует удар! Нанесенный урон: 12. Оставшееся здоровье: 15.",
                    "Маша загоняет якорь в зад Дену, уверяя что так было нужно. Нанесенный урон: 9. Оставшееся здоровье: 121."),
                new SkirmishLog("Ден",
                    "Вова",
                    "Ден подпаляет ресницы Вове, сильно за это извиняясь. Нанесенный урон: 12. Оставшееся здоровье: 15.",
                    "Вова загоняет якорь в зад Дену, уверяя что так было нужно. Но Ден уворачивается! Нанесенный урон: 9. Оставшееся здоровье: 121."),

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
        }

        return result;
    }
}