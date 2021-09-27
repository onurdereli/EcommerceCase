namespace Shared.Messages.Events.Concrete
{
    public class OrderItem
    {
        public string ProductId { get; set; }

        public string ProductCode { get; set; }
        
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}