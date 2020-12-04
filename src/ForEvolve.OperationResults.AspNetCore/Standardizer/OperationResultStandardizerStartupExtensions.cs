using ForEvolve.OperationResults;
using ForEvolve.OperationResults.Standardizer;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OperationResultStandardizerStartupExtensions
    {
        /// <summary>
        /// Adds the default ForEvolve operation result standardizer filters.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection.</returns>
        /// <remarks>This subsystem should to be revised.</remarks>
        public static IServiceCollection AddForEvolveOperationResultStandardizer(this IServiceCollection services)
        {
            services
                .AddLogging()
                .AddSingleton<IPropertyNameFormatter, DefaultPropertyNameFormatter>()
                .AddSingleton<IPropertyValueFormatter, DefaultPropertyValueFormatter>()
                .AddSingleton<IOperationResultStandardizer, DefaultOperationResultStandardizer>()
                .AddOptions<DefaultOperationResultStandardizerOptions>()
            ;
            services
                .Configure<MvcOptions>(options =>
                {
                    options.Filters.Add<OperationResultStandardizerActionFilter<CreatedAtActionResult>>();
                    options.Filters.Add<OperationResultStandardizerActionFilter<CreatedAtRouteResult>>();
                    options.Filters.Add<OperationResultStandardizerActionFilter<CreatedResult>>();

                    options.Filters.Add<OperationResultStandardizerActionFilter<OkObjectResult>>();

                    options.Filters.Add<OperationResultStandardizerActionFilter<BadRequestObjectResult>>();
                    options.Filters.Add<OperationResultStandardizerActionFilter<ConflictObjectResult>>();
                    options.Filters.Add<OperationResultStandardizerActionFilter<NotFoundObjectResult>>();
                    options.Filters.Add<OperationResultStandardizerActionFilter<UnprocessableEntityObjectResult>>();
                });
            return services;
        }
    }
}
