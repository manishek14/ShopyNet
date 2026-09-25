using Shop.Query.Order.DTOs;
using System;

namespace Shop.Query.Order.GetById
{
    public record GetOrderByIdQuery(Guid Id) : IBaseQuery<OrderDto>;
}