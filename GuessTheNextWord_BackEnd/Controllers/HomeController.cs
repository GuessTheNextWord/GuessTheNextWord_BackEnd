using Microsoft.AspNetCore.Mvc;

namespace GuessTheNextWord_BackEnd.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("InitializeGame", "Game");
        }
    }
}
