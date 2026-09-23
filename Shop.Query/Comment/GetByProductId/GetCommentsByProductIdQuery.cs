using Shop.Query.Comment.DTOs;
using System;
using System.Collections.Generic;

namespace Shop.Query.Comment.GetByProductId
{
    public record GetCommentsByProductIdQuery(Guid ProductId) : IBaseQuery<List<CommentDto>>;
}