using TALABAT.API.MiddleWares;

namespace TALABAT.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication UseMiddlewaresPipeline (this WebApplication app)
        {

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleWare>();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseStaticFiles();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            return app;
        }
    }
}
