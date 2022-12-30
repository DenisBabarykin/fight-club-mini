using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightClub.Dto
{
    /// <summary>
    /// Конфиг команды.
    /// </summary>
    [Serializable]
    public sealed class TeamConfig
    {
        /// <summary>
        /// Коллекция игроков в команде.
        /// </summary>
        [JsonRequired]
        public List<PlayerConfig> Players { get; set; } = new List<PlayerConfig>();

        public TeamConfig()
        {
            
        }

        /// <param name="players">Коллекция игроков в команде.</param>
        public TeamConfig(List<PlayerConfig> players)
        {
            Players = players;
        }
    }
}
