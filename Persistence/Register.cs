using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;

namespace Persistence
{
    public static class Register
    {
        public static void RegisterPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ParkingTicketDbContext>(options => {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
