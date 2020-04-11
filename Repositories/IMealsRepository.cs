using ASPNETapp2.Models;
namespace ASPNETapp2.Repositories
{
    public interface IMealsRepository : IRepository<Meal>
    {
        Meal FindByName(string mealName);
    }
}