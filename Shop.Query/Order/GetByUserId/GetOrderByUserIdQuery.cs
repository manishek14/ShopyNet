using Shop.Query.Order.DTOs;
using System;
using System.Collections.Generic;

namespace Shop.Query.Order.GetByUserId
{
    public record GetOrderByUserIdQuery(Guid UserId) : IBaseQuery<List<OrderDto>>;
}