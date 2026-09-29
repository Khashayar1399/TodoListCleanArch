using TodoCleanArch.Application;
using TodoCleanArch.Infrastructure;
using TodoCleanArch.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// ۱. تنظیم سیاست CORS برای دسترسی فرانت‌اند
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod(); // این متدهای PUT, PATCH, DELETE را مجاز می‌کند
    });
});

// ۲. اضافه کردن سرویس‌های کنترلر و سواگر
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ۳. رجیستر سرویس‌های Application و Infrastructure
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices("Data Source=todos.db");

var app = builder.Build();

// ۴. ساخت خودکار جداول دیتابیس
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ۵. استفاده از خط مشی CORS (حتماً قبل از MapControllers و UseAuthorization باشد)
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();