using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DataTransferObject;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repository
{
    public class TransportRepository : ITransportRepository
    {
        private readonly DataContext _context;

        public TransportRepository(DataContext context)
        {
            _context = context;
        }

        public bool TransportExists(int transport_id)
        {
            return _context.Transports.Any(p => p.transport_id == transport_id);
        }

        public Transports GetTransport(int id)
        {
            return _context.Transports.Include(l => l.Locations).Include(s => s.Statuses).Include(c => c.Categories).Where(p => p.transport_id == id).FirstOrDefault();
        }

        public ICollection<Transports> GetTransports()
        {

            return _context.Transports.Include(l => l.Locations).Include(s => s.Statuses).Include(c => c.Categories).OrderBy(p => p.transport_id).ToList();
        }

        public ICollection<Maintenances> GetMaintenancesByTransportId(int transport_id)
        {
            return _context.Maintenances.Include(w => w.Worker).Include(t => t.Transport).Include(r => r.Repair).Where(m => m.transport_id == transport_id).ToList();
        }

        public ICollection<Orders> GetOrdersByTransportId(int transport_id)
        {
            return _context.Orders
            .Include(u => u.User)
            .Include(t => t.Transport)
            .Include(s => s.Status)
            .Where(o => o.transport_id == transport_id).ToList();
        }

        public bool CreateTransport(Transports Transport)
        {
          
            _context.Add(Transport);

            return Save();
        }

        public bool Save()
        {
            try { 
                int saved = _context.SaveChanges();
                return saved > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public bool UpdateTransport(Transports transport)
        {
            _context.Update(transport);
            return Save();
        }

        public bool DeleteTransport(Transports transport)
        {
            _context.Remove(transport);
            return Save();
        }
    }
}
