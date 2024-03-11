using System.ComponentModel.DataAnnotations;

namespace SalesCodeSpace.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }


        public Product Product { get; set; }


        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }


        //TODO: Pending to change to the correct path
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"http://localhost:5196/images/noimage.png"
            : $"https://rafasaintssales2023.blob.core.windows.net/products/{ImageId}";

    }
}