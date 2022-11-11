using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Helpers;
using Services.Interfaces;
using Services.Services;
using SocialApp.DataAccess.Data;
using SocialApp.DataAccess.Interfaces;

namespace SocialApp.API.Extensions;

public static class AddApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        services.AddDbContext<SocialAppContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("DefaultConnection"));

        });
        return services;
    }
}