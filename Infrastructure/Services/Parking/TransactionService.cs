using CSharpFunctionalExtensions;
using Domain.Entities.Parking;
using Infrastructure.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Infrastructure.Services.Parking
{
    internal interface ITransactionService
    {
        Task<Result<Guid>> AddTransaction(Transaction transaction);

        Task<Result<Transaction?>> GetTransactionById(Guid transactionId);

        Task<List<Transaction>> GetTransactions(int pageNumber, int pageSize);
    }

    internal class TransactionService : ITransactionService
    {
        private readonly ParkingTicketDbContext _parkingTicketDbContext;
        private readonly DbSet<Transaction> _transactions;

        public TransactionService(ParkingTicketDbContext parkingTicketDbContext)
        {
            _parkingTicketDbContext = parkingTicketDbContext;
            _transactions = _parkingTicketDbContext.Set<Transaction>();
        }

        public async Task<Result<Guid>> AddTransaction(Transaction transaction)
        {
            if(transaction is not null)
            {
                await _transactions.AddAsync(transaction);
                await _parkingTicketDbContext.SaveChangesAsync();

                return Result.Success(transaction.Id);
            }

            return Result.Failure<Guid>("Transaction cannot be null");
        }

        public async Task<Result<Transaction?>> GetTransactionById(Guid transactionId)
        {
            if(transactionId != Guid.Empty)
            {
                var transaction = await _transactions.FirstOrDefaultAsync(t => t.Id == transactionId);
                
                return Result.Success(transaction);
            }

            return Result.Failure<Transaction?>("Transaction Id cannot be empty");
        }

        public async Task<List<Transaction>> GetTransactions(int pageNumber, int pageSize)
        {
            return await GetPagedList<Transaction>.GetList(_transactions, pageNumber, pageSize);
        }
    }
}
