using Infrastructure.Interfaces.Employee;
using Infrastructure.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Infrastructure.Services.Employee
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly ParkingTicketDbContext _parkingTicketDbContext;
        private readonly DbSet<Domain.Entities.Common.Address> _employeeaddress;
        private readonly DbSet<Domain.Entities.Employee.Employee> _employees;

        public EmployeeService(ParkingTicketDbContext parkingTicketDbContext)
        {
            _parkingTicketDbContext = parkingTicketDbContext;
            _employeeaddress = _parkingTicketDbContext.Set<Domain.Entities.Common.Address>();
            _employees = _parkingTicketDbContext.Set<Domain.Entities.Employee.Employee>();
        }

        public async Task<Guid> AddEmployee(Domain.Entities.Employee.Employee employee, 
            Domain.Entities.Common.Address address,
            CancellationToken cancellationToken)
        {
            await _employeeaddress.AddAsync(address, cancellationToken);
            await _employees.AddAsync(employee, cancellationToken);

            await _parkingTicketDbContext.SaveChangesAsync(cancellationToken);

            return employee.Id;
        }

        public async Task<Domain.Entities.Employee.Employee?> GetEmployeeById(Guid employeeId, CancellationToken cancellationToken)
        {
            return await _employees.FirstOrDefaultAsync(e => e.Id == employeeId, cancellationToken);
        }

        public async Task<List<Domain.Entities.Employee.Employee>> GetEmployees(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await GetPagedList<Domain.Entities.Employee.Employee>.GetList(_employees, pageNumber, pageSize, cancellationToken);
        }
    }
}
