using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Register
    {
        public static void RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<Services.Common.IAddressService, Services.Common.AddressService>();
            services.AddScoped<Services.Company.ICompanyService, Services.Company.CompanyService>();
            services.AddScoped<Services.Customer.ICustomerService, Services.Customer.CustomerService>();
            services.AddScoped<Services.Employee.IEmployeeService, Services.Employee.EmployeeService>();
            services.AddScoped<Services.Parking.IParkingService, Services.Parking.ParkingService>();
            services.AddScoped<Services.Parking.ITransactionService, Services.Parking.TransactionService>();
            services.AddScoped<Services.Vehicle.IVehicleService, Services.Vehicle.VehicleService>();
        }
    }
}
