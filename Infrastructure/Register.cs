using Infrastructure.Interfaces.Common;
using Infrastructure.Interfaces.Company;
using Infrastructure.Interfaces.Customer;
using Infrastructure.Interfaces.Employee;
using Infrastructure.Interfaces.Parking;
using Infrastructure.Interfaces.Vehicle;
using Infrastructure.Services.Common;
using Infrastructure.Services.Company;
using Infrastructure.Services.Customer;
using Infrastructure.Services.Employee;
using Infrastructure.Services.Parking;
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
        }
    }
}
