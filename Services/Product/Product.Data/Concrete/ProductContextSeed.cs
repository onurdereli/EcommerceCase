using System.Collections.Generic;
using MongoDB.Driver;

namespace Product.Data.Concrete
{
    public class ProductContextSeed
    {
        public static void SeedData(IMongoCollection<Models.Entities.Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetConfigureProducts());
            }
        }

        private static IEnumerable<Models.Entities.Product> GetConfigureProducts()
        {
            return new List<Models.Entities.Product>()
            {
                  new()
                  {
                    Code = "123",
                    Price = 500.00M,
                    Stock = 100
                  },
                  new()
                  {
                    Code = "123",
                    Price = 500.00M,
                    Stock = 100
                  }
            };
        }
    }
}
