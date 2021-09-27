using Shared.Messages.Events.Abstract;

namespace Shared.Messages.Events.Concrete
{
    public class StockQuantityReducedEvent : IStockQuantityReducedEvent
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
