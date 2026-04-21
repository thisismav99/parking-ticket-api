using Application.Applications.Parking.DTO;
using Application.Utilities.Helpers;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Parking;
using MediatR;

namespace Application.Applications.Parking.Command
{
    internal record AddTransactionCommand(AddTransactionDTO AddTransactionDTO) : IRequest<Result<Guid>>;

    internal class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, Result<Guid>>
    {
        private readonly ITransactionService _transactionService;

        public AddTransactionCommandHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<Result<Guid>> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = new Domain.Entities.Parking.Transaction(
                request.AddTransactionDTO.AmountPaid,
                request.AddTransactionDTO.IsCard,
                request.AddTransactionDTO.IsCash,
                request.AddTransactionDTO.ParkingId,
                request.AddTransactionDTO.CreatedBy,
                request.AddTransactionDTO.IsActive);

            var result = await _transactionService.AddTransaction(transaction, cancellationToken);

            if(result == Guid.Empty)
            {
                return Result.Failure<Guid>(GetError.Error("transaction"));
            }

            return Result.Success(result);
        }
    }
}
