using Application.Applications.Employee.DTO;
using Application.Utilities.Helpers;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Employee;
using Mapster;
using MediatR;

namespace Application.Applications.Employee.Query
{
    internal record class GetEmployeeByIdQuery(Guid EmployeeId) : IRequest<Result<ResponseEmployeeDTO?>>;

    internal class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Result<ResponseEmployeeDTO?>>
    {
        private readonly IEmployeeService _employeeService;

        public GetEmployeeByIdQueryHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<Result<ResponseEmployeeDTO?>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetEmployeeById(request.EmployeeId, cancellationToken);

            if(employee is null)
            {
                return Result.Failure<ResponseEmployeeDTO?>(GetError.NotFound(request.EmployeeId));
            }

            ResponseEmployeeDTO? employeeDTO = employee.Adapt<ResponseEmployeeDTO?>();

            return Result.Success(employeeDTO);
        }
    }
}
