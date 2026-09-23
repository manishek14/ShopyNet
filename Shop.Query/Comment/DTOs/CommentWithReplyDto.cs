using System;
using System.Collections.Generic;

namespace Shop.Query.Comment.DTOs
{
    public class CommentWithReplyDto : CommentDto
    {
        public List<CommentDto> Replies { get; set; } = new();
    }
}