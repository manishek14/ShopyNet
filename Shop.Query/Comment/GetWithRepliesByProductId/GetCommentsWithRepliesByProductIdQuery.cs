using Shop.Query.Comment.DTOs;
using System;
using System.Collections.Generic;

namespace Shop.Query.Comment.GetWithRepliesByProductId
{
    public record GetCommentsWithRepliesByProductIdQuery(Guid ProductId)
        : IBaseQuery<List<CommentWithReplyDto>>;
}