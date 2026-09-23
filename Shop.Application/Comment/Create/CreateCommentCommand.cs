using Common.Aplication;
using Common.Application;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Comment.Create
{
    public record CreateCommentCommand(Guid UserId, Guid ProductId, string Content) : IBaseCommand;
}