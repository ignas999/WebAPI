using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface ICategoryRepository
    {
        public ICollection<Categories> GetCategories();


        //vienai categorijos paieskai
        Categories GetCategory(int id);

        Categories GetCategory(string name);

        ICollection<Transports> GetTransportsByCategory(int id);

        bool CategoryExists(int id);

        //Create methods

        bool CreateCategory(Categories Category);

        bool UpdateCategory(Categories Category);
        bool DeleteCategory(Categories Category);

        bool Save();
    }
}
