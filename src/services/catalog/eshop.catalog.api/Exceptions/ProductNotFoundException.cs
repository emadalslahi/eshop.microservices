using eshop.buildingblocks.Exceptions;

namespace eshop.catalog.api.Exceptions
{
    public class ProductNotFoundException:NotFoundException
    {
        public ProductNotFoundException(Guid id ):base("Product" , id)
        {
                
        }
    }
}
