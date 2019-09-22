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
    public List<Project> GetProject(int projectId)
    {
      var projects = db.Projects.Where(project => project.Id == projectId).ToList();
      return projects;
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


  }
}