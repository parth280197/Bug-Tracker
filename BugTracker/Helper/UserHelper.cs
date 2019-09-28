using BugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

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

    /// <summary>
    /// Gives list of users from the array of string containing userIds.
    /// </summary>
    /// <param name="userIds">Array of string containing userIds.</param>
    /// <returns>List of Users.</returns>
    public List<User> GetAllUsersFromIds(string[] userIds)
    {
      var users = new List<User>();
      if (userIds.Length != 0)
      {
        foreach (string id in userIds)
        {
          users.Add(db.Users.Find(id));
        }
      }
      return users;
    }

    /// <summary>
    /// Gives User object who have the same specified userId.
    /// </summary>
    /// <param name="userId">for User to be fetched.</param>
    /// <returns>User object as per specified userId.</returns>
    public User GetUserFromId(string userId)
    {
      User user = new User();
      if (!string.IsNullOrEmpty(userId))
      {
        user = db.Users.Find(userId);
      }
      return user;
    }

    /// <summary>
    /// Get user role.
    /// </summary>
    /// <param name="userId">to fetch userRole for specific user.</param>
    /// <returns>user role as string.</returns>
    public string GetUserRole(string userId)
    {
      string userRole = _userManager.GetRoles(userId).ToList().First();
      return userRole;
    }

    /// <summary>
    /// Gives List of users who have specified role.
    /// </summary>
    /// <param name="roleName">To get all user in perticuar role.</param>
    /// <returns>List of users which have specified role.</returns>
    public List<User> GetUsersFromRole(string roleName)
    {
      var users = db.Users.ToList();
      List<User> result = new List<User>();
      foreach (var user in users)
      {
        if (_userManager.IsInRole(user.Id, roleName))
        {
          result.Add(user);
        }
      }
      return result;
    }
  }
}