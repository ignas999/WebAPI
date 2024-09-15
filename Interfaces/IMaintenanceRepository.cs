using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IMaintenanceRepository
    {
        public ICollection<Maintenances> GetMaintenances();

        public bool MaintenanceExists(int maintenance_id);

        Maintenances GetMaintenance(int id);

        bool CreateMaintenance(Maintenances maintenance);

        bool Save();
        bool UpdateMaintenance(Maintenances maintenances);
        bool DeleteMaintenance(Maintenances maintenances);
    }
}
