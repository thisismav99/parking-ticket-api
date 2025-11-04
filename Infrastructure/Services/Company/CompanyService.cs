using CSharpFunctionalExtensions;
using Infrastructure.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Infrastructure.Services.Company
{
    internal interface ICompanyService
    {
        Task<Result<Guid>> AddCompany(Domain.Entities.Company.Company company);

        Task <List<Domain.Entities.Company.Company>> GetCompanies(int pageNumber, int pageSize);

        Task<Result<Domain.Entities.Company.Company?>> GetCompanyById(Guid companyId);
    }

    internal class CompanyService : ICompanyService
    {
        private readonly ParkingTicketDbContext _parkingTicketDbContext;
        private readonly DbSet<Domain.Entities.Company.Company> _companies;

        public CompanyService(ParkingTicketDbContext parkingTicketDbContext)
        {
            _parkingTicketDbContext = parkingTicketDbContext;
            _companies = _parkingTicketDbContext.Set<Domain.Entities.Company.Company>();
        }

        public async Task<Result<Guid>> AddCompany(Domain.Entities.Company.Company company)
        {
            if(company is not null)
            {
                await _companies.AddAsync(company);
                await _parkingTicketDbContext.SaveChangesAsync();

                return Result.Success(company.Id);
            }

            return Result.Failure<Guid>("Company cannot be null");
        }

        public async Task<List<Domain.Entities.Company.Company>> GetCompanies(int pageNumber, int pageSize)
        {
            return await GetPagedList<Domain.Entities.Company.Company>.GetList(_companies, pageNumber, pageSize);
        }

        public async Task<Result<Domain.Entities.Company.Company?>> GetCompanyById(Guid companyId)
        {
            if(companyId != Guid.Empty)
            {
                var company = await _companies.FirstOrDefaultAsync(c => c.Id == companyId);
                
                return Result.Success(company);
            }

            return Result.Failure<Domain.Entities.Company.Company?>("Company Id cannot be empty");
        }
    }
}
