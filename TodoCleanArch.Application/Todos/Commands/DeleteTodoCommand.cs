using MediatR;
using TodoCleanArch.Application.Interfaces;

namespace TodoCleanArch.Application.Todos.Commands;

public record DeleteTodoCommand(Guid Id) : IRequest<bool>;

public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, bool>
{
    private readonly ITodoRepository _repository;

    public DeleteTodoCommandHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (todo is null) return false;

        await _repository.DeleteAsync(todo, cancellationToken);
        return true;
    }
}