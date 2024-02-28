namespace SalesCodeSpace.Data.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }

        public Product Product { get; set; } = null!;

        public Category Category { get; set; } = null!;

    }
}