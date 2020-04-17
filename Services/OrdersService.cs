using ASPNETapp2.Models;
using System.Collections.Generic;
using ASPNETapp2.Repositories;

namespace ASPNETapp2.Services
{
    public class OrdersService : IService<Order>
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

        public Order FindById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Order Add(Order newObject)
        {
            throw new System.NotImplementedException();
        }

        public string Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public Order Update(Order updatedObject)
        {
            throw new System.NotImplementedException();
        }
    }
}