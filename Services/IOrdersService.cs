﻿using ASPNETapp2.Models;

namespace ASPNETapp2.Services
{
    interface IOrdersService : IService<ResponseObject<Order>,Order>
    {
        ResponseObject<Order> PayForOrder(int orderId);
    }
}