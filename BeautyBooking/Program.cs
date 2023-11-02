using BeautyBooking.Data;
using Microsoft.EntityFrameworkCore;
using BeautyBooking.Data.Interfaces;
using BeautyBooking.Data.Services;

var builder = WebApplication.CreateBuilder(args);

//Add db context
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

//Services configuration
builder.Services.AddScoped<IClientsService, ClientsService>();
//builder.Services.AddScoped<IProducersService, ProducersService>();
//builder.Services.AddScoped<ICinemasService, CinemasService>();
//builder.Services.AddScoped<IMoviesService, MoviesService>();

//Authentication and authorization
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>();
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddUserStore<AppDbContext>();
//builder.Services.AddMemoryCache();
//builder.Services.AddSession();
//builder.Services.AddAuthentication(options =>
//{
//	options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//});

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

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
	pattern: "{controller=Account}/{action=Register}");

//Seed db
AppDbInitializer.Seed(app);

app.Run();
