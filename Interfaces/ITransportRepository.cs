using WebApi.DataTransferObject;
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface ITransportRepository
    {
        public ICollection<Transports> GetTransports();

        public bool TransportExists(int transport_id);


        Transports GetTransport(int id);

        public ICollection<Maintenances> GetMaintenancesByTransportId(int transport_id);

        public ICollection<Orders> GetOrdersByTransportId(int transport_id);

        //Create methods

        //because this table has foreign key ids
        bool CreateTransport(Transports Transport);

        bool Save();
        bool UpdateTransport(Transports transport);
        bool DeleteTransport(Transports transport);

    }
}
