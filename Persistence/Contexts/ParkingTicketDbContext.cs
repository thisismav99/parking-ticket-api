using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.Common;
using Persistence.Configurations.Company;
using Persistence.Configurations.Customer;
using Persistence.Configurations.Employee;
using Persistence.Configurations.Parking;
using Persistence.Configurations.Vehicle;

namespace Persistence.Contexts
{
    internal class ParkingTicketDbContext : DbContext
    {
        public ParkingTicketDbContext(DbContextOptions<ParkingTicketDbContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new ParkingConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
        }
    }
}
