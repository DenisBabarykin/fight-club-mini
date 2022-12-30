using FightClub.Core;
using FightClub.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightClub.Server.Controllers;

[Route("api/skirmish")]
[ApiController]
public class SkirmishApiController : ControllerBase
{
    private readonly IFightClubFacade _fightClubFacade;
    private readonly ILogger<SkirmishApiController>? _logger;

    public SkirmishApiController(IFightClubFacade fightClubFacade, ILogger<SkirmishApiController>? logger)
    {
        _fightClubFacade = fightClubFacade;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> PostSkirmishDecision([FromBody] SkirmishDecision skirmishDecision)
    {
        await _fightClubFacade.MakeSkirmishDecisionAsync(skirmishDecision);

        return Ok();
    }
}