using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services
{
    public static class EmailsService
    {
        public static IServiceCollection ConfigureEmailService(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var smtpSection = builder.Configuration.GetSection("Smtp");

            services.AddFluentEmail(smtpSection["From"], smtpSection["FromName"] ?? smtpSection["From"])
                    .AddSmtpSender(
                        smtpSection["Host"],
                        int.Parse(smtpSection["Port"]!)    
                    );

            return services;
        }
    }
}