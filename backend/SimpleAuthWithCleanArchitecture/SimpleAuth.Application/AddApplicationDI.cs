
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SimpleAuth.Application.Facades;
using SimpleAuth.Application.Interfaces;
using SimpleAuth.Application.UseCases.Auth;
using SimpleAuth.Application.Validator.AuthValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application
{
    public static class AddApplicationDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.Scan(scan => scan
            .FromAssemblyOf<LoginUseCase>()
            //With every class it scan choose class end with "UseCase" include internal class
            .AddClasses(clasess => clasess.Where(c => c.Name.EndsWith("UseCase")), publicOnly: false)
            .AsSelf()
            .WithScopedLifetime());

            services.Scan(scan => scan
            .FromAssemblyOf<LoginUseCase>()
            .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Facade")), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services.AddValidatorsFromAssemblyContaining<RefreshTokenRequestValidator>();
            return services;
        }
    }
}