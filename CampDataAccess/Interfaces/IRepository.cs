using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampDataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Get(string id);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(string id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}