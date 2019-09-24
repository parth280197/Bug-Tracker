using BugTracker.Models;
using BugTracker.Models.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
  public class TicketController : Controller
  {
    ApplicationDbContext db;
    public TicketController()
    {
      db = new ApplicationDbContext();
    }
    // GET: Tickets
    public ActionResult Index()
    {
      return View();
    }
    public ActionResult Create()
    {
      TicketFormViewModel viewModel = new TicketFormViewModel()
      {
        Projects = new SelectList(db.Projects.ToList(), "Id", "Name"),
        TicketTypes = new SelectList(db.TicketTypes.ToList(), "Id", "Name"),
        TicketPriorities = new SelectList(db.TicketPriorities.ToList(), "Id", "Name"),
      };
      return View(viewModel);
    }

  }
}