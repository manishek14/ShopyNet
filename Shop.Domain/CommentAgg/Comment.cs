using Common.Domain;
using Shop.Domain.CommentAgg.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.CommentAgg
{
    public class Comment : BaseAggregate
    {
        public Guid UserId { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid ReplyId { get; private set; }
        public string content { get; private set; }
        public CommentStatus status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Comment(Guid userId, Guid productId, string content)
        {
            UserId = userId;
            ProductId = productId;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            this.content = content;
            status = CommentStatus.Pending;
        }

        public void Edit(string content)
        {
            this.content = content;
            UpdatedAt = DateTime.Now;    
        }

        public void ChangeStatus(CommentStatus status)
        {
            this.status = status;
            UpdatedAt = DateTime.Now;
        }
    }
}
