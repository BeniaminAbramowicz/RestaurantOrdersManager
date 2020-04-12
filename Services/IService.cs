using System.Collections.Generic;

namespace ASPNETapp2.Services
{
    public interface IService<T>
    {
        IEnumerable<T> FindAll();
        T FindById(int id);
        T FindByName(string name);
        T Add(T newObject);
        string Remove(int id);
        T Update(T updatedObject);
    }
}