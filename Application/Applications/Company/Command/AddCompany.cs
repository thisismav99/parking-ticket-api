using Application.Utilities.Extensions;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Company;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Applications.Company.Command
{
    internal record class AddCompanyCommand([Required, MaxLength(100)] string Name,
        [MaxLength(500)] string? Description,
        [Required, MaxLength(100)] string CreatedBy,
        [Required] bool IsActive) : IRequest<Result<Guid>>;

    internal class AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand, Result<Guid>>
    {
        private readonly ICompanyService _companyService;

        public AddCompanyCommandHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<Result<Guid>> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            var errors = ValidatorHelper<AddCompanyCommand>.Errors(request);
            bool hasErrors = !string.IsNullOrEmpty(errors);

            if (hasErrors)
            {
                return Result.Failure<Guid>(errors);
            }

            var company = new Domain.Entities.Company.Company(request.Name,
                request.Description,
                request.CreatedBy,
                request.IsActive);

            var result = await _companyService.AddCompany(company, cancellationToken);

            if (result == Guid.Empty)
            {
                return Result.Failure<Guid>("Error saving company.");
            }

            return Result.Success(result);
        }
    }
}
