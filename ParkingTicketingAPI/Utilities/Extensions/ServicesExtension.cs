using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace ParkingTicketingAPI.Utilities.Extensions
{
    public static class ServicesExtension
    {
        public static void RateLimiting(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("fixed", o =>
                {
                    o.Window = TimeSpan.FromSeconds(10);
                    o.PermitLimit = 5;
                    o.QueueLimit = 5;
                    o.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });
            });
        }
    }
}
