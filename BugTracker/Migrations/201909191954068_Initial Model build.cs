namespace BugTracker.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class InitialModelbuild : DbMigration
  {
    public override void Up()
    {
      CreateTable(
          "dbo.Projects",
          c => new
          {
            Id = c.Int(nullable: false, identity: true),
            Name = c.String(nullable: false),
          })
          .PrimaryKey(t => t.Id);

      CreateTable(
          "dbo.AspNetUsers",
          c => new
          {
            Id = c.String(nullable: false, maxLength: 128),
            Email = c.String(maxLength: 256),
            EmailConfirmed = c.Boolean(nullable: false),
            PasswordHash = c.String(),
            SecurityStamp = c.String(),
            PhoneNumber = c.String(),
            PhoneNumberConfirmed = c.Boolean(nullable: false),
            TwoFactorEnabled = c.Boolean(nullable: false),
            LockoutEndDateUtc = c.DateTime(),
            LockoutEnabled = c.Boolean(nullable: false),
            AccessFailedCount = c.Int(nullable: false),
            UserName = c.String(nullable: false, maxLength: 256),
          })
          .PrimaryKey(t => t.Id)
          .Index(t => t.UserName, unique: true, name: "UserNameIndex");

      CreateTable(
          "dbo.Tickets",
          c => new
          {
            Id = c.Int(nullable: false, identity: true),
            Title = c.String(nullable: false),
            Description = c.String(nullable: false),
            Created = c.DateTime(nullable: false),
            Updated = c.DateTime(),
            ProjectId = c.Int(nullable: false),
            TicketTypeId = c.Int(nullable: false),
            TicketPrioritiesId = c.Int(nullable: false),
            TicketStatusId = c.Int(nullable: false),
            OwnerUserId = c.String(nullable: false, maxLength: 128),
            AssignedToUserId = c.String(nullable: false, maxLength: 128),
          })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
          .ForeignKey("dbo.TicketPriorities", t => t.TicketPrioritiesId, cascadeDelete: true)
          .ForeignKey("dbo.TicketStatuses", t => t.TicketStatusId, cascadeDelete: true)
          .ForeignKey("dbo.TicketTypes", t => t.TicketTypeId, cascadeDelete: true)
          .ForeignKey("dbo.AspNetUsers", t => t.AssignedToUserId, cascadeDelete: false)
          .ForeignKey("dbo.AspNetUsers", t => t.OwnerUserId, cascadeDelete: false)
          .Index(t => t.ProjectId)
          .Index(t => t.TicketTypeId)
          .Index(t => t.TicketPrioritiesId)
          .Index(t => t.TicketStatusId)
          .Index(t => t.OwnerUserId)
          .Index(t => t.AssignedToUserId);

      CreateTable(
          "dbo.TicketPriorities",
          c => new
          {
            Id = c.Int(nullable: false, identity: true),
            Name = c.String(nullable: false),
          })
          .PrimaryKey(t => t.Id);

      CreateTable(
          "dbo.TicketStatuses",
          c => new
          {
            Id = c.Int(nullable: false, identity: true),
            Name = c.String(nullable: false),
          })
          .PrimaryKey(t => t.Id);

      CreateTable(
          "dbo.TicketTypes",
          c => new
          {
            Id = c.Int(nullable: false, identity: true),
            Name = c.String(nullable: false),
          })
          .PrimaryKey(t => t.Id);

      CreateTable(
          "dbo.AspNetUserClaims",
          c => new
          {
            Id = c.Int(nullable: false, identity: true),
            UserId = c.String(nullable: false, maxLength: 128),
            ClaimType = c.String(),
            ClaimValue = c.String(),
          })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
          .Index(t => t.UserId);

      CreateTable(
          "dbo.AspNetUserLogins",
          c => new
          {
            LoginProvider = c.String(nullable: false, maxLength: 128),
            ProviderKey = c.String(nullable: false, maxLength: 128),
            UserId = c.String(nullable: false, maxLength: 128),
          })
          .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
          .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
          .Index(t => t.UserId);

      CreateTable(
          "dbo.AspNetUserRoles",
          c => new
          {
            UserId = c.String(nullable: false, maxLength: 128),
            RoleId = c.String(nullable: false, maxLength: 128),
          })
          .PrimaryKey(t => new { t.UserId, t.RoleId })
          .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
          .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
          .Index(t => t.UserId)
          .Index(t => t.RoleId);

      CreateTable(
          "dbo.TicketAttachments",
          c => new
          {
            Id = c.Int(nullable: false, identity: true),
            TicketId = c.Int(nullable: false),
            FilePath = c.String(nullable: false),
            Description = c.String(nullable: false),
            Created = c.DateTime(nullable: false),
            UserId = c.String(nullable: false, maxLength: 128),
            FileUrl = c.String(nullable: false),
          })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
          .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
          .Index(t => t.TicketId)
          .Index(t => t.UserId);

      CreateTable(
          "dbo.TicketComments",
          c => new
          {
            Id = c.Int(nullable: false, identity: true),
            Comment = c.String(nullable: false),
            Created = c.DateTime(nullable: false),
            TicketId = c.Int(nullable: false),
            UserId = c.String(nullable: false, maxLength: 128),
          })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
          .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
          .Index(t => t.TicketId)
          .Index(t => t.UserId);

      CreateTable(
          "dbo.TicketHistories",
          c => new
          {
            Id = c.Int(nullable: false, identity: true),
            TicketId = c.Int(nullable: false),
            Property = c.String(nullable: false),
            OldValue = c.String(nullable: false),
            NewValue = c.String(nullable: false),
            Changed = c.DateTime(nullable: false),
            UserId = c.String(nullable: false, maxLength: 128),
          })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
          .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
          .Index(t => t.TicketId)
          .Index(t => t.UserId);

      CreateTable(
          "dbo.TicketNotificatioins",
          c => new
          {
            Id = c.Int(nullable: false, identity: true),
            TicketId = c.Int(nullable: false),
            UserId = c.Int(nullable: false),
            User_Id = c.String(maxLength: 128),
          })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
          .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
          .Index(t => t.TicketId)
          .Index(t => t.User_Id);

      CreateTable(
          "dbo.AspNetRoles",
          c => new
          {
            Id = c.String(nullable: false, maxLength: 128),
            Name = c.String(nullable: false, maxLength: 256),
          })
          .PrimaryKey(t => t.Id)
          .Index(t => t.Name, unique: true, name: "RoleNameIndex");

      CreateTable(
          "dbo.UserProjects",
          c => new
          {
            User_Id = c.String(nullable: false, maxLength: 128),
            Project_Id = c.Int(nullable: false),
          })
          .PrimaryKey(t => new { t.User_Id, t.Project_Id })
          .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
          .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
          .Index(t => t.User_Id)
          .Index(t => t.Project_Id);

    }

    public override void Down()
    {
      DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
      DropForeignKey("dbo.TicketNotificatioins", "User_Id", "dbo.AspNetUsers");
      DropForeignKey("dbo.TicketNotificatioins", "TicketId", "dbo.Tickets");
      DropForeignKey("dbo.TicketHistories", "UserId", "dbo.AspNetUsers");
      DropForeignKey("dbo.TicketHistories", "TicketId", "dbo.Tickets");
      DropForeignKey("dbo.TicketComments", "UserId", "dbo.AspNetUsers");
      DropForeignKey("dbo.TicketComments", "TicketId", "dbo.Tickets");
      DropForeignKey("dbo.TicketAttachments", "UserId", "dbo.AspNetUsers");
      DropForeignKey("dbo.TicketAttachments", "TicketId", "dbo.Tickets");
      DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
      DropForeignKey("dbo.UserProjects", "Project_Id", "dbo.Projects");
      DropForeignKey("dbo.UserProjects", "User_Id", "dbo.AspNetUsers");
      DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
      DropForeignKey("dbo.Tickets", "OwnerUserId", "dbo.AspNetUsers");
      DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
      DropForeignKey("dbo.Tickets", "AssignedToUserId", "dbo.AspNetUsers");
      DropForeignKey("dbo.Tickets", "TicketTypeId", "dbo.TicketTypes");
      DropForeignKey("dbo.Tickets", "TicketStatusId", "dbo.TicketStatuses");
      DropForeignKey("dbo.Tickets", "TicketPrioritiesId", "dbo.TicketPriorities");
      DropForeignKey("dbo.Tickets", "ProjectId", "dbo.Projects");
      DropIndex("dbo.UserProjects", new[] { "Project_Id" });
      DropIndex("dbo.UserProjects", new[] { "User_Id" });
      DropIndex("dbo.AspNetRoles", "RoleNameIndex");
      DropIndex("dbo.TicketNotificatioins", new[] { "User_Id" });
      DropIndex("dbo.TicketNotificatioins", new[] { "TicketId" });
      DropIndex("dbo.TicketHistories", new[] { "UserId" });
      DropIndex("dbo.TicketHistories", new[] { "TicketId" });
      DropIndex("dbo.TicketComments", new[] { "UserId" });
      DropIndex("dbo.TicketComments", new[] { "TicketId" });
      DropIndex("dbo.TicketAttachments", new[] { "UserId" });
      DropIndex("dbo.TicketAttachments", new[] { "TicketId" });
      DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
      DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
      DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
      DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
      DropIndex("dbo.Tickets", new[] { "AssignedToUserId" });
      DropIndex("dbo.Tickets", new[] { "OwnerUserId" });
      DropIndex("dbo.Tickets", new[] { "TicketStatusId" });
      DropIndex("dbo.Tickets", new[] { "TicketPrioritiesId" });
      DropIndex("dbo.Tickets", new[] { "TicketTypeId" });
      DropIndex("dbo.Tickets", new[] { "ProjectId" });
      DropIndex("dbo.AspNetUsers", "UserNameIndex");
      DropTable("dbo.UserProjects");
      DropTable("dbo.AspNetRoles");
      DropTable("dbo.TicketNotificatioins");
      DropTable("dbo.TicketHistories");
      DropTable("dbo.TicketComments");
      DropTable("dbo.TicketAttachments");
      DropTable("dbo.AspNetUserRoles");
      DropTable("dbo.AspNetUserLogins");
      DropTable("dbo.AspNetUserClaims");
      DropTable("dbo.TicketTypes");
      DropTable("dbo.TicketStatuses");
      DropTable("dbo.TicketPriorities");
      DropTable("dbo.Tickets");
      DropTable("dbo.AspNetUsers");
      DropTable("dbo.Projects");
    }
  }
}
