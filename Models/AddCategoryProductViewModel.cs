using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SalesCodeSpace.Models
{
    public class AddCategoryProductViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Categoria")]
        [Range(1, int.MaxValue, ErrorMessage = "Deve seleccionar uma categoria.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }

}