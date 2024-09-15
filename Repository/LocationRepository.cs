
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DataContext _context;

        public LocationRepository(DataContext context)
        {
            _context = context;
        }



        public Locations GetLocation(int id)
        {
            return _context.Locations.Where(p=> p.location_id == id).FirstOrDefault();
        }

        public Locations GetLocation(string name)
        {
            throw new NotImplementedException();
        }

        public ICollection<Locations> GetLocations()
        {
            return _context.Locations.OrderBy(p=> p.location_id).ToList();
        }

        public ICollection<Transports> GetTransportsByLocation(int id)
        {
           // return _context.Transports.Where(p=> p.location_id == id).ToList();
            return _context.Transports.Include(l => l.Locations).Include(s => s.Statuses).Include(c => c.Categories).Where(p => p.location_id == id).ToList();


        }

        public ICollection<Workers> GetWorkersByLocation(int id)
        {
            return _context.Workers.Include(p => p.Privillege).Include(l => l.Location).Where(p => p.location_id == id).ToList();
        }

        public bool LocationExists(int id)
        {
            return _context.Locations.Any(l => l.location_id == id);
        }

        public bool CreateLocation(Locations location)
        {
            _context.Add(location);
            //you can do what you want before saving the entity

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateLocation(Locations location)
        {
            _context.Update(location);
            return Save();
        }
        public bool DeleteLocation(Locations location)
        {
            _context.Remove(location);
            return Save();
        }
    }
}
