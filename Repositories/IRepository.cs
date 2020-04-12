using System.Collections.Generic;

namespace ASPNETapp2.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> FindAll();
        T FindById(int id);
        T FindByName(string name);
        T Add(T newObject);
        void Remove(int id);
        T Update(T updatedObject);
    }
}