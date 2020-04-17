using System.Collections.Generic;
using ASPNETapp2.Models;

namespace ASPNETapp2.Services
{
    public interface IService<T>
    {
        IEnumerable<T> FindAll(SearchCondition condition);
        T FindById(int id);
        T Add(T newObject);
        string Remove(int id);
        T Update(T updatedObject);
    }
}