using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace FightClub.Dto
{
    /// <summary>
    /// Параметры движка боя.
    /// </summary>
    [Serializable]
    public class FightEngineParams
    {
        /// <summary>
        /// Коэффициент силы.
        /// </summary>
        [JsonRequired]
        public double Ws { get; set; } = 1;

        /// <summary>
        /// Коэффициент выносливости.
        /// </summary>
        [JsonRequired]
        public double We { get; set; } = 10;

        /// <summary>
        /// Коэффициент ловкости.
        /// </summary>
        [JsonRequired] 
        public double Wa { get; set; } = 3;

        /// <summary>
        /// Коэффициент интуиции.
        /// </summary>
        [JsonRequired] 
        public double Wi { get; set; } = 2.5;

        /// <summary>
        /// Базовый урон по блоку.
        /// </summary>
        [JsonRequired]
        public double Wb { get; set; } = 1;

        /// <summary>
        /// Базовый урон по увороту.
        /// </summary>
        [JsonRequired]
        public double Wd { get; set; } = 1;

        public FightEngineParams()
        {
            
        }

        /// <param name="ws">Коэффициент силы.</param>
        /// <param name="we">Коэффициент выносливости.</param>
        /// <param name="wa">Коэффициент ловкости.</param>
        /// <param name="wi">Коэффициент интуиции.</param>
        /// <param name="wb">Базовый урон по блоку.</param>
        /// <param name="wd">Базовый урон по увороту.</param>
        public FightEngineParams(double ws, double we, double wa, double wi, double wb, double wd)
        {
            Ws = ws;
            We = we;
            Wa = wa;
            Wi = wi;
            Wb = wb;
            Wd = wd;
        }
    }
}
