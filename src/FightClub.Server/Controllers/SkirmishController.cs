using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FightClub.Server.Controllers;

[Route("skirmish")]
public class SkirmishController : Controller
{
    [HttpGet("{mainPlayerName}")]
    public ActionResult Index([FromRoute] string mainPlayerName)
    {
        ViewBag.PlayerName = mainPlayerName;
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