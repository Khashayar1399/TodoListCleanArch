using MediatR;
using TodoCleanArch.Application.Interfaces;
using TodoList.Domain.Entities;

namespace TodoCleanArch.Application.Todos.Commands;

// درخواست ایجاد تسک جدید که شناسه تسک ساخته شده را برمی‌گرداند
public record CreateTodoCommand(string Title, string? Description) : IRequest<Guid>;

// هندلر مسئول اجرای سناریوی ساخت تسک
public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Guid>
{
    private readonly ITodoRepository _repository;

    public CreateTodoCommandHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        // ۱. استفاده از منطق دامنه برای ایجاد شیء
        var todo = TodoItem.Create(request.Title, request.Description);

        // ۲. ذخیره از طریق قرارداد ریپازیتوری
        await _repository.AddAsync(todo, cancellationToken);

        // ۳. بازگرداندن شناسه
        return todo.Id;
    }
}