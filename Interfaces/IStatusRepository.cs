using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IStatusRepository
    {
        public ICollection<Statuses> GetStatuses();

        Statuses GetStatus(int id);

        Statuses GetStatus(string name);

        public ICollection<Transports> GetTransports(int Status_id);

        public ICollection<Orders> GetOrdersByStatusId(int Status_id);

        bool StatusExists(int status_id);

        bool CreateStatus(Statuses status);
        bool UpdateStatus(Statuses status);
        bool DeleteStatus(Statuses status);
        bool Save();
    }
}
