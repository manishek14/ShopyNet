using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Domain.CommentAgg.Services
{
    public interface ICommentDomainService
    {
        bool IsProductExist(Guid productId);

        Task<bool> IsProductExistAsync(Guid productId, CancellationToken cancellationToken = default);

        bool IsUserExist(Guid userId);

        Task<bool> IsUserExistAsync(Guid userId, CancellationToken cancellationToken = default);

        bool IsCommentLimitExceeded(Guid userId);
    }
}