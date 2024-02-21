using Microsoft.EntityFrameworkCore;
using Reactivities.Application.Activities;
using Reactivities.Application.Core;
using Reactivities.Persistence;

namespace Reactivities.Api.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddDbContext<DataContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")))
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly))
            .AddAutoMapper(typeof(MappingProfiles).Assembly)
            .AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy",
                    policy => { policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"); });
            });

        return services;
    }
}