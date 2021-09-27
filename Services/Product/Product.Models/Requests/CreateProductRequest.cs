namespace Product.Models.Requests
{
    public class CreateProductRequest
    {
        public string Code { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }
    }
}
