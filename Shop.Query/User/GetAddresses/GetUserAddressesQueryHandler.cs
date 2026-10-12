using Microsoft.EntityFrameworkCore;
using Shop.Application.Caching;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Query.User.GetAddresses
{
    internal class GetUserAddressesQueryHandler
        : IBaseQueryHandler<GetUserAddressesQuery, List<UserAddressDto>>
    {
        private readonly ShopContext _shopContext;
        private readonly ICacheService _cache;

        public GetUserAddressesQueryHandler(ShopContext shopContext, ICacheService cache)
        {
            _shopContext = shopContext;
            _cache = cache;
        }

        public async Task<List<UserAddressDto>> Handle(
            GetUserAddressesQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = CacheKeys.UserAddresses(request.UserId);

            return await _cache.GetOrSetAsync(
                cacheKey,
                async () =>
                {
                    var addresses = await _shopContext.UserAddresses
                        .AsNoTracking()
                        .Where(a => a.UserId == request.UserId)
                        .ToListAsync(cancellationToken);

                    return addresses.Select(a => new UserAddressDto
                    {
                        Id = a.Id,
                        UserId = a.UserId,
                        Province = a.Province,
                        City = a.City,
                        PostalCode = a.PostalCode,
                        MailingAddress = a.MailingAddress,
                        PhoneNumber = a.PhoneNumber,
                        Name = a.Name,
                        Family = a.Family,
                        NationalCode = a.NationalCode,
                        IsActive = a.IsActive,
                        CreatedDate = a.CreatedAt,
                        UpdatedDate = a.UpdatedAt
                    }).ToList();
                },
                TimeSpan.FromMinutes(30),
                cancellationToken);
        }
    }
}