using BugTracker.Helper;
using BugTracker.Models;
using BugTracker.Models.ViewModel;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
  public class TicketController : Controller
  {
    ApplicationDbContext db;
    UserHelper userHelper;
    public TicketController()
    {
      db = new ApplicationDbContext();
      userHelper = new UserHelper(db);
    }
    // GET: Tickets
    public ActionResult Index()
    {
      return View();
    }
    [Authorize(Roles = "Submitter")]
    public ActionResult Create()
    {
      var user = userHelper.GetUserFromId(User.Identity.GetUserId());
      TicketFormViewModel viewModel = new TicketFormViewModel()
      {
        Projects = new SelectList(user.Projects.ToList(), "Id", "Name"),
        TicketTypes = new SelectList(db.TicketTypes.ToList(), "Id", "Name"),
        TicketPriorities = new SelectList(db.TicketPriorities.ToList(), "Id", "Name"),
      };
      return View(viewModel);
    }

  }
}