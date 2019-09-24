namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketTableUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "AssignedToUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "OwnerUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Tickets", new[] { "OwnerUserId" });
            DropIndex("dbo.Tickets", new[] { "AssignedToUserId" });
            AlterColumn("dbo.Tickets", "OwnerUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Tickets", "AssignedToUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Tickets", "OwnerUserId");
            CreateIndex("dbo.Tickets", "AssignedToUserId");
            AddForeignKey("dbo.Tickets", "AssignedToUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Tickets", "OwnerUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "OwnerUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "AssignedToUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Tickets", new[] { "AssignedToUserId" });
            DropIndex("dbo.Tickets", new[] { "OwnerUserId" });
            AlterColumn("dbo.Tickets", "AssignedToUserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Tickets", "OwnerUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Tickets", "AssignedToUserId");
            CreateIndex("dbo.Tickets", "OwnerUserId");
            AddForeignKey("dbo.Tickets", "OwnerUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Tickets", "AssignedToUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
