using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

var connectionString = "server=localhost;database=seletivorh;user=root;password=''";

builder.Services.AddDbContext<MvcMovieContext>(options =>
     options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// if (builder.Environment.IsDevelopment())
// {
//     builder.Services.AddDbContext<MvcMovieContext>(options =>
//         options.UseSqlite(builder.Configuration.GetConnectionString("MvcMovieContext")));
// }
// else
// {
//     builder.Services.AddDbContext<MvcMovieContext>(options =>
//         options.UseSqlServer(builder.Configuration.GetConnectionString("ProductionMvcMovieContext")));
// }

// Add services to the container.
builder.Services.AddControllersWithViews();

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
