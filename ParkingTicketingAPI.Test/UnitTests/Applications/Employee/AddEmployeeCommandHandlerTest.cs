using Application.Applications.Employee.Command;
using ParkingTicketingAPI.Test.UnitTests.Applications.Employee.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Employee
{
    public sealed class AddEmployeeCommandHandlerTest : IClassFixture<EmployeeFixture>
    {
        private readonly EmployeeFixture _employeeFixture;

        public AddEmployeeCommandHandlerTest(EmployeeFixture employeeFixture)
        {
            _employeeFixture = employeeFixture;
        }

        [Fact(DisplayName = "Employee Id should be returned when adding a valid employee")]
        public async Task AddEmployeeCommandHandlerSuccess()
        {
            // Arrange
            _employeeFixture.SetupAddEmployee();

            // Act
            var sut = new AddEmployeesCommandHandler(_employeeFixture.MockEmployeeService().Object);

            var result = await sut.Handle(new AddEmployeeCommand(_employeeFixture.ValidAddEmployeeDTO()), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(_employeeFixture.GetEmployeeGuid(), result.Value);
            Assert.IsType<Guid>(result.Value);
            _employeeFixture.VerifyAddEmployee();
        }
    }
}
