using CSharpFunctionalExtensions;
using Infrastructure.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Infrastructure.Services.Employee
{
    internal interface IEmployeeService
    {
        Task<Result<Guid>> AddEmployee(Domain.Entities.Employee.Employee employee);

        Task<List<Domain.Entities.Employee.Employee>> GetEmployees(int pageNumber, int pageSize);

        Task<Result<Domain.Entities.Employee.Employee?>> GetEmployeeById(Guid employeeId);
    }

    internal class EmployeeService : IEmployeeService
    {
        private readonly ParkingTicketDbContext _parkingTicketDbContext;
        private readonly DbSet<Domain.Entities.Employee.Employee> _employees;

        public EmployeeService(ParkingTicketDbContext parkingTicketDbContext)
        {
            _parkingTicketDbContext = parkingTicketDbContext;
            _employees = _parkingTicketDbContext.Set<Domain.Entities.Employee.Employee>();
        }

        public async Task<Result<Guid>> AddEmployee(Domain.Entities.Employee.Employee employee)
        {
            if (employee is not null)
            {
                await _employees.AddAsync(employee);
                await _parkingTicketDbContext.SaveChangesAsync();

                return Result.Success(employee.Id);
            }

            return Result.Failure<Guid>("Employee cannot be null");
        }

        public async Task<Result<Domain.Entities.Employee.Employee?>> GetEmployeeById(Guid employeeId)
        {
            if(employeeId != Guid.Empty)
            {
                var employee = await _employees.FirstOrDefaultAsync(e => e.Id == employeeId);

                return Result.Success(employee);
            }

            return Result.Failure<Domain.Entities.Employee.Employee?>("Employee Id cannot be empty");
        }

        public async Task<List<Domain.Entities.Employee.Employee>> GetEmployees(int pageNumber, int pageSize)
        {
            return await GetPagedList<Domain.Entities.Employee.Employee>.GetList(_employees, pageNumber, pageSize);
        }
    }
}
