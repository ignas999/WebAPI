namespace WebApi.DataTransferObject
{
    public class WorkersDtoCreate
    {
        public int worker_id { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public int location_id { get; set; }

        public int privillege_id { get; set; }

    }
}
