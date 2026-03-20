using Application.Parking.DTO;
using Infrastructure.Interfaces.Parking;
using Mapster;
using MediatR;

namespace Application.Parking.Query
{
    internal record class GetTransactionsQuery(int PageNumber, int PageSize) : IRequest<List<ResponseTransactionDTO>>;

    internal class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<ResponseTransactionDTO>>
    {
        private readonly ITransactionService _transactionService;

        public GetTransactionsQueryHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<List<ResponseTransactionDTO>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _transactionService.GetTransactions(request.PageNumber, request.PageSize, cancellationToken);

            List<ResponseTransactionDTO> transactionDTOs = transactions.Adapt<List<ResponseTransactionDTO>>();

            return transactionDTOs;
        }
    }
}
