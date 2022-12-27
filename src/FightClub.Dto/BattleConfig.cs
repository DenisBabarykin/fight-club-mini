using System;
using System.Collections.Generic;
using System.Text;

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
        public Team TeamOne { get; set; } = new Team();

        /// <summary>
        /// Вторая команда.
        /// </summary>
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
