using EVABookShopAPI.DB;
using Microsoft.EntityFrameworkCore;
using Scrutor;
using EVABookShopAPI.Service.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Database context
builder.Services.AddDbContext<EVABookShopAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add response caching
builder.Services.AddResponseCaching();

// Register controllers with JsonPatch support and configure cache profiles
builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("Default30", new Microsoft.AspNetCore.Mvc.CacheProfile
    {
        Duration = 30,
        Location = Microsoft.AspNetCore.Mvc.ResponseCacheLocation.Client,
        NoStore = false
    });
    options.CacheProfiles.Add("Server60", new Microsoft.AspNetCore.Mvc.CacheProfile
    {
        Duration = 60,
        Location = Microsoft.AspNetCore.Mvc.ResponseCacheLocation.Any,
        NoStore = false
    });
})
.AddNewtonsoftJson(); // Enable JsonPatch support

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Service scanning via Scrutor
builder.Services.Scan(selector => selector
    .FromAssemblies(
        EVABookShopAPI.Service.AssemblyReference.Assembly,
        EVABookShopAPI.Repository.AssemblyReference.Assembly,
        EVABookShopAPI.UnitofWork.AssemblyReference.Assembly)
    .AddClasses()
    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
    .AsImplementedInterfaces()
    .WithScopedLifetime());

// AutoMapper
builder.Services.AddAutoMapper(typeof(CategoryMappingProfile).Assembly, typeof(BookMappingProfile).Assembly);

var app = builder.Build();

// Development tools
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable response caching middleware
app.UseResponseCaching();

// Authorization (if needed)
app.UseAuthorization();

// Map endpoints
app.MapControllers();

app.Run();