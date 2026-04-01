using Application.Applications.Company.DTO;
using Infrastructure.Interfaces.Company;
using Mapster;
using MediatR;

namespace Application.Applications.Company.Query
{
    internal record class GetCompaniesQuery(int PageNumber, int PageSize) : IRequest<List<ResponseCompanyDTO>>;

    internal class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, List<ResponseCompanyDTO>>
    {
        private readonly ICompanyService _companyService;

        public GetCompaniesQueryHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<List<ResponseCompanyDTO>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = await _companyService.GetCompanies(request.PageNumber, request.PageSize, cancellationToken);

            List<ResponseCompanyDTO> companyDTOs = companies.Adapt<List<ResponseCompanyDTO>>();

            return companyDTOs;
        }
    }
}
