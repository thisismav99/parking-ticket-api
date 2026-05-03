using Application.Applications.Company.Command;
using ParkingTicketingAPI.Test.UnitTests.Applications.Company.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Company
{
    public sealed class AddCompanyCommandHandlerTest : IClassFixture<CompanyFixture>
    {
        private readonly CompanyFixture _companyFixture;

        public AddCompanyCommandHandlerTest(CompanyFixture companyFixture)
        {
            _companyFixture = companyFixture;
        }

        [Fact(DisplayName = "Company Id should be returned when adding a valid company")]
        public async Task AddCompanyCommandHandlerSuccess()
        {
            // Arrange
            _companyFixture.SetupAddCompany();

            // Act
            var sut = new AddCompanyCommandHandler(_companyFixture.MockCompanyService().Object);

            var result = await sut.Handle(new AddCompanyCommand(_companyFixture.ValidAddCompanyDTO()), CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(_companyFixture.GetCompanyGuid(), result.Value);
            Assert.IsType<Guid>(result.Value);
            _companyFixture.VerifyAddCompany();
        }
    }
}
