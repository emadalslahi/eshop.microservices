namespace Eshop.Basket.Api.models
{
    public class ShopingCart
    {
       public string UserName { get; set; } = default!;
        public List<ShopingCartItem> Items { get; set; } = new();
        public decimal TotalPrice =>Items.Sum(x => x.Price * x.Quantity);

        public ShopingCart(string userName)
        {
          this.UserName = userName;
        }

        public ShopingCart()
        {
            
        }
    }
}
