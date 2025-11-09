using Domain.Entities.Parking;

namespace Infrastructure.Interfaces.Parking
{
    internal interface ITransactionService
    {
        Task<Guid> AddTransaction(Transaction transaction, CancellationToken cancellationToken);

        Task<Transaction?> GetTransactionById(Guid transactionId, CancellationToken cancellationToken);

        Task<List<Transaction>> GetTransactions(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
