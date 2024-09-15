namespace WebApi.DataTransferObject
{
    public class TransactionsDtoCreate
    {
        public int transaction_id { get; set; }

        public int user_id { get; set; }

        public double amount { get; set; }

        public int worker_id { get; set; }

        public DateOnly date { get; set; }
    }
}
