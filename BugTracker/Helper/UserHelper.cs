using BugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BugTracker.Helper
{
  public class UserHelper
  {
    ApplicationDbContext _db;
    RoleManager<IdentityRole> roleManager;
    public UserHelper(ApplicationDbContext db)
    {
      this._db = db;
      roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
    }

    /// <summary>
    /// Creates new Role in the database.
    /// </summary>
    /// <param name="roleName">Name to be added in roles table.</param>
    public void DefineNewRole(string roleName)
    {
      roleManager.Create(new IdentityRole(roleName));
    }
  }
}