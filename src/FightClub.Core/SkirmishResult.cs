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

    public EventType PlayerOneEvent { get; }

    public Player PlayerTwo { get; }

    public int PlayerTwoInfilctedDamage { get; }

    public EventType PlayerTwoEvent { get; }

    public SkirmishResult(Player playerOne, int playerOneInflictedDamage, EventType playerOneEvent, Player playerTwo, int playerTwoInfilctedDamage, EventType playerTwoEvent)
    {
        PlayerOne = playerOne;
        PlayerOneInflictedDamage = playerOneInflictedDamage;
        PlayerOneEvent = playerOneEvent;
        PlayerTwo = playerTwo;
        PlayerTwoInfilctedDamage = playerTwoInfilctedDamage;
        PlayerTwoEvent = playerTwoEvent;
    }
}