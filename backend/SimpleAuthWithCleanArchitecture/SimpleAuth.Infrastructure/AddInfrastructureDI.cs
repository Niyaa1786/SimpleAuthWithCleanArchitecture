using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleAuth.Application.Interfaces.Common;
using SimpleAuth.Application.Interfaces.Shared;
using SimpleAuth.Infrastructure.Data;
using SimpleAuth.Infrastructure.Persistence.Repositories;
using SimpleAuth.Infrastructure.Security;
using SimpleAuth.Infrastructure.Services;


namespace SimpleAuth.Infrastructure
{
    public static class AddInfrastructureDI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITokenGenerator,TokenGenerator>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddScoped<ICloudinaryService, CloudinaryService>();

            return services;
        }
    }
}
