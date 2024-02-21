using System.ComponentModel.DataAnnotations;

namespace CursoYoutubeProgramadorTech.Models
{
    public class RecoverPasswordModel
    {

        [Required(ErrorMessage = "Login é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }
    }
}
