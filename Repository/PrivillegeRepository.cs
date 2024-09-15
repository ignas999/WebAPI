using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repository
{
    public class PrivillegeRepository : IPrivillegeRepository
    {
        private readonly DataContext _context;

        public PrivillegeRepository(DataContext context) 
        {
            _context = context;
        }
        public Privilleges GetPrivillegeById(int id)
        {
            return _context.Privilleges.Where(p=> p.privillege_id == id).FirstOrDefault();
        }

        public Privilleges GetPrivillegeByName(string name)
        {
            return _context.Privilleges.Where(p => p.name == name).FirstOrDefault();
        }

        public ICollection<Privilleges> GetPrivilleges()
        {
            return _context.Privilleges.OrderBy(p => p.privillege_id).ToList();
        }

        public ICollection<Workers> GetWorkersByPrivillegeId(int id)
        {
            return _context.Workers.Include(p => p.Privillege).Include(l => l.Location).Where(p => p.privillege_id == id).ToList();

        }

        public bool PrivillegeExists(int id)
        {
            return _context.Privilleges.Any(p => p.privillege_id == id);
        }
    }
}
