using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightClub.Dto;
using FightClub.FileUtils;

namespace FightClub.Core;

public class Commentator : ICommentator
{
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
    private readonly List<RoundLog> _roundLogs = new List<RoundLog>();
    private readonly Random _random = new Random();

    private readonly IJokeExtractor _jokeExtractor;
    private readonly IActionExtractor _actionExtractor;

    public Commentator(IJokeExtractor jokeExtractor, IActionExtractor actionExtractor)
    {
        _jokeExtractor = jokeExtractor;
        _actionExtractor = actionExtractor;
    }

    public async Task<RoundLog?> GetLogsAsync(int round)
    {
        await _semaphore.WaitAsync();
        try
        {
            return _roundLogs.FirstOrDefault(l => l.Round == round);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task AppendRoundLogs(List<SkirmishResult> skirmishResults)
    {
        await _semaphore.WaitAsync();
        try
        {
            int round = _roundLogs.Any() ? _roundLogs.Max(l => l.Round) + 1 : 1;
            List<SkirmishLog> skirmishLogs = new List<SkirmishLog>();
            foreach (var skirmishResult in skirmishResults)
            {
                string eventOne = "";
                if (skirmishResult.PlayerTwoEvent == EventType.Block)
                    eventOne = $" Но {skirmishResult.PlayerTwo.Name} заблокировал удар!";

                if (skirmishResult.PlayerTwoEvent == EventType.Dodge)
                    eventOne = $" Но {skirmishResult.PlayerTwo.Name} увернулся!";

                if (skirmishResult.PlayerTwoEvent == EventType.CounteredDodge)
                    eventOne = $"{skirmishResult.PlayerTwo.Name} пытался увернуться, но {skirmishResult.PlayerOne.Name} предугадал!";


                string eventTwo = "";
                if (skirmishResult.PlayerOneEvent == EventType.Block)
                    eventTwo = $" Но {skirmishResult.PlayerOne.Name} заблокировал его удар!";

                if (skirmishResult.PlayerOneEvent == EventType.Dodge)
                    eventTwo = $" Но {skirmishResult.PlayerOne.Name} увернулся!";

                if (skirmishResult.PlayerOneEvent == EventType.CounteredDodge)
                    eventTwo = $" {skirmishResult.PlayerOne.Name} пытался увернуться, но {skirmishResult.PlayerTwo.Name} предугадал!";

                skirmishLogs.Add(new SkirmishLog(skirmishResult.PlayerOne.Name,
                    skirmishResult.PlayerTwo.Name,
                    $"{skirmishResult.PlayerOne.Name} {GetAction(skirmishResult.PlayerOneBodyHit)} {skirmishResult.PlayerTwo.DativeName}, {GetJoke()}.{eventOne} Нанесено урона: {skirmishResult.PlayerOneInflictedDamage}. Оставшееся здоровье {skirmishResult.PlayerTwo.CurrentHp}.",
                    $"{skirmishResult.PlayerTwo.Name} {GetAction(skirmishResult.PlayerTwoBodyHit)} {skirmishResult.PlayerOne.DativeName}, {GetJoke()}.{eventTwo} Нанесено урона: {skirmishResult.PlayerTwoInflictedDamage}. Оставшееся здоровье {skirmishResult.PlayerOne.CurrentHp}."));
            }

            _roundLogs.Add(new RoundLog(round, skirmishLogs));
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task ResetAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            _roundLogs.Clear();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private string GetAction(BodyParts bodyPart)
    {
        string action = _actionExtractor.GetBodyParts()[bodyPart].First();
        try
        {
            List<string> strings = _actionExtractor.GetBodyParts()[bodyPart];
            action = strings[_random.Next(strings.Count)];
        }
        catch
        {
            
        }
        
        return action;
    }

    private string GetJoke()
    {
        string joke = _jokeExtractor.GetJokes().First();
        try
        {
            List<string> strings = _jokeExtractor.GetJokes();
            joke = strings[_random.Next(strings.Count)];
        }
        catch
        {

        }

        return joke;
    }
}