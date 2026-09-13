using Microsoft.EntityFrameworkCore;
using Talabat.Repository;

namespace TALABAT.API.Extensions
{
    public static class DatabaseServiceExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<TalabatContext>(options
                => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            return Services;
        }

    }
}
