using BugTracker.Models;
using BugTracker.Models.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
  public class ProjectController : Controller
  {
    ApplicationDbContext db;
    public ProjectController()
    {
      db = new ApplicationDbContext();
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
      return View(viewModel);
    }
  }
}