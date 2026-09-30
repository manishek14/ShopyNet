using Common.Aplication;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Comment.ChangeStatus;
using Shop.Application.Comment.Create;
using Shop.Application.Comment.Edit;
using Shop.Presentation.Facade.Comment;
using Shop.Query.Comment.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentFacade _commentFacade;

        public CommentController(ICommentFacade commentFacade)
        {
            _commentFacade = commentFacade;
        }

        // Query
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDto>> GetCommentById(Guid id)
        {
            var comment = await _commentFacade.GetCommentById(id);
            if (comment == null) return NotFound(new { message = "Comment not found" });
            return Ok(comment);
        }

        [HttpGet("product/{productId}")]
        public async Task<ActionResult<List<CommentDto>>> GetCommentsByProductId(Guid productId)
        {
            var comments = await _commentFacade.GetCommentsByProductId(productId);
            return Ok(comments);
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentDto>>> GetComments()
        {
            var comments = await _commentFacade.GetComments();
            return Ok(comments);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<CommentFilterData>> GetCommentsByFilter(
            [FromQuery] CommentFilterParams filterParams)
        {
            var result = await _commentFacade.GetCommentsByFilter(filterParams);
            return Ok(result);
        }

        // Command
        [HttpPost]
        public async Task<ActionResult<OperationResult>> CreateComment(
            [FromBody] CreateCommentCommand command)
        {
            var result = await _commentFacade.Create(command);
            if (result.Status == OperationResultStatus.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResult>> EditComment(
            Guid id, [FromBody] EditCommentCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { message = "Id in URL does not match Id in body" });

            var result = await _commentFacade.Edit(command);
            if (result.Status == OperationResultStatus.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("{id}/change-status")]
        public async Task<ActionResult<OperationResult>> ChangeStatus(
            Guid id, [FromBody] ChangeCommentStatusCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { message = "Id in URL does not match Id in body" });

            var result = await _commentFacade.ChangeStatus(command);
            if (result.Status == OperationResultStatus.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}