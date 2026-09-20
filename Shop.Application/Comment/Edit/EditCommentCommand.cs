using Common.Aplication;
using Common.Application;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Comment.Edit
{
    public record EditCommentCommand(Guid Id, Guid UserId, string Content) : IBaseCommand;
}