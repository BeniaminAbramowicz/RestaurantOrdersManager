using ASPNETapp2.Models;

namespace ASPNETapp2.Services
{
    public interface IMealsService : IService<Meal>
    {
        Meal FindByName(string mealName);
    }
}