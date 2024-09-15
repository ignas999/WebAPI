using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IOrderRepository
    {

        public ICollection<Orders> GetOrders();

        public Orders GetOrder(int order_id);

        public bool OrderExists (int Id);

        bool CreateOrder(Orders order);

        bool Save();
        bool UpdateOrder(Orders order);
    }
}
