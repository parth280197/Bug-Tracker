using BugTracker.Helper;
using BugTracker.Models;
using BugTracker.Models.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace BugTracker.Controllers
{

  public class ProjectController : Controller
  {
    ApplicationDbContext db;
    UserHelper userHelper;
    ProjectHelper projectHelper;
    public ProjectController()
    {
      db = new ApplicationDbContext();
      userHelper = new UserHelper(db);
      projectHelper = new ProjectHelper(db);
    }
    // GET: Project
    public ActionResult List()
    {
      var projects = projectHelper.GetAllProject();
      return View(projects);
    }
    [Authorize(Roles = "Admin,ProjectManager")]
    public ActionResult Create()
    {
      ViewBag.Action = "Create";
      ProjectFormViewModel viewModel = new ProjectFormViewModel()
      {
        Project = new Project(),
        UsersList = new SelectList(db.Users.ToList(), "Id", "UserName")
      };

      return View("CreateOrUpdateForm", viewModel);
    }
    [Authorize(Roles = "Admin,ProjectManager")]
    public ActionResult Update(int id)
    {
      ViewBag.Action = "Update";
      ProjectFormViewModel viewModel = new ProjectFormViewModel()
      {
        Project = projectHelper.GetProject(id),
        UsersList = new SelectList(db.Users.ToList(), "Id", "UserName"),
        SelectedId = projectHelper.GetProjectUserIds(id)
      };

      return View("CreateOrUpdateForm", viewModel);
    }
    [Authorize(Roles = "Admin,ProjectManager")]
    [HttpPost]
    public ActionResult CreateOrUpdateForm(ProjectFormViewModel viewModel)
    {
      //if ProjectId is 0 means its new created project
      if (viewModel.Project.Id == 0)
      {
        Project project = new Project()
        {
          Name = viewModel.Project.Name,
          Users = userHelper.GetAllUsersFromIds(viewModel.SelectedId),
        };

        projectHelper.AddProject(project);
      }
      //Else project need to update
      else
      {
        Project project = new Project()
        {
          Id = viewModel.Project.Id,
          Name = viewModel.Project.Name,
          Users = userHelper.GetAllUsersFromIds(viewModel.SelectedId),
        };

        projectHelper.UpdateProject(project);
      }

      return RedirectToAction("List");
    }
  }
}