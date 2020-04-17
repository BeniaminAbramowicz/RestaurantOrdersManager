using ASPNETapp2.Models;
using System.Collections.Generic;

namespace ASPNETapp2.Repositories
{
    public class OrdersRepository : IRepository<Order>
    {
        public IEnumerable<Order> FindAll(SearchCondition condition)
        {
            return DBConnection.EntityMapper.QueryForList<Order>("GetOrdersList", condition);
        }

        public Order FindById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Order Add(Order newObject)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public Order Update(Order updatedObject)
        {
            throw new System.NotImplementedException();
        }
    }
}