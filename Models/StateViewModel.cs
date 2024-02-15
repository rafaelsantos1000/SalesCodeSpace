using System.ComponentModel.DataAnnotations;

namespace SalesCodeSpace.Models
{
    public class StateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Localidade/Estado")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caractéres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Name { get; set; }

        public int CountryId { get; set; }
    }
}