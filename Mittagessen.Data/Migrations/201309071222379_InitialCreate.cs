namespace Mittagessen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Password = c.Binary(),
                        PasswordSalt = c.Binary(),
                        Email = c.String(),
                        Roles = c.String(),
                        RegistrationDate = c.DateTime(nullable: false),
                        LastAccessDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EnrollmentDate = c.DateTime(nullable: false),
                        EnrolledById = c.Guid(nullable: false),
                        EnrolledForLunchId = c.Guid(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.EnrolledById, cascadeDelete: true)
                .ForeignKey("dbo.Lunches", t => t.EnrolledForLunchId, cascadeDelete: true)
                .Index(t => t.EnrolledById)
                .Index(t => t.EnrolledForLunchId);
            
            CreateTable(
                "dbo.Lunches",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LunchDate = c.DateTime(nullable: false),
                        CookedMealId = c.Guid(nullable: false),
                        NumberOfPortions = c.Int(nullable: false),
                        NumberOfEnrollments = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Meals", t => t.CookedMealId, cascadeDelete: true)
                .Index(t => t.CookedMealId);
            
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Ingredients = c.String(),
                        ImageName = c.String(),
                        AverageRating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MealRatings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RatedById = c.Guid(nullable: false),
                        RatedMealId = c.Guid(nullable: false),
                        Rating = c.Double(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.RatedById, cascadeDelete: true)
                .ForeignKey("dbo.Meals", t => t.RatedMealId, cascadeDelete: true)
                .Index(t => t.RatedById)
                .Index(t => t.RatedMealId);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Text = c.String(),
                        IsRepeatable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.MealRatings", new[] { "RatedMealId" });
            DropIndex("dbo.MealRatings", new[] { "RatedById" });
            DropIndex("dbo.Lunches", new[] { "CookedMealId" });
            DropIndex("dbo.Enrollments", new[] { "EnrolledForLunchId" });
            DropIndex("dbo.Enrollments", new[] { "EnrolledById" });
            DropForeignKey("dbo.MealRatings", "RatedMealId", "dbo.Meals");
            DropForeignKey("dbo.MealRatings", "RatedById", "dbo.Users");
            DropForeignKey("dbo.Lunches", "CookedMealId", "dbo.Meals");
            DropForeignKey("dbo.Enrollments", "EnrolledForLunchId", "dbo.Lunches");
            DropForeignKey("dbo.Enrollments", "EnrolledById", "dbo.Users");
            DropTable("dbo.News");
            DropTable("dbo.MealRatings");
            DropTable("dbo.Meals");
            DropTable("dbo.Lunches");
            DropTable("dbo.Enrollments");
            DropTable("dbo.Users");
        }
    }
}
