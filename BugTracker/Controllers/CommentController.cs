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
    public CommentController()
    {
      db = new ApplicationDbContext();
      ticketHelper = new TicketHelper(db);
    }
    // GET: Comment
    public ActionResult List(int id)
    {
      //Additional filtering remaining to check wheather user itself is involved in the ticket or user 
      //tried to get access to data from url.
      Ticket ticket = ticketHelper.GetTicketFromId(id);
      List<TicketComments> ticketComments = ticket.TicketComments.ToList();
      ViewBag.TicketId = id;
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