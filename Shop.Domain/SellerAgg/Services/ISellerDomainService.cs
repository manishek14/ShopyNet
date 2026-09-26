using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Domain.SellerAgg.Services
{
    public interface ISellerDomainService
    {
        bool IsNationalCodeExist(string nationalCode);

        Task<bool> IsNationalCodeExistAsync(string nationalCode, CancellationToken cancellationToken = default);

        bool IsShopNameExist(string shopName);
        Task<bool> IsShopNameExistAsync(string shopName, CancellationToken cancellationToken = default);

        bool IsUserExist(Guid userId);
        Task<bool> IsUserExistAsync(Guid userId, CancellationToken cancellationToken = default);

        bool IsProductExist(Guid productId);

        Task<bool> IsProductExistAsync(Guid productId, CancellationToken cancellationToken = default);
    }
}