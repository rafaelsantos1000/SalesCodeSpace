using SalesCodeSpace.Data.Entities;

namespace SalesCodeSpace.Models
{
    public class HomeViewModel
    {
        public ICollection<Product> Products { get; set; }

        public float Quantity { get; set; }
    }

}