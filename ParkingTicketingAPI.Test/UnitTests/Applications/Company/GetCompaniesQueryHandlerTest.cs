using Application.Applications.Company.DTO;
using Application.Applications.Company.Query;
using Mapster;
using ParkingTicketingAPI.Test.UnitTests.Applications.Company.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Company
{
    public sealed class GetCompaniesQueryHandlerTest : IClassFixture<CompanyFixture>
    {
        private readonly CompanyFixture _companyFixture;

        public GetCompaniesQueryHandlerTest(CompanyFixture companyFixture)
        {
            _companyFixture = companyFixture;
        }

        [Fact(DisplayName = "Company list should be returned when data exists")]
        public async Task GetCompaniesQueryHandlerReturnsList()
        {
            // Arrange
            _companyFixture.SetupGetCompanies();

            // Act
            var sut = new GetCompaniesQueryHandler(_companyFixture.MockCompanyService().Object);

            var result = await sut.Handle(new GetCompaniesQuery(1, 10), CancellationToken.None);
            result.Adapt<List<ResponseCompanyDTO>>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ResponseCompanyDTO>>(result);
            _companyFixture.VerifyGetCompanies();
        }
    }
}
