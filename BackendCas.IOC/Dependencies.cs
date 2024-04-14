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

namespace BackendCas.IOC
{
    public static class Dependencies
    {
        public static void InyectionDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BackendCasContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CnxSql"));
            });
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IAdministratorService, AdministratorService>();
            services.AddScoped<IEventsCa, EventsCaService>();

        }
    }
}
