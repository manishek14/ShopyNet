using Microsoft.EntityFrameworkCore;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.Ef;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Persistent.Ef.UserAgg
{
    internal class WalletRepository : BaseRepository<Wallet>
    {
        public WalletRepository(ShopContext context) : base(context)
        {
        }
    }
}
