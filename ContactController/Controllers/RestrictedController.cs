using CursoYoutubeProgramadorTech.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CursoYoutubeProgramadorTech.Controllers
{
    [LoggedInUser]
    public class RestrictedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
