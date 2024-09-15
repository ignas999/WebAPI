using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }
        public Orders GetOrder(int order_id)
        {
            var query = _context.Orders.Where(o => o.order_id == order_id);
            var sql = query.ToQueryString();
            Console.WriteLine(sql);

            return _context.Orders
                .Include(u => u.User)
                .Include(t => t.Transport)
                .Include(s => s.Status)
                .Where(o => o.order_id == order_id).FirstOrDefault();
        }

        public ICollection<Orders> GetOrders()
        {
            return _context.Orders
            .Include(u => u.User)
            .Include(t => t.Transport)
            .Include(s => s.Status)
            .OrderBy(o => o.order_id)
            .ToList();
        }

        public bool OrderExists(int id)
        {
            return _context.Orders.Any(o => o.order_id == id);
        }

        public bool CreateOrder(Orders order)
        {

            _context.Add(order);

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

        public bool UpdateOrder(Orders order)
        {
            _context.Update(order);
            return Save();
        }
    }
}
