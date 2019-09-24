using BugTracker.Helper;
using BugTracker.Models;
using BugTracker.Models.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
  public class TicketController : Controller
  {
    ApplicationDbContext db;
    UserHelper userHelper;
    TicketHelper ticketHelper;
    public TicketController()
    {
      db = new ApplicationDbContext();
      userHelper = new UserHelper(db);
      ticketHelper = new TicketHelper(db);
    }
    // GET: Tickets
    public ActionResult List()
    {
      var tickets = userHelper.GetUserFromId(User.Identity.GetUserId()).CreatedTickets.ToList();
      return View(tickets);
    }
    [Authorize(Roles = "Submitter")]
    [HttpGet]
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

    [HttpPost]
    public ActionResult Create(TicketFormViewModel viewModel)
    {
      Ticket ticket = new Ticket()
      {
        Created = DateTime.Now,
        OwnersUser = userHelper.GetUserFromId(User.Identity.GetUserId()),
        ProjectId = viewModel.ProjectId,
        TicketPrioritiesId = viewModel.TicketPriorityId,
        TicketTypeId = viewModel.TicketTypeId,
        Title = viewModel.Title,
        Description = viewModel.Description,
      };

      int storedTicketId = ticketHelper.AddTicket(ticket);
      string attachmentPath = ticketHelper.saveFile(viewModel.File);

      TicketAttachments ticketAttachment = new TicketAttachments()
      {
        TicketId = storedTicketId,
        User = userHelper.GetUserFromId(User.Identity.GetUserId()),
        Created = DateTime.Now,
        FilePath = attachmentPath,
        Description = viewModel.Description,
      };

      ticketHelper.AddTicketAttachment(ticketAttachment);


      return RedirectToAction("Index");
    }

  }
}