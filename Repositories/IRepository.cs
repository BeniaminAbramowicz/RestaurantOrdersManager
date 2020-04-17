using System.Collections.Generic;
using ASPNETapp2.Models;

namespace ASPNETapp2.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> FindAll(SearchCondition condition);
        T FindById(int id);
        T Add(T newObject);
        void Remove(int id);
        T Update(T updatedObject);
    }
}