using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Product.Models.Requests;
using Product.Services.Abstract;
using Shared.ControllerBase;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest createProductRequest)
        {
            return CreateActionResultInstance(await _productService.Create(createProductRequest));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductRequest updateProductRequest)
        {
            return CreateActionResultInstance(await _productService.Update(updateProductRequest));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return CreateActionResultInstance(await _productService.Delete(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return CreateActionResultInstance(await _productService.GetProducts());
        }

        [HttpGet]
        [Route("[action]/{code}", Name = "GetProductByCode")]
        public async Task<IActionResult> GetProductByCode(string code)
        {
            return CreateActionResultInstance(await _productService.GetProductByCode(code));
        }

        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        public async Task<IActionResult> GetProductById(string id)
        {
            return CreateActionResultInstance(await _productService.GetProductById(id));
        }
    }
}
