using Microsoft.AspNetCore.Mvc;
using EShop.Application;
using EShop.DAL;

namespace EShop.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("getAll")]
        public IEnumerable<ProductDto> Get([FromQuery] decimal? priceFilter, [FromQuery] string? priceSortOrder)
        {
            var productRepository = new JsonProductRepository();
            var productHandler = new ProductHandler(productRepository);
            var products = productHandler.Get();

            if (priceFilter is not null)
                products = products.Where(p => p.Price <= priceFilter);

            if (!string.IsNullOrEmpty(priceSortOrder))
            {
                products = priceSortOrder.ToLower() == "desc"
                    ? products.OrderByDescending(p => p.Price)
                    : products.OrderBy(p => p.Price);
            }

            var listOfDto = new List<ProductDto>();
            foreach (var product in products)
                listOfDto.Add(new ProductDto { Name = product.Name, Price = product.Price });

            return listOfDto;
        }

        [HttpGet("getById")]
        public IEnumerable<ProductDto> GetById([FromQuery] int? id)
        {
            var productRepository = new JsonProductRepository();
            var handler = new ProductHandler(productRepository);
            var products = handler.GetById(id);

            var listOfDto = new List<ProductDto>();
            foreach (var product in products)
                listOfDto.Add(new ProductDto { Name = product.Name, Price = product.Price });

            return listOfDto;
        }

    }
}
