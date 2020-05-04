using ASPNETapp2.Models;
using System.Data.Entity;

namespace ASPNETapp2.RepositoriesEF
{
    public class RestaurantDBContext : DbContext
    {
        public DbSet<Meal> Meals { get; set; }
    }
}