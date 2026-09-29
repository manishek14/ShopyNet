using Common.Aplication;
using MediatR;
using Shop.Application.Role.Create;
using Shop.Application.Role.Edit;
using Shop.Application.Role.SetPermissions;
using Shop.Query.Role.DTOs;
using Shop.Query.Role.GetByFilter;
using Shop.Query.Role.GetById;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.Role
{
    internal class RoleFacade : IRoleFacade
    {
        private readonly IMediator _mediator;

        public RoleFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Create(CreateRoleCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> Edit(EditRoleCommand command)
            => await _mediator.Send(command);

        public async Task<OperationResult> SetPermissions(SetRolePermissionsCommand command)
            => await _mediator.Send(command);

        public async Task<RoleDto> GetRoleById(Guid id)
            => await _mediator.Send(new GetRoleByIdQuery(id));

        public async Task<RoleFilterData> GetRolesByFilterQuery(RoleFilterParams FilterParams)
            => await _mediator.Send(new GetRolesByFilterQuery(FilterParams));
    }
}