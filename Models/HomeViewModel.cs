using SalesCodeSpace.Data.Entities;
using SalesCodeSpace.Utils;

namespace SalesCodeSpace.Models
{
    public class HomeViewModel
    {
        public PaginatedList<Product> Products { get; set; }

        public ICollection<Category> Categories { get; set; }

        public float Quantity { get; set; }
    }

}