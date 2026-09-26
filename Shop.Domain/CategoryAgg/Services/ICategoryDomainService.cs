using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Domain.CategoryAgg.Services
{
    public interface ICategoryDomainService
    {
        bool IsSlugExist(string slug);

        Task<bool> IsSlugExistAsync(string slug, CancellationToken cancellationToken = default);

        bool IsCategoryExist(Guid categoryId);

        Task<bool> IsCategoryExistAsync(Guid categoryId, CancellationToken cancellationToken = default);
    }
}