using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IPrivillegeRepository
    {
        public ICollection<Privilleges> GetPrivilleges();

        Privilleges GetPrivillegeById(int id);

        Privilleges GetPrivillegeByName(string name);

        ICollection<Workers> GetWorkersByPrivillegeId(int id);

        bool PrivillegeExists(int id);
    }
}
