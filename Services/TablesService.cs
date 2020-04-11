using ASPNETapp2.Models;
using ASPNETapp2.Repositories;
using System.Collections.Generic;

namespace ASPNETapp2.Services
{
    public class TablesService : IService<Table>
    {
        private readonly IRepository<Table> _repository;

        public TablesService()
        {
            _repository = new TablesRepository();
        }

        public IEnumerable<Table> FindAll()
        {
            return _repository.FindAll();
        }

        public Table FindById(int tableId)
        {
            return _repository.FindById(tableId);
        }

        public Table Add(Table newTable)
        {
            return _repository.Add(newTable);
        }

        public void Remove(int tableId)
        {
            _repository.Remove(tableId);
        }

        public Table Update(Table updatedObject)
        {
            return _repository.Update(updatedObject);
        }
    }
}