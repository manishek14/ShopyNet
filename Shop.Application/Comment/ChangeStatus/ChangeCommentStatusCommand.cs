using Common.Aplication;
using Common.Application;
using Shop.Domain.CommentAgg.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Comment.ChangeStatus
{
    public record ChangeCommentStatusCommand( Guid Id,CommentStatus Status) : IBaseCommand;
}