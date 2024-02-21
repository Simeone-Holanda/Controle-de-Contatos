using CursoYoutubeProgramadorTech.Enums;
using CursoYoutubeProgramadorTech.Helper;
using System.ComponentModel.DataAnnotations;

namespace CursoYoutubeProgramadorTech.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome do usuário é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Login do usuário é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Email do usuário é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido. ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O perfil informado não é válido. ")]
        public ProfileEnum? Profile { get; set; }

        [Required(ErrorMessage = "Senha do usuário é obrigatório")]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual List<ContactModel> Contacts { get; set; }    
        public bool CheckPassword(string password)
        {
            return password.GenerateHash() == Password;
        }

        public void SetPasswordHash()
        {
            if (Password != null)
            {
                Password = Cryptography.GenerateHash(Password);
            }
        }

        public void SetNewPassowrd(string newPassword)
        {
            Password = newPassword.GenerateHash();
        }

        public string GenerateNewPassowrd()
        {
            string newPassword = Guid.NewGuid().ToString().Substring(0, 8);
            Password = newPassword.GenerateHash();
            return newPassword;
        }
    }

}
