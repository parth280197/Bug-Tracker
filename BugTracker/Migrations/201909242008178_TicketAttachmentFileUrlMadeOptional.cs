namespace BugTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketAttachmentFileUrlMadeOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TicketAttachments", "FileUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TicketAttachments", "FileUrl", c => c.String(nullable: false));
        }
    }
}
