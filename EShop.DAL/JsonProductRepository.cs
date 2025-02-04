using System.Reflection;
using System.Text.Json;
using EShop.Domain;

namespace EShop.DAL
{
    public class JsonProductRepository: IProductRepository
    {
        public IEnumerable<Product> Get()
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation)!;
            var JsonFilePath = Path.Combine(assemblyDirectory, "Products.json");
            var json = File.ReadAllText(JsonFilePath);
            var products = JsonSerializer.Deserialize<Product[]>(json);

            return products ?? Array.Empty<Product>();
        }
    }
}
