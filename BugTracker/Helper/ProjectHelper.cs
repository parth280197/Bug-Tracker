using BugTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.Helper
{
  public class ProjectHelper
  {
    ApplicationDbContext db;
    public ProjectHelper(ApplicationDbContext db)
    {
      this.db = db;
    }

    /// <summary>
    /// Gives all projects.
    /// </summary>
    /// <returns>List of projects</returns>
    public List<Project> GetAllProject()
    {
      var projects = db.Projects.ToList();
      return projects;
    }

    /// <summary>
    /// Gives the project for supplied projectId.
    /// </summary>
    /// <param name="projectId">Unique project Id for the project.</param>
    /// <returns>List of projects</returns>
    public Project GetProject(int projectId)
    {
      var project = db.Projects.Find(projectId);
      return project;
    }

    /// <summary>
    /// Gives all project in which specified user is involve.
    /// </summary>
    /// <param name="userId">of user for which projects to be searched</param>
    /// <returns>List of projects in which specified user involved.</returns>
    public List<Project> GetUserProjects(string userId)
    {
      var projects = db.Users.Find(userId).Projects.ToList();
      return projects;
    }

    /// <summary>
    /// Add new project.
    /// </summary>
    /// <param name="project">Project to be added.</param>
    public void AddProject(Project project)
    {
      db.Projects.Add(project);
      db.SaveChanges();
    }

    public void UpdateProject(Project project)
    {
      if (project != null)
      {
        var projectInDb = GetProject(project.Id);

        projectInDb.Name = project.Name;

        List<User> selectedUsers = project.Users.ToList();

        //Get all users to remove in removed list
        List<User> removedUsers = new List<User>();

        foreach (User user in projectInDb.Users)
        {
          if (!selectedUsers.Contains(user))
          {
            removedUsers.Add(user);
          }
        }

        //Remove it from project
        foreach (var user in removedUsers)
        {
          projectInDb.Users.Remove(user);
          db.SaveChanges();
        }

        //add all new user selected from view model
        foreach (var user in selectedUsers)
        {
          if (!projectInDb.Users.Contains(user))
          {
            projectInDb.Users.Add(user);
          }
        }
        db.SaveChanges();
      }
    }
    /// <summary>
    /// Gives all usersId of users involved in specified project.
    /// </summary>
    /// <param name="projectId">unique projectId for the project</param>
    /// <returns>Array of the string containing userIds.</returns>
    public string[] GetProjectUserIds(int projectId)
    {
      var project = GetProject(projectId);
      string[] userIds = project.Users.Select(user => user.Id).ToArray();

      return userIds;
    }
  }
}