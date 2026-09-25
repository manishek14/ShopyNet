using Microsoft.EntityFrameworkCore;
using Shop.Domain.UserAgg;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.User.DTOs;
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

        public GetUserAddressesQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<List<UserAddressDto>> Handle(
            GetUserAddressesQuery request,
            CancellationToken cancellationToken)
        {
            var addresses = await _shopContext.Set<UserAddress>()
                .Where(a => a.UserId == request.UserId && a.IsActive)
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
        }
    }
}