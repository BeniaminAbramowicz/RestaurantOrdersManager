using System.Collections.Generic;

namespace ASPNETapp2.Services
{
    public interface IService<T>
    {
        IEnumerable<T> FindAll();
        T FindById(int id);
        T Add(T newObject);
        void Remove(int id);
        T Update(T updatedObject);
    }
}