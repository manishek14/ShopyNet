using Common.Aplication;
using Shop.Application.Comment.ChangeStatus;
using Shop.Application.Comment.Create;
using Shop.Application.Comment.Edit;
using Shop.Query.Comment.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.Comment
{
    public interface ICommentFacade
    {
        Task<OperationResult> Create(CreateCommentCommand command);
        Task<OperationResult> Edit(EditCommentCommand command);
        Task<OperationResult> ChangeStatus(ChangeCommentStatusCommand command);

        Task<CommentDto> GetCommentById(Guid id);
        Task<List<CommentDto>> GetCommentsByProductId(Guid productId);
        Task<List<CommentDto>> GetComments();
    }
}