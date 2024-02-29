using System.ComponentModel.DataAnnotations;

namespace SalesCodeSpace.Models
{
    public class AddProductImageViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Foto")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public IFormFile ImageFile { get; set; }
    }

}