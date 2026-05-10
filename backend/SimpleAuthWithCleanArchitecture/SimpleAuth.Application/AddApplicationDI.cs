
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SimpleAuth.Application.Interfaces;
using SimpleAuth.Application.Validator;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application
{
    public static class AddApplicationDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<RefreshTokenRequestValidator>();
            return services;
        }
    }
}