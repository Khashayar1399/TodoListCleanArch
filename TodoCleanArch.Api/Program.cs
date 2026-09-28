using TodoCleanArch.Application;
using TodoCleanArch.Infrastructure;
using TodoCleanArch.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// ۱. اضافه کردن سرویس‌های کنترلر و سواگر
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ۲. رجیستر کردن سرویس‌های لایه Application (MediatR و هندلرها)
builder.Services.AddApplicationServices();

// ۳. رجیستر کردن سرویس‌های لایه Infrastructure (DbContext و ریپازیتوری‌ها با SQLite)
builder.Services.AddInfrastructureServices("Data Source=todos.db");

var app = builder.Build();

// ۴. ساخت خودکار فایل و جدول دیتابیس در زمان اجرای برنامه
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
    dbContext.Database.EnsureCreated();
}

// ۵. پایپ‌لاین اجرای برنامه و Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();