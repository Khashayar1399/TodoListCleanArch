using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoCleanArch.Application.Interfaces;
using TodoCleanArch.Infrastructure.Data;
using TodoCleanArch.Infrastructure.Repositories;

namespace TodoCleanArch.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TodoDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<ITodoRepository, TodoRepository>();

        return services;
    }
}