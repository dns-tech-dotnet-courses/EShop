using Microsoft.AspNetCore.Mvc;
using EShop.Application;

namespace EShop.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ProductDto> Get([FromQuery] decimal? priceFilter, [FromQuery] string? priceSortOrder)
        {
            var productHandler = new ProductHandler();
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

    }
}
