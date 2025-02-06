using EShop.Domain;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace EShop.DAL
{
    public class DbProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        public DbProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgresConnection")!;
        }
        public async Task<IEnumerable<Product>> Get()
        {
            var products = new List<Product>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using var command = new NpgsqlCommand("SELECT id, name, price::numeric FROM products", connection);
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var product = new Product
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2)
                    };
                    products.Add(product);
                }
            }

            return products;
        }
    }
}
