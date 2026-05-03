using Application.Applications.Company.DTO;
using Infrastructure.Interfaces.Company;
using Moq;
using ParkingTicketingAPI.Test.UnitTests.Fixtures;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Company.Fixture
{
    public class CompanyFixture : BaseFixture
    {
        private readonly Mock<ICompanyService> _mockCompanyService;

        public CompanyFixture()
        {
            _mockCompanyService = new Mock<ICompanyService>(MockBehavior.Strict);
        }

        internal Mock<ICompanyService> MockCompanyService() => _mockCompanyService;

        public Guid GetCompanyGuid() => guid;

        #region AddCompanyCommandHandler
        public AddCompanyDTO ValidAddCompanyDTO()
        {
            return new AddCompanyDTO()
            {
                Name = "Company 1",
                Description = "Description 1",
                CreatedBy = "User 1",
                IsActive = true
            };
        }

        public void SetupAddCompany()
        {
            _mockCompanyService.Setup(c => c.AddCompany(It.IsAny<Domain.Entities.Company.Company>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(guid);
        }

        public void VerifyAddCompany()
        {
            _mockCompanyService.Verify(c => c.AddCompany(It.IsAny<Domain.Entities.Company.Company>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region GetCompanyByIdQueryHandler
        internal Domain.Entities.Company.Company ValidGetCompanyById()
        {
            return new Domain.Entities.Company.Company("Company 1",
                "Description 1",
                "User 1",
                true);
        }

        public void SetupValidGetByIdCompany()
        {
            _mockCompanyService.Setup(c => c.GetCompanyById(It.Is<Guid>(id => id == guid), It.IsAny<CancellationToken>()))
               .ReturnsAsync(ValidGetCompanyById());
        }

        public void SetupInvalidGetByIdCompany()
        {
            _mockCompanyService.Setup(c => c.GetCompanyById(It.Is<Guid>(id => id == invalidGuid), It.IsAny<CancellationToken>()))
               .ReturnsAsync((Domain.Entities.Company.Company?)null);
        }

        public void VerifyValidGetByIdCompany()
        {
            _mockCompanyService.Verify(c => c.GetCompanyById(It.Is<Guid>(id => id == guid), It.IsAny<CancellationToken>()), Times.Once);
        }

        public void VerifyInvalidGetByIdCompany()
        {
            _mockCompanyService.Verify(c => c.GetCompanyById(It.Is<Guid>(id => id == invalidGuid), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region GetCompaniesQueryHandler
        internal List<Domain.Entities.Company.Company> ValidGetCompanies()
        {
            return new List<Domain.Entities.Company.Company>()
            {
                new Domain.Entities.Company.Company("Company 1",
                "Description 1",
                "User 1",
                true),
                new Domain.Entities.Company.Company("Company 2",
                "Description 2",
                "User 2",
                true),
                new Domain.Entities.Company.Company("Company 3",
                "Description 3",
                "User 3",
                true),
            };
        }

        public void SetupGetCompanies()
        {
            _mockCompanyService.Setup(c => c.GetCompanies(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidGetCompanies());
        }

        public void VerifyGetCompanies()
        {
            _mockCompanyService.Verify(c => c.GetCompanies(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion
    }
}
