using Microsoft.EntityFrameworkCore;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.Ef;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Persistent.Ef.UserAgg
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly DbSet<UserToken> _userTokens;

        public UserRepository(ShopContext context) : base(context)
        {
            _userTokens = context.Set<UserToken>();
        }

        public async Task<User> GetByEmailAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<User> GetByPhoneNumberAsync(
            string phoneNumber,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber, cancellationToken);
        }

        public async Task<User> GetWithDetailsAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(u => u.UserRoles)
                .Include(u => u.Wallets)
                .Include(u => u.UserAddresses)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }

        public async Task<bool> IsEmailExistAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<bool> IsPhoneNumberExistAsync(
            string phoneNumber,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AnyAsync(u => u.PhoneNumber == phoneNumber, cancellationToken);
        }

        public async Task AddTokenAsync(
            UserToken token,
            CancellationToken cancellationToken = default)
        {
            await _userTokens.AddAsync(token, cancellationToken);
        }

        public async Task RemoveTokenAsync(
            Guid tokenId,
            CancellationToken cancellationToken = default)
        {
            var token = await _userTokens
                .FirstOrDefaultAsync(t => t.Id == tokenId, cancellationToken);

            if (token != null)
                _userTokens.Remove(token);
        }

        public async Task<UserToken?> GetTokenByJwtTokenAsync(
            string hashJwtToken,
            CancellationToken cancellationToken = default)
        {
            return await _userTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.HashJwtToken == hashJwtToken, cancellationToken);
        }

        public async Task<UserToken?> GetTokenByRefreshTokenAsync(
            string hashRefreshToken,
            CancellationToken cancellationToken = default)
        {
            return await _userTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.HashRefreshToken == hashRefreshToken, cancellationToken);
        }

        public async Task RemoveAllUserTokensAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var tokens = await _userTokens
                .Where(t => t.UserId == userId)
                .ToListAsync(cancellationToken);

            if (tokens.Any())
                _userTokens.RemoveRange(tokens);
        }
    }
}