using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightClub.FileUtils;

public class JokeExtractorMock : IJokeExtractor
{
    public List<string> GetJokes()
    {
        return new List<string>()
        {
            "насвистывая Марсельезу",
            "наблюдая то же самое в любимом фильме",
            "импровизируя в связи с чрезвычайной ситуацией",
            "хвалясь своей находчивостью"
        };
    }
}