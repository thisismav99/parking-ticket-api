using Infrastructure.Interfaces.Common;
using Infrastructure.Interfaces.Company;
using Infrastructure.Interfaces.Customer;
using Infrastructure.Interfaces.Employee;
using Infrastructure.Interfaces.Parking;
using Infrastructure.Interfaces.Token;
using Infrastructure.Interfaces.User;
using Infrastructure.Interfaces.Vehicle;
using Infrastructure.Services.Common;
using Infrastructure.Services.Company;
using Infrastructure.Services.Customer;
using Infrastructure.Services.Employee;
using Infrastructure.Services.Parking;
using Infrastructure.Services.Token;
using Infrastructure.Services.User;
using Infrastructure.Services.Vehicle;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Register
    {
        public static void RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IParkingService, ParkingService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
        }
    }
}
