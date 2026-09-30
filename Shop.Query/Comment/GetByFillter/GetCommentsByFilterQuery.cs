using Shop.Query.Comment.DTOs;

namespace Shop.Query.Comment.GetByFilter
{
    public record GetCommentsByFilterQuery(CommentFilterParams FilterParams)
        : IBaseQuery<CommentFilterData>;
}