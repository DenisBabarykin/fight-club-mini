using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightClub.Server.Controllers;

[Route("skirmish")]
public class SkirmishController : Controller
{
    [HttpGet("{playerName}")]
    public ActionResult Index([FromRoute] string playerName)
    {
        ViewBag.PlayerName = playerName;
        ViewBag.EnemyName = "Серчик";
        ViewBag.PlayerMaxHp = 1000;
        ViewBag.PlayerCurrentHp = 350;
        ViewBag.EnemyMaxHp = 1000;
        ViewBag.EnemyCurrentHp = 350;
        ViewBag.CanFight = true;
        ViewBag.GameStarted = true;
        ViewBag.IsAlive = true;
        return View();
    }
}