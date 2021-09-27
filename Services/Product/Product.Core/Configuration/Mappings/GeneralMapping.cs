using AutoMapper;
using Product.Models.Dtos;
using Product.Models.Requests;

namespace Product.Core.Configuration.Mappings
{
    public class GeneralMapping: Profile
    {
        public GeneralMapping()
        {
            CreateMap<Models.Entities.Product, ProductDto>()
                .ReverseMap();
            CreateMap<Models.Entities.Product, CreateProductRequest>()
                .ReverseMap();
            CreateMap<Models.Entities.Product, UpdateProductRequest>()
                .ReverseMap();
        }
    }
}
