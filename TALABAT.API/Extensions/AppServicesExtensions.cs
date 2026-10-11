using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entites;
using Talabat.Core.Interfaces;
using Talabat.Repository.Repositories;
using TALABAT.API.Errors;

namespace TALABAT.API.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection Services)
        {
             Services.AddControllers();

            Services.AddScoped(typeof(IGenaricRepository<>),typeof(GenaricRepository<>));


            //Why GetAssemblies()? This tells AutoMapper to scan ALL your projects for classes that inherit from Profile
            //— so it finds any one automatically.
            Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #region HandleValidationError
            // ── Validation Error Response ─────────────────────────
            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actioncontext) =>
                {
                    var errors = actioncontext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                              .SelectMany(p => p.Value.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();

                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });
            #endregion


            return Services;
        }

    }
}
