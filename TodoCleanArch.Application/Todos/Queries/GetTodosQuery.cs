using MediatR;
using TodoCleanArch.Application.Common;
using TodoCleanArch.Application.Interfaces;

namespace TodoCleanArch.Application.Todos.Queries;

// درخواست دریافت تمام تسک‌ها
public record GetTodosQuery : IRequest<IReadOnlyList<TodoDto>>;

// هندلر مسئول دریافت و تبدیل داده‌ها به DTO
public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, IReadOnlyList<TodoDto>>
{
    private readonly ITodoRepository _repository;

    public GetTodosQueryHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<TodoDto>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(cancellationToken);

        // نگاشت به DTO
        return items.Select(x => new TodoDto(
            x.Id,
            x.Title,
            x.Description,
            x.IsCompleted,
            x.CreatedAt
        )).ToList();
    }
}