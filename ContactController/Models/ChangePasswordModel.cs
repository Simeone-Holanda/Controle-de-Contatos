using System.ComponentModel.DataAnnotations;

namespace CursoYoutubeProgramadorTech.Models
{
    public class ChangePasswordModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Senha atual é obrigatório")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Nova senha é obrigatório")]
        public string NewPassowrd { get; set; }

        [Required(ErrorMessage = "Confirmar nova senha é obrigatório")]
        [Compare("NewPassowrd", ErrorMessage = "As senhas não conferem. ")]
        public string ConfirmNewPassowrd { get; set; }
    }
}
