using Microsoft.EntityFrameworkCore;
using studyEnglishWords.Models;

var builder = WebApplication.CreateBuilder(args);

// Настройка CORS. Необходимо для решение ошибки CORS policy: No 'Access-Control-Allow-Origin' header is present on the requested resource
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:8080") 
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Настройка логирования
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Логирование в консоль
builder.Logging.AddDebug();   // Логирование в отладчик (Debug)

// Добавляем контроллеры и представления
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

// Редирект на HTTPS
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowFrontend"); // Применяем политику CORS
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller}/{action=Index}/{id?}");

app.Run();
