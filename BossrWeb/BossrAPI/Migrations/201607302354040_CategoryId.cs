namespace BossrAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Creatures", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Creatures", new[] { "Category_Id" });
            RenameColumn(table: "dbo.Creatures", name: "Category_Id", newName: "CategoryId");
            AlterColumn("dbo.Creatures", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Creatures", "CategoryId");
            AddForeignKey("dbo.Creatures", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Creatures", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Creatures", new[] { "CategoryId" });
            AlterColumn("dbo.Creatures", "CategoryId", c => c.Int());
            RenameColumn(table: "dbo.Creatures", name: "CategoryId", newName: "Category_Id");
            CreateIndex("dbo.Creatures", "Category_Id");
            AddForeignKey("dbo.Creatures", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
