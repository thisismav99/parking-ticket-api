using Application.Applications.Employee.DTO;
using Infrastructure.Interfaces.Employee;
using Mapster;
using MediatR;

namespace Application.Applications.Employee.Query
{
    internal record GetEmployeesQuery(int PageNumber, int PageSize) : IRequest<List<ResponseEmployeeDTO>>;

    internal class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<ResponseEmployeeDTO>>
    {
        private readonly IEmployeeService _employeeService;

        public GetEmployeesQueryHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<List<ResponseEmployeeDTO>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeService.GetEmployees(request.PageNumber, request.PageSize, cancellationToken);

            List<ResponseEmployeeDTO> employeeDTOs = employees.Adapt<List<ResponseEmployeeDTO>>();

            return employeeDTOs;
        }
    }
}
