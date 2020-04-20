namespace ASPNETapp2.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public double MealUnitPrice { get; set; }

        public Meal() { }
        public Meal(MealDTO mealDTO)
        {
            MealName = mealDTO.MealName;
            MealUnitPrice = mealDTO.MealUnitPrice;
        }
    }
}