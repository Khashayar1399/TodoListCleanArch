namespace TodoCleanArch.Domain.Exceptions;

// کلاس پایه برای تمام خطاهای لایه دامنه
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }
}

// خطای اختصاصی زمانی که عنوان تسک نامعتبر است
public class InvalidTitleException : DomainException
{
    public InvalidTitleException() : base("عنوان تسک نمی‌تواند خالی باشد.")
    {
    }

    public InvalidTitleException(string message) : base(message)
    {
    }
}