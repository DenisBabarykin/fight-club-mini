using System;
using System.Collections.Generic;
using System.Text;

namespace FightClub.Dto
{
    public class PlayerSkirmishState
    {
        public string EnemyName { get; } = "";

        public int PlayerMaxHp { get; }

        public int PlayerCurrentHp { get; }

        public int EnemyMaxHp { get; }

        public int EnemyCurrentHp { get; }

        public PlayerSkirmishState(string enemyName, int playerMaxHp, int playerCurrentHp, int enemyMaxHp, int enemyCurrentHp)
        {
            EnemyName = enemyName;
            PlayerMaxHp = playerMaxHp;
            PlayerCurrentHp = playerCurrentHp;
            EnemyMaxHp = enemyMaxHp;
            EnemyCurrentHp = enemyCurrentHp;
        }
    }
}
