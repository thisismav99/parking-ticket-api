using Application.Applications.Employee.DTO;
using Infrastructure.Interfaces.Employee;
using Moq;
using ParkingTicketingAPI.Test.UnitTests.Fixtures;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Employee.Fixture
{
    public class EmployeeFixture : BaseFixture
    {
        private readonly Mock<IEmployeeService> _mockEmployeeService;

        public EmployeeFixture()
        {
            _mockEmployeeService = new Mock<IEmployeeService>(MockBehavior.Strict);
        }

        internal Mock<IEmployeeService> MockEmployeeService() => _mockEmployeeService;

        public Guid GetEmployeeGuid() => guid;

        #region AddEmployeeCommandHandler
        public AddEmployeeDTO ValidAddEmployeeDTO()
        {
            return new AddEmployeeDTO()
            {
                FirstName = "Pat",
                MiddleName = "Q",
                LastName = "Kim",
                AddressId = guid,
                CompanyId = invalidGuid,
                CreatedBy = "User 1",
                IsActive = true
            };
        }

        public void SetupAddEmployee()
        {
            _mockEmployeeService.Setup(e => e.AddEmployee(It.IsAny<Domain.Entities.Employee.Employee>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(guid);
        }

        public void VerifyAddEmployee()
        {
            _mockEmployeeService.Verify(e => e.AddEmployee(It.IsAny<Domain.Entities.Employee.Employee>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region GetEmployeeByIdQueryHandler
        internal Domain.Entities.Employee.Employee ValidGetEmployeeById()
        {
            return new Domain.Entities.Employee.Employee("Pat",
                "Q",
                "Kim",
                guid,
                invalidGuid,
                "User 1",
                true);
        }

        public void SetupValidGetByIdEmployee()
        {
            _mockEmployeeService.Setup(e => e.GetEmployeeById(It.Is<Guid>(id => id == guid), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidGetEmployeeById());
        }

        public void SetupInvalidGetByIdEmployee()
        {
            _mockEmployeeService.Setup(e => e.GetEmployeeById(It.Is<Guid>(id => id == invalidGuid), It.IsAny<CancellationToken>()))
               .ReturnsAsync((Domain.Entities.Employee.Employee?)null);
        }

        public void VerifyValidGetByIdEmployee()
        {
            _mockEmployeeService.Verify(e => e.GetEmployeeById(It.Is<Guid>(id => id == guid), It.IsAny<CancellationToken>()), Times.Once);
        }

        public void VerifyInvalidGetByIdEmployee()
        {
            _mockEmployeeService.Verify(e => e.GetEmployeeById(It.Is<Guid>(id => id == invalidGuid), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region GetEmployeesQueryHandler
        internal List<Domain.Entities.Employee.Employee> ValidGetEmployees()
        {
            return new List<Domain.Entities.Employee.Employee>()
            {
                new Domain.Entities.Employee.Employee("Pat",
                "Q",
                "Kim",
                guid,
                invalidGuid,
                "User 1",
                true),
                new Domain.Entities.Employee.Employee("Sam",
                null,
                "Lee",
                guid,
                invalidGuid,
                "User 2",
                true),
                new Domain.Entities.Employee.Employee("Tia",
                "R",
                "Wong",
                guid,
                invalidGuid,
                "User 3",
                true)
            };
        }

        public void SetupGetEmployees()
        {
            _mockEmployeeService.Setup(e => e.GetEmployees(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidGetEmployees());
        }

        public void VerifyGetEmployees()
        {
            _mockEmployeeService.Verify(e => e.GetEmployees(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion
    }
}
