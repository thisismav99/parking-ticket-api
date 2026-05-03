using Application.Applications.Parking.DTO;
using Infrastructure.Interfaces.Parking;
using Moq;
using ParkingTicketingAPI.Test.UnitTests.Fixtures;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Parking.Fixture
{
    public class ParkingFixture : BaseFixture
    {
        private readonly Mock<IParkingService> _mockParkingService;

        public ParkingFixture()
        {
            _mockParkingService = new Mock<IParkingService>(MockBehavior.Strict);
        }

        internal Mock<IParkingService> MockParkingService() => _mockParkingService;

        public Guid GetParkingGuid() => guid;

        private static readonly DateTime SamplePark = new(2025, 1, 1, 8, 0, 0, DateTimeKind.Utc);

        #region AddParkingCommandHandler
        public AddParkingDTO ValidAddParkingDTO()
        {
            return new AddParkingDTO()
            {
                ParkDateTime = SamplePark,
                ExitDateTime = null,
                VehicleId = guid,
                EmployeeId = invalidGuid,
                CreatedBy = "User 1",
                IsActive = true
            };
        }

        public void SetupAddParking()
        {
            _mockParkingService.Setup(p => p.AddParking(It.IsAny<Domain.Entities.Parking.Parking>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(guid);
        }

        public void VerifyAddParking()
        {
            _mockParkingService.Verify(p => p.AddParking(It.IsAny<Domain.Entities.Parking.Parking>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region GetParkingByIdQueryHandler
        internal Domain.Entities.Parking.Parking ValidGetParkingById()
        {
            return new Domain.Entities.Parking.Parking(SamplePark,
                null,
                guid,
                invalidGuid,
                "User 1",
                true);
        }

        public void SetupValidGetByIdParking()
        {
            _mockParkingService.Setup(p => p.GetParkingById(It.Is<Guid>(id => id == guid), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidGetParkingById());
        }

        public void SetupInvalidGetByIdParking()
        {
            _mockParkingService.Setup(p => p.GetParkingById(It.Is<Guid>(id => id == invalidGuid), It.IsAny<CancellationToken>()))
               .ReturnsAsync((Domain.Entities.Parking.Parking?)null);
        }

        public void VerifyValidGetByIdParking()
        {
            _mockParkingService.Verify(p => p.GetParkingById(It.Is<Guid>(id => id == guid), It.IsAny<CancellationToken>()), Times.Once);
        }

        public void VerifyInvalidGetByIdParking()
        {
            _mockParkingService.Verify(p => p.GetParkingById(It.Is<Guid>(id => id == invalidGuid), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region GetParkingsQueryHandler
        internal List<Domain.Entities.Parking.Parking> ValidGetParkings()
        {
            return new List<Domain.Entities.Parking.Parking>()
            {
                new Domain.Entities.Parking.Parking(SamplePark,
                null,
                guid,
                invalidGuid,
                "User 1",
                true),
                new Domain.Entities.Parking.Parking(SamplePark.AddHours(1),
                SamplePark.AddHours(3),
                guid,
                invalidGuid,
                "User 2",
                true),
                new Domain.Entities.Parking.Parking(SamplePark.AddDays(1),
                null,
                guid,
                invalidGuid,
                "User 3",
                true)
            };
        }

        public void SetupGetParkings()
        {
            _mockParkingService.Setup(p => p.GetParkings(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidGetParkings());
        }

        public void VerifyGetParkings()
        {
            _mockParkingService.Verify(p => p.GetParkings(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion
    }
}
