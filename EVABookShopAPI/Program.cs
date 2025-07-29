using EVABookShopAPI.DB;
using Microsoft.EntityFrameworkCore;
using Scrutor;
using EVABookShopAPI.Service.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EVABookShopAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Scan(selector => selector
        .FromAssemblies(
            EVABookShopAPI.Service.AssemblyReference.Assembly,
            EVABookShopAPI.Repository.AssemblyReference.Assembly,
            EVABookShopAPI.UnitofWork.AssemblyReference.Assembly)
        .AddClasses()
        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        .AsImplementedInterfaces()
        .WithScopedLifetime());

builder.Services.AddAutoMapper(typeof(CategoryMappingProfile).Assembly, typeof(BookMappingProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
