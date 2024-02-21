using CursoYoutubeProgramadorTech.Data;
using CursoYoutubeProgramadorTech.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoYoutubeProgramadorTech.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ConnectionContext _connectionContext;
        public UserRepository(ConnectionContext connection) {
            _connectionContext = connection;
        }
        public UserModel Add(UserModel user)
        {
            try
            {
                user.CreatedAt = DateTime.UtcNow;
                user.SetPasswordHash();
                _connectionContext.Users.Add(user);
                _connectionContext.SaveChanges();
                return user;
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                return user;
            }
            
        }

        public bool Delete(int id)
        {
            UserModel? user = GetById(id);
            if (user == null)
            {
                throw new Exception("Ocorreu um erro ao deletar o usuário. ");
            }
            _connectionContext.Users.Remove(user);
            _connectionContext.SaveChanges();
            return true;
        }

        public List<UserModel> GetAll()
        {
            return _connectionContext.Users
                .Include(u => u.Contacts)
                .ToList();
        }

        public UserModel? GetById(int Id)
        {
            return _connectionContext.Users.FirstOrDefault(c => c.Id == Id);
        }

        public UserModel? GetByLoginAndEmail(string login, string email)
        {
            return _connectionContext.Users.FirstOrDefault(u => 
                u.Login.ToUpper() == login.ToUpper() && 
                u.Email.ToUpper() == email.ToUpper()
                );
        }

        public UserModel? GetUserByLogin(string login)
        {
            return _connectionContext.Users.FirstOrDefault(c => c.Login.ToUpper() == login.ToUpper());
        }

        public UserModel Update(UserModel userUpdate)
        {
            UserModel? user = GetById(userUpdate.Id);
            if (userUpdate == null || user == null)
            {
                throw new Exception("Ocorreu um erro ao atualizar os dados do usuário. ");
            }
            user.Name = userUpdate.Name;
            user.Email = userUpdate.Email;
            user.Login = userUpdate.Login;
            user.Profile = userUpdate.Profile;
            user.UpdatedAt = DateTime.UtcNow;
            _connectionContext.Users.Update(user);
            _connectionContext.SaveChanges();
            return user;
        }
        
        public UserModel ChangePassword(ChangePasswordModel data)
        {
            UserModel user = GetById(data.Id);

            if (user == null) 
                throw new Exception("Ocorreu um erro ao atualizar as senhas, usuário não encontrado!");

            if(!user.CheckPassword(data.CurrentPassword)) 
                throw new Exception("Senha atual não confere");

            if(user.CheckPassword(data.NewPassowrd))
                throw new Exception("Nova senha deve ser diferente da senha atual!");

            user.SetNewPassowrd(data.NewPassowrd);
            user.UpdatedAt = DateTime.UtcNow;

            _connectionContext.Update(user);
            _connectionContext.SaveChanges();
            return user;
        }
    }
}
