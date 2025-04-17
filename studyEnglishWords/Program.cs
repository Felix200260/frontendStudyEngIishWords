using Microsoft.EntityFrameworkCore;
using studyEnglishWords;
using studyEnglishWords.Models;
using studyEnglishWords.Service;

var builder = WebApplication.CreateBuilder(args);

// настройки сессий
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .EnableSensitiveDataLogging() // для логирование БД
        .LogTo(Console.WriteLine)); // для логирование БД

// регистрирует UserService в DI контейнере с областью действия Scoped, что означает, что каждый HTTP-запрос будет использовать новый экземпляр сервиса.
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DeckService>();

// Добавляем поддержку Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

// Настройка Swagger
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Study English Words API v1");
    c.RoutePrefix = "swagger";
});

app.UseSession();

// Редирект на HTTPS
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowFrontend"); // Применяем политику CORS
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller}/{action=Index}/{id?}");

// Редирект с корневой страницы на Swagger
app.MapGet("/", context => {
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();
