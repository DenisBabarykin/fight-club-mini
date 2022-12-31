using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightClub.Dto;

namespace FightClub.FileUtils;

public interface IActionExtractor
{
    Dictionary<BodyParts, List<string>> GetBodyParts();
}