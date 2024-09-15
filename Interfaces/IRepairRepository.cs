using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IRepairRepository
    {
        public ICollection<Repairs> GetRepairs();

        Repairs GetRepair(int id);



        bool RepairExists(int id);

        public ICollection<Maintenances> GetMaintenancesByRepairId(int repair_id);

        bool CreateRepair(Repairs repair);
        bool UpdateRepair(Repairs repair);
        bool DeleteRepair(Repairs repair);
        bool Save();
    }
}
