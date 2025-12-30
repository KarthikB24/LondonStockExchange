using LSE.Application.Abstraction.Security;
using LSE.Application.Commands; 
using LSE.Infrastructure.Persistence;
using LSE.Infrastructure.Repositories.Security;
using LSE.Infrastructure.Repositories.Stocks; 
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using System.Data; 

namespace LSE.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
                   this IServiceCollection services, IConfiguration configuration)
        {
            // EF Core Write DB
            services.AddDbContext<LSEWriteDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LSEWriteDbContext")));

            // EF Core Read DB 
            services.AddDbContext<LSEReadDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LSEReadDbContext")));

            // Dapper connection for read
            services.AddScoped<IDbConnection>(_ =>
                new SqlConnection(configuration.GetConnectionString("LSEReadDbContext")));

            //services.AddScoped<IStockQueryRepository, StockQueryRepository>();
            //services.AddScoped<IAddTradeRepository, AddTradeRepository>();
            //services.AddScoped<IGetTradeQueryRepository, GetTradeQueryRepository>();
            // auto-register all repositories
            services.Scan(scan => scan
                .FromAssemblyOf<StockRepository>()  // pick your infra assembly
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // JWT SERVICE
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            // MemoryCache for latest price cache
            services.AddMemoryCache();

            return services;
        }
    }
}
