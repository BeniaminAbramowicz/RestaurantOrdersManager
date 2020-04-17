using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNETapp2.Models
{
    public class Table
    {
        public int TableId { get; set; }
        public string TableName { get; set; }

        public Table() { }
        public Table(string tableName)
        {
            TableName = tableName;
        }
        public Table(int tableId, string tableName)
        {
            TableId = tableId;
            TableName = tableName;
        }
    }
}