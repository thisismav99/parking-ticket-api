namespace Infrastructure.Interfaces.Company
{
    internal interface ICompanyService
    {
        Task<Guid> AddCompany(Domain.Entities.Company.Company company, CancellationToken cancellationToken);

        Task<List<Domain.Entities.Company.Company>> GetCompanies(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<Domain.Entities.Company.Company?> GetCompanyById(Guid companyId, CancellationToken cancellationToken);
    }
}
