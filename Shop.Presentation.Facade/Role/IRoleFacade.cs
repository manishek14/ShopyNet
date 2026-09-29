using Common.Aplication;
using Shop.Application.Role.Create;
using Shop.Application.Role.Edit;
using Shop.Application.Role.SetPermissions;
using Shop.Query.Role.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.Role
{
    public interface IRoleFacade
    {
        Task<OperationResult> Create(CreateRoleCommand command);
        Task<OperationResult> Edit(EditRoleCommand command);
        Task<OperationResult> SetPermissions(SetRolePermissionsCommand command);

        Task<RoleDto> GetRoleById(Guid id);
        Task<RoleFilterData> GetRolesByFilterQuery(RoleFilterParams FilterParams);
    }
}