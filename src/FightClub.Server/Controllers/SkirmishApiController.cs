using FightClub.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightClub.Server.Controllers;

[Route("api/skirmish")]
[ApiController]
public class SkirmishApiController : ControllerBase
{
    [HttpPost]
    public IActionResult PostSkirmishDecision([FromBody] SkirmishDecision skirmishDecision)
    {

        return Ok();
    }
}