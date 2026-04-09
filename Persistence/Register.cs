using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence.Contexts;
using System.Text;

namespace Persistence
{
    public static class Register
    {
        public static void RegisterPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            DbContexts(services, 
                configuration.GetConnectionString("ParkingDb")!, 
                configuration.GetConnectionString("UserDb")!);
            Identity(services);
            Jwt(services, configuration);
        }

        private static void DbContexts(IServiceCollection services,
            string parkingDbConnectionString,
            string userDbConnectionString)
        {
            services.AddDbContext<ParkingTicketDbContext>(options =>
            {
                options.UseSqlServer(parkingDbConnectionString);
            });

            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(userDbConnectionString);
            });
        }

        private static void Identity(IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();
        }

        private static void Jwt(IServiceCollection services, IConfiguration configuration)
        {
            var jwt = configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwt["Key"]!);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt["Issuer"],
                    ValidAudience = jwt["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            services.AddAuthorization();
        }
    }
}
