using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;
using Web.Models.Dtos;
using Web.Models.ViewModels;

namespace Web.Client.Abstract
{
    public interface IProductClient
    {
        Task<Response<ProductViewModel>> CreateProduct(CreateProductDto createProductDto);

        Task<Response<ProductViewModel>> GetProductById(string productId);

        Task<Response<List<ProductViewModel>>> GetProducts();

        Task<Response<NoContent>> DeleteProduct(string id);
    }
}
