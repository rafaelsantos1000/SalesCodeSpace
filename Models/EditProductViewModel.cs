using System.ComponentModel.DataAnnotations;

namespace SalesCodeSpace.Models
{
    public class EditProductViewModel
    {
        public int Id { get; set; }


        [Display(Name = "Nome")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} carateres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Name { get; set; } = null!;


        [Display(Name = "Descrição")]
        [MaxLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} carateres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Description { get; set; } = null!;


        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Preço")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public float Stock { get; set; }

    }
}