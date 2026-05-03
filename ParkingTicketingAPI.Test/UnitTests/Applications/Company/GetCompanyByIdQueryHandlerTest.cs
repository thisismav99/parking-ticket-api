using Application.Applications.Company.DTO;
using Application.Applications.Company.Query;
using Application.Utilities.Helpers;
using Mapster;
using ParkingTicketingAPI.Test.UnitTests.Applications.Company.Fixture;

namespace ParkingTicketingAPI.Test.UnitTests.Applications.Company
{
    public sealed class GetCompanyByIdQueryHandlerTest : IClassFixture<CompanyFixture>
    {
        private readonly CompanyFixture _companyFixture;

        public GetCompanyByIdQueryHandlerTest(CompanyFixture companyFixture)
        {
            _companyFixture = companyFixture;
        }

        [Theory(DisplayName = "Company should be returned when a valid company id is entered and not found when invalid")]
        [InlineData("3f9b2a6c-8d4e-4f71-9c2a-5e7b1a93d6f8")]
        [InlineData("4f9b2a6c-8d4e-4f71-9c2a-5e7b1a93d6f9")]
        public async Task GetCompanyByIdQueryHandlerReturns(string guid)
        {
            // Arrange
            Guid companyId = Guid.Parse(guid);
            _companyFixture.SetupValidGetByIdCompany();
            _companyFixture.SetupInvalidGetByIdCompany();

            // Act
            var sut = new GetCompanyByIdQueryHandler(_companyFixture.MockCompanyService().Object);

            var result = await sut.Handle(new GetCompanyByIdQuery(companyId), CancellationToken.None);
            result.Adapt<ResponseCompanyDTO>();

            // Assert
            if (companyId == _companyFixture.GetCompanyGuid())
            {
                Assert.True(result.IsSuccess);
                Assert.NotNull(result.Value);
                Assert.IsType<ResponseCompanyDTO>(result.Value);
                _companyFixture.VerifyValidGetByIdCompany();
            }
            else
            {
                Assert.True(result.IsFailure);
                Assert.Equal(GetError.NotFound(companyId), result.Error);
                Assert.IsType<string>(result.Error);
                _companyFixture.VerifyInvalidGetByIdCompany();
            }
        }
    }
}
