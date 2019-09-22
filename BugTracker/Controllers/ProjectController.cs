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
    public ActionResult Index()
    {
      return View();
    }
    public ActionResult CreateOrUpdateForm()
    {
      ProjectFormViewModel viewModel = new ProjectFormViewModel()
      {
        Project = new Project(),
        UsersList = new SelectList(db.Users.ToList(), "Id", "UserName")
      };
      return View(viewModel);
    }

    [HttpPost]
    public ActionResult CreateOrUpdateForm(ProjectFormViewModel viewModel)
    {
      Project project = new Project()
      {
        Name = viewModel.Project.Name,
        Users = userHelper.GetAllUsersFromIds(viewModel.SelectedId),
      };

      projectHelper.AddProject(project);
      return RedirectToAction("Index");
    }
  }
}