using Microsoft.EntityFrameworkCore;
using TodoCleanArch.Application.Interfaces;
using TodoCleanArch.Domain.Entities;

namespace TodoCleanArch.Infrastructure.Data;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;

    public TodoRepository(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<TodoList>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Todos
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<TodoList?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Todos.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task AddAsync(TodoList item, CancellationToken cancellationToken = default)
    {
        await _context.Todos.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TodoList item, CancellationToken cancellationToken = default)
    {
        _context.Todos.Update(item);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TodoList item, CancellationToken cancellationToken = default)
    {
        _context.Todos.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
    }
}