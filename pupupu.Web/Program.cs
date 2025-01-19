using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using pupupu.Web.Common;
using pupupu.Dal.Repositories;
using pupupu.Bll.Services;

var builder = WebApplication.CreateBuilder(args);

// добавления сервисов в контейнер
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddDbContext<BookOrderSystemContext>(options =>
    options.UseLazyLoadingProxies()
        .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty
    , b => b.MigrationsAssembly("pupupu.Web")));

builder.Services.Configure<IdentityOptions>(options =>
{
    // Настройки пароля
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Настройки блокировки
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Настройки пользователя
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
});


builder.Services.AddControllersWithViews();

// регистрация репозиториев 
builder.Services.AddScoped<IBookRepository, BookRepository>();
// регистрация сервисов
builder.Services.AddScoped<IBookServiceInterface, BookService>();

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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
