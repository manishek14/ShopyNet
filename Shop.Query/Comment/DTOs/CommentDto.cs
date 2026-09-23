using Common.Query;
using Shop.Domain.CommentAgg.Enums;
using System;

namespace Shop.Query.Comment.DTOs
{
    public class CommentDto : BaseDto
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid? ReplyId { get; set; }
        public string Content { get; set; }
        public CommentStatus Status { get; set; }
    }
}