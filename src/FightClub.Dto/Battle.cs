using System;
using System.Collections.Generic;
using System.Text;

namespace FightClub.Dto
{
    /// <summary>
    /// Информация про идущий бой.
    /// </summary>
    [Serializable]
    public class Battle
    {
        /// <summary>
        /// Первая команда.
        /// </summary>
        public Team TeamOne { get; set; } = new Team();

        /// <summary>
        /// Вторая команда.
        /// </summary>
        public Team TeamTwo { get; set; } = new Team();

        /// <summary>
        /// Номер раунда. После инициализации в BattleManager.InitRoundAsync() начинается с 1.
        /// </summary>
        public int Round { get; set; } = 0;

        /// <summary>
        /// Закончился ли бой. True если да, False иначе.
        /// </summary>
        public bool IsFinished { get; set; } = false;

        public Battle()
        {
            
        }

        /// <param name="teamOne">Первая команда.</param>
        /// <param name="teamTwo">Вторая команда.</param>
        /// <param name="round">Номер раунда.</param>
        /// <param name="isFinished">Закончился ли бой. True если да, False иначе.</param>
        public Battle(Team teamOne, Team teamTwo, int round, bool isFinished)
        {
            TeamOne = teamOne;
            TeamTwo = teamTwo;
            Round = round;
            IsFinished = isFinished;
        }
    }
}
