using MongoDB.Driver;

namespace Product.Data.Abstract
{
    public interface IProductContext
    {
        IMongoCollection<Models.Entities.Product> Products { get; }
    }
}
