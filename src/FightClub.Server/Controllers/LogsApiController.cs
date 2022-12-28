using System.ComponentModel.DataAnnotations;
using FightClub.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightClub.Server.Controllers;

[Route("api/logs")]
[ApiController]
public class LogsApiController : ControllerBase
{
    private readonly Mock _mock;

    public LogsApiController(Mock mock)
    {
        _mock = mock;
    }

    [HttpGet("{round:int}")]
    public IActionResult GetRoundLog([FromRoute][Required] int round)
    {
        var roundLog = _mock.GetLogsTestData(round);
        return roundLog != null ? Ok(roundLog) : NotFound();
    }
}
