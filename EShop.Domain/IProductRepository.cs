namespace EShop.Domain
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> Get();
    }
}
