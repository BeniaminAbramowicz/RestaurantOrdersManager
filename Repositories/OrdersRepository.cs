using ASPNETapp2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPNETapp2.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        public IEnumerable<Order> FindAll(SearchCondition condition)
        {
            return DBConnection.EntityMapper.QueryForList<Order>("GetOrdersList", condition);
        }

        public Order FindById(int orderId)
        {
            return DBConnection.EntityMapper.QueryForObject<Order>("GetOrderById", orderId);
        }

        public Order Add(Order newOrder)
        {
            DBConnection.EntityMapper.BeginTransaction();
            DBConnection.EntityMapper.Insert("AddOrder", newOrder);
            int newOrderId = DBConnection.EntityMapper.QueryForObject<int>("ReturnOrder", "");
            for(var i = 0; i < newOrder.OrderItems.Count; i++)
            {
                newOrder.OrderItems[i].OrderId = newOrderId;
                DBConnection.EntityMapper.Insert("AddOrderItems", newOrder.OrderItems[i]);
            }
            DBConnection.EntityMapper.CommitTransaction();
            return FindById(newOrderId);
        }

        public void Remove(int orderId)
        {
            DBConnection.EntityMapper.BeginTransaction();
            DBConnection.EntityMapper.Delete("RemoveOrderItems", orderId);
            DBConnection.EntityMapper.Delete("RemoveOrder", orderId);
            DBConnection.EntityMapper.CommitTransaction();
        }

        public Order Update(Order updatedOrder)
        {
            DBConnection.EntityMapper.BeginTransaction();
            DBConnection.EntityMapper.Update("UpdateOrder", updatedOrder);
            foreach(var orderItem in updatedOrder.OrderItems)
            {
                DBConnection.EntityMapper.Update("UpdateOrderItem", orderItem);
            }
            DBConnection.EntityMapper.CommitTransaction();
            return FindById(updatedOrder.OrderId);
        }

        public void PayForOrder(int orderId)
        {
            DBConnection.EntityMapper.Update("PayForOrder", orderId);
        }
    }
}