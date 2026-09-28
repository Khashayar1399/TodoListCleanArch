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

    // دریافت لیست همه تسک‌ها
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TodoDto>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetTodosQuery(), cancellationToken);
        return Ok(result);
    }

    // ثبت یک تسک جدید
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateTodoRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateTodoCommand(request.Title, request.Description);
        var id = await _sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetAll), new { id }, id);
    }
}

// مدل ورودی برای درخواست کلاینت
public record CreateTodoRequest(string Title, string? Description);