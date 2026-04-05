using Application.Utilities.Extensions;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Employee;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Applications.Employee.Command
{
    internal record class AddEmployeesCommand([Required, MaxLength(50)] string FirstName,
        [MaxLength(50)] string? MiddleName,
        [Required, MaxLength(50)] string LastName,
        [Required] Guid AddressId,
        [Required] Guid CompanyId,
        [Required, MaxLength(100)] string CreatedBy,
        [Required] bool IsActive) : IRequest<Result<Guid>>;

    internal class AddEmployeesCommandHandler : IRequestHandler<AddEmployeesCommand, Result<Guid>>
    {
        private readonly IEmployeeService _employeeService;

        public AddEmployeesCommandHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<Result<Guid>> Handle(AddEmployeesCommand request, CancellationToken cancellationToken)
        {
            var error = ValidatorHelper<AddEmployeesCommand>.Errors(request);
            bool hasErrors = !string.IsNullOrEmpty(error);

            if(hasErrors)
            {
                return Result.Failure<Guid>(error);
            }

            var employee = new Domain.Entities.Employee.Employee(request.FirstName,
                request.MiddleName,
                request.LastName,
                request.AddressId,
                request.CompanyId,
                request.CreatedBy,
                request.IsActive);

            var result = await _employeeService.AddEmployee(employee, cancellationToken);

            if(result == Guid.Empty)
            {
                return Result.Failure<Guid>("Error saving employee.");
            }

            return Result.Success(result);
        }
    }
}
