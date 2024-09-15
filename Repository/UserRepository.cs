using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repository
{
    public class UserRepository :IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Orders> GetOrdersByUserId(int user_id)
        {
            return _context.Orders
            .Include(u => u.User)
            .Include(t => t.Transport)
            .Include(s => s.Status)
            .Where(o => o.user_id == user_id).ToList();
        }

        public ICollection<Transactions> GetTransactionsByUserId(int user_id)
        {
            return _context.Transactions.Include(w => w.Worker).Include(u => u.User).Where(t => t.user_id == user_id).ToList();
        }

        public Users GetUser(int id)
        {
           return _context.Users.Where(p => p.user_id == id).FirstOrDefault();
        }

        public Users GetUser(string email)
        {
            return _context.Users.Where(p => p.email == email).FirstOrDefault();
        }

        public ICollection<Users> GetUsers()
        {
            return _context.Users.OrderBy(p => p.user_id).ToList();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(p => p.user_id == id);
        }

        public bool UserExists(string email)
        {
            return _context.Users.Where(p => p.email == email).Any();
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
		public bool UpdateUser(Users user)
		{
			_context.Update(user);
			return Save();
		}
	}
}
