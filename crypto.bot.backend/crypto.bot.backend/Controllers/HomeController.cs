using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace crypto.bot.backend.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var html = await System.IO.File.ReadAllTextAsync("wwwroot\\index.html").ConfigureAwait(false);
            return View((object)html);
        }
    }
}