using Common.Query.Filter;
using Shop.Domain.UserAgg.Enums;

namespace Shop.Query.User.DTOs
{
    public class UserFilterParams : BaseFilter.BaseFilterParam
    {
        public string? Name { get; set; }
        public string? Family { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Gender? Gender { get; set; }  
        public bool? IsActive { get; set; }
    }
}