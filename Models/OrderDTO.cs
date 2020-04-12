using System.Collections.Generic;
using System.Linq;

namespace ASPNETapp2.Models
{
    public class OrderDTO
    {
        private List<OrderItemDTO> _sentOrderItems;
        public List<OrderItemDTO> SentOrderItems { get => _sentOrderItems; set => _sentOrderItems = value.OfType<OrderItemDTO>().ToList(); }
        public int SentTableNumber { get; set; }
    }

    public class OrderItemDTO
    {
        public string MealName { get; set; }
        public int Quantity { get; set; }
    }
}