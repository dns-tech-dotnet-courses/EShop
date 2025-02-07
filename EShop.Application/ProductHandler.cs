using EShop.Domain;
using FluentResults;

namespace EShop.Application
{
    public class ProductHandler
    {
        private readonly IProductRepository _productRepository;

        public ProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<IEnumerable<Product>>> Get()
        {
            var productsResult = await _productRepository.Get();
            return productsResult;
        }

        public async Task<Result<IEnumerable<Product>>> GetById(int? id)
        {
            var listOfProducts = new List<Product>();
            var productsResult = await _productRepository.Get();
            if (productsResult.IsFailed)
                return productsResult;

            foreach (var product in productsResult.Value) {
                if (product.Id == id)
                {
                    listOfProducts.Add(product);
                    return listOfProducts;
                }
            }
            return Result.Ok<IEnumerable<Product>>(listOfProducts);
        }
    }
}