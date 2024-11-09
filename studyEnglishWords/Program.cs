using Microsoft.EntityFrameworkCore;
using studyEnglishWords.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавляем контроллеры и представления
builder.Services.AddControllersWithViews();
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "wwwroot"; // Папка для фронтенда
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated(); // Проверяет соединение и создаёт таблицы, если их нет
}

// Если не режим разработки, используем обработчик ошибок и HSTS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseSpaStaticFiles();
}
else
{
    // Использование Vite Dev Server в режиме разработки
    app.UseSpa(spa =>
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
    });
}

// Редирект на HTTPS
app.UseHttpsRedirection();
app.UseStaticFiles();

// Настройка маршрутизации
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Использование SPA для Production
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "wwwroot"; // Путь для собранных фронтенд файлов
});

// Запуск приложения
app.Run();
