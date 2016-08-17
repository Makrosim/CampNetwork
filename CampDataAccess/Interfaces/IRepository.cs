using System;
using System.Collections.Generic;

namespace CampDataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Get(string id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}