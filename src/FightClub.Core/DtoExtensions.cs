using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FightClub.Dto;

namespace FightClub.Core;

public static class DtoExtensions
{
    public static Battle Clone(this Battle battle)
    {
        return new Battle(battle.TeamOne.Clone(), battle.TeamTwo.Clone(), battle.Round, battle.IsFinished);
    }

    public static Team Clone(this Team team)
    {
        return new Team(team.Players.Select(p => p.Clone()).ToList());
    }

    public static Player Clone(this Player player)
    {
        return new Player(player.Name,
            player.DativeName,
            player.AvatarId,
            player.Strength,
            player.Endurance,
            player.Agility,
            player.Intuition,
            player.MaxHp,
            player.CurrentHp,
            player.Items.ToList());
    }

    public static Player? GetPlayer(this Battle battle, string playerName)
    {
        var player = battle.TeamOne.Players.FirstOrDefault(p => p.Name == playerName);
        if (player == null)
        {
            player = battle.TeamTwo.Players.FirstOrDefault(p => p.Name == playerName);
        }

        return player;
    }
}