﻿namespace Product.Models.Requests
{
    public class UpdateProductRequest
    {
        public string Id { get; set; }

        public string Code { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }
    }
}
