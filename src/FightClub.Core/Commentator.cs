using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightClub.Dto;

namespace FightClub.Core;

public class Commentator : ICommentator
{
    public Task<RoundLog?> GetLogsAsync(int round)
    {
        return Task.FromResult<RoundLog?>(null);
    }

    public Task ResetAsync()
    {
        return Task.CompletedTask;
    }
}