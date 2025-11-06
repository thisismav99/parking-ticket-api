using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    internal static class Register
    {
        public static void RegisterApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Register).Assembly));
        }
    }
}
