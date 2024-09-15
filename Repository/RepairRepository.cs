using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repository
{
    public class RepairRepository :IRepairRepository
    {
        private readonly DataContext _context;

        public RepairRepository(DataContext context)
        {
            _context = context;
        }



        public ICollection<Maintenances> GetMaintenancesByRepairId(int repair_id)
        {
            return _context.Maintenances.Include(w => w.Worker).Include(t => t.Transport).Include(r => r.Repair).Where(m => m.repair_id == repair_id).OrderBy(m => m.maintenance_id).ToList();
        }

        public Repairs GetRepair(int id)
        {
            return _context.Repairs.Where(o => o.repair_id == id).FirstOrDefault();
        }

        public ICollection<Repairs> GetRepairs()
        {
            return _context.Repairs.OrderBy(o => o.repair_id).ToList();
        }

        public bool RepairExists(int id)
        {
            return _context.Repairs.Any(o => o.repair_id == id);
        }
        public bool CreateRepair(Repairs repair)
        {
            _context.Add(repair);
            //you can do what you want before saving the entity

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool UpdateRepair(Repairs repair) {
            _context.Update(repair);
            return Save();
        }

        public bool DeleteRepair(Repairs repair)
        {
            _context.Remove(repair);
            return Save();
        }
    }
}
