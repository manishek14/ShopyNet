using Shop.Query.Seller.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Query.Seller
{
    internal static class SellerMapper
    {
        public static SellerDto? Map(this Domain.SellerAgg.Seller? seller)
        {
            if (seller is null)
                return null;

            return new SellerDto
            {
                Id = seller.Id,
                UserId = seller.UserId,
                ShopName = seller.ShopName,
                NationalCode = seller.NationalCode,
                Status = seller.Status,
                CreatedDate = seller.CreatedAt,
                UpdatedDate = seller.UpdatedAt,
                Inventories = seller.Inventories?.Select(MapInventory).ToList()
                              ?? new List<SellerInventoryDto>()
            };
        }

        public static List<SellerDto> MapList(this List<Domain.SellerAgg.Seller>? sellers)
        {
            var result = new List<SellerDto>();
            if (sellers == null)
                return result;

            foreach (var seller in sellers)
            {
                result.Add(new SellerDto
                {
                    Id = seller.Id,
                    UserId = seller.UserId,
                    ShopName = seller.ShopName,
                    NationalCode = seller.NationalCode,
                    Status = seller.Status,
                    CreatedDate = seller.CreatedAt,
                    UpdatedDate = seller.UpdatedAt,
                    Inventories = new List<SellerInventoryDto>()  
                });
            }

            return result;
        }

        private static SellerInventoryDto MapInventory(Domain.SellerAgg.SellerInventory inventory)
        {
            return new SellerInventoryDto
            {
                Id = inventory.Id,
                SellerId = inventory.SellerId,
                ProductId = inventory.ProductId,
                Count = inventory.Count,
                Price = inventory.Price
            };
        }
    }
}