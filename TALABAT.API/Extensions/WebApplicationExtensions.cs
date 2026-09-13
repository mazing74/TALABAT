namespace TALABAT.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication UseMiddlewaresPipeline (this WebApplication app)
        {

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();

            return app;
        }
    }
}
