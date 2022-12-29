using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FightClub.Dto
{
    /// <summary>
    /// Игрок.
    /// </summary>
    [Serializable]
    public sealed class Player
    {
        /// <summary>
        /// Имя в именительном падеже.
        /// </summary>
        [JsonRequired]
        public string Name { get; set; } = "";

        /// <summary>
        /// Имя в дательном падеже.
        /// </summary>
        [JsonRequired]
        public string DativeName { get; set; } = "";

        /// <summary>
        /// Числовой идентификатор аватарки. Если 0 - то не задан.
        /// </summary>
        [JsonRequired]
        public int AvatarId { get; set; }

        /// <summary>
        /// Суммарные очки силы.
        /// </summary>
        [JsonRequired]
        public int Strength { get; set; }

        /// <summary>
        /// Суммарные очки выносливости.
        /// </summary>
        [JsonRequired]
        public int Endurance { get; set; }

        /// <summary>
        /// Суммарные очки ловкости.
        /// </summary>
        [JsonRequired]
        public int Agility { get; set; }

        /// <summary>
        /// Суммарные очки интуиции.
        /// </summary>
        [JsonRequired]
        public int Intuition { get; set; }

        /// <summary>
        /// Первоначальное (максимальное) количество здоровья.
        /// </summary>
        [JsonRequired]
        public int MaxHp { get; set; }

        /// <summary>
        /// Текущее количество здоровья. Уменьшается в процессе боя. Если 0 или меньше, то Игрок уже в нокауте.
        /// </summary>
        [JsonRequired]
        public int CurrentHp { get; set; }

        /// <summary>
        /// Коллекция с числовыми идентификаторами купленных вещей. Если вещей нет, то коллекция пустая.
        /// </summary>
        [JsonRequired]
        public List<int> Items { get; set; } = new List<int>();

        public Player()
        {
            
        }

        /// <param name="name">Имя в именительном падеже.</param>
        /// <param name="dativeName">Имя в дательном падеже.</param>
        /// <param name="avatarId">Числовой идентификатор аватарки. Если 0 - то не задан.</param>
        /// <param name="strength">Суммарные очки силы.</param>
        /// <param name="endurance">Суммарные очки выносливости.</param>
        /// <param name="agility">Суммарные очки ловкости.</param>
        /// <param name="intuition">Суммарные очки интуиции.</param>
        /// <param name="maxHp">Первоначальное (максимальное) количество здоровья.</param>
        /// <param name="currentHp">Текущее количество здоровья. Уменьшается в процессе боя. Если 0 или меньше, то Игрок уже в нокауте.</param>
        /// <param name="items">Коллекция с числовыми идентификаторами купленных вещей. Если вещей нет, то коллекция пустая.</param>
        public Player(string name, string dativeName, int avatarId, int strength, int endurance, int agility, int intuition, int maxHp, int currentHp, List<int> items)
        {
            Name = name;
            DativeName = dativeName;
            AvatarId = avatarId;
            Strength = strength;
            Endurance = endurance;
            Agility = agility;
            Intuition = intuition;
            MaxHp = maxHp;
            CurrentHp = currentHp;
            Items = items;
        }
    }
}
