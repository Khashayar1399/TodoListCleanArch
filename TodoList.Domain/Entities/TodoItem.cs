using TodoCleanArch.Domain.Exceptions;

namespace TodoList.Domain.Entities;

public class TodoItem
{
    // شناسه یکتا
    public Guid Id { get; private set; }

    // عنوان تسک
    public string Title { get; private set; } = string.Empty;

    // توضیحات اختیاری
    public string? Description { get; private set; }

    // وضعیت انجام شدن
    public bool IsCompleted { get; private set; }

    // زمان ایجاد
    public DateTime CreatedAt { get; private set; }

    // سازنده خصوصی برای جلوگیری از ساخت مستقیم شیء بدون رعایت قوانین دامنه
    private TodoItem() { }

    // یک متد سازنده (Factory Method) برای ساخت تسک جدید طبق قوانین بیزینس
    public static TodoItem Create(string title, string? description = null)
    {
        ValidateTitle(title);

        return new TodoItem
        {
            Id = Guid.NewGuid(),
            Title = title.Trim(),
            Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim(),
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };
    }

    // رفتار بیزینس: تغییر وضعیت به انجام‌شده
    public void Complete()
    {
        IsCompleted = true;
    }

    // رفتار بیزینس: بازگرداندن به حالت انجام‌نشده
    public void MarkIncomplete()
    {
        IsCompleted = false;
    }

    // رفتار بیزینس: ویرایش عنوان به همراه اعتبارسنجی
    public void UpdateTitle(string newTitle)
    {
        ValidateTitle(newTitle);
        Title = newTitle.Trim();
    }

    // ویرایش توضیحات
    public void UpdateDescription(string? newDescription)
    {
        Description = string.IsNullOrWhiteSpace(newDescription) ? null : newDescription.Trim();
    }

    // قانون دامنه: عنوان نباید خالی یا فقط فاصله باشد
    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new InvalidTitleException();
        }
    }
}