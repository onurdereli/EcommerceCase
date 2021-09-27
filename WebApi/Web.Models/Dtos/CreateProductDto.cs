namespace Web.Models.Dtos
{
    public class CreateProductDto
    {
        public string Code { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }
    }
}
