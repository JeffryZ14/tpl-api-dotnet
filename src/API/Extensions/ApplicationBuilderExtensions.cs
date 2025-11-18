namespace API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication UseApi(this WebApplication app)
        {
            if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.MapHealthChecks("/health");
            app.MapControllers();

            return app;
        }
    }
}
