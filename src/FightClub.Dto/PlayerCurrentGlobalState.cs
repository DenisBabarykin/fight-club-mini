using System;
using System.Collections.Generic;
using System.Text;

namespace FightClub.Dto
{
    public class PlayerCurrentGlobalState
    {
        public PlayerSkirmishState? PlayerSkirmishState { get; }
        
        public PlayerBattleState PlayerBattleState { get; }

        public PlayerCurrentGlobalState(PlayerSkirmishState? playerSkirmishState, PlayerBattleState playerBattleState)
        {
            PlayerSkirmishState = playerSkirmishState;
            PlayerBattleState = playerBattleState;
        }
    }
}
