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

        public IEnumerable<Product> Get()
        {
            var products = _productRepository.Get();
            return products;
        }

        public IEnumerable<Product> GetById(int? id)
        {
            var listOfProducts = new List<Product>();
            var products = _productRepository.Get();
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