using System.ComponentModel.DataAnnotations;
using FightClub.Core;
using FightClub.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightClub.Server.Controllers;

[Route("api/logs")]
[ApiController]
public class LogsApiController : ControllerBase
{
    private readonly IFightClubFacade _fightClubFacade;
    private readonly ILogger<LogsApiController>? _logger;

    public LogsApiController(IFightClubFacade fightClubFacade, ILogger<LogsApiController>? logger)
    {
        _fightClubFacade = fightClubFacade;
        _logger = logger;
    }

    [HttpGet("{round:int}")]
    public async Task<IActionResult> GetRoundLog([FromRoute][Required] int round)
    {
        var roundLog = await _fightClubFacade.GetLogsAsync(round);
        return roundLog != null ? Ok(roundLog) : NotFound();
    }
}
