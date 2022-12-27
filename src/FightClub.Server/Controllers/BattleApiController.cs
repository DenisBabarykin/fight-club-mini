using FightClub.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightClub.Server.Controllers;

[Route("api/battle")]
[ApiController]
public class BattleApiController : ControllerBase
{
    [HttpGet]
    public IActionResult GetBattle()
    {
        var battle = GetTestData();
        return battle != null ? Ok(battle) : NotFound();
    }

    [HttpDelete]
    public IActionResult StartNewBattle()
    {
        _requestsCount = 0;
        _round = 1;
        return Ok();
    }

    #region TestData

    private static Battle? GetTestData()
    {
        Battle? result = null;

        if (_requestsCount++ > 1)
        {
            result = new Battle(
                new Team(new List<Player>()
                {
                    new Player("Ден", "Дену", 1, 15, 12, 5, 5, 150, 121, new List<int>() { 1, 2, 3, 4, 5}),
                    new Player("Вика", "Вику", 2, 5, 5, 20, 5, 50, 45, new List<int>() { 6, 7, 8, 9, 10 }),
                    new Player("Макс", "Максу", 3, 6, 7, 6, 16, 60, 0, Enumerable.Range(11, 5).ToList())
                }),
                new Team(new List<Player>()
                {
                    new Player("Света", "Свету", 4, 6, 7, 6, 16, 60, 15, Enumerable.Range(16, 5).ToList()),
                    new Player("Маша", "Машу", 5, 15, 12, 5, 5, 150, 121, Enumerable.Range(21, 5).ToList()),
                    new Player("Вова", "Вову", 6, 5, 5, 20, 5, 50, 45, Enumerable.Range(26, 5).ToList())
                }),
                _round < 10 ? _round++ : _round,
                _round >= 10
            );
        }

        return result;
    }
    private static int _requestsCount = 0;
    private static int _round = 1;

    #endregion
}
