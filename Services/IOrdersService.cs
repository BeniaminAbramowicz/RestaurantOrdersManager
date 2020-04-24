using ASPNETapp2.Models;

namespace ASPNETapp2.Services
{
    interface IOrdersService : IService<ResponseObject<Order>,Order>
    {
        ResponseObject<Order> RemovePosition(int orderItemId, int orderId);
        ResponseObject<OrderItem> AddPosition(OrderItem newPosition);
        ResponseObject<Order> PayForOrder(int orderId);
    }
}
