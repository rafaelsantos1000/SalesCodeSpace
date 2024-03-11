using System.ComponentModel.DataAnnotations;

namespace SalesCodeSpace.Models
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Palavra-passe")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} carateres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nova Palavra-passe")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} carateres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "A nova palavra-passe e a confirmação, não são iguais.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmação da nova palavra-passe")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo {0} deve ter entre {2} y {1} carácteres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Confirm { get; set; }
    }
}