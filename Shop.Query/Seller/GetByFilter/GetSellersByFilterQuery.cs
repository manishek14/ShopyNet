using Shop.Query.Seller.DTOs;

namespace Shop.Query.Seller.GetByFilter
{
    public record GetSellersByFilterQuery(SellerFilterParams FilterParams)
        : IBaseQuery<SellerFilterData>;
}