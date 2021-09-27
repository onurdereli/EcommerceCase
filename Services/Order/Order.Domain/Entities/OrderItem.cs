using System.Net.NetworkInformation;
using Order.Domain.Core.BaseEntities;

namespace Order.Domain.Entities
{
    public class OrderItem: Entity
    {
        public string ProductId { get; private set; }

        public string ProductCode { get; private set; }
        
        public decimal Price { get; private set; }

        public int Quantity { get; private set; }
        
        public OrderItem(string productId, string productCode, decimal price, int quantity)
        {
            ProductId = productId;
            ProductCode = productCode;
            Price = price;
            Quantity = quantity;
        }
    }
}
