namespace BugTracker.Migrations
{
  using BugTracker.Helper;
  using System.Data.Entity.Migrations;

  internal sealed class Configuration : DbMigrationsConfiguration<BugTracker.Models.ApplicationDbContext>
  {
    private UserHelper _userHelper;
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(BugTracker.Models.ApplicationDbContext context)
    {
      //  This method will be called after migrating to the latest version.

      //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
      //  to avoid creating duplicate seed data.
      _userHelper = new UserHelper(context);
      if (!_userHelper.CheckRole("Admin"))
      {
        _userHelper.DefineNewRole("Admin");
      }
      if (!_userHelper.CheckRole("ProjectManager"))
      {
        _userHelper.DefineNewRole("ProjectManager");
      }
      if (!_userHelper.CheckRole("Developer"))
      {
        _userHelper.DefineNewRole("Developer");
      }
      if (!_userHelper.CheckRole("Submitter"))
      {
        _userHelper.DefineNewRole("Submitter");
      }
    }
  }
}
