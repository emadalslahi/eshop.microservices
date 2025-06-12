namespace eshop.catalog.api.Exceptions
{
    public class ProductNotFoundException:Exception
    {
        public ProductNotFoundException():base(" Product Is Not Found.!")
        {
                
        }
    }
}
