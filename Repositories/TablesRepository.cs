using ASPNETapp2.Models;
using System.Collections.Generic;

namespace ASPNETapp2.Repositories
{
    public class TablesRepository : IExtendedRepository<Table>
    {
        public IEnumerable<Table> FindAll(SearchCondition condition)
        {
            return DBConnection.EntityMapper.QueryForList<Table>("GetTablesList", condition);
        }

        public Table FindById(int tableId)
        {
            return DBConnection.EntityMapper.QueryForObject<Table>("GetTableById", tableId);
        }

        public Table FindByName(string tableName)
        {
            return DBConnection.EntityMapper.QueryForObject<Table>("GetTableByName", tableName);
        }

        public Table Add(Table newTable)
        {
            DBConnection.EntityMapper.Insert("AddTable", newTable);
            return FindById(DBConnection.EntityMapper.QueryForObject<int>("ReturnTable", ""));
        }

        public void Remove(int tableId)
        {
            DBConnection.EntityMapper.Delete("RemoveTable", tableId);
        }

        public Table Update(Table updatedTable)
        {
            DBConnection.EntityMapper.Update("UpdateTable", updatedTable);
            return FindById(updatedTable.TableId);
        }
    }
}