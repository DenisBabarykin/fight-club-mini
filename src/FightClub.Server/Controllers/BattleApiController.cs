using FightClub.Core;
using FightClub.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightClub.Server.Controllers;

[Route("api/battle")]
[ApiController]
public class BattleApiController : ControllerBase
{
    private readonly IFightClubFacade _fightClubFacade;
    private readonly ILogger<BattleApiController>? _logger;

    public BattleApiController(IFightClubFacade fightClubFacade, ILogger<BattleApiController>? logger)
    {
        _fightClubFacade = fightClubFacade;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetBattle()
    {
        var battle = await _fightClubFacade.GetBattleStateAsync();
        return battle != null ? Ok(battle) : NotFound();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBattle()
    {
        await _fightClubFacade.DeleteBattleAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> StartNewBattle([FromBody] BattleConfig battleConfig)
    {
        await _fightClubFacade.StartNewBattleAsync(battleConfig);
        return Ok();
    }
}
