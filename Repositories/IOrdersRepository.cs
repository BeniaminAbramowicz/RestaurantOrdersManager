using ASPNETapp2.Models;

namespace ASPNETapp2.Repositories
{
    interface IOrdersRepository : IRepository<ResponseObject<Order>, Order>
    {
        ResponseObject<OrderItem> FindOrderItemById(int orderItemId);
        ResponseObject<Order> RemovePosition(int orderItemId, int orderId);
        ResponseObject<OrderItem> AddPosition(OrderItem newPosition);
        ResponseObject<Order> PayForOrder(int orderId);
    }
}
