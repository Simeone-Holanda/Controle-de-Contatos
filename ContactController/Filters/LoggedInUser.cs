using CursoYoutubeProgramadorTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace CursoYoutubeProgramadorTech.Filters
{
    public class LoggedInUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? userSession = context.HttpContext.Session.GetString("sessionUserLoggedin");

            if (string.IsNullOrEmpty(userSession))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Login" }, { "action", "Index"}
                });
            }
            else
            {
                UserModel? user = JsonConvert.DeserializeObject<UserModel>(userSession);

                if (user == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                                    {
                                        { "controller", "Login" }, { "action", "Index"}
                                    });
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
