using CursoYoutubeProgramadorTech.Models;

namespace CursoYoutubeProgramadorTech.Repositories
{
    public interface IUserRepository
    {
        UserModel Add(UserModel user);

        List<UserModel> GetAll();

        UserModel? GetById(int Id);

        UserModel? GetByLoginAndEmail(string login, string email);

        UserModel Update(UserModel user);

        UserModel ChangePassword(ChangePasswordModel data);

        bool Delete(int id);

        UserModel? GetUserByLogin(string Login);
    }
}
