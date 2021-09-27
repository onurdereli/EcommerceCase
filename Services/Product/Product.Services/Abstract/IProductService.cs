using System.Collections.Generic;
using System.Threading.Tasks;
using Product.Models.Dtos;
using Product.Models.Requests;
using Shared.Dtos;

namespace Product.Services.Abstract
{
    public interface IProductService
    {
        Task<Response<IEnumerable<ProductDto>>> GetProducts();

        Task<Response<IEnumerable<ProductDto>>> GetProductByCode(string code);

        Task<Response<ProductDto>> GetProductById(string id);

        Task<Response<NoContent>> Create(CreateProductRequest createProductRequest);

        Task<Response<NoContent>> Update(UpdateProductRequest updateProductRequest);

        Task<Response<NoContent>> Delete(string id);
    }
}
