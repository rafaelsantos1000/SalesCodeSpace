using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesCodeSpace.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} carateres.")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descrição")]
        [MaxLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} carateres.")]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Preço")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public float Stock { get; set; }

        public ICollection<ProductCategory>? ProductCategories { get; set; }

        [Display(Name = "Categorias")]
        public int CategoriesNumber => ProductCategories == null ? 0 : ProductCategories.Count;

        public ICollection<ProductImage>? ProductImages { get; set; }

        [Display(Name = "Fotos")]
        public int ImagesNumber => ProductImages == null ? 0 : ProductImages.Count;

        [Display(Name = "Foto")]
        public string ImageFullPath => ProductImages == null || ProductImages.Count == 0
            ? $"http://localhost:5196/images/noimage.png"
            : ProductImages.FirstOrDefault()!.ImageFullPath;

    }
}