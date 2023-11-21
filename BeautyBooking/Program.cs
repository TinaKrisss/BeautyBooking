using BeautyBooking.Data;
using Microsoft.EntityFrameworkCore;
using BeautyBooking.Data.Interfaces;
using BeautyBooking.Data.Services;
using BeautyBooking.Data.Static;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

//Add db context
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

//Services configuration
builder.Services.AddScoped<IClientsService, ClientsService>();
builder.Services.AddScoped<IServicesService, ServicesService>();
builder.Services.AddScoped<IMastersService, MastersService>();
builder.Services.AddScoped<IPasswordCreator, PasswordCreator>();
// Add services to the container.
builder.Services.AddControllersWithViews();//.AddRazorRuntimeCompilation();

builder.Services.AddSession();

var app = builder.Build();
app.UseSession();

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
	pattern: "{controller=Services}/{action=Index}");

//Seed db
AppDbInitializer.Seed(app);
AdminData.Username = builder.Configuration["Admin:Username"];
AdminData.Password = builder.Configuration["Admin:Password"];

app.Run();
