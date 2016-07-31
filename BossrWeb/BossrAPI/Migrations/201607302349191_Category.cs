namespace BossrAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Creatures", "Category_Id", c => c.Int());
            CreateIndex("dbo.Creatures", "Category_Id");
            AddForeignKey("dbo.Creatures", "Category_Id", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Creatures", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Creatures", new[] { "Category_Id" });
            DropColumn("dbo.Creatures", "Category_Id");
            DropTable("dbo.Categories");
        }
    }
}
