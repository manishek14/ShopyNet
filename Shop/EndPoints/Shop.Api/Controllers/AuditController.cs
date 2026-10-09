using Common.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Shop.Query.Audit.DTOs;
using Shop.Query.Audit.GetByFilter;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [EnableRateLimiting("ApiPolicy")]
    public class AuditController : ApiController
    {
        private readonly IMediator _mediator;

        public AuditController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<ApiResult<AuditLogFilterData>>> GetAuditLogs(
            [FromQuery] AuditLogFilterParams filterParams)
        {
            var result = await _mediator.Send(new GetAuditLogsByFilterQuery(filterParams));
            return Ok(result);
        }
    }
}