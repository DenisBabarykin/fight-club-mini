using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightClub.Dto;

namespace FightClub.Core;

public class PlayerDefense
{
    public Player Player { get; }

    public Player Enemy { get; }

    public BodyParts DefenderBlock { get; }

    public PlayerDefense(Player player, Player enemy, BodyParts defenderBlock)
    {
        Player = player;
        Enemy = enemy;
        DefenderBlock = defenderBlock;
    }
}