
using API.Common;
using Application.Behaviors;
using Application.Common;
using Application.Mapping;
using Application.Products.Commands;
using Application.Products.Handlers;
using FluentValidation;
using MediatR;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<CreateProductHandler>());

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<ProductProfile>();
            cfg.AddProfile<ValueObjectProfile>();
        });

        services.AddValidatorsFromAssemblyContaining<CreateProductCommand>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RetryBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuditLoggingBehavior<,>));

        services.AddHttpContextAccessor();                    
        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        return services;
    }
}