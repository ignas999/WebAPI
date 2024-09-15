using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IUserRepository
    {
        public ICollection<Users> GetUsers();

        public Users GetUser(int id);

        public Users GetUser(string email);

        public bool UserExists(int id);
        public bool UserExists(string email);

        public ICollection<Transactions> GetTransactionsByUserId(int user_id);

        public ICollection<Orders> GetOrdersByUserId(int user_id);

		bool UpdateUser(Users user);
	}
}
