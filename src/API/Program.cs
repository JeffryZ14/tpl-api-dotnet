using API.Middlewares;
using AspNetCoreRateLimit;
using Infrastructure.Extensions;
using API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation(builder.Configuration)
                .AddInfrastructure(builder.Configuration)
                .AddApplication();


var app = builder.Build();

app.UseCors("AllowApiTemplate");

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

Console.WriteLine("Connection string: " + builder.Configuration.GetConnectionString("DefaultConnection"));
Console.WriteLine("Environment: " + builder.Environment.EnvironmentName);

//app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
//app.UseMiddleware<ApiKeyMiddleware>();

app.UseIpRateLimiting();

app.MapControllers();

app.Run();