using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightClub.Dto;

namespace FightClub.Core;

public class Defense
{
    public Player Attacker { get; }

    public Player Defender { get; }

    public BodyParts DefenderBlock { get; }

    public Defense(Player attacker, Player defender, BodyParts defenderBlock)
    {
        Attacker = attacker;
        Defender = defender;
        DefenderBlock = defenderBlock;
    }
}