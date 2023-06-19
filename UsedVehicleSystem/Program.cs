using UsedVehicleSystem.Database;
using UsedVehicleSystem.Facade;
using UsedVehicleSystem.Mediator;
using UsedVehicleSystem.Repository;
using UsedVehicleSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddDbContext<SystemDBContext>();

builder.Services.AddScoped<IRepoMediator, RepoMediator>();
builder.Services.AddScoped<IFacadeService, FacadeService>();

builder.Services.AddScoped<IAracRepository, AracRepository>();
builder.Services.AddScoped<IAracYonetimi, AracYonetimi>();

builder.Services.AddScoped<IHesapYonetimi, HesapYonetimi>();
builder.Services.AddScoped<IUyeRepository, UyeRepository>();

builder.Services.AddScoped<IIlanRepository, IlanRepository>();
builder.Services.AddScoped<IIlanYonetimi, IlanYonetimi>();

builder.Services.AddScoped<IYorumRepository, YorumRepository>();
builder.Services.AddScoped<IYorumYonetimi, YorumYonetimi>();

builder.Services.AddScoped<ISistemYoneticisiRepository, SistemYoneticisiRepository>();
builder.Services.AddScoped<ISistemYonetimi, SistemYonetimi>();

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
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
