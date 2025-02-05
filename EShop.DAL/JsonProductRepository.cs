using System.Reflection;
using System.Text.Json;
using EShop.Domain;

namespace EShop.DAL
{
    public class JsonProductRepository: IProductRepository
    {
        public async Task<IEnumerable<Product>> Get()
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation)!;
            var jsonFilePath = Path.Combine(assemblyDirectory, "Products.json");

            var json = await File.ReadAllTextAsync(jsonFilePath);
            var products = JsonSerializer.Deserialize<Product[]>(json);

            return products ?? Array.Empty<Product>();
        }
    }
}
