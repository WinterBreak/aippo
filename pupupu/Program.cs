using Microsoft.EntityFrameworkCore;
using pupupu.Common;
using pupupu.Repositories;
using pupupu.Repositories.Interfaces;
using pupupu.Services;
using pupupu.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// добавления сервисов в контейнер
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BookOrderSystemContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty));

// регистрация репозиториев 
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
// регистрация сервисов
builder.Services.AddScoped<IBookServiceInterface, BookService>();
builder.Services.AddScoped<IAdminPanelUserManagementService, AdminPanelUserManagementService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();