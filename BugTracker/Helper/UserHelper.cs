using BugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace BugTracker.Helper
{
  public class UserHelper
  {
    ApplicationDbContext db;
    private RoleManager<IdentityRole> _roleManager;
    private UserManager<User> _userManager;
    public UserHelper(ApplicationDbContext db)
    {
      this.db = db;
      _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
      _userManager = new UserManager<User>(new UserStore<User>(db));
    }

    /// <summary>
    /// Creates new Role in the database.
    /// </summary>
    /// <param name="roleName">Name to be added in roles table.</param>
    public void DefineNewRole(string roleName)
    {
      _roleManager.Create(new IdentityRole(roleName));
    }

    /// <summary>
    /// Get role name from role Id.
    /// </summary>
    /// <param name="roleId">to get corresponding role name.</param>
    /// <returns></returns>
    public string GetRole(string roleId)
    {
      return _roleManager.FindById(roleId).Name;
    }

    /// <summary>
    /// Check if specified roleName exist or not.
    /// </summary>
    /// <param name="roleName">to be checked for.</param>
    /// <returns></returns>
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