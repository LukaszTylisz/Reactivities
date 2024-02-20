using Microsoft.EntityFrameworkCore;
using Reactivities.Application.Activities;
using Reactivities.Application.Core;
using Reactivities.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDbContext<DataContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")))
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly))
    .AddAutoMapper(typeof(MappingProfiles).Assembly)
    .AddCors(opt =>
    {
        opt.AddPolicy("CorsPolicy", policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
        });
    });
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app
    .UseCors("CorsPolicy")
    .UseAuthorization()
    .UseHttpsRedirection();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();