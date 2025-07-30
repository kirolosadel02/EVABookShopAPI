using EVABookShopAPI.DB;
using Microsoft.EntityFrameworkCore;
using Scrutor;
using EVABookShopAPI.Service.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using FluentValidation.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Database context
builder.Services.AddDbContext<EVABookShopAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add response caching
builder.Services.AddResponseCaching();

// Register controllers and configure cache profiles
builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("Default30", new CacheProfile
    {
        Duration = 30,
        Location = ResponseCacheLocation.Client,
        NoStore = false
    });
    options.CacheProfiles.Add("Server60", new CacheProfile
    {
        Duration = 60,
        Location = ResponseCacheLocation.Any,
        NoStore = false
    });
}).AddNewtonsoftJson();

// Configure API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0); // Default to v1.0
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(), // Reads version from URL segment (v{version})
        new QueryStringApiVersionReader("version"), // Fallback: ?version=1.0
        new HeaderApiVersionReader("X-Version") // Fallback: X-Version header
    );
    options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
}).AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV"; // Format: v1.0, v2.0, etc.
    setup.SubstituteApiVersionInUrl = true;
});

// Swagger with versioning support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1.0",
        Title = "EVA BookShop API v1.0",
        Description = "BookShop API version 1.0"
    });

    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Version = "v2.0", 
        Title = "EVA BookShop API v2.0",
        Description = "BookShop API version 2.0"
    });
});

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

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(EVABookShopAPI.Service.AssemblyReference.Assembly, includeInternalTypes: true);
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        return new BadRequestObjectResult(new
        {
            Message = "Validation failed",
            Errors = errors
        });
    };
});


var app = builder.Build();

// Development tools
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "EVA BookShop API v1.0");
        // Add more versions as needed
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "EVA BookShop API v2.0");
    });
}

// Enable response caching middleware
app.UseResponseCaching();

// Authorization (if needed)
app.UseAuthorization();

// Map endpoints
app.MapControllers();

app.Run();