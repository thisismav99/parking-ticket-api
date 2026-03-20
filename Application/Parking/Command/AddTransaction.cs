using Application.Utilities.Extensions;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Parking;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Parking.Command
{
    internal record AddTransactionCommand([Required] decimal AmountToPay,
        [Required] decimal AmountPaid,
        [Required] bool IsCard,
        [Required] bool IsCash,
        [Required, MaxLength(100)] string CreatedBy,
        [Required] bool IsActive) : IRequest<Result<Guid>>;

    internal class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, Result<Guid>>
    {
        private readonly ITransactionService _transactionService;

        public AddTransactionCommandHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<Result<Guid>> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            var errors = ValidatorHelper<AddTransactionCommand>.Errors(request);
            bool hasErrors = !string.IsNullOrEmpty(errors);

            if (hasErrors)
            {
                return Result.Failure<Guid>(errors);
            }

            var transaction = new Domain.Entities.Parking.Transaction(
                request.AmountToPay,
                request.AmountPaid,
                request.IsCard,
                request.IsCash,
                request.CreatedBy,
                request.IsActive);

            var result = await _transactionService.AddTransaction(transaction, cancellationToken);

            if(result == Guid.Empty)
            {
                return Result.Failure<Guid>("Error saving transaction.");
            }

            return Result.Success(result);
        }
    }
}
