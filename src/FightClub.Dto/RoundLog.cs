using System;
using System.Collections.Generic;
using System.Text;

namespace FightClub.Dto
{
    /// <summary>
    /// Общий лог целого раунда. Содержит логи всех стычек (skirmish).
    /// </summary>
    [Serializable]
    public class RoundLog
    {
        /// <summary>
        /// Номер раунда.
        /// </summary>
        public int Round { get; set; }

        /// <summary>
        /// Логи стычек в рамках данного раунда.
        /// </summary>
        public List<SkirmishLog> SkirmishLogs { get; set; } = new List<SkirmishLog>();

        public RoundLog()
        {
            
        }

        /// <param name="round">Номер раунда.</param>
        /// <param name="skirmishLogs">Логи стычек в рамках данного раунда.</param>
        public RoundLog(int round, List<SkirmishLog> skirmishLogs)
        {
            Round = round;
            SkirmishLogs = skirmishLogs;
        }
    }
}
