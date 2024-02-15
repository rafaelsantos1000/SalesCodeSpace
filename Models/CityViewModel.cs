using System.ComponentModel.DataAnnotations;

namespace SalesCodeSpace.Models
{
    public class CityViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Cidade")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caractéres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Name { get; set; }

        public int StateId { get; set; }
    }
}