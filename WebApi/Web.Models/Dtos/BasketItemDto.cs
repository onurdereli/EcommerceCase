namespace Web.Models.Dtos
{
    public class BasketItemDto
    {
        public string ProductId { get; set; }

        public string ProductCode { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}