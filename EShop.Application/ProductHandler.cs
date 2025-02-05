using EShop.Domain;

namespace EShop.Application
{
    public class ProductHandler
    {
        private readonly IProductRepository _productRepository;

        public ProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Get()
        {
            var products = await _productRepository.Get();
            return products;
        }

        public async Task<IEnumerable<Product>> GetById(int? id)
        {
            var listOfProducts = new List<Product>();
            var products = await _productRepository.Get();
            foreach (var product in products) {
                if (product.Id == id)
                {
                    listOfProducts.Add(product);
                    return listOfProducts;
                }
            }
            return listOfProducts;
        }
    }
}