namespace ASPNETapp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMeal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        MealId = c.Int(nullable: false, identity: true),
                        MealName = c.String(nullable: false),
                        MealUnitPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.MealId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Meals");
        }
    }
}
