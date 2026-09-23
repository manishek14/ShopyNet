using Shop.Domain.CommentAgg;
using Shop.Query.Comment.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Query.Comment
{
    internal static class CommentMapper
    {
        public static CommentDto? Map(this Domain.CommentAgg.Comment? comment)
        {
            if (comment is null)
                return null;

            return new CommentDto
            {
                Id = comment.Id,
                UserId = comment.UserId,
                ProductId = comment.ProductId,
                ReplyId = comment.ReplyId == Guid.Empty ? null : comment.ReplyId,
                Content = comment.content,
                Status = comment.status,
                CreatedDate = comment.CreatedAt,
                UpdatedDate = comment.UpdatedAt
            };
        }

        public static List<CommentDto> Map(this List<Domain.CommentAgg.Comment>? comments)
        {
            var result = new List<CommentDto>();
            if (comments == null)
                return result;

            foreach (var comment in comments)
            {
                result.Add(Map(comment)!);
            }

            return result;
        }

        public static List<CommentWithReplyDto> MapWithReplies(this List<Domain.CommentAgg.Comment>? comments)
        {
            var result = new List<CommentWithReplyDto>();
            if (comments == null)
                return result;

            var rootComments = comments.Where(c => c.ReplyId == Guid.Empty).ToList();
            var replies = comments.Where(c => c.ReplyId != Guid.Empty).ToList();

            foreach (var root in rootComments)
            {
                var dto = new CommentWithReplyDto
                {
                    Id = root.Id,
                    UserId = root.UserId,
                    ProductId = root.ProductId,
                    ReplyId = null,
                    Content = root.content,
                    Status = root.status,
                    CreatedDate = root.CreatedAt,
                    UpdatedDate = root.UpdatedAt,
                    Replies = replies
                        .Where(r => r.ReplyId == root.Id)
                        .Select(r => Map(r)!)
                        .ToList()
                };
                result.Add(dto);
            }

            return result;
        }
    }
}