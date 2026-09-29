using Microsoft.EntityFrameworkCore;
using TodoCleanArch.Domain.Entities;

namespace TodoCleanArch.Infrastructure.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

    public DbSet<TodoList> Todos => Set<TodoList>();
}