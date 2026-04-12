using Application.Applications.Company.DTO;
using Application.Utilities.Helpers;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Company;
using MediatR;

namespace Application.Applications.Company.Command
{
    internal record class AddCompanyCommand(AddCompanyDTO AddCompanyDTO) : IRequest<Result<Guid>>;

    internal class AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand, Result<Guid>>
    {
        private readonly ICompanyService _companyService;

        public AddCompanyCommandHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<Result<Guid>> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = new Domain.Entities.Company.Company(request.AddCompanyDTO.Name,
                request.AddCompanyDTO.Description,
                request.AddCompanyDTO.CreatedBy,
                request.AddCompanyDTO.IsActive);

            var result = await _companyService.AddCompany(company, cancellationToken);

            if (result == Guid.Empty)
            {
                return Result.Failure<Guid>(GetError.Error("company"));
            }

            return Result.Success(result);
        }
    }
}
