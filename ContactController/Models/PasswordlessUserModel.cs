using CursoYoutubeProgramadorTech.Enums;
using System.ComponentModel.DataAnnotations;

namespace CursoYoutubeProgramadorTech.Models
{
    public class PasswordlessUserModel
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
    }
}
