using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightClub.Dto;

namespace FightClub.FileUtils;

public class ActionExtractorMock : IActionExtractor
{
    public Dictionary<BodyParts, List<string>> GetBodyParts()
    {
        return new Dictionary<BodyParts, List<string>>
        {
            [BodyParts.Head] = new List<string>()
            {
                "отрывает ухо",
                "бреет брови"
            },
            [BodyParts.Body] = new List<string>()
            {
                "тыкает в пупок",
                "мнёт бока"
            },
            [BodyParts.Groin] = new List<string>()
            {
                "исследует булки",
                "кусает за жопу"
            },
            [BodyParts.Legs] = new List<string>()
            {
                "щекотит левую ногу",
                "сажает на шпагат"
            }
        };
    }
}