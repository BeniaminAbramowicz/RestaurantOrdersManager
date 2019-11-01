using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETapp2.Models
{
    public class SentOrder
    {
        private List<SentOrderItem> _sentOrderItems;
        public List<SentOrderItem> SentOrderItems { get => _sentOrderItems; set => _sentOrderItems = value.OfType<SentOrderItem>().ToList(); }

        private int _sentTableNumber;
        public int SentTableNumber { get => _sentTableNumber; set => _sentTableNumber = value; }
    }

    public class SentOrderItem
    {
        private string _sentMealName;
        public string SentMealName { get => _sentMealName; set => _sentMealName = value; }

        private int _sentQuantity;
        public int SentQuantity { get => _sentQuantity; set => _sentQuantity = value; }

    }
}