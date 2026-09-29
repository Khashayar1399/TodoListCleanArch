using MediatR;
using TodoCleanArch.Application.Interfaces;

namespace TodoCleanArch.Application.Todos.Commands;

public record ToggleTodoCommand(Guid Id) : IRequest<bool>;

public class ToggleTodoCommandHandler : IRequestHandler<ToggleTodoCommand, bool>
{
    private readonly ITodoRepository _repository;

    public ToggleTodoCommandHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(ToggleTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (todo is null) return false;

        todo.ToggleComplete();
        await _repository.UpdateAsync(todo, cancellationToken);
        return true;
    }
}