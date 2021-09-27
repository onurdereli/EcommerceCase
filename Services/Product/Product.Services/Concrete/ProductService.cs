using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Product.Data.Abstract;
using Product.Models.Dtos;
using Product.Models.Requests;
using Product.Services.Abstract;
using Shared.Dtos;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Product.Services.Concrete
{
    public class ProductService: IProductService
    {
        private readonly IProductContext _context;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;

        public ProductService(ILogger<ProductService> logger, IMapper mapper, IProductContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Response<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _context.Products.Find(p => true).ToListAsync();

            return Response<IEnumerable<ProductDto>>.Success(_mapper.Map<IEnumerable<ProductDto>>(products), 200);
        }
        
        public async Task<Response<IEnumerable<ProductDto>>> GetProductByCode(string code)
        {
            var filter = Builders<Models.Entities.Product>.Filter.ElemMatch(p => p.Code, code);
            var products = await _context.Products.Find(filter).ToListAsync();

            return Response<IEnumerable<ProductDto>>.Success(_mapper.Map<IEnumerable<ProductDto>>(products), 200);
        }
        public async Task<Response<ProductDto>> GetProductById(string id)
        {
            var product = await _context
                .Products
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();

            return Response<ProductDto>.Success(_mapper.Map<ProductDto>(product), 200);
        }

        public async Task<Response<NoContent>> Create(CreateProductRequest createProductRequest)
        {
            var product = _mapper.Map<Models.Entities.Product>(createProductRequest);
            await _context.Products.InsertOneAsync(product);

            return Response<NoContent>.Success(201);
        }

        public async Task<Response<NoContent>> Update(UpdateProductRequest updateProductRequest)
        {
            var product = _mapper.Map<Models.Entities.Product>(updateProductRequest);

            var updateResult = await _context
                .Products
                .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0
                ? Response<NoContent>.Success(204)
                : Response<NoContent>.Fail("The product could not be updated", 500);
        }

        public async Task<Response<NoContent>> Delete(string id)
        {
            FilterDefinition<Models.Entities.Product> filter = Builders<Models.Entities.Product>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                .Products
                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0
                ? Response<NoContent>.Success(204)
                : Response<NoContent>.Fail("The product could not be updated", 500);
        }
    }
}
