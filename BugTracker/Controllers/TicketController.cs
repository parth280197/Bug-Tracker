using BugTracker.Helper;
using BugTracker.Models;
using BugTracker.Models.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
  [Authorize]
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
    [Authorize(Roles = "Submitter,Developer")]
    public ActionResult ListForSubmitterOrDeveloper()
    {
      string userId = User.Identity.GetUserId();
      string userRole = userHelper.GetUserRole(userId);
      List<Ticket> tickets = new List<Ticket>();
      if (userRole == "Submitter")
      {
        tickets = userHelper.GetUserFromId(userId).CreatedTickets.ToList();
      }
      else
      {
        tickets = userHelper.GetUserFromId(userId).AssignedTickets.ToList();
      }

      return View(tickets);
    }

    [Authorize(Roles = "Admin,ProjectManager")]
    public ActionResult ListForAdminOrProjectManager()
    {
      string userId = User.Identity.GetUserId();
      string userRole = userHelper.GetUserRole(userId);
      List<Ticket> tickets = new List<Ticket>();
      if (userRole == "ProjectManager")
      {
        var projects = userHelper.GetUserFromId(userId).Projects.ToList();
        foreach (var project in projects)
        {
          tickets.AddRange(project.Tickets);
        }
      }
      else
      {
        tickets = db.Tickets.ToList();
      }

      return View(tickets);
    }

    [Authorize(Roles = "Submitter")]
    [HttpGet]
    public ActionResult Create()
    {
      ViewBag.Action = "Create";
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

      if (viewModel.File != null)
      {
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
      }

      return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult Edit(int id)
    {
      ViewBag.Action = "Edit";
      var user = userHelper.GetUserFromId(User.Identity.GetUserId());
      Ticket ticket = ticketHelper.GetTicketFromId(id);

      TicketEditFormViewModel viewModel = new TicketEditFormViewModel()
      {
        Title = ticket.Title,
        Description = ticket.Description,
        Projects = new SelectList(user.Projects.ToList(), "Id", "Name"),
        ProjectId = ticket.ProjectId,
        TicketTypes = new SelectList(db.TicketTypes.ToList(), "Id", "Name"),
        TicketTypeId = ticket.TicketTypeId,
        TicketPriorities = new SelectList(db.TicketPriorities.ToList(), "Id", "Name", ticket.TicketPrioritiesId.ToString()),
        TicketPriorityId = ticket.TicketPrioritiesId,
      };
      return View(viewModel);
    }

    [HttpPost]
    public ActionResult Edit(TicketEditFormViewModel viewModel)
    {
      ticketHelper.SubmitterUpdateTicket(viewModel);
      return RedirectToAction("List");
    }

  }
}