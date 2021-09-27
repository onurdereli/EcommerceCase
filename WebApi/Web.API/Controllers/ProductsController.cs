using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shared.ControllerBase;
using Web.Client.Abstract;
using Web.Models.Dtos;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IProductClient _productClient;

        public ProductsController(IProductClient productClient)
        {
            _productClient = productClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            return CreateActionResultInstance(await _productClient.CreateProduct(createProductDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            return CreateActionResultInstance(await _productClient.GetProductById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return CreateActionResultInstance(await _productClient.GetProducts());
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProducts(string id)
        {
            return CreateActionResultInstance(await _productClient.DeleteProduct(id));
        }
    }
}
