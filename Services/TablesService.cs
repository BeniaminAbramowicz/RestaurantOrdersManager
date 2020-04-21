using ASPNETapp2.Models;
using ASPNETapp2.Repositories;

namespace ASPNETapp2.Services
{
    public class TablesService : IExtendedService<ResponseObject<Table>,Table>
    {
        private readonly IExtendedRepository<ResponseObject<Table>,Table> _tablesRepository;

        public TablesService()
        {
            _tablesRepository = new TablesRepository();
        }

        public ResponseObject<Table> FindAll(SearchCondition condition)
        {
            return _tablesRepository.FindAll(condition);
        }

        public ResponseObject<Table> FindById(int tableId)
        {
            return _tablesRepository.FindById(tableId);
        }

        public ResponseObject<Table> FindByName(string tableName)
        {
            return _tablesRepository.FindByName(tableName);
        }

        public ResponseObject<Table> Add(Table newTable)
        {
            ResponseObject<Table> tableExists = new ResponseObject<Table>()
            {
                ResponseData = FindByName(newTable.TableName).ResponseData
            };
            if (tableExists.ResponseData == null)
            {
                return _tablesRepository.Add(newTable);
            }
            else
            {
                return new ResponseObject<Table>() { Message = "Table already exists in the database" };
            }
        }

        public ResponseObject<Table> Remove(int tableId)
        {
            ResponseObject<Table> tableExists = new ResponseObject<Table>()
            {
                ResponseData = FindById(tableId).ResponseData
            };
            if (tableExists.ResponseData != null)
            {
                _tablesRepository.Remove(tableId);
                return new ResponseObject<Table>() { Message = "Successfully removed the table" };
            }
            else
            {
                return new ResponseObject<Table>() { Message = "Chosen table doesn't exist in the database" };
            }
        }

        public ResponseObject<Table> Update(Table updatedTable)
        {
            ResponseObject<Table> tableExists = new ResponseObject<Table>()
            {
                ResponseData = FindById(updatedTable.TableId).ResponseData
            };
            if (tableExists.ResponseData != null)
            {
                return _tablesRepository.Update(updatedTable);
            }
            else
            {
                return new ResponseObject<Table>() { Message = "Chosen table doesn't exist in the database" };
            }
        }
    }
}