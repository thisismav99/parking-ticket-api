using Application.Applications.Employee.DTO;
using Application.Utilities.Helpers;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Employee;
using MediatR;

namespace Application.Applications.Employee.Command
{
    internal record class AddEmployeeCommand(AddEmployeeDTO AddEmployeeDTO) : IRequest<Result<Guid>>;

    internal class AddEmployeesCommandHandler : IRequestHandler<AddEmployeeCommand, Result<Guid>>
    {
        private readonly IEmployeeService _employeeService;

        public AddEmployeesCommandHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<Result<Guid>> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Domain.Entities.Employee.Employee(request.AddEmployeeDTO.FirstName,
                request.AddEmployeeDTO.MiddleName,
                request.AddEmployeeDTO.LastName,
                request.AddEmployeeDTO.AddressId,
                request.AddEmployeeDTO.CompanyId,
                request.AddEmployeeDTO.CreatedBy,
                request.AddEmployeeDTO.IsActive);

            var result = await _employeeService.AddEmployee(employee, cancellationToken);

            if(result == Guid.Empty)
            {
                return Result.Failure<Guid>(GetError.Error("employee"));
            }

            return Result.Success(result);
        }
    }
}
