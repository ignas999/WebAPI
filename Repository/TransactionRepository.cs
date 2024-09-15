using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _context;

        public TransactionRepository(DataContext context)
        {
            _context = context;
        }



        public Transactions GetTransaction(int id)
        {
            return _context.Transactions.Include(w => w.Worker).Include(u => u.User).Where(t => t.transaction_id == id).FirstOrDefault();
        }

        public ICollection<Transactions> GetTransactions()
        {
            return _context.Transactions.Include(w => w.Worker).Include(u => u.User).OrderBy(t=> t.transaction_id).ToList();
        }

        public bool Save()
        {
            try
            {
                int saved = _context.SaveChanges();
                return saved > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CreateTransaction(Transactions Transaction)
        {

            _context.Add(Transaction);

            return Save();
        }

        public bool TransactionExists(int transaction_id)
        {
            return _context.Transactions.Any(p => p.transaction_id == transaction_id);
        }

        public bool UpdateTransaction(Transactions transaction)
        {
            _context.Update(transaction);
            return Save();
        }

        public bool DeleteTransaction(Transactions transaction)
        {
            _context.Remove(transaction);
            return Save();
        }
    }
}
