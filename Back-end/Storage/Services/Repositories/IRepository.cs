using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Services
{
    public interface IRepository<T>
    {
        IEnumerable<T> Select();
        T Select(int id);
        void Create(T obj);
        void Update(T obj);
        void Delete(int id);
    }
}
