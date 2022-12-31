using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightClub.Dto;

namespace FightClub.Core;

public class PlayerAttack
{
    public Player Player { get; }

    public Player Enemy { get; }

    public BodyParts AttackerHit { get; }

    public PlayerAttack(Player player, Player enemy, BodyParts attackerHit)
    {
        Player = player;
        Enemy = enemy;
        AttackerHit = attackerHit;
    }
}