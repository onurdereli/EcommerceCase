using System.Collections.Generic;
using System.Threading.Tasks;
using Order.Domain.Core.BaseRepositories;

namespace Order.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Entities.Order>
    {
        Task<IEnumerable<Entities.Order>> GetOrdersByUserId(string userId);
    }
}
