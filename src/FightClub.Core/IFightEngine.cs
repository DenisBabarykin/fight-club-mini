using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightClub.Dto;

namespace FightClub.Core;

public interface IFightEngine
{
    public void SetParams(FightEngineParams fightEngineParams);

    public int CalcMaxHp(int endurancePoints);
}