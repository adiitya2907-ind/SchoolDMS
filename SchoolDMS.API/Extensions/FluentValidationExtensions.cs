using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolDMS.API.Validators;

namespace SchoolDMS.API.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<RegisterDTOValidator>();
            
            return services;
        }
    }
}
