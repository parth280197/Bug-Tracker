using BugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace BugTracker.Helper
{
  public class UserHelper
  {
    private ApplicationDbContext _db;
    private RoleManager<IdentityRole> _roleManager;
    private UserManager<IdentityUser> _userManager;
    public UserHelper(ApplicationDbContext db)
    {
      this._db = db;
      _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
      _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_db));
    }

    /// <summary>
    /// Creates new Role in the database.
    /// </summary>
    /// <param name="roleName">Name to be added in roles table.</param>
    public void DefineNewRole(string roleName)
    {
      _roleManager.Create(new IdentityRole(roleName));
    }

    public bool CheckRole(string roleName)
    {
      return _roleManager.RoleExists(roleName);
    }

    /// <summary>
    /// Assign user to the specified role.
    /// </summary>
    /// <param name="userId">for User to be assigned to role.</param>
    /// <param name="roleName">for Role to be associated with user.</param>
    public void AssignRole(string userId, string roleName)
    {
      if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(roleName))
      {
        _userManager.AddToRole(userId, roleName);
      }
      else
      {
        throw new NullReferenceException("userId and roleName can't be null!");
      }
    }
  }
}