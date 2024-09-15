namespace WebApi.DataTransferObject
{
    public class MaintenancesDtoCreate
    {
        public int maintenance_id { get; set; }

        public int worker_id { get; set; }

        public int transport_id { get; set; }

        public int repair_id { get; set; }

        public double price { get; set; }

        public int mileage { get; set; }

        public DateOnly date { get; set; }
    }
}
