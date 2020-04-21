using ASPNETapp2.Models;

namespace ASPNETapp2.Repositories
{
    interface IOrdersRepository : IRepository<ResponseObject<Order>, Order>
    {
        ResponseObject<Order> PayForOrder(int orderId);
    }
}
