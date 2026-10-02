using Clean_Arch.Query.Shared.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Domain.UserAgg.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<User> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);
        Task<User> GetWithDetailsAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<bool> IsEmailExistAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> IsPhoneNumberExistAsync(string phoneNumber, CancellationToken cancellationToken = default);

        Task AddTokenAsync(UserToken token, CancellationToken cancellationToken = default);
        Task RemoveTokenAsync(Guid tokenId, CancellationToken cancellationToken = default);
        Task<UserToken?> GetTokenByJwtTokenAsync(string hashJwtToken, CancellationToken cancellationToken = default);
        Task<UserToken?> GetTokenByRefreshTokenAsync(string hashRefreshToken, CancellationToken cancellationToken = default);
        Task RemoveAllUserTokensAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}