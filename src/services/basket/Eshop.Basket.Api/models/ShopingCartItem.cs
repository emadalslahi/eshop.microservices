namespace Eshop.Basket.Api.models
{
    public class ShopingCartItem
    {
        public int Quantity { get; set; } = default!;
        public string Color { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public Guid ProductId { get; set; }= default!;
        public string ProductName { get; set; }= default!;
    }
}
