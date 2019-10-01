using BugTracker.Helper;
using BugTracker.Models;
using BugTracker.Models.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
  public class TicketAttachmentsController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();
    private TicketHelper ticketHelper;
    private UserHelper userHelper;

    public TicketAttachmentsController()
    {
      ticketHelper = new TicketHelper(db);
      userHelper = new UserHelper(db);
    }
    public ActionResult List(int id)
    {
      User loggedInUser = userHelper.GetUserFromId(User.Identity.GetUserId());
      ViewBag.TicketId = id;
      if (ticketHelper.isUserExistInTicket(loggedInUser.Id, id))
      {
        var ticketAttachments = ticketHelper.GetTicketFromId(id).TicketAttachments.ToList();
        return View(ticketAttachments);
      }
      return View();
    }

    public ActionResult Create(int id)
    {
      AttachmentFormViewModel viewModel = new AttachmentFormViewModel()
      {
        TicketId = id,
      };
      return View(viewModel);
    }

    // POST: TicketAttachments/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(AttachmentFormViewModel viewModel)
    {
      User loggedInUser = userHelper.GetUserFromId(User.Identity.GetUserId());
      if (ticketHelper.isUserExistInTicket(loggedInUser.Id, viewModel.TicketId))
      {
        TicketAttachments attachments = new TicketAttachments
        {
          TicketId = viewModel.TicketId,
          UserId = User.Identity.GetUserId(),
          FilePath = ticketHelper.saveFile(viewModel.File),
          Description = viewModel.Description,
          Created = DateTime.Now,
        };
        db.TicketAttachments.Add(attachments);
        db.SaveChanges();
      }
      return View();
    }

    public ActionResult Download(int id)
    {
      var path = db.TicketAttachments.Find(id).FilePath.ToString();
      var mime = MimeMapping.GetMimeMapping(path);
      if (true)
      {
        return File(db.TicketAttachments.Find(id).FilePath.ToString(), mime);
      }
      return View();
    }
    // GET: TicketAttachments/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      TicketAttachments ticketAttachments = db.TicketAttachments.Find(id);
      if (ticketAttachments == null)
      {
        return HttpNotFound();
      }
      ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketAttachments.TicketId);
      ViewBag.UserId = new SelectList(db.Users, "Id", "Email", ticketAttachments.UserId);
      return View(ticketAttachments);
    }

    // POST: TicketAttachments/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,TicketId,FilePath,Description,Created,UserId,FileUrl")] TicketAttachments ticketAttachments)
    {
      if (ModelState.IsValid)
      {
        db.Entry(ticketAttachments).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketAttachments.TicketId);
      ViewBag.UserId = new SelectList(db.Users, "Id", "Email", ticketAttachments.UserId);
      return View(ticketAttachments);
    }

    // GET: TicketAttachments/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      TicketAttachments ticketAttachments = db.TicketAttachments.Find(id);
      if (ticketAttachments == null)
      {
        return HttpNotFound();
      }
      return View(ticketAttachments);
    }

    // POST: TicketAttachments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      TicketAttachments ticketAttachments = db.TicketAttachments.Find(id);
      db.TicketAttachments.Remove(ticketAttachments);
      db.SaveChanges();
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
