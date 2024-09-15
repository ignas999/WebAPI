using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface ILocationRepository
    {
        public ICollection<Locations> GetLocations();


        //vienai categorijos paieskai
        Locations GetLocation(int id);

        Locations GetLocation(string name);

        ICollection<Transports> GetTransportsByLocation(int id);

        ICollection<Workers> GetWorkersByLocation(int id);

        bool LocationExists(int id);

        bool CreateLocation(Locations location);
        bool UpdateLocation(Locations location);

        bool DeleteLocation(Locations location);
        bool Save();
    }
}
