using FluentResults;

namespace EShop.Domain
{
    public interface IProductRepository
    {
        public Task<Result<IEnumerable<Product>>> Get();
    }
}
