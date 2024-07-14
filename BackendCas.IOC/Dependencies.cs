using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BackendCas.DAL.Repositories;
using BackendCas.DAL.Repositories.Contrat;
using BackendCas.DAL.DBContext;
using BackendCas.UTILITY;
using BackendCas.BLL.Services.Contrat;
using BackendCas.BLL.Services;

namespace BackendCas.IOC;

public static class Dependencies
{
    public static void InyectionDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(("CnxSql"));
        try
        {
            var optionsBuilder = new DbContextOptionsBuilder<BackendCasContext>();
            optionsBuilder.UseSqlServer(connectionString);
            using (var context = new BackendCasContext(optionsBuilder.Options))
            {
                context.Database.OpenConnection();
                context.Database.CloseConnection();
            }
        }
        catch
        {
            connectionString = configuration.GetConnectionString("DockerSql");
        }
        
        services.AddDbContext<BackendCasContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddAutoMapper(typeof(AutoMapperProfile));

        services.AddScoped<IAdministratorService, AdministratorService>();
        services.AddScoped<IAutorizacionService, AuthorizationService>();
        services.AddScoped<IEventsCa, EventsCaService>();
    }
}