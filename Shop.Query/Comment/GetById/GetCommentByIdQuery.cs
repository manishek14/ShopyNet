using Shop.Query.Comment.DTOs;
using System;

namespace Shop.Query.Comment.GetById
{
    public record GetCommentByIdQuery(Guid Id) : IBaseQuery<CommentDto>;
}