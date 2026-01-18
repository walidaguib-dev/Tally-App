using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public static class DI

    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            // Register presentation services here
            return services;
        }
    }
}
