namespace TodoCleanArch.Domain.Entities;

public class TodoList
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? DueDate { get; private set; }

    // سازنده بدون پارامتر برای استفاده EF Core
    private TodoList() { }

    public TodoList(string title, string? description, DateTime? dueDate = null)
    {
        SetTitle(title);
        Description = description;
        Id = Guid.NewGuid();
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
        DueDate = dueDate;
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("عنوان تسک نمی‌‌تواند خالی باشد.");

        Title = title.Trim();
    }

    public void Update(string title, string? description, DateTime? dueDate)
    {
        SetTitle(title);
        Description = description;
        DueDate = dueDate;
    }

    public void ToggleComplete()
    {
        IsCompleted = !IsCompleted;
    }
}