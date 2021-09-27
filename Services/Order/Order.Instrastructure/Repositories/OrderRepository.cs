using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order.Domain.Repositories;
using Order.Instrastructure.Data;
using Order.Instrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Order.Instrastructure.Repositories
{
    public class OrderRepository : Repository<Domain.Entities.Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Domain.Entities.Order>> GetOrdersByUserId(string userId)
        {
            return await DbContext.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
    }
}
