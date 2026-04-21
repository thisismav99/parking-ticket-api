using Domain.Entities.Parking;
using Infrastructure.Interfaces.Parking;
using Infrastructure.Utilities.Calculations;
using Infrastructure.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Infrastructure.Services.Parking
{
    internal class TransactionService : ITransactionService
    {
        private readonly ParkingTicketDbContext _parkingTicketDbContext;
        private readonly DbSet<Transaction> _transactions;
        private readonly DbSet<Domain.Entities.Parking.Parking> _parkings;

        public TransactionService(ParkingTicketDbContext parkingTicketDbContext)
        {
            _parkingTicketDbContext = parkingTicketDbContext;
            _transactions = _parkingTicketDbContext.Set<Transaction>();
            _parkings = _parkingTicketDbContext.Set<Domain.Entities.Parking.Parking>();
        }

        public async Task<Guid> AddTransaction(Transaction transaction, CancellationToken cancellationToken)
        {
            var totalHours = await _parkings.Where(x => x.Id == transaction.ParkingId)
                .Select(p => p.TotalHours)
                .FirstOrDefaultAsync(cancellationToken);

            var amountToPay = new AmountToPay(totalHours);
            transaction.AmountToPay = amountToPay.Value;

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
