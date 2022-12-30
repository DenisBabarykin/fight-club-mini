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
        var headActions = new List<string>()
        {
            "отрывает ухо",
            "бреет брови"
        };

        var bodyActions = new List<string>()
        {
            "тыкает в пупок",
            "мнёт бока"
        };

        var groinActions = new List<string>()
        {
            "исследует булки",
            "кусает за жопу"
        };

        var legsActions = new List<string>()
        {
            "щекотит левую ногу",
            "сажает на шпагат"
        };

        var dict = new Dictionary<BodyParts, List<string>>();
        dict[BodyParts.Head] = headActions;
        dict[BodyParts.Body] = bodyActions;
        dict[BodyParts.Groin] = groinActions;
        dict[BodyParts.Legs] = legsActions;

        return dict;
    }
}