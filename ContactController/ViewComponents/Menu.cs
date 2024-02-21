using CursoYoutubeProgramadorTech.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CursoYoutubeProgramadorTech.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string? userSession = HttpContext.Session.GetString("sessionUserLoggedin");
            if (string.IsNullOrWhiteSpace(userSession)) return null;

            UserModel? user = JsonConvert.DeserializeObject<UserModel>(userSession);

            return View(user);
        }
    }
}
