using CursoYoutubeProgramadorTech.Models;

namespace CursoYoutubeProgramadorTech.Helper
{
    public interface ISessionUser
    {
        public UserModel? FindUserSession();

        public void CreateUserSession(UserModel user);

        public void RemoveUserSession();
    }
}
