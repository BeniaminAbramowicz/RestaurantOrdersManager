using ASPNETapp2.Models;
using System.Collections.Generic;
using System.Linq;

namespace ASPNETapp2.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        public ResponseObject<Order> FindAll(SearchCondition condition)
        {
            try
            {
                IEnumerable<Order> ordersList = DBConnection.EntityMapper.QueryForList<Order>("GetOrdersList", condition);
                if (ordersList == null || !ordersList.Any())
                {
                    return new ResponseObject<Order>() { Message = "List of orders is empty" };
                }
                else
                {
                    return new ResponseObject<Order>() { ResponseList = ordersList };
                }
            }
            catch
            {
                return new ResponseObject<Order>() { Message = "There was an error processing the request. Try again later" };
            }
        }

        public ResponseObject<Order> FindById(int orderId)
        {
            try
            {
                ResponseObject<Order> orderResponse = new ResponseObject<Order>()
                {
                    ResponseData = DBConnection.EntityMapper.QueryForObject<Order>("GetOrderById", orderId)
                }; 
                if (orderResponse.ResponseData == null)
                {
                    orderResponse.Message = "Order with a given id doesn't exist in database";
                    return orderResponse;
                }
                else
                {
                    return orderResponse;
                }
            }
            catch
            {
                return new ResponseObject<Order>() { Message = "There was an error processing the request. Try again later" };
            }
        }

        public ResponseObject<OrderItem> FindOrderItemById(int orderItemId)
        {
            try
            {
                ResponseObject<OrderItem> orderItemResponse = new ResponseObject<OrderItem>()
                {
                    ResponseData = DBConnection.EntityMapper.QueryForObject<OrderItem>("GetOrderItemById", orderItemId)
                };
                if(orderItemResponse.ResponseData == null)
                {
                    orderItemResponse.Message = "Order item with a given id doesn't exist in database";
                    return orderItemResponse;
                }
                else
                {
                    return orderItemResponse;
                }
            }
            catch
            {
                return new ResponseObject<OrderItem>() { Message = "There was an error processing the request. Try again later" };
            }
        }

        public ResponseObject<Order> Add(Order newOrder)
        {
            try
            {
                DBConnection.EntityMapper.BeginTransaction();
                DBConnection.EntityMapper.Insert("AddOrder", newOrder);
                int newOrderId = DBConnection.EntityMapper.QueryForObject<int>("ReturnOrder", "");
                for (var i = 0; i < newOrder.OrderItems.Count; i++)
                {
                    newOrder.OrderItems[i].OrderId = newOrderId;
                    DBConnection.EntityMapper.Insert("AddOrderItems", newOrder.OrderItems[i]);
                }
                DBConnection.EntityMapper.CommitTransaction();
                ResponseObject<Order> orderResponse = new ResponseObject<Order>()
                {
                    ResponseData = FindById(newOrderId).ResponseData
                };
                orderResponse.Message = "Successfully added the Order";
                return orderResponse;
            }
            catch
            {
                DBConnection.EntityMapper.RollBackTransaction();
                return new ResponseObject<Order>() { Message = "There was an error while adding new order. Try again later" };
            } 
        }

        public ResponseObject<Order> Remove(int orderId)
        {
            try
            {
                DBConnection.EntityMapper.BeginTransaction();
                DBConnection.EntityMapper.Delete("RemoveOrderItems", orderId);
                DBConnection.EntityMapper.Delete("RemoveOrder", orderId);
                DBConnection.EntityMapper.CommitTransaction();
                return new ResponseObject<Order>() { Message = "Successfully removed the order" };
            }
            catch
            {
                DBConnection.EntityMapper.RollBackTransaction();
                return new ResponseObject<Order>() { Message = "There was an error while removing the order. Try again later" };
            } 
        }

        public ResponseObject<OrderItem> AddPosition(OrderItem newPosition)
        {
            try
            {
                DBConnection.EntityMapper.BeginTransaction();
                DBConnection.EntityMapper.Insert("AddOrderItems", newPosition);
                OrderItem returnedPosition = FindOrderItemById(DBConnection.EntityMapper.QueryForObject<int>("ReturnOrderItem", "")).ResponseData;
                double totalPrice = FindById(newPosition.OrderId).ResponseData.TotalPrice;
                DBConnection.EntityMapper.Update("UpdateTotalPrice", new UpdateOrderPrice() { OrderId = newPosition.OrderId, NewPrice = totalPrice + newPosition.Price });
                DBConnection.EntityMapper.CommitTransaction();
                return new ResponseObject<OrderItem>() { ResponseData = returnedPosition, Message = "Added new item to order" };
            }
            catch
            {
                DBConnection.EntityMapper.RollBackTransaction();
                return new ResponseObject<OrderItem>() { Message = "There was an error while adding new position to the order. Try again later" };
            }
        }

        public ResponseObject<OrderItem> UpdateOrderItem(OrderItem updatedOrderItem)
        {
            try
            {
                DBConnection.EntityMapper.BeginTransaction();
                double price = FindOrderItemById(updatedOrderItem.OrderItemId).ResponseData.Price;
                DBConnection.EntityMapper.Update("UpdateOrderItem", updatedOrderItem);
                Order order = DBConnection.EntityMapper.QueryForObject<Order>("GetOrderByOrderItemId", updatedOrderItem.OrderItemId);
                order.TotalPrice -= price;
                DBConnection.EntityMapper.Update("UpdateTotalPrice", new UpdateOrderPrice() { OrderId = order.OrderId, NewPrice = order.TotalPrice + updatedOrderItem.Price });
                OrderItem updatedPosition = FindOrderItemById(updatedOrderItem.OrderItemId).ResponseData;
                DBConnection.EntityMapper.CommitTransaction();
                return new ResponseObject<OrderItem>() { ResponseData = updatedPosition, Message = "Updated chosen position" };
            }
            catch
            {
                DBConnection.EntityMapper.RollBackTransaction();
                return new ResponseObject<OrderItem>() { Message = "There was an error while updating order position. Try again later" };
            }
        }

        public ResponseObject<Order> RemovePosition(int orderItemId, int orderId)
        {
            try
            {
                DBConnection.EntityMapper.BeginTransaction();
                Order order = FindById(orderId).ResponseData;
                double deletedPositionPrice = FindOrderItemById(orderItemId).ResponseData.Price;
                DBConnection.EntityMapper.Delete("RemoveSingleItem", orderItemId);
                DBConnection.EntityMapper.Update("UpdateTotalPrice", new UpdateOrderPrice() { OrderId = orderId, NewPrice = order.TotalPrice - deletedPositionPrice });
                order = FindById(orderId).ResponseData;
                DBConnection.EntityMapper.CommitTransaction();
                return new ResponseObject<Order>() { ResponseData = order, Message = "Successfully removed order position" };
            }
            catch
            {
                DBConnection.EntityMapper.RollBackTransaction();
                return new ResponseObject<Order>() { Message = "There was an error while removing order position. Try again later" };
            }
        }

        public ResponseObject<Order> Update(Order updatedOrder)
        {
            try
            {
                DBConnection.EntityMapper.BeginTransaction();
                DBConnection.EntityMapper.Update("UpdateOrder", updatedOrder);
                foreach (var orderItem in updatedOrder.OrderItems)
                {
                    DBConnection.EntityMapper.Update("UpdateOrderItem", orderItem);
                }
                DBConnection.EntityMapper.CommitTransaction();
                ResponseObject<Order> orderResponse = new ResponseObject<Order>()
                {
                    ResponseData = FindById(updatedOrder.OrderId).ResponseData
                };
                return orderResponse;
            }
            catch
            {
                DBConnection.EntityMapper.RollBackTransaction();
                return new ResponseObject<Order>() { Message = "There was an error while updating the order. Try again later" };
            }
            
        }

        public ResponseObject<Order> PayForOrder(int orderId)
        {
            try
            {
                DBConnection.EntityMapper.Update("PayForOrder", orderId);
                return new ResponseObject<Order>() { Message = "Order status has been changed to Paid" };
            }
            catch
            {
                return new ResponseObject<Order>() { Message = "There was an error while updating status of the order. Try again later" };
            } 
        }
    }
}