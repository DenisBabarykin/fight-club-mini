using Newtonsoft.Json;
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
        /// <summary>
        /// Коллекция игроков в команде.
        /// </summary>
        [JsonRequired]
        public List<Player> Players { get; set; } = new List<Player>();

        public Team()
        {
            
        }

        /// <param name="players">Коллекция игроков в команде.</param>
        public Team(List<Player> players)
        {
            Players = players;
        }
    }
}
