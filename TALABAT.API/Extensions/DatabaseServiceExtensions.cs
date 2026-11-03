using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Repository;
using Talabat.Repository.Identity;

namespace TALABAT.API.Extensions
{
    public static class DatabaseServiceExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            //My DataBase Connection
            Services.AddDbContext<TalabatContext>(options
                => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Identity DataBase Connection
            Services.AddDbContext<AppIdentityDbContext>(options
               => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            //Redis Connection
            Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connection = Configuration.GetConnectionString("RedisConnection");

                return ConnectionMultiplexer.Connect(connection);
            });


            return Services;
        }

    }
}
