﻿using ASPNETapp2.Models;

namespace ASPNETapp2.Services
{
    interface IOrdersService : IService<Order>
    {
        void PayForOrder(int orderId);
    }
}
