using Shared.Entities;

namespace Product.Models.Dtos
{
    public class ProductDto : IDto
    {
        public string Id { get; set; }

        public string Code { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }
    }
}
