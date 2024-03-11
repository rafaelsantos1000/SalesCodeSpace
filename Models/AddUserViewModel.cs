using System.ComponentModel.DataAnnotations;
using SalesCodeSpace.Enums;

namespace SalesCodeSpace.Models
{
    public class AddUserViewModel : EditUserViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Deves inserir um email válido.")]
        [MaxLength(100, ErrorMessage = "O campo {0} deve ter no máximo máximo {1} carateres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Palavra-Pase")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} carateres.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "A Palavra-Passe e Confirmação não são iguais.")]
        [Display(Name = "Confirmação da Palavra-Passe")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} carateres.")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Tipo de utilizador")]
        public UserType UserType { get; set; }
    }
}