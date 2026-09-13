using Talabat.Core.Entites;
using Talabat.Core.Interfaces;
using Talabat.Repository.Repositories;
using TALABAT.API.Helpers;

namespace TALABAT.API.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection Services)
        {
            Services.AddScoped<IGenaricRepository<Product>, GenaricRepository<Product>>();


            //Why GetAssemblies()? This tells AutoMapper to scan ALL your projects for classes that inherit from Profile
            //— so it finds any one automatically.
            Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return Services;
        }

    }
}
