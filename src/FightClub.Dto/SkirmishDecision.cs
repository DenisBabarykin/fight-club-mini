using System;
using System.Collections.Generic;
using System.Text;

namespace FightClub.Dto
{
    /// <summary>
    /// Решение игрока по атаке и блоку в стычке.
    /// </summary>
    [Serializable]
    public class SkirmishDecision
    {
        /// <summary>
        /// Имя игрока в именительном падеже, принимающего решение.
        /// </summary>
        public string PlayerName { get; set; } = "";

        /// <summary>
        /// Имя противника игрока в именительном падеже.
        /// </summary>
        public string EnemyName { get; set; } = "";

        /// <summary>
        /// Место удара.
        /// </summary>
        public string Hit { get; set; } = "";

        /// <summary>
        /// Место блока.
        /// </summary>
        public string Block { get; set; } = "";

        public SkirmishDecision()
        {
            
        }

        /// <param name="playerName">Имя игрока в именительном падеже, принимающего решение.</param>
        /// <param name="enemyName">Имя противника игрока в именительном падеже.</param>
        /// <param name="hit">Место удара.</param>
        /// <param name="block">Место блока.</param>
        public SkirmishDecision(string playerName, string enemyName, string hit, string block)
        {
            PlayerName = playerName;
            EnemyName = enemyName;
            Hit = hit;
            Block = block;
        }
    }
}
