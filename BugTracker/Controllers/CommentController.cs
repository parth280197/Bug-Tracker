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
      ViewBag.TicketId = id;

      //only show list of comments if logged user really involved in the supplied ticket.
      if (ticketHelper.isUserExistInTicket(loggedInUser.Id, id))
      {
        ticketComments = ticketHelper.GetTicketFromId(id).TicketComments.ToList();
      }

      return View(ticketComments);
    }
    public ActionResult UserCommentList(int id)
    {
      User loggedInUser = userHelper.GetUserFromId(User.Identity.GetUserId());
      ViewBag.TicketId = id;
      var editableComments = loggedInUser.TicketComments.Where(comment => comment.TicketId == id).ToList();
      return View(editableComments);
    }
    [HttpGet]
    public ActionResult Create(int id)
    {
      ViewBag.TicketId = id;
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
        User loggedInUser = userHelper.GetUserFromId(User.Identity.GetUserId());
        if (ticketHelper.isUserExistInTicket(loggedInUser.Id, ticketComments.TicketId))
        {
          ticketComments.Created = DateTime.Now;
          db.TicketComments.Add(ticketComments);
          db.SaveChanges();
        }
      }
      return RedirectToAction("List", new { id = ticketComments.TicketId });

    }

    [HttpGet]
    public ActionResult Edit(int id)
    {
      ViewBag.TicketId = id;
      TicketComments comment = db.TicketComments.Find(id);
      TicketComments ticketComments = new TicketComments()
      {
        UserId = comment.UserId,
        Comment = comment.Comment,
        TicketId = comment.TicketId,
        Created = comment.Created,
      };
      return View(ticketComments);
    }

    [HttpPost]
    public ActionResult Edit(TicketComments ticketComments)
    {
      if (ModelState.IsValid)
      {
        User loggedInUser = userHelper.GetUserFromId(User.Identity.GetUserId());
        if (ticketHelper.isUserExistInTicket(loggedInUser.Id, ticketComments.TicketId))
        {
          TicketComments commentInDb = db.TicketComments.Find(ticketComments.Id);
          commentInDb.Comment = ticketComments.Comment;
          db.SaveChanges();
        }
      }
      return RedirectToAction("List", new { id = ticketComments.TicketId });

    }
    public ActionResult Delete(int id)
    {
      TicketComments commentInDb = db.TicketComments.Find(id);
      db.TicketComments.Remove(commentInDb);
      db.SaveChanges();
      return RedirectToAction("List", new { id = commentInDb.TicketId });

    }
    public ActionResult RedirectToTickets()
    {
      var role = userHelper.GetUserRole(User.Identity.GetUserId());
      if (role == "Admin" || role == "ProjectManager")
        return RedirectToAction("ListForAdminOrProjectManager", "Ticket");
      else
      {
        return RedirectToAction("ListForSubmitterOrDeveloper", "Ticket");
      }
    }
  }
}