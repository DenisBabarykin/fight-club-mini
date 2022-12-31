using FightClub.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightClub.Core;

public interface ICommentator
{
    Task<RoundLog?> GetLogsAsync(int round);

    Task AppendRoundLogs(List<SkirmishResult> skirmishResults);

    Task ResetAsync();
}