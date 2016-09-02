using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CampDataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);
        IEnumerable<T> GetAll();
        T Get(int id);
        T Get(string id);
        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);
        void Update(T item);
        void Delete(int id);
    }
}