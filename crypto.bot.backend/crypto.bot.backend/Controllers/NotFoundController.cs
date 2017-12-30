using Microsoft.AspNetCore.Mvc;

namespace crypto.bot.backend.Controllers
{
    
    public class NotFoundController : Controller
    {
        //[Route("{*url}", Order = 999)]
        public IActionResult CatchAll()
        {
            return View();
        }
    }
}