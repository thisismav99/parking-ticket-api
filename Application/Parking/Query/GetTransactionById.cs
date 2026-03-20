using Application.Parking.DTO;
using CSharpFunctionalExtensions;
using Infrastructure.Interfaces.Parking;
using Mapster;
using MediatR;

namespace Application.Parking.Query
{
    internal record class GetTransactionByIdQuery(Guid TransactionId) : IRequest<Result<ResponseTransactionDTO?>>;

    internal class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, Result<ResponseTransactionDTO?>>
    {
        private readonly ITransactionService _transactionService;

        public GetTransactionByIdQueryHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<Result<ResponseTransactionDTO?>> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionService.GetTransactionById(request.TransactionId, cancellationToken);

            if(transaction is null)
            {
                return Result.Failure<ResponseTransactionDTO?>($"No transaction found for Id: {request.TransactionId}");
            }

            ResponseTransactionDTO? transactionDTO = transaction.Adapt<ResponseTransactionDTO?>();

            return Result.Success(transactionDTO);
        }
    }
}
