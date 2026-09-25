using Common.Query.Filter;
using Shop.Domain.OrderAgg.Enums;
using System;

namespace Shop.Query.Order.DTOs
{
    public class OrderFilterParams : BaseFilter.BaseFilterParam
    {
        public Guid? UserId { get; set; }
        public OrderStatus? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}