using Microsoft.AspNetCore.Mvc;
using EShop.Application;

namespace EShop.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ProductHandler _handler;

        public ProductsController(ProductHandler productHandler)
        {
            _handler = productHandler;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get([FromQuery] decimal? priceFilter, [FromQuery] string? priceSortOrder)
        {
            var productsResult = await _handler.Get();
            if (productsResult.IsFailed) {
                return StatusCode(503, "Произошла ошибка при попытке получить товары");
            }

            var products = productsResult.Value;

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
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetById([FromQuery] int? id)
        {
            var productsResult = await _handler.GetById(id);
            if (productsResult.IsFailed)
            {
                return StatusCode(503, "Произошла ошибка при попытке получить товары");
            }

            var products = productsResult.Value;

            var listOfDto = new List<ProductDto>();
            foreach (var product in products)
                listOfDto.Add(new ProductDto { Name = product.Name, Price = product.Price });

            return listOfDto;
        }

    }
}
