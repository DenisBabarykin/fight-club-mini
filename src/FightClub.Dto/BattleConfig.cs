using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace FightClub.Dto
{
    /// <summary>
    /// Конфиг, инициализирующий битву.
    /// </summary>
    [Serializable]
    public class BattleConfig
    {
        /// <summary>
        /// Первая команда.
        /// </summary>
        [JsonRequired]
        public Team TeamOne { get; set; } = new Team();

        /// <summary>
        /// Вторая команда.
        /// </summary>
        [JsonRequired]
        public Team TeamTwo { get; set; } = new Team();

        public BattleConfig()
        {

        }

        /// <param name="teamOne">Первая команда.</param>
        /// <param name="teamTwo">Вторая команда.</param>
        public BattleConfig(Team teamOne, Team teamTwo)
        {
            TeamOne = teamOne;
            TeamTwo = teamTwo;
        }
    }
}
