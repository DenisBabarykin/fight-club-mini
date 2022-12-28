using FightClub.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightClub.Server.Controllers;

[Route("api/battle")]
[ApiController]
public class BattleApiController : ControllerBase
{
    private readonly Mock _mock;

    public BattleApiController(Mock mock)
    {
        _mock = mock;
    }

    [HttpGet]
    public IActionResult GetBattle()
    {
        var battle = _mock.GetBattleTestData();
        return battle != null ? Ok(battle) : NotFound();
    }

    [HttpDelete]
    public IActionResult StartNewBattle()
    {
        _mock.Reset();
        return Ok();
    }
}
