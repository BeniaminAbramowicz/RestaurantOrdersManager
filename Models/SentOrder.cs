using System.Collections.Generic;
using System.Linq;

namespace ASPNETapp2.Models
{
    public class SentOrder
    {
        private List<SentOrderItem> _sentOrderItems;
        public List<SentOrderItem> SentOrderItems { get => _sentOrderItems; set => _sentOrderItems = value.OfType<SentOrderItem>().ToList(); }
        public int SentTableNumber { get; set; }
    }

    public class SentOrderItem
    {
        public string SentMealName { get; set; }
        public int SentQuantity { get; set; }
    }
}