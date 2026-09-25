using Shop.Query.Order.DTOs;

namespace Shop.Query.Order.GetByFilter
{
    public record GetOrdersByFilterQuery(OrderFilterParams FilterParams)
        : IBaseQuery<OrderFilterData>;
}