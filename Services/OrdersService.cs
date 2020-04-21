using ASPNETapp2.Models;
using ASPNETapp2.Repositories;

namespace ASPNETapp2.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IRepository<ResponseObject<Order>,Order> _ordersRepository;

        public OrdersService()
        {
            _ordersRepository = new OrdersRepository();
        }

        public ResponseObject<Order> FindAll(SearchCondition condition)
        {
            return _ordersRepository.FindAll(condition);
        }

        public ResponseObject<Order> FindById(int orderId)
        {
            return _ordersRepository.FindById(orderId);
        }

        public ResponseObject<Order> Add(Order newOrder)
        {
            return _ordersRepository.Add(newOrder);
        }

        public ResponseObject<Order> Remove(int orderId)
        {
            throw new System.NotImplementedException();
        }

        public ResponseObject<Order> Update(Order updatedOrder)
        {
            throw new System.NotImplementedException();
        }

        public ResponseObject<Order> PayForOrder(int orderId)
        {
            throw new System.NotImplementedException();
        }
    }
}