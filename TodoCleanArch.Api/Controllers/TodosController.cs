using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoCleanArch.Application.Common;
using TodoCleanArch.Application.Todos.Commands;
using TodoCleanArch.Application.Todos.Queries;

namespace TodoCleanArch.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ISender _sender;

    public TodosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TodoDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetTodosQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateTodoRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateTodoCommand(request.Title, request.Description, request.DueDate);
        var id = await _sender.Send(command, cancellationToken);
        return Ok(id);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTodoRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateTodoCommand(id, request.Title, request.Description, request.DueDate);
        var result = await _sender.Send(command, cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPatch("{id:guid}/toggle")]
    public async Task<IActionResult> Toggle(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new ToggleTodoCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteTodoCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}

public record CreateTodoRequest(string Title, string? Description, DateTime? DueDate);
public record UpdateTodoRequest(string Title, string? Description, DateTime? DueDate);