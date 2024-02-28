using System.ComponentModel.DataAnnotations;

namespace SalesCodeSpace.Models
{
    public class RecoverPasswordViewModel
    {
        [Display(Name ="Email")]
        [Required(ErrorMessage ="O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage ="Deve inserir um email válido.")]
        public string? Email { get; set; }
    }
}