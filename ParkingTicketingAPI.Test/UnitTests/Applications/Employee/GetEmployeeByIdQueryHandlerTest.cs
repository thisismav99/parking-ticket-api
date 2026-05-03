using Application.Applications.Employee.DTO;
using Application.Applications.Employee.Query;
using Application.Utilities.Helpers;
using Mapster;
using ParkingTicketingAPI.Test.UnitTests.Applications.Employee.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Employee
{
    public sealed class GetEmployeeByIdQueryHandlerTest : IClassFixture<EmployeeFixture>
    {
        private readonly EmployeeFixture _employeeFixture;

        public GetEmployeeByIdQueryHandlerTest(EmployeeFixture employeeFixture)
        {
            _employeeFixture = employeeFixture;
        }

        [Theory(DisplayName = "Employee should be returned when a valid employee id is entered and not found when invalid")]
        [InlineData("3f9b2a6c-8d4e-4f71-9c2a-5e7b1a93d6f8")]
        [InlineData("4f9b2a6c-8d4e-4f71-9c2a-5e7b1a93d6f9")]
        public async Task GetEmployeeByIdQueryHandlerReturns(string guid)
        {
            // Arrange
            Guid employeeId = Guid.Parse(guid);
            _employeeFixture.SetupValidGetByIdEmployee();
            _employeeFixture.SetupInvalidGetByIdEmployee();

            // Act
            var sut = new GetEmployeeByIdQueryHandler(_employeeFixture.MockEmployeeService().Object);

            var result = await sut.Handle(new GetEmployeeByIdQuery(employeeId), CancellationToken.None);
            result.Adapt<ResponseEmployeeDTO>();

            // Assert
            if (employeeId == _employeeFixture.GetEmployeeGuid())
            {
                Assert.True(result.IsSuccess);
                Assert.NotNull(result.Value);
                Assert.IsType<ResponseEmployeeDTO>(result.Value);
                _employeeFixture.VerifyValidGetByIdEmployee();
            }
            else
            {
                Assert.True(result.IsFailure);
                Assert.Equal(GetError.NotFound(employeeId), result.Error);
                Assert.IsType<string>(result.Error);
                _employeeFixture.VerifyInvalidGetByIdEmployee();
            }
        }
    }
}
