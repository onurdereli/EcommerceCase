using System.Collections.Generic;
using System.Linq;
using Product.API.Controllers;
using Product.Models.Dtos;
using Product.Models.Requests;
using Product.Services.Abstract;
using Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Product.Test
{
    public class ProductApiControllerTest
    {
        private readonly Mock<IProductService> _mockRepo;

        private readonly ProductsController _controller;

        private readonly List<ProductDto> _products;

        public ProductApiControllerTest()
        {
            _mockRepo = new();
            _controller = new(_mockRepo.Object);
            _products = SetTestProducts();
        }

        [Fact]
        public async void PostProduct_ActionExecutes_ReturnResponseNoContent()
        {
            ProductDto product = _products.First();

            var createProductRequest = GetCreateProductRequest(product);

            Response<NoContent> productResponse = new()
            {
                IsSuccessfull = true,
                StatusCode = 201,
                Data = new NoContent(),
                Errors = null
            };
            _mockRepo.Setup(repo => repo.Create(createProductRequest)).ReturnsAsync(productResponse);
            
            var result = await _controller.Create(createProductRequest);

            var okResult = Assert.IsType<ObjectResult>(result);

            var returnProduct = Assert.IsAssignableFrom<Response<NoContent>>(okResult.Value);

            Assert.IsType<NoContent>(returnProduct.Data);

            Assert.Equal(productResponse.StatusCode, returnProduct.StatusCode);

            _mockRepo.Verify(repo => repo.Create(createProductRequest), Times.Once);
        }

        [Theory]
        [InlineData("1")]
        public async void PutProduct_ActionExecutes_ReturnResponseNoContext(string productId)
        {
            var product = GetProductById(productId);

            var updateProductRequest = GetUpdateProductRequest(product);
            
            Response<NoContent> productResponse = new()
            {
                IsSuccessfull = true,
                StatusCode = 204,
                Data = new NoContent(),
                Errors = null
            };

            _mockRepo.Setup(repo => repo.Update(updateProductRequest)).ReturnsAsync(productResponse);

            var result = await _controller.Update(updateProductRequest);

            var okResult = Assert.IsType<ObjectResult>(result);

            var returnProducts = Assert.IsAssignableFrom<Response<NoContent>>(okResult.Value);

            Assert.IsType<NoContent>(returnProducts.Data);

            Assert.Equal(productResponse.StatusCode, returnProducts.StatusCode);

            _mockRepo.Verify(repo => repo.Update(updateProductRequest), Times.Once);
        }

        [Theory]
        [InlineData("1")]
        public async void GetProduct_IdValid_ReturnResponseProduct(string productId)
        {
            var product = GetProductById(productId);

            Response<ProductDto> productResponse = new()
            {
                IsSuccessfull = true,
                StatusCode = 200,
                Data = product,
                Errors = null
            };

            _mockRepo.Setup(repo => repo.GetProductById(productId)).ReturnsAsync(productResponse);

            var result = await _controller.GetProductById(productId);

            var okResult = Assert.IsType<ObjectResult>(result);

            var returnProduct = Assert.IsAssignableFrom<Response<ProductDto>>(okResult.Value);
            
            Assert.IsType<ProductDto>(returnProduct.Data);

            Assert.Equal(productId, returnProduct.Data.Id);

            _mockRepo.Verify(repo => repo.GetProductById(productId), Times.Once);
        }

        [Theory]
        [InlineData("PRD-1001")]
        public async void GetProduct_CodeValid_ReturnResponseProducts(string productCode)
        {
            var products = GetProductsByCode(productCode);

            Response<IEnumerable<ProductDto>> productResponse = new()
            {
                IsSuccessfull = true,
                StatusCode = 200,
                Data = products,
                Errors = null
            };
            
            _mockRepo.Setup(repo => repo.GetProductByCode(productCode)).ReturnsAsync(productResponse);

            var result = await _controller.GetProductByCode(productCode);

            var okResult = Assert.IsType<ObjectResult>(result);

            var returnProducts = Assert.IsAssignableFrom<Response<IEnumerable<ProductDto>>>(okResult.Value);

            Assert.Equal(productResponse.Data.ToList().First().Id, returnProducts.Data.First().Id);

            _mockRepo.Verify(repo => repo.GetProductByCode(productCode), Times.Once);
        }

        [Fact]
        public async void GetProducts_ActionExecutes_ReturnResponseWithProducts()
        {
            Response<IEnumerable<ProductDto>> productResponse = new()
            {
                IsSuccessfull = true,
                StatusCode = 200,
                Data = _products,
                Errors = null
            };

            _mockRepo.Setup(repo => repo.GetProducts()).ReturnsAsync(productResponse);

            var result = await _controller.GetProducts();

            var okResult = Assert.IsType<ObjectResult>(result);

            var returnProducts = Assert.IsAssignableFrom<Response<IEnumerable<ProductDto>>>(okResult.Value);

            Assert.IsType<List<ProductDto>>(returnProducts.Data);

            Assert.Equal(2, returnProducts.Data.ToList().Count);
        }


        #region private methods
        private ProductDto GetProductById(string productId)
        {
            return _products.First(x => x.Id == productId);
        }
        
        private IEnumerable<ProductDto> GetProductsByCode(string productCode)
        {
            return _products.Where(x => x.Code == productCode).ToList();
        }

        private static CreateProductRequest GetCreateProductRequest(ProductDto product)
        {
            return new CreateProductRequest { Code = product.Code, Price = product.Price, Stock = product.Stock };
        }
        private static UpdateProductRequest GetUpdateProductRequest(ProductDto product)
        {
            return new UpdateProductRequest { Id = product.Id, Code = product.Code, Price = product.Price, Stock = product.Stock };
        }

        private static List<ProductDto> SetTestProducts()
        {
            return new()
            {
                new()
                {
                    Id = "1",
                    Code = "PRD-1001",
                    Price = 50.99M,
                    Stock = 100
                },
                new()
                {
                    Id = "2",
                    Code = "PRD-1002",
                    Price = 20.00M,
                    Stock = 30
                }
            };
        }


        #endregion
    }
}
