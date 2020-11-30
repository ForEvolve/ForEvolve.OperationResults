using ForEvolve.OperationResults.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OperationResultAspNetCoreStartupExtensions
    {
        /// <summary>
        /// Adds operation results Asp.Net Core filters.
        /// This includes an interceptor that returns a non-successful operation result 
        /// when the ModelBinder is not able to create the model; see <see cref="ModelBinderErrorActionFilter"/> for more info.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddForEvolveOperationResultModelBinderErrorActionFilter(this IServiceCollection services)
        {
            services.AddSingleton<ModelBinderErrorActionFilter>();
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.AddService<ModelBinderErrorActionFilter>();
            });
            return services;
        }
    }
}
