using System.Web.Mvc;

namespace BugTracker.Controllers
{
  public class TicketController : Controller
  {
    // GET: Tickets
    public ActionResult Index()
    {
      return View();
    }
    public ActionResult Create()
    {
      return View();
    }

  }
}