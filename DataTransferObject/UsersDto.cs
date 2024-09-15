namespace WebApi.DataTransferObject
{
    public class UsersDto
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public double balance { get; set; }
        public string? phone { get; set; }
    }
}
