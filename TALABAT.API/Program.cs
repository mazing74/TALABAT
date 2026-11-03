
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entites.Identity;
using Talabat.Repository;
using Talabat.Repository.Data.DataSeed;
using Talabat.Repository.Identity;
using TALABAT.API.Extensions;

namespace TALABAT.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // ── Database ────────────────────── Extension Method 
            builder.Services.AddDatabaseServices(builder.Configuration);

            // ── ADD Microsoft Identity ────────────Extension Method 
            builder.Services.AddIdentityServices(builder.Configuration);

            // ── Swagger WITH JWT support ────────────────Extension Method
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ── AppService ────────────────────── Extension Method 
            builder.Services.AddAppServices();


            var app = builder.Build();

            #region UbdateDataBase
            var scoped = app.Services.CreateScope();
            var services = scoped.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbcontext = services.GetRequiredService<TalabatContext>();
                await dbcontext.Database.MigrateAsync();

                var IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbContext.Database.MigrateAsync();
                var userManger = services.GetRequiredService<UserManager<AppUser>>();


                await AppIdentityDbContextSeed.SeedUserAsync(userManger);
                await TalabatSeed.seedAsync(dbcontext);

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred while migrating the database.");

            }
            #endregion 

            #region Configure the HTTP request pipeline
            //app da el kestral nfso//
            //Extension Method In My App That Had all middlewares
            app.UseMiddlewaresPipeline();
            #endregion



        }
    }
}
