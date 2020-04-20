using ASPNETapp2.Models;

namespace ASPNETapp2.Repositories
{
    interface IOrdersRepository : IRepository<Order>
    {
        void PayForOrder(int orderId);
    }
}
