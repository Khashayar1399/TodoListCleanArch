using MediatR;
using TodoCleanArch.Application.Interfaces;
using TodoCleanArch.Domain.Entities;

namespace TodoCleanArch.Application.Todos.Commands;

public record CreateTodoCommand(string Title, string? Description, DateTime? DueDate = null) : IRequest<Guid>;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Guid>
{
    private readonly ITodoRepository _repository;

    public CreateTodoCommandHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var item = new TodoList(request.Title, request.Description, request.DueDate);
        await _repository.AddAsync(item, cancellationToken);
        return item.Id;
    }
}