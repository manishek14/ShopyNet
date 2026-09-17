using Clean_Arch.Query.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.OrderAgg.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
    }
}
