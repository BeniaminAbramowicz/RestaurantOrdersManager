using ASPNETapp2.Models;

namespace ASPNETapp2.Services
{
    public class RestaurantFacade
    {
        private readonly IExtendedService<ResponseObject<Meal>,Meal> _mealsService;
        private readonly IExtendedService<ResponseObject<Table>, Table> _tablesService;
        private readonly IOrdersService _ordersService;

        public RestaurantFacade()
        {
            _mealsService = new MealsService();
            _tablesService = new TablesService();
            _ordersService = new OrdersService();
        }

        public ResponseObject<Meal> FindAllMeals(SearchCondition condition)
        {
            return _mealsService.FindAll(condition);
        }

        public ResponseObject<Meal> FindMealById(int mealId)
        {
            return _mealsService.FindById(mealId);
        }

        public ResponseObject<Meal> FindMealByName(string mealName)
        {
            return _mealsService.FindByName(mealName);
        }

        public ResponseObject<Meal> AddMeal(Meal newMeal)
        {
            return _mealsService.Add(newMeal);
        }

        public ResponseObject<Meal> RemoveMeal(int mealId)
        {
            return _mealsService.Remove(mealId);
        }

        public ResponseObject<Meal> UpdateMeal(Meal updatedMeal)
        {
            return _mealsService.Update(updatedMeal);
        }

        public ResponseObject<Table> FindAllTables(SearchCondition condition)
        {
            return _tablesService.FindAll(condition);
        }

        public ResponseObject<Table> FindTableById(int tableId)
        {
            return _tablesService.FindById(tableId);
        }

        public ResponseObject<Table> AddTable(Table newTable)
        {
            return _tablesService.Add(newTable);
        }

        public ResponseObject<Table> RemoveTable(int tableId)
        {
            return _tablesService.Remove(tableId);
        }

        public ResponseObject<Table> UpdateTable(Table updatedTable)
        {
            return _tablesService.Update(updatedTable);
        }

        public ResponseObject<Order> FindAllOrders(SearchCondition condition)
        {
            return _ordersService.FindAll(condition);
        }

        public ResponseObject<Order> FindOrderById(int orderId)
        {
            return _ordersService.FindById(orderId);
        }

        public ResponseObject<Order> AddOrder(Order newOrder)
        {
            return _ordersService.Add(newOrder);
        }

        public ResponseObject<Order> RemoveOrder(int orderId)
        {
            return _ordersService.Remove(orderId);
        }

        public ResponseObject<Order> RemovePosition(int orderItemId, int orderId)
        {
            return _ordersService.RemovePosition(orderItemId, orderId);
        }
    }
}