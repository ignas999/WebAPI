using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repository
{
    public class StatusRepository: IStatusRepository
    {
        private readonly DataContext _context;

        public StatusRepository(DataContext context)
        {
            _context = context;
        }



        public ICollection<Orders> GetOrdersByStatusId(int status_id)
        {
            return _context.Orders.Include(u => u.User).Include(t => t.Transport).Include(s => s.Status).Where(o => o.status_id == status_id).ToList();
        }

        public Statuses GetStatus(int id)
        {
            //gauname status pagal status_id
            //_context turi Statuses table
            //where salyga pereina per visus objektus Statuses Sarase 
            // elementai kurie patenkins salyga bus grazinami / i sita return contexta
            //first or default grazina tik pirma ir vienintele reiksme
            return _context.Statuses.Where(p=> p.status_id == id).FirstOrDefault();
        }

        public Statuses GetStatus(string name)
        {
            return _context.Statuses.Where(p => p.name == name).FirstOrDefault();
        }

        public ICollection<Statuses> GetStatuses()
        {
            return _context.Statuses.OrderBy(p => p.status_id).ToList();
        }



        public ICollection<Transports> GetTransports(int Status_id)
        {
            //grazintu tik item be jokiu relationships
            //var transports = _context.Transports.Where(p => p.status_id == Status_id).ToList();
            
            var transports2 = _context.Transports.Include(l => l.Locations).Include(s => s.Statuses).Include(c => c.Categories).Where(p => p.status_id == Status_id).ToList();

            if (transports2.Count() > 0)
            {
                return (ICollection<Transports>)transports2;
            }

            return (ICollection<Transports>)transports2;


        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CreateStatus(Statuses status)
        {
            _context.Add(status);
            //you can do what you want before saving the entity

            return Save();
        }

        public bool StatusExists(int status_id)
        {
            return _context.Statuses.Any(p => p.status_id == status_id);
        }

        public bool UpdateStatus(Statuses status)
        {
            _context.Update(status);
            return Save();
        }

        public bool DeleteStatus(Statuses status)
        {
            _context.Remove(status);
            return Save();
        }
    }

}
