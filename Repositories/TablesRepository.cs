using ASPNETapp2.Models;
using System.Collections.Generic;
using System.Linq;

namespace ASPNETapp2.Repositories
{
    public class TablesRepository : IExtendedRepository<ResponseObject<Table>,Table>
    {
        public ResponseObject<Table> FindAll(SearchCondition condition)
        {
            try
            {
                IEnumerable<Table> tablesList = DBConnection.EntityMapper.QueryForList<Table>("GetTablesList", condition);
                if(tablesList == null || !tablesList.Any())
                {
                    return new ResponseObject<Table>() { Message = "List of tables is empty" };
                }
                else
                {
                    return new ResponseObject<Table>() { ResponseList = tablesList };
                }
            }
            catch
            {
                return new ResponseObject<Table>(){ Message = "There was an error processing the request. Try again later" };
            }
        }

        public ResponseObject<Table> FindById(int tableId)
        {
            try
            {
                ResponseObject<Table> tableResponse = new ResponseObject<Table>()
                {
                    ResponseData = DBConnection.EntityMapper.QueryForObject<Table>("GetTableById", tableId)
                };
                if (tableResponse.ResponseData == null)
                {
                    tableResponse.Message = "Table with a given id doesn't exist in database";
                    return tableResponse;
                }
                else
                {
                    return tableResponse;
                }
            }
            catch
            {
                return new ResponseObject<Table>() { Message = "There was an error processing the request. Try again later" };
            }
        }

        public ResponseObject<Table> FindByName(string tableName)
        {
            try
            {
                ResponseObject<Table> tableResponse = new ResponseObject<Table>()
                {
                    ResponseData = DBConnection.EntityMapper.QueryForObject<Table>("GetTableByName", tableName)
                };
                if (tableResponse.ResponseData == null)
                {
                    tableResponse.Message = "Table with a given name doesn't exist in database";
                    return tableResponse;
                }
                else
                {
                    return tableResponse;
                }
            }
            catch
            {
                return new ResponseObject<Table>() { Message = "There was an error processing the request. Try again later" };
            }
        }

        public ResponseObject<Table> Add(Table newTable)
        {
            try
            {
                DBConnection.EntityMapper.Insert("AddTable", newTable);
                ResponseObject<Table> tableResponse = new ResponseObject<Table>()
                {
                    ResponseData = FindById(DBConnection.EntityMapper.QueryForObject<int>("ReturnTable", "")).ResponseData
                };
                return tableResponse;
            }
            catch
            {
                return new ResponseObject<Table>() { Message = "There was an error while adding new table. Try again later" };
            }
        }

        public ResponseObject<Table> Remove(int tableId)
        {
            try
            {
                DBConnection.EntityMapper.Delete("RemoveTable", tableId);
                ResponseObject<Table> response = new ResponseObject<Table>()
                {
                    Message = "Successfully remove the table"
                };
                return response;
            }
            catch
            {
                return new ResponseObject<Table>() { Message = "There was an error while removing the table. Try again later" };
            }           
        }

        public ResponseObject<Table> Update(Table updatedTable)
        {
            try
            {
                DBConnection.EntityMapper.Update("UpdateTable", updatedTable);
                ResponseObject<Table> tableResponse = new ResponseObject<Table>()
                {
                    ResponseData = FindById(updatedTable.TableId).ResponseData
                };
                tableResponse.Message = "Successfully updated the table";
                return tableResponse;
            }
            catch
            {
                return new ResponseObject<Table>() { Message = "There was an error while updating the table. Try again later" };
            }   
        }
    }
}