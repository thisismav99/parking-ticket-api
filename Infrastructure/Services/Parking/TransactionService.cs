using Domain.Entities.Parking;
using Infrastructure.Interfaces.Parking;
using Infrastructure.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Infrastructure.Services.Parking
{
    internal class TransactionService : ITransactionService
    {
        private readonly ParkingTicketDbContext _parkingTicketDbContext;
        private readonly DbSet<Transaction> _transactions;

        public TransactionService(ParkingTicketDbContext parkingTicketDbContext)
        {
            _parkingTicketDbContext = parkingTicketDbContext;
            _transactions = _parkingTicketDbContext.Set<Transaction>();
        }

        public async Task<Guid> AddTransaction(Transaction transaction, CancellationToken cancellationToken)
        {
            await _transactions.AddAsync(transaction, cancellationToken);
            await _parkingTicketDbContext.SaveChangesAsync(cancellationToken);

            return transaction.Id;
        }

        public async Task<Transaction?> GetTransactionById(Guid transactionId, CancellationToken cancellationToken)
        {
            return await _transactions.FirstOrDefaultAsync(t => t.Id == transactionId, cancellationToken);
        }

        public async Task<List<Transaction>> GetTransactions(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await GetPagedList<Transaction>.GetList(_transactions, pageNumber, pageSize, cancellationToken);
        }
    }
}
