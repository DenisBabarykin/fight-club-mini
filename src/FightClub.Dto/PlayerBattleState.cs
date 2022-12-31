using System;
using System.Collections.Generic;
using System.Text;

namespace FightClub.Dto
{
    public enum PlayerBattleState
    {
        GameNotStarted = 0,
        Fighting = 1,
        WaitingForNewRound = 2,
        Takedown = 3,
        Win = 4,
        Lose = 5,
        Draw = 6,
    }
}
