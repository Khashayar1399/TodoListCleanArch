using Microsoft.EntityFrameworkCore;
using TodoCleanArch.Application.Interfaces;
using TodoCleanArch.Infrastructure.Data;
using TodoList.Domain.Entities;

namespace TodoCleanArch.Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _dbContext;

    public TodoRepository(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TodoItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Todos.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Todos.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TodoItem todo, CancellationToken cancellationToken = default)
    {
        await _dbContext.Todos.AddAsync(todo, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TodoItem todo, CancellationToken cancellationToken = default)
    {
        _dbContext.Todos.Update(todo);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TodoItem todo, CancellationToken cancellationToken = default)
    {
        _dbContext.Todos.Remove(todo);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}