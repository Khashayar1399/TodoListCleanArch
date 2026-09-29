using MediatR;
using TodoCleanArch.Application.Common;
using TodoCleanArch.Application.Interfaces;

namespace TodoCleanArch.Application.Todos.Queries;

public record GetTodosQuery : IRequest<IReadOnlyList<TodoDto>>;

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, IReadOnlyList<TodoDto>>
{
    private readonly ITodoRepository _repository;

    public GetTodosQueryHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<TodoDto>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var list = await _repository.GetAllAsync(cancellationToken);
        return list.Select(t => new TodoDto(
            t.Id,
            t.Title,
            t.Description,
            t.IsCompleted,
            t.CreatedAt,
            t.DueDate
        )).ToList();
    }
}