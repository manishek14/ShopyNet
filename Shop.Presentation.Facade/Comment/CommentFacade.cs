using Common.Aplication;
using MediatR;
using Shop.Application.Comment.ChangeStatus;
using Shop.Application.Comment.Create;
using Shop.Application.Comment.Edit;
using Shop.Query.Comment.DTOs;
using Shop.Query.Comment.GetById;
using Shop.Query.Comment.GetByFilter;
using Shop.Query.Comment.GetByProductId;
using Shop.Query.Comment.GetList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.Comment
{
    internal class CommentFacade : ICommentFacade
    {
        private readonly IMediator _mediator;

        public CommentFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Create(CreateCommentCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> Edit(EditCommentCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> ChangeStatus(ChangeCommentStatusCommand command)
            => await _mediator.Send(command);

        public async Task<CommentDto> GetCommentById(Guid id)
            => await _mediator.Send(new GetCommentByIdQuery(id));

        public async Task<List<CommentDto>> GetCommentsByProductId(Guid productId)
            => await _mediator.Send(new GetCommentsByProductIdQuery(productId));

        public async Task<List<CommentDto>> GetComments()
            => await _mediator.Send(new GetCommentsListQuery());

        public async Task<CommentFilterData> GetCommentsByFilter(CommentFilterParams filterParams)
            => await _mediator.Send(new GetCommentsByFilterQuery(filterParams));
    }
}