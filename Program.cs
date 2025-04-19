using BasicMVC.Context;
using BasicMVC.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITutorialRepository, TutorialRepository>(); // registering the Dependency

// register article service
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

// Configure DbContext with Npgsql and NetTopologySuite
builder.Services.AddDbContext<TutorialDbContext>(options =>
   options.UseNpgsql(builder.Configuration.GetConnectionString("TutorialDatabase"), o => o.UseNetTopologySuite()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // responsible to serve the static files from the wwwroot folder (default folder)
// to serve the static files from any onther folder we use below overloaded method
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(Directory.GetCurrentDirectory(), "StaticFileTesting")),
    RequestPath = "/StaticFileTesting"
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Here the default action method is index.html. If you write action=Privacy, the default page that will be loaded is Privacy

app.Run();
