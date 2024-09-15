using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IWorkerRepository
    {
        public ICollection<Workers> GetWorkers();

        public Workers GetWorker(int id);

        public ICollection<Maintenances> GetMaintenancesByWorkerId(int worker_id);

        public ICollection<Transactions> GetTransactionsByWorkerId(int worker_id);

        bool WorkerExists(int id);

        bool CreateWorker(Workers worker);

        bool Save();
        bool UpdateWorker(Workers worker);
        bool DeleteWorker(Workers worker);

        public Workers GetWorkerfromlogin(string username, string password);
        public Workers GetWorkerFromUsername(string username);

	}
}
