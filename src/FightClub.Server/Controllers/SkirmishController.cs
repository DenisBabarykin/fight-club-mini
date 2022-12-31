using FightClub.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightClub.Server.Controllers;

[Route("skirmish")]
public class SkirmishController : Controller
{
    private readonly IFightClubFacade _fightClubFacade;
    private readonly ILogger<SkirmishController>? _logger;

    public SkirmishController(IFightClubFacade fightClubFacade, ILogger<SkirmishController>? logger)
    {
        _fightClubFacade = fightClubFacade;
        _logger = logger;
    }

    [HttpGet("{playerName}")]
    public async Task<ActionResult> Index([FromRoute] string playerName)
    {
        var state = await _fightClubFacade.GetPlayerCurrentGlobalStateAsync(playerName);

        ViewBag.PlayerBattleState = state.PlayerBattleState;
        ViewBag.PlayerName = playerName;

        if (state.PlayerSkirmishState != null)
        {
            ViewBag.EnemyName = state.PlayerSkirmishState.EnemyName;
            ViewBag.PlayerMaxHp = state.PlayerSkirmishState.PlayerMaxHp;
            ViewBag.PlayerCurrentHp = state.PlayerSkirmishState.PlayerCurrentHp;
            ViewBag.EnemyMaxHp = state.PlayerSkirmishState.EnemyMaxHp;
            ViewBag.EnemyCurrentHp = state.PlayerSkirmishState.EnemyCurrentHp;
        }
        
        return View();
    }
}