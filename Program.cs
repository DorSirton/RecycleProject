using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecyclingProject.Data.Contexts;
using RecyclingProject.Data.Repositories.Classes;
using RecyclingProject.Data.Repositories.Interfaces;
using RecyclingProject.Services.Classes;
using RecyclingProject.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RecycleProjectDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RecyclingConnectionString")));
builder.Services.AddDbContext<AuthenticationContext>(options => options.UseSqlite("Data Source=c:\\temp\\user.db"));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<AuthenticationContext>();
builder.Services.AddControllersWithViews();

//repositories
builder.Services.AddScoped<ICollectorRepository, CollectorRepository>();
builder.Services.AddScoped<IRecyclerRepository, RecyclerRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//services
builder.Services.AddScoped<ICollectorService, CollectorService>();
builder.Services.AddScoped<IRecyclerService, RecyclerService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
