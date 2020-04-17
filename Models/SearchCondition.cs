using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETapp2.Models
{
    public class SearchCondition
    {
        public string Condition { get; set; }

        public SearchCondition() { }

        public SearchCondition(string condition)
        {
            Condition = condition;
        }
    }
}