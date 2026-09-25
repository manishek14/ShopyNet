using Common.Query.Filter;

namespace Shop.Query.Role.DTOs
{
    public class RoleFilterParams : BaseFilter.BaseFilterParam
    {
        public string? Title { get; set; }
    }
}