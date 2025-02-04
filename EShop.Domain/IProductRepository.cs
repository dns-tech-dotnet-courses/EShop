namespace EShop.Domain
{
    public interface IProductRepository
    {
        public IEnumerable<Product> Get();
    }
}
