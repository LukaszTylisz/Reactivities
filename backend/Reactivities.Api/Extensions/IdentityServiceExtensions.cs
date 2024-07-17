﻿using Reactivities.Domain;
using Reactivities.Persistence;

namespace Reactivities.Api.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityCore<AppUser>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = false;
        })
        .AddEntityFrameworkStores<DataContext>();

        services.AddAuthentication();

        return services;
    }
}
