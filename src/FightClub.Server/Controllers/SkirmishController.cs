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
        ViewBag.CanFight = new Random().Next(0, 3) == 1;
        ViewBag.GameStarted = new Random().Next(0, 2) == 1;
        ViewBag.IsAlive = new Random().Next(0, 3) == 1;
        return View();
    }

    // POST: SkirmishController/Edit/5
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Edit(int id, IFormCollection collection)
    //{
    //    try
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //    catch
    //    {
    //        return View();
    //    }
    //}
}