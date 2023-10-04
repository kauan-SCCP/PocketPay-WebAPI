using Microsoft.EntityFrameworkCore;
using pocketpay.Models;
using pocketpay.Repositories.Interfaces;
using System.Security.Principal;

namespace pocketpay.Repositories
{
    public class TransferenceRepository : ITransferenceRepository
    {
        private BankContext _context;
        public TransferenceRepository(BankContext context)
        {
            _context = context;
        }

        public async Task<TransferenceModels> Create(AccountModel sender, AccountModel receiver, double value)
        {
            var newTransference = new TransferenceModels();

            newTransference.Sender = sender;
            newTransference.Receiver = receiver;
            newTransference.Value = value;

            _context.AddAsync(newTransference);
            _context.SaveChanges();

            return newTransference;
        }

        public async Task<IEnumerable<TransferenceModels>> FindByAccount(AccountModel account)
        {
            var transference = await _context.Transferences
                .Include(transference => transference.Sender)
                .Include(transference => transference.Receiver)
                .Where(transference => transference.Sender == account || transference.Receiver == account)
                .ToListAsync();

            return transference;
        }

        public async Task<TransferenceModels> FindById(Guid id)
        {
            var transference = await _context.Transferences
                .Include(transference => transference.Sender)
                .Include(transference => transference.Receiver)
                .FirstOrDefaultAsync(transference => transference.Id == id);

            return transference;
        }

        public async Task<IEnumerable<TransferenceModels>> FindByReceiver(AccountModel receiver)
        {
            var transference = await _context.Transferences
                .Include(transference => transference.Sender)
                .Include(transference => transference.Receiver)
                .Where(transference => transference.Receiver == receiver)
                .ToListAsync();

            return transference;
        }

        public async Task<IEnumerable<TransferenceModels>> FindBySender(AccountModel sender)
        {
            var transference = await _context.Transferences
                .Include(transference => transference.Sender)
                .Include(transference => transference.Receiver)
                .Where(transference => transference.Sender == sender)
                .ToListAsync();

            return transference;
        }

        public async Task<TransferenceModels> FindByTransaction(TransactionModel transaction)
        {
            var transference = await _context.Transferences
                .Include(transference => transference.Transaction)
                .FirstOrDefaultAsync(transference => transference.Transaction == transaction);

            return transference;
        }

        public async Task<TransferenceModels> Revert(TransferenceModels transference)
        {
            var transaction = transference.Transaction;
            transaction.Status = TransactionStatus.Reverted;

            return transaction;
        }
    }
}
