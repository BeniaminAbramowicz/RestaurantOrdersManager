using ASPNETapp2.Models;
using System.Collections.Generic;
using ASPNETapp2.Repositories;

namespace ASPNETapp2.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IRepository<Order> _ordersRepository;

        public OrdersService()
        {
            _ordersRepository = new OrdersRepository();
        }

        public IEnumerable<Order> FindAll(SearchCondition condition)
        {
            return _ordersRepository.FindAll(condition);
        }

        public Order FindById(int orderId)
        {
            return _ordersRepository.FindById(orderId);
        }

        public Order Add(Order newOrder)
        {
            return _ordersRepository.Add(newOrder);
        }

        public string Remove(int orderId)
        {
            throw new System.NotImplementedException();
        }

        public Order Update(Order updatedOrder)
        {
            throw new System.NotImplementedException();
        }

        public void PayForOrder(int orderId)
        {
            throw new System.NotImplementedException();
        }
    }
}