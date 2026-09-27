using Microsoft.EntityFrameworkCore;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;   
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.Ef;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Persistent.Ef.UserAgg
{
    internal class UserRepository : BaseRepository<User>, IUserRepository   // ✅ IUserRepository اضافه شد
    {
        public UserRepository(ShopContext context) : base(context)
        {
        }

        // متدهای اختصاصی IUserRepository (اگه وجود داره)
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
    }
}