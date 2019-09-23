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
    public class TicketPrioritiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketPriorities
        public ActionResult Index()
        {
            return View(db.TicketPriorities.ToList());
        }

        // GET: TicketPriorities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketPriorities ticketPriorities = db.TicketPriorities.Find(id);
            if (ticketPriorities == null)
            {
                return HttpNotFound();
            }
            return View(ticketPriorities);
        }

        // GET: TicketPriorities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketPriorities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] TicketPriorities ticketPriorities)
        {
            if (ModelState.IsValid)
            {
                db.TicketPriorities.Add(ticketPriorities);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticketPriorities);
        }

        // GET: TicketPriorities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketPriorities ticketPriorities = db.TicketPriorities.Find(id);
            if (ticketPriorities == null)
            {
                return HttpNotFound();
            }
            return View(ticketPriorities);
        }

        // POST: TicketPriorities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] TicketPriorities ticketPriorities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketPriorities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticketPriorities);
        }

        // GET: TicketPriorities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketPriorities ticketPriorities = db.TicketPriorities.Find(id);
            if (ticketPriorities == null)
            {
                return HttpNotFound();
            }
            return View(ticketPriorities);
        }

        // POST: TicketPriorities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketPriorities ticketPriorities = db.TicketPriorities.Find(id);
            db.TicketPriorities.Remove(ticketPriorities);
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
