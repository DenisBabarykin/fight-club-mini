using System;
using System.Collections.Generic;
using System.Text;

namespace FightClub.Dto
{
    /// <summary>
    /// Команда.
    /// </summary>
    [Serializable]
    public sealed class Team
    {
        public List<Player> Players { get; set; } = new List<Player>();

        public Team()
        {
            
        }

        public Team(List<Player> players)
        {
            Players = players;
        }
    }
}
