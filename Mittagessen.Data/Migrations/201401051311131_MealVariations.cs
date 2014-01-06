namespace Mittagessen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MealVariations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MealVariations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MealId = c.Guid(nullable: false),
                        Name = c.String(),
                        RequiresDeadLine = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Meals", t => t.MealId, cascadeDelete: true)
                .Index(t => t.MealId);
            
            AddColumn("dbo.Enrollments", "MealVariationId", c => c.Guid());
            AddForeignKey("dbo.Enrollments", "MealVariationId", "dbo.MealVariations", "Id");
            CreateIndex("dbo.Enrollments", "MealVariationId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MealVariations", new[] { "MealId" });
            DropIndex("dbo.Enrollments", new[] { "MealVariationId" });
            DropForeignKey("dbo.MealVariations", "MealId", "dbo.Meals");
            DropForeignKey("dbo.Enrollments", "MealVariationId", "dbo.MealVariations");
            DropColumn("dbo.Enrollments", "MealVariationId");
            DropTable("dbo.MealVariations");
        }
    }
}
