namespace eshop.catalog.api.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageFileName { get; set; } = string.Empty;
        public List<string> Category { get; set; } = default!;
    }
}
