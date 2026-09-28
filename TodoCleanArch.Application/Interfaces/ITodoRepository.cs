using TodoList.Domain.Entities;

namespace TodoCleanArch.Application.Interfaces;

public interface ITodoRepository
{
    Task<TodoItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(TodoItem todo, CancellationToken cancellationToken = default);
    Task UpdateAsync(TodoItem todo, CancellationToken cancellationToken = default);
    Task DeleteAsync(TodoItem todo, CancellationToken cancellationToken = default);
}