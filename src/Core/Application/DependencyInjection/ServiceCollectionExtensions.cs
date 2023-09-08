using Application.Services;
using Application.UseCases.Commands;
using Application.UseCases.Events;
using Application.UseCases.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        => services.AddApplicationServices().AddCommandInteractors().AddEventInteractors().AddQueryInteractors();

    private static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services.AddScoped<IDefineReminderInteractor, DefineReminderInteractor>();

    private static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services.AddScoped<ICallbackWhenReminderElapsedInteractor, CallbackWhenReminderElapsedInteractor>();

    private static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services.AddScoped<IGetScheduledTimeInteractor, GetScheduledTimeInteractor>();

    private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services.AddScoped<IApplicationService, ApplicationService>();
}