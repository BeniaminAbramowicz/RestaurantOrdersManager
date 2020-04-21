using System.Collections.Generic;
using ASPNETapp2.Models;

namespace ASPNETapp2.Services
{
    public interface IService<T,K>
    {
        T FindAll(SearchCondition condition);
        T FindById(int id);
        T Add(K newObject);
        T Remove(int id);
        T Update(K updatedObject);
    }
}