using System;
using System.Collections.Generic;
using System.Text;

namespace FightClub.Dto
{
    /// <summary>
    /// Лог стычки между двумя игроками в рамках одного раунда.
    /// </summary>
    [Serializable]
    public class SkirmishLog
    {
        /// <summary>
        /// Имя игрока из первой команды в именительном падеже.
        /// </summary>
        public string PlayerOneName { get; set; } = "";

        /// <summary>
        /// Имя игрока из второй команды в именительном падеже.
        /// </summary>
        public string PlayerTwoName { get; set; } = "";

        /// <summary>
        /// Текст результатов атаки первого игрока на второго для комментатора.
        /// </summary>
        public string PlayerOneAttackText { get; set; } = "";

        /// <summary>
        /// Текст результатов атаки второго игрока на первого для комментатора.
        /// </summary>
        public string PlayerTwoAttackText { get; set; } = "";

        public SkirmishLog()
        {
            
        }

        /// <param name="playerOneName">Имя игрока из первой команды в именительном падеже.</param>
        /// <param name="playerTwoName">Имя игрока из второй команды в именительном падеже.</param>
        /// <param name="playerOneAttackText">Текст результатов атаки первого игрока на второго для комментатора.</param>
        /// <param name="playerTwoAttackText">Текст результатов атаки второго игрока на первого для комментатора.</param>
        public SkirmishLog(string playerOneName, string playerTwoName, string playerOneAttackText, string playerTwoAttackText)
        {
            PlayerOneName = playerOneName;
            PlayerTwoName = playerTwoName;
            PlayerOneAttackText = playerOneAttackText;
            PlayerTwoAttackText = playerTwoAttackText;
        }
    }
}
