using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Domain.ProductAgg.Services
{
    public interface IProductDomainService
    {
        bool IsSlugExist(string slug);

        Task<bool> IsSlugExistAsync(string slug, CancellationToken cancellationToken = default);

        bool IsProductExist(Guid productId);

        Task<bool> IsProductExistAsync(Guid productId, CancellationToken cancellationToken = default);
    }
}