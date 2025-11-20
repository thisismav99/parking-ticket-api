namespace Infrastructure.Interfaces.Employee
{
    internal interface IEmployeeService
    {
        Task<Guid> AddEmployee(Domain.Entities.Employee.Employee employee, 
            Domain.Entities.Common.Address address,
            CancellationToken cancellationToken);

        Task<List<Domain.Entities.Employee.Employee>> GetEmployees(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<Domain.Entities.Employee.Employee?> GetEmployeeById(Guid employeeId, CancellationToken cancellationToken);
    }
}
