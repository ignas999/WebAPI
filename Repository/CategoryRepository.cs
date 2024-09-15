using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context) {
            _context = context;
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.category_id == id);
        }

        public ICollection<Categories> GetCategories()
        {
            return _context.Categories.OrderBy(p=> p.category_id).ToList();

        }

        public Categories GetCategory(int id)
        {
            return _context.Categories.Where(p => p.category_id == id).FirstOrDefault();
        }

        public Categories GetCategory(string name)
        {
            return _context.Categories.Where(p => p.name == name).FirstOrDefault();
            
        }

        public ICollection<Transports> GetTransportsByCategory(int id)
        {
            //Returns values without nested relationships
            //var transports = _context.Transports.Where(p=> p.category_id == id).ToList();

            var transports2 = _context.Transports.Include(l => l.Locations).Include(s => s.Statuses).Include(c => c.Categories).Where(p => p.category_id == id).ToList();



            return (ICollection<Transports>)transports2; 
        }

        public bool CreateCategory(Categories Category)
        {
            //there is a change tracker
            //this will start tracking
            //add ,update , modify
            //connected vs disconnected
            //EntityState.Added = this is a disconected state, if you see this
            //but we always use connected states

            _context.Add(Category);
            //you can do what you want before saving the entity

            return Save();

           
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Categories Category)
        {
            _context.Update(Category);
            return Save();
        }

        public bool DeleteCategory(Categories Category)
        {
            _context.Remove(Category);
            return Save();
        }
    }
}
