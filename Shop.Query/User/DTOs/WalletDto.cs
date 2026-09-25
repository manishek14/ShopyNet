using Common.Query;
using Shop.Domain.UserAgg.Enums;
using System;

namespace Shop.Query.User.DTOs
{
    public class WalletDto : BaseDto
    {
        public Guid UserId { get; set; }
        public int Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public WalletType Type { get; set; }
        public bool IsFinally { get; set; }
        public DateTime? FinallyAt { get; set; }
    }
}