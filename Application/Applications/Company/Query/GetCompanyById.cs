using Application.Applications.Company.DTO;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Company;
using Mapster;
using MediatR;

namespace Application.Applications.Company.Query
{
    internal record class GetCompanyByIdQuery(Guid CompanyId) : IRequest<Result<ResponseCompanyDTO?>>;

    internal class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, Result<ResponseCompanyDTO?>>
    {
        private readonly ICompanyService _companyService;

        public GetCompanyByIdQueryHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<Result<ResponseCompanyDTO?>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyService.GetCompanyById(request.CompanyId, cancellationToken);

            if (company is null)
            {
                return Result.Failure<ResponseCompanyDTO?>($"No company found for Id: {request.CompanyId}");
            }

            ResponseCompanyDTO? companyDTO = company.Adapt<ResponseCompanyDTO?>();

            return Result.Success(companyDTO);
        }
    }
}
