﻿using CorrelationId.DependencyInjection;

namespace WebAPI.Extensions;

public static class ServiceCollectionExtensions
{
    private const string LoggingScopeKey = "CorrelationId";

    public static IServiceCollection AddCorrelationId(this IServiceCollection services)
        => services.AddDefaultCorrelationId(options =>
        {
            options.RequestHeader =
                options.ResponseHeader =
                    options.LoggingScopeKey = LoggingScopeKey;

            options.UpdateTraceIdentifier =
                options.AddToLoggingScope = true;
        });
    
    public static void AddSwagger(this IServiceCollection services)
        => services.AddSwaggerGen();
}