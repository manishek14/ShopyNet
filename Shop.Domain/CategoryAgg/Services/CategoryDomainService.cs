using Shop.Domain.CategoryAgg;
using Shop.Domain.CategoryAgg.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.CategoryAgg.Service
{
    public class CategoryDomainService : ICategoryDomainService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryDomainService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository
                ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public bool IsSlugExist(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return false;

            return _categoryRepository.Exists(c => c.Slug == slug);
        }

        public async Task<bool> IsSlugExistAsync(
            string slug,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return false;

            return await _categoryRepository.ExistsAsync(
                c => c.Slug == slug,
                cancellationToken);
        }

        public bool IsCategoryExist(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
                return false;

            return _categoryRepository.Exists(c => c.Id == categoryId);
        }

        public async Task<bool> IsCategoryExistAsync(
            Guid categoryId,
            CancellationToken cancellationToken = default)
        {
            if (categoryId == Guid.Empty)
                return false;

            return await _categoryRepository.ExistsAsync(
                c => c.Id == categoryId,
                cancellationToken);
        }
    }
}