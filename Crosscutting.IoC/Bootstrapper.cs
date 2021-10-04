using Data;
using Data.Data;
using Data.Repositorio;
using Domain.Model.Interface.Service;
using Domain.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Domain.Model.Interface.Repositorio;

namespace Crosscutting.IoC
{
    public static class Bootstrapper
    {
        public static void RegisterServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<FabricantesContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("FabricantesContext")));


            services.AddTransient<IProcessadorService, ProcessadorService>();
            services.AddTransient<IProcessadorRepository, ProcessadorRepository>();

            services.AddTransient<IFabricanteService, FabricanteService>();
            services.AddTransient<IFabricanteRepository, FabricanteRepository>();

        }
    }
}
