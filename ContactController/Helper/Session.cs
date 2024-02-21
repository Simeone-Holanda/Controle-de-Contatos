using CursoYoutubeProgramadorTech.Models;
using Newtonsoft.Json;

namespace CursoYoutubeProgramadorTech.Helper
{
    public class Session : ISessionUser
    {
        private readonly IHttpContextAccessor _httpContext;

        public Session(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public void CreateUserSession(UserModel user)
        {
            string value = JsonConvert.SerializeObject(user);
            _httpContext?.HttpContext?.Session.SetString("sessionUserLoggedin", value);
        }

        public UserModel? FindUserSession()
        {
            string? sessionUser = _httpContext?.HttpContext?.Session.GetString("sessionUserLoggedin");

            if (string.IsNullOrEmpty(sessionUser)) return null;

            return JsonConvert.DeserializeObject<UserModel>(sessionUser);
        }

        public void RemoveUserSession()
        {
            _httpContext?.HttpContext?.Session.Remove("sessionUserLoggedin");
        }
    }
}
