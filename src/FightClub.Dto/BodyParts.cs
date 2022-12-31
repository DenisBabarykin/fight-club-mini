using System;
using System.Collections.Generic;
using System.Text;

namespace FightClub.Dto
{
    /// <summary>
    /// Части тела.
    /// </summary>
    [Flags]
    public enum BodyParts
    {
        /// <summary>
        /// Ничего (дефолт).
        /// </summary>
        None = 0,

        /// <summary>
        /// Голова.
        /// </summary>
        Head = 1,

        /// <summary>
        /// Корпус.
        /// </summary>
        Body = 2,

        /// <summary>
        /// Пах.
        /// </summary>
        Groin = 4,

        /// <summary>
        /// Ноги.
        /// </summary>
        Legs = 8
    }
}
