using System.ComponentModel.DataAnnotations;

namespace CursoYoutubeProgramadorTech.Models
{
    public class ContactModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Nome do contato é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email do contato é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefone do contato é obrigatório")]
        [Phone(ErrorMessage =" O telefone informado não é válido. ")]
        public string Phone { get; set; }

        public int? UserId { get; set; }

        public UserModel? User { get; set; }
    }
}
