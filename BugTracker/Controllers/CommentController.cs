using BugTracker.Helper;
using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
  public class CommentController : Controller
  {
    ApplicationDbContext db;
    TicketHelper ticketHelper;
    UserHelper userHelper;
    public CommentController()
    {
      db = new ApplicationDbContext();
      userHelper = new UserHelper(db);
      ticketHelper = new TicketHelper(db);
    }
    // GET: Comment
    public ActionResult List(int id)
    {
      User loggedInUser = userHelper.GetUserFromId(User.Identity.GetUserId());
      List<TicketComments> ticketComments = new List<TicketComments>();
      if (ticketHelper.isUserExistInTicket(loggedInUser.Id, id))
      {
        ticketComments = ticketHelper.GetTicketFromId(id).TicketComments.ToList();
      }
      return View(ticketComments);
    }

    [HttpGet]
    public ActionResult Create(int id)
    {
      TicketComments ticketComments = new TicketComments()
      {
        UserId = User.Identity.GetUserId(),
        TicketId = id,
      };
      return View(ticketComments);
    }
    [HttpPost]
    public ActionResult Create(TicketComments ticketComments)
    {
      if (ModelState.IsValid)
      {
        ticketComments.Created = DateTime.Now;
        db.TicketComments.Add(ticketComments);
        db.SaveChanges();
      }
      return RedirectToAction("List", new { id = ticketComments.TicketId });

    }
  }
}