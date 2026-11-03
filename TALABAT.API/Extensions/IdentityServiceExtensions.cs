using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entites.Identity;
using Talabat.Repository.Identity;

namespace TALABAT.API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services,IConfiguration configuration)
        {
            #region Microsoft Identity
            Services.AddIdentity<AppUser, IdentityRole>(options =>
           {
               options.Password.RequireDigit = true;
               options.Password.RequiredLength = 6;
               options.Password.RequireUppercase = false;
               options.Password.RequireNonAlphanumeric = false;
               options.User.RequireUniqueEmail = true;
               // Lockout — after 5 wrong attempts, lock for 5 minutes
               // Protects against brute force attacks
               options.Lockout.MaxFailedAccessAttempts = 4;
               options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

           }).AddEntityFrameworkStores<AppIdentityDbContext>()
                          .AddDefaultTokenProviders();
            
            #endregion

            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
       //do reverse engineering 3shan y3rf y3ml validate l token ELygay
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true, // Reject expired tokens
               ValidateIssuerSigningKey = true,
               ValidIssuer = configuration["JWT:ValidIssuer"],
               ValidAudience = configuration["JWT:ValidAudince"],
               IssuerSigningKey = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
           };
           options.SaveToken = true;
           options.RequireHttpsMetadata = true; //The Reques gay be https only
       });

            return Services;
        }


    }
}
