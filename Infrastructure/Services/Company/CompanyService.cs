using Infrastructure.Interfaces.Company;
using Infrastructure.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Infrastructure.Services.Company
{
    internal class CompanyService : ICompanyService
    {
        private readonly ParkingTicketDbContext _parkingTicketDbContext;
        private readonly DbSet<Domain.Entities.Company.Company> _companies;

        public CompanyService(ParkingTicketDbContext parkingTicketDbContext)
        {
            _parkingTicketDbContext = parkingTicketDbContext;
            _companies = _parkingTicketDbContext.Set<Domain.Entities.Company.Company>();
        }

        public async Task<Guid> AddCompany(Domain.Entities.Company.Company company, CancellationToken cancellationToken)
        {
            await _companies.AddAsync(company, cancellationToken);
            await _parkingTicketDbContext.SaveChangesAsync(cancellationToken);

            return company.Id;
        }

        public async Task<List<Domain.Entities.Company.Company>> GetCompanies(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await GetPagedList<Domain.Entities.Company.Company>.GetList(_companies, pageNumber, pageSize, cancellationToken);
        }

        public async Task<Domain.Entities.Company.Company?> GetCompanyById(Guid companyId, CancellationToken cancellationToken)
        {
            return await _companies.FirstOrDefaultAsync(c => c.Id == companyId, cancellationToken);
        }
    }
}
