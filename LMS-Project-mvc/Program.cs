using Microsoft.EntityFrameworkCore;
using LMS_Project_mvc.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


////*** mssql
//builder.Services.AddDbContext<MediaDB>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("MsSQLConnection")));

//*** mysql
builder.Services.AddDbContextPool<MediaDB>(opt => opt.UseMySQL(builder.Configuration.GetConnectionString("MySQLConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Media}/{action=Index}/{id?}");

app.Run();
