using Common.Aplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Shop.Application.Role.Create;
using Shop.Application.Role.Edit;
using Shop.Application.Role.SetPermissions;
using Shop.Presentation.Facade.Role;
using Shop.Query.Role.DTOs;
using System;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("ApiPolicy")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleFacade _roleFacade;

        public RoleController(IRoleFacade roleFacade)
        {
            _roleFacade = roleFacade;
        }

        // Query
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RoleDto>> GetRoleById(Guid id)
        {
            var role = await _roleFacade.GetRoleById(id);
            if (role == null) return NotFound(new { message = "Role not found" });
            return Ok(role);
        }

        [HttpGet("filter")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RoleFilterData>> GetRolesByFilter(
            [FromQuery] RoleFilterParams filterParams)
        {
            var result = await _roleFacade.GetRolesByFilterQuery(filterParams);
            return Ok(result);
        }

        // Command
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OperationResult>> CreateRole([FromBody] CreateRoleCommand command)
        {
            var result = await _roleFacade.Create(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OperationResult>> EditRole(
            Guid id, [FromBody] EditRoleCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { message = "Id in URL does not match Id in body" });

            var result = await _roleFacade.Edit(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}/set-permissions")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<OperationResult>> SetPermissions(
            Guid id, [FromBody] SetRolePermissionsCommand command)
        {
            if (id != command.RoleId)
                return BadRequest(new { message = "Id in URL does not match RoleId in body" });

            var result = await _roleFacade.SetPermissions(command);
            return result.Status == OperationResultStatus.Success ? Ok(result) : BadRequest(result);
        }
    }
}