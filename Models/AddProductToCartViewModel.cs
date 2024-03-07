using System.ComponentModel.DataAnnotations;
using SalesCodeSpace.Data.Entities;

namespace SalesCodeSpace.Models;

public class AddProductToCartViewModel
{
    public int Id { get; set; }

    [Display(Name = "Nome")]
    [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string Name { get; set; }

    [DataType(DataType.MultilineText)]
    [Display(Name = "Descrição")]
    [MaxLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    public string Description { get; set; }

    [DisplayFormat(DataFormatString = "{0:C2}")]
    [Display(Name = "Preço")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public decimal Price { get; set; }

    [DisplayFormat(DataFormatString = "{0:N2}")]
    [Display(Name = "Stock")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public float Stock { get; set; }

    [Display(Name = "Categorias")]
    public string Categories { get; set; }

    public ICollection<ProductImage> ProductImages { get; set; }

    [DisplayFormat(DataFormatString = "{0:N2}")]
    [Display(Name = "Quantidade")]
    [Range(0.0000001, float.MaxValue, ErrorMessage = "Deve inserir um valor maior que zero na quantidade.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public float Quantity { get; set; }

    [DataType(DataType.MultilineText)]
    [Display(Name = "Comentários")]
    public string Remarks { get; set; }

}
