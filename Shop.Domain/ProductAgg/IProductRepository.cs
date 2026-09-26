using Clean_Arch.Query.Shared.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Domain.ProductAgg.Repository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetBySlugAsync(
            string slug,
            CancellationToken cancellationToken = default);

        Task<Product> GetWithDetailsAsync(
            Guid productId,
            CancellationToken cancellationToken = default);

        Task<bool> IsSlugExistAsync(
            string slug,
            CancellationToken cancellationToken = default);
    }
}