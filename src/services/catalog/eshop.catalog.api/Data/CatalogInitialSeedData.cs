using Marten.Schema;

namespace eshop.catalog.api.Data;

public class CatalogInitialSeedData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync()) {
            return;
        }

        session.Store<Product>(GetInitialSeedDataForProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetInitialSeedDataForProducts() =>
        new List<Product>
        {
        new Product{ 
            Id= new Guid(),
            Name ="IPhone X",
            Description ="iphone type x is the best phone ever",
            ImageFileName="product-01.png",
            Price =100.99m,
            Category = new List<string> { "Smart-Phones"}
        },

        new Product{
            Id= new Guid(),
            Name ="Samsung Galaxi 24",
            Description ="samsung galaxy mobile with android system",
            ImageFileName="product-02.png",
            Price =100.99m,
            Category = new List<string> { "Smart-Phones"}
        },

        new Product{
            Id= new Guid(),
            Name ="Dell Pressisions",
            Description ="Laptop Dell Pressision 5520",
            ImageFileName="product-03.png",
            Price =100.99m,
            Category = new List<string> { "Laptops"}
        },

        new Product{
            Id= new Guid(),
            Name ="IFace Xtra",
            Description ="Face recognitions",
            ImageFileName="product-04.png",
            Price =100.99m,
            Category = new List<string> { "BIO-Devices"}
        },

        new Product{
            Id= new Guid(),
            Name ="Sony Brava",
            Description ="Smart Screen with android system",
            ImageFileName="product-05.png",
            Price =100.99m,
            Category = new List<string> { "Smart-Screens"}
        },

        }; 
}
