using MediatR;
using TodoCleanArch.Application.Interfaces;

namespace TodoCleanArch.Application.Todos.Commands;

public record UpdateTodoCommand(Guid Id, string Title, string? Description, DateTime? DueDate) : IRequest<bool>;

public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, bool>
{
    private readonly ITodoRepository _repository;

    public UpdateTodoCommandHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (todo is null) return false;

        todo.Update(request.Title, request.Description, request.DueDate);
        await _repository.UpdateAsync(todo, cancellationToken);
        return true;
    }
}