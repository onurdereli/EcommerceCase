using Product.Core.Configuration.Settings;
using Product.Data.Abstract;
using MongoDB.Driver;
namespace Product.Data.Concrete
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IProductDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Products = database.GetCollection<Models.Entities.Product>(settings.CollectionName);
            //ProductContextSeed.SeedData(Products);
        }

        public IMongoCollection<Models.Entities.Product> Products { get; }
    }
}
