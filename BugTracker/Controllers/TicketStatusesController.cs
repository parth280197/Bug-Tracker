using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;

namespace BugTracker.Controllers
{
    public class TicketStatusesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketStatuses
        public ActionResult Index()
        {
            return View(db.TicketStatuses.ToList());
        }

        // GET: TicketStatuses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketStatuses ticketStatuses = db.TicketStatuses.Find(id);
            if (ticketStatuses == null)
            {
                return HttpNotFound();
            }
            return View(ticketStatuses);
        }

        // GET: TicketStatuses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketStatuses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] TicketStatuses ticketStatuses)
        {
            if (ModelState.IsValid)
            {
                db.TicketStatuses.Add(ticketStatuses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticketStatuses);
        }

        // GET: TicketStatuses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketStatuses ticketStatuses = db.TicketStatuses.Find(id);
            if (ticketStatuses == null)
            {
                return HttpNotFound();
            }
            return View(ticketStatuses);
        }

        // POST: TicketStatuses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] TicketStatuses ticketStatuses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketStatuses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticketStatuses);
        }

        // GET: TicketStatuses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketStatuses ticketStatuses = db.TicketStatuses.Find(id);
            if (ticketStatuses == null)
            {
                return HttpNotFound();
            }
            return View(ticketStatuses);
        }

        // POST: TicketStatuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketStatuses ticketStatuses = db.TicketStatuses.Find(id);
            db.TicketStatuses.Remove(ticketStatuses);
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
