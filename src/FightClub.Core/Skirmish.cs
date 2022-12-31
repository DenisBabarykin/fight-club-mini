using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightClub.Dto;

namespace FightClub.Core;

internal class Skirmish
{
    public Player PlayerOne { get; }

    public BodyParts PlayerOneHit { get; }

    public BodyParts PlayerOneBlock { get; }

    public Player PlayerTwo { get; }

    public BodyParts PlayerTwoHit { get; }

    public BodyParts PlayerTwoBlock { get; }

    public Skirmish(Player playerOne, BodyParts playerOneHit, BodyParts playerOneBlock, Player playerTwo, BodyParts playerTwoHit, BodyParts playerTwoBlock)
    {
        PlayerOne = playerOne;
        PlayerOneHit = playerOneHit;
        PlayerOneBlock = playerOneBlock;
        PlayerTwo = playerTwo;
        PlayerTwoHit = playerTwoHit;
        PlayerTwoBlock = playerTwoBlock;
    }
}