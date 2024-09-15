using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repository
{
    public class MaintenanceRepository : IMaintenanceRepository

    {
        private readonly DataContext _context;

        public MaintenanceRepository(DataContext context)
        {
            _context = context;
        }

        public Maintenances GetMaintenance(int id)
        {
            return _context.Maintenances.Include(w => w.Worker).Include(t => t.Transport).Include(r => r.Repair).Where(m => m.maintenance_id == id).FirstOrDefault();
        }

        public ICollection<Maintenances> GetMaintenances()
        {
            return _context.Maintenances.Include(w => w.Worker).Include(t => t.Transport).Include(r => r.Repair).OrderBy(m => m.maintenance_id).ToList();
        }

        public bool MaintenanceExists(int maintenance_id)
        {
            return _context.Maintenances.Any(m => m.maintenance_id == maintenance_id);
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

        public bool CreateMaintenance(Maintenances maintenance)
        {
            _context.Add(maintenance);

            return Save();
        }

        public bool UpdateMaintenance(Maintenances maintenances)
        {
            _context.Update(maintenances);
            return Save();
        }

        public bool DeleteMaintenance(Maintenances maintenances)
        {
            _context.Remove(maintenances);
            return Save();
        }
    }
}
