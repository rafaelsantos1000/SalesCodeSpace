using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SalesCodeSpace.Models
{
    public class CreateProductViewModel : EditProductViewModel
    {
        [Display(Name = "Categoria")]
        [Range(1, int.MaxValue, ErrorMessage = "Deve escolher uma categoria.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }


        [Display(Name = "Foto")]
        public IFormFile? ImageFile { get; set; }

    }
}