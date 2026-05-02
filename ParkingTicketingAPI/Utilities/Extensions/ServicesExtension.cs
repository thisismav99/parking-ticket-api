using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi;
using System.Threading.RateLimiting;

namespace ParkingTicketingAPI.Utilities.Extensions
{
    public static class ServicesExtension
    {
        public static void Swagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new() { Title = "Parking Ticketing API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = []
                });
            });
        }

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
