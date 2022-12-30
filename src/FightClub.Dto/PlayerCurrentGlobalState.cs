using System;
using System.Collections.Generic;
using System.Text;

namespace FightClub.Dto
{
    public class PlayerCurrentGlobalState
    {
        public PlayerSkirmishState? PlayerSkirmishState { get; }
        
        public bool CanFight { get; }

        public bool GameStarted { get; }

        public bool IsAlive { get; }

        public bool IsWin { get; }

        public PlayerCurrentGlobalState(PlayerSkirmishState? playerSkirmishState, bool canFight, bool gameStarted, bool isAlive, bool isWin)
        {
            PlayerSkirmishState = playerSkirmishState;
            CanFight = canFight;
            GameStarted = gameStarted;
            IsAlive = isAlive;
            IsWin = isWin;
        }
    }
}
