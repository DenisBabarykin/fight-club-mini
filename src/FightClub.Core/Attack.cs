using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightClub.Dto;

namespace FightClub.Core;

public class Attack
{
    public Player Attacker { get; }

    public Player Defender { get; }

    public BodyParts AttackerHit { get; }

    public BodyParts DefenderBlock { get; set; }

    public Attack(Player attacker, Player defender, BodyParts attackerHit)
    {
        Attacker = attacker;
        Defender = defender;
        AttackerHit = attackerHit;
    }
}