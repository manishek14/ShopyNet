using Shop.Query.Comment.DTOs;
using System.Collections.Generic;

namespace Shop.Query.Comment.GetList
{
    public record GetCommentsListQuery() : IBaseQuery<List<CommentDto>>;
}