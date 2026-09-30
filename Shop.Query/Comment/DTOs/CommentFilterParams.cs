using Common.Query.Filter;
using Shop.Domain.CommentAgg.Enums;
using System;

namespace Shop.Query.Comment.DTOs
{
    public class CommentFilterParams : BaseFilter.BaseFilterParam
    {
        public Guid? UserId { get; set; }
        public Guid? ProductId { get; set; }
        public string? Content { get; set; }
        public CommentStatus? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}