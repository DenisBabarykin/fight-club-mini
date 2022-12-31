using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightClub.Dto;

namespace FightClub.Core;

public class SkirmishResult
{
    public Player PlayerOne { get; }

    public int PlayerOneInflictedDamage { get; }

    public BodyParts PlayerOneBodyHit { get; }

    public EventType PlayerOneEvent { get; }

    public Player PlayerTwo { get; }

    public int PlayerTwoInflictedDamage { get; }

    public BodyParts PlayerTwoBodyHit { get; }

    public EventType PlayerTwoEvent { get; }

    public SkirmishResult(Player playerOne, int playerOneInflictedDamage, BodyParts playerOneBodyHit, EventType playerOneEvent, Player playerTwo, int playerTwoInfilctedDamage, BodyParts playerTwoBodyHit, EventType playerTwoEvent)
    {
        PlayerOne = playerOne;
        PlayerOneInflictedDamage = playerOneInflictedDamage;
        PlayerOneBodyHit = playerOneBodyHit;
        PlayerOneEvent = playerOneEvent;
        PlayerTwo = playerTwo;
        PlayerTwoInflictedDamage = playerTwoInfilctedDamage;
        PlayerTwoBodyHit = playerTwoBodyHit;
        PlayerTwoEvent = playerTwoEvent;
    }
}