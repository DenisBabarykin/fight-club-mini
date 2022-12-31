using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightClub.Core;

internal static class ProbabilityCalculator
{
    private static readonly Random Random = new Random();

    public static bool IsEventHappened(double eventProbabilityPercent)
    {
        return Random.NextDouble() <= eventProbabilityPercent / 100;
    }
}