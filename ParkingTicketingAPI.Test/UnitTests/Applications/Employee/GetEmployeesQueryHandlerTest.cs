using Application.Applications.Employee.DTO;
using Application.Applications.Employee.Query;
using Mapster;
using ParkingTicketingAPI.Test.UnitTests.Applications.Employee.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Employee
{
    public sealed class GetEmployeesQueryHandlerTest : IClassFixture<EmployeeFixture>
    {
        private readonly EmployeeFixture _employeeFixture;

        public GetEmployeesQueryHandlerTest(EmployeeFixture employeeFixture)
        {
            _employeeFixture = employeeFixture;
        }

        [Fact(DisplayName = "Employee list should be returned when data exists")]
        public async Task GetEmployeesQueryHandlerReturnsList()
        {
            // Arrange
            _employeeFixture.SetupGetEmployees();

            // Act
            var sut = new GetEmployeesQueryHandler(_employeeFixture.MockEmployeeService().Object);

            var result = await sut.Handle(new GetEmployeesQuery(1, 10), CancellationToken.None);
            result.Adapt<List<ResponseEmployeeDTO>>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ResponseEmployeeDTO>>(result);
            _employeeFixture.VerifyGetEmployees();
        }
    }
}
