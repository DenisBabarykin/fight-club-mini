using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightClub.Dto;

namespace FightClub.Core;

public class FightEngine : IFightEngine
{
    private FightEngineParams Params { get; set; } = new FightEngineParams();

    public void SetParams(FightEngineParams fightEngineParams)
    {
        Params = fightEngineParams;
    }

    public int CalcMaxHp(int endurancePoints) => Convert.ToInt32(Math.Round(endurancePoints * Params.We));

    public List<SkirmishResult> ProcessRound(List<PlayerAttack> attacks, List<PlayerDefense> defenses)
    {
        var skirmishes = BuildSkirmishes(attacks.ToList(), defenses.ToList());
        return skirmishes.Select(skirmish => ProcessSkirmish(skirmish)).ToList();
    }

    private static List<Skirmish> BuildSkirmishes(List<PlayerAttack> attacks, List<PlayerDefense> defenses)
    {
        var skirmishes = new List<Skirmish>();

        while (attacks.Any())
        {
            var playerOneAttack = attacks.First();
            attacks.Remove(playerOneAttack);
            var playerOneDefense = defenses.First(d => playerOneAttack.Player == d.Player && playerOneAttack.Enemy == d.Enemy);
            defenses.Remove(playerOneDefense);

            var playerTwoAttack =
                attacks.First(a => a.Player == playerOneAttack.Enemy && a.Enemy == playerOneAttack.Player);
            attacks.Remove(playerTwoAttack);
            var playerTwoDefense = defenses.First(d => playerTwoAttack.Player == d.Player && playerTwoAttack.Enemy == d.Enemy);
            defenses.Remove(playerTwoDefense);

            skirmishes.Add(new Skirmish(playerOneAttack.Player,
                playerOneAttack.AttackerHit,
                playerOneDefense.DefenderBlock,
                playerTwoAttack.Player,
                playerTwoAttack.AttackerHit,
                playerTwoDefense.DefenderBlock));
        }

        return skirmishes;
    }

    private SkirmishResult ProcessSkirmish(Skirmish skirmish)
    {
        Player playerOne = skirmish.PlayerOne;
        int playerOneInflictedDamage;
        EventType playerOneEvent;
        Player playerTwo = skirmish.PlayerTwo;
        int playerTwoInflictedDamage;
        EventType playerTwoEvent;

        if (skirmish.PlayerTwoBlock.HasFlag(skirmish.PlayerOneHit))
        {
            playerTwoEvent = EventType.Block;
            playerOneInflictedDamage = Convert.ToInt32(Math.Round(playerOne.Intuition * Params.Wb));
        }
        else
        {
            bool isDodge = ProbabilityCalculator.IsEventHappened(playerTwo.Agility * Params.Wa);
            if (!isDodge)
            {
                playerTwoEvent = EventType.SimpleHit;
                playerOneInflictedDamage = Convert.ToInt32(Math.Round(playerOne.Strength * Params.Ws));
            }
            else
            {
                playerTwoEvent = EventType.Dodge;
                playerOneInflictedDamage = Convert.ToInt32(Math.Round(playerOne.Intuition * Params.Wd));
                bool counterDodge = ProbabilityCalculator.IsEventHappened(playerOne.Intuition * Params.Wi);
                if (counterDodge)
                {
                    playerTwoEvent = EventType.CounteredDodge;
                    playerOneInflictedDamage = Convert.ToInt32(Math.Round(playerOne.Strength * Params.Ws));
                }
            }
        }

        if (skirmish.PlayerOneBlock.HasFlag(skirmish.PlayerTwoHit))
        {
            playerOneEvent = EventType.Block;
            playerTwoInflictedDamage = Convert.ToInt32(Math.Round(playerTwo.Intuition * Params.Wb));
        }
        else
        {
            bool isDodge = ProbabilityCalculator.IsEventHappened(playerOne.Agility * Params.Wa);
            if (!isDodge)
            {
                playerOneEvent = EventType.SimpleHit;
                playerTwoInflictedDamage = Convert.ToInt32(Math.Round(playerTwo.Strength * Params.Ws));
            }
            else
            {
                playerOneEvent = EventType.Dodge;
                playerTwoInflictedDamage = Convert.ToInt32(Math.Round(playerTwo.Intuition * Params.Wd));
                bool counterDodge = ProbabilityCalculator.IsEventHappened(playerTwo.Intuition * Params.Wi);
                if (counterDodge)
                {
                    playerOneEvent = EventType.CounteredDodge;
                    playerTwoInflictedDamage = Convert.ToInt32(Math.Round(playerTwo.Strength * Params.Ws));
                }
            }
        }

        playerOne.CurrentHp -= playerTwoInflictedDamage;
        playerOne.CurrentHp = playerOne.CurrentHp < 0 ? 0 : playerOne.CurrentHp;
        playerTwo.CurrentHp -= playerOneInflictedDamage;
        playerTwo.CurrentHp = playerTwo.CurrentHp < 0 ? 0 : playerTwo.CurrentHp;
        return new SkirmishResult(playerOne, playerOneInflictedDamage, playerOneEvent, playerTwo,
            playerTwoInflictedDamage, playerTwoEvent);
    }
}