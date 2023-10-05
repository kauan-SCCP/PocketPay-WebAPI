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

        public async Task<TransferenceModel> Create(TransactionModel transaction, AccountModel sender, AccountModel receiver, double value)
        {
            var newTransference = new TransferenceModel();

            newTransference.Sender = sender;
            newTransference.Receiver = receiver;
            newTransference.Value = value;
            newTransference.Transaction = transaction;

            await _context.AddAsync(newTransference);
            _context.SaveChanges();

            return newTransference;
        }

        public async Task<IEnumerable<TransferenceModel>> FindByAccount(AccountModel account)
        {
            var transference = await _context.Transferences
                .Include(transference => transference.Sender)
                .Include(transference => transference.Receiver)
                .Include(transference => transference.Transaction)
                .Where(transference => transference.Sender == account || transference.Receiver == account)
                .ToListAsync();

            return transference;
        }

        public async Task<TransferenceModel> FindById(Guid id)
        {
            var transference = await _context.Transferences
                .Include(transference => transference.Sender)
                .Include(transference => transference.Receiver)
                .Include(transference => transference.Transaction)
                .FirstOrDefaultAsync(transference => transference.Id == id);

            return transference;
        }

        public async Task<IEnumerable<TransferenceModel>> FindByReceiver(AccountModel receiver)
        {
            var transference = await _context.Transferences
                .Include(transference => transference.Sender)
                .Include(transference => transference.Receiver)
                .Include(transference => transference.Transaction)
                .Where(transference => transference.Receiver == receiver)
                .ToListAsync();

            return transference;
        }

        public async Task<IEnumerable<TransferenceModel>> FindBySender(AccountModel sender)
        {
            var transference = await _context.Transferences
                .Include(transference => transference.Sender)
                .Include(transference => transference.Receiver)
                .Include(transference => transference.Transaction)
                .Where(transference => transference.Sender == sender)
                .ToListAsync();

            return transference;
        }

        public async Task<TransferenceModel> FindByTransaction(TransactionModel transaction)
        {
            var transference = await _context.Transferences
                .Include(transference => transference.Transaction)
                .Include(transference => transference.Transaction)
                .FirstOrDefaultAsync(transference => transference.Transaction == transaction);

            return transference;
        }
    }
}
