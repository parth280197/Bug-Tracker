namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketTableUpdatedRIcketStatusIdNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "TicketStatusId", "dbo.TicketStatuses");
            DropIndex("dbo.Tickets", new[] { "TicketStatusId" });
            AlterColumn("dbo.Tickets", "TicketStatusId", c => c.Int());
            CreateIndex("dbo.Tickets", "TicketStatusId");
            AddForeignKey("dbo.Tickets", "TicketStatusId", "dbo.TicketStatuses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TicketStatusId", "dbo.TicketStatuses");
            DropIndex("dbo.Tickets", new[] { "TicketStatusId" });
            AlterColumn("dbo.Tickets", "TicketStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "TicketStatusId");
            AddForeignKey("dbo.Tickets", "TicketStatusId", "dbo.TicketStatuses", "Id", cascadeDelete: true);
        }
    }
}
