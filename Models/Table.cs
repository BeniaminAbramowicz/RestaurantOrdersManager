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
    }
}