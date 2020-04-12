using ASPNETapp2.Models;
using ASPNETapp2.Repositories;
using System.Collections.Generic;

namespace ASPNETapp2.Services
{
    public class TablesService : IService<Table>
    {
        private readonly IRepository<Table> _tablesRepository;

        public TablesService()
        {
            _tablesRepository = new TablesRepository();
        }

        public IEnumerable<Table> FindAll()
        {
            return _tablesRepository.FindAll();
        }

        public Table FindById(int tableId)
        {
            return _tablesRepository.FindById(tableId);
        }

        public Table FindByName(string tableName)
        {
            return _tablesRepository.FindByName(tableName);
        }

        public Table Add(Table newTable)
        {
            Table tableExists = FindByName(newTable.TableName);
            if (tableExists == null)
            {
                return _tablesRepository.Add(newTable);
            }
            else
            {
                return null;
            }
        }

        public string Remove(int tableId)
        {
            Table mealExists = FindById(tableId);
            if (mealExists != null)
            {
                _tablesRepository.Remove(tableId);
                return "Pomyślnie usunięto stolik";
            }
            else
            {
                return "Wybrany stolik nie istnieje w bazie stolików";
            }
        }

        public Table Update(Table updatedTable)
        {
            Table tableExists = FindById(updatedTable.TableId);
            if (tableExists != null)
            {
                return _tablesRepository.Update(updatedTable);
            }
            else
            {
                return null;
            }
        }
    }
}