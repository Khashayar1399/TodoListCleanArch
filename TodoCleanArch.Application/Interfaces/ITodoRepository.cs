using TodoCleanArch.Domain.Entities;

namespace TodoCleanArch.Application.Interfaces;

public interface ITodoRepository
{
    Task<IReadOnlyList<TodoList>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TodoList?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(TodoList item, CancellationToken cancellationToken = default);
    Task UpdateAsync(TodoList item, CancellationToken cancellationToken = default);
    Task DeleteAsync(TodoList item, CancellationToken cancellationToken = default);
}