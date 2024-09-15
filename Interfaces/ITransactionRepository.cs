using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface ITransactionRepository
    {
        public ICollection<Transactions> GetTransactions();

        Transactions GetTransaction(int id);

        bool TransactionExists(int transaction_id);

        bool CreateTransaction(Transactions Transaction);

        bool Save();
        bool UpdateTransaction(Transactions transaction);
        bool DeleteTransaction(Transactions transaction);
    }
}
