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

        /// <summary>
        /// Параметры боевого движка.
        /// </summary>
        [JsonRequired]
        public FightEngineParams FightEngineParams { get; set; } = new FightEngineParams();

        public BattleConfig()
        {

        }

        /// <param name="teamOne">Первая команда.</param>
        /// <param name="teamTwo">Вторая команда.</param>
        /// <param name="fightEngineParams">Параметры боевого движка.</param>
        public BattleConfig(Team teamOne, Team teamTwo, FightEngineParams fightEngineParams)
        {
            TeamOne = teamOne;
            TeamTwo = teamTwo;
            FightEngineParams = fightEngineParams;
        }
    }
}
