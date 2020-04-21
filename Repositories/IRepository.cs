using System.Collections.Generic;
using ASPNETapp2.Models;

namespace ASPNETapp2.Repositories
{
    public interface IRepository<T,K>
    {
        T FindAll(SearchCondition condition);
        T FindById(int id);
        T Add(K newObject);
        T Remove(int id);
        T Update(K updatedObject);
    }
}