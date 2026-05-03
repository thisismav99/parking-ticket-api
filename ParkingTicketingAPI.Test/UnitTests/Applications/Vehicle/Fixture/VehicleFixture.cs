using Application.Applications.Vehicle.DTO;
using Infrastructure.Interfaces.Vehicle;
using Moq;
using ParkingTicketingAPI.Test.UnitTests.Fixtures;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Vehicle.Fixture
{
    public class VehicleFixture : BaseFixture
    {
        private readonly Mock<IVehicleService> _mockVehicleService;

        public VehicleFixture()
        {
            _mockVehicleService = new Mock<IVehicleService>(MockBehavior.Strict);
        }

        internal Mock<IVehicleService> MockVehicleService() => _mockVehicleService;

        public Guid GetVehicleGuid() => guid;

        #region AddVehicleCommandHandler
        public AddVehicleDTO ValidAddVehicleDTO()
        {
            return new AddVehicleDTO()
            {
                PlateNo = "ABC-1234",
                Brand = "BrandX",
                Color = "Blue",
                Model = "Sedan",
                IsElectric = true,
                IsHybrid = false,
                CustomerId = guid,
                CreatedBy = "User 1",
                IsActive = true
            };
        }

        public void SetupAddVehicle()
        {
            _mockVehicleService.Setup(v => v.AddVehicle(It.IsAny<Domain.Entities.Vehicle.Vehicle>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(guid);
        }

        public void VerifyAddVehicle()
        {
            _mockVehicleService.Verify(v => v.AddVehicle(It.IsAny<Domain.Entities.Vehicle.Vehicle>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region GetVehicleByIdQueryHandler
        internal Domain.Entities.Vehicle.Vehicle ValidGetVehicleById()
        {
            return new Domain.Entities.Vehicle.Vehicle("ABC-1234",
                "BrandX",
                "Blue",
                "Sedan",
                true,
                false,
                guid,
                "User 1",
                true);
        }

        public void SetupValidGetByIdVehicle()
        {
            _mockVehicleService.Setup(v => v.GetVehicleById(It.Is<Guid>(id => id == guid), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidGetVehicleById());
        }

        public void SetupInvalidGetByIdVehicle()
        {
            _mockVehicleService.Setup(v => v.GetVehicleById(It.Is<Guid>(id => id == invalidGuid), It.IsAny<CancellationToken>()))
               .ReturnsAsync((Domain.Entities.Vehicle.Vehicle?)null);
        }

        public void VerifyValidGetByIdVehicle()
        {
            _mockVehicleService.Verify(v => v.GetVehicleById(It.Is<Guid>(id => id == guid), It.IsAny<CancellationToken>()), Times.Once);
        }

        public void VerifyInvalidGetByIdVehicle()
        {
            _mockVehicleService.Verify(v => v.GetVehicleById(It.Is<Guid>(id => id == invalidGuid), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region GetVehiclesQueryHandler
        internal List<Domain.Entities.Vehicle.Vehicle> ValidGetVehicles()
        {
            return new List<Domain.Entities.Vehicle.Vehicle>()
            {
                new Domain.Entities.Vehicle.Vehicle("ABC-1234",
                "BrandX",
                "Blue",
                "Sedan",
                true,
                false,
                guid,
                "User 1",
                true),
                new Domain.Entities.Vehicle.Vehicle("XYZ-9999",
                "BrandY",
                null,
                null,
                false,
                false,
                null,
                "User 2",
                true),
                new Domain.Entities.Vehicle.Vehicle("LMN-5555",
                "BrandZ",
                "Red",
                "Hatch",
                false,
                false,
                guid,
                "User 3",
                true)
            };
        }

        public void SetupGetVehicles()
        {
            _mockVehicleService.Setup(v => v.GetVehicles(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidGetVehicles());
        }

        public void VerifyGetVehicles()
        {
            _mockVehicleService.Verify(v => v.GetVehicles(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion
    }
}
