using System.Collections.Generic;
using System.Linq;
using Order.Domain.Core.BaseEntities;

namespace Order.Domain.Entities
{
    public class Order: Entity, IAggregateRoot
    {
        public string UserId { get; private set; }

        public decimal? TotalPrice { get; private set; }

        public decimal? TotalDiscountPrice { get; private set; }

        public decimal? CargoPrice { get; private set; }

        public decimal? TotalDiscountCargoPrice { get; private set; }

        private readonly List<OrderItem> _orderItems;

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order()
        {

        }

        public Order(string userId, decimal totalPrice, decimal totalDiscountPrice, decimal cargoPrice, decimal totalDiscountCargoPrice)
        {
            _orderItems = new List<OrderItem>();
            UserId = userId;
            TotalPrice = totalPrice;
            TotalDiscountCargoPrice = totalDiscountCargoPrice;
            TotalDiscountPrice = totalDiscountPrice;
            CargoPrice = cargoPrice;
        }

        public void AddOrderItem(string productId, string productCode, decimal price, int quantity)
        {
            var existProduct = _orderItems.Any(item => item.ProductId == productId);

            if (!existProduct)
            {
                OrderItem orderItem = new(productId, productCode, price, quantity);

                _orderItems.Add(orderItem);
            }
        }

        public decimal GetUsedAmount()
        {
            return TotalDiscountPrice.HasValue
                ? TotalPrice.Value - TotalDiscountPrice.Value
                : TotalDiscountCargoPrice.HasValue ? CargoPrice.Value - TotalDiscountCargoPrice.Value : 0;
        }
    }
}
