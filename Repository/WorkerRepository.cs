using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repository
{
    public class WorkerRepository :IWorkerRepository
    {
        private readonly DataContext _context;

        public WorkerRepository(DataContext context)
        {
            _context = context;
            
        }

        public ICollection<Maintenances> GetMaintenancesByWorkerId(int worker_id)
        {
            return _context.Maintenances.Include(w => w.Worker).Include(t => t.Transport).Include(r => r.Repair).Where(m => m.worker_id == worker_id).OrderBy(m => m.worker_id).ToList();
        }

        public ICollection<Transactions> GetTransactionsByWorkerId(int worker_id)
        {
            return _context.Transactions.Include(w => w.Worker).Include(u => u.User).Where(t=> t.worker_id == worker_id).OrderBy(t => t.transaction_id).ToList();
        }

        public Workers GetWorker(int id)
        {
          
            return _context.Workers.Include(l=> l.Location).Include(p=> p.Privillege).Where(w => w.worker_id == id).FirstOrDefault();
        }

        public ICollection<Workers> GetWorkers()
        {
            return _context.Workers.AsNoTracking().Include(l => l.Location).Include(p => p.Privillege).OrderBy(p => p.worker_id).ToList();
        }

        public bool CreateWorker(Workers worker)
        {
			worker.password = BCrypt.Net.BCrypt.HashPassword(worker.password);

			_context.Add(worker);

            return Save();
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


        public bool WorkerExists(int id)
        {
            return _context.Workers.Any(p => p.worker_id == id);
        }

        public bool UpdateWorker(Workers worker)
        {
            worker.password = BCrypt.Net.BCrypt.HashPassword(worker.password);
			_context.Update(worker);
            return Save();
        }

        public bool DeleteWorker(Workers worker)
        {
            _context.Remove(worker);
            return Save();
        }

        public Workers GetWorkerfromlogin(string username , string password)
        {



            return _context.Workers.Include(l => l.Location).Include(p => p.Privillege).Where(w => w.username == username).Where(w => w.password == password).FirstOrDefault();
        }

        public Workers GetWorkerFromUsername(string username)
        {
			return _context.Workers.Where(w => w.username == username).FirstOrDefault();

		}
	}
}
