using System.Collections.Generic;
using System.Linq;

namespace ASPNETapp2.Models
{
    public class OrderDTO
    {
        private List<OrderItemDTO> _orderItems;
        public List<OrderItemDTO> OrderItems { get => _orderItems; set => _orderItems = value.OfType<OrderItemDTO>().ToList(); }
        public int TableNumber { get; set; }
    }
}