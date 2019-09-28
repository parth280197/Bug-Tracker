using BugTracker.Models;
using BugTracker.Models.ViewModel;
using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace BugTracker.Helper
{
  public class TicketHelper
  {
    private ApplicationDbContext db;
    private UserHelper userHelper;
    public TicketHelper(ApplicationDbContext db)
    {
      this.db = db;
      userHelper = new UserHelper(db);
    }

    /// <summary>
    /// Add new Ticket in database.
    /// </summary>
    /// <param name="ticket">Ticket object to be added in database.</param>
    /// <returns>Stored ticketId</returns>
    public int AddTicket(Ticket ticket)
    {
      if (ticket != null)
      {
        db.Tickets.Add(ticket);
        db.SaveChanges();
      }
      return ticket.Id;
    }

    /// <summary>
    /// Add ticket attachment in database.
    /// </summary>
    /// <param name="ticketAttachment">Ticket attachment to be added in database.</param>
    public void AddTicketAttachment(TicketAttachments ticketAttachment)
    {
      if (ticketAttachment != null)
      {
        db.TicketAttachments.Add(ticketAttachment);
        db.SaveChanges();
      }
    }

    /// <summary>
    /// Save the file locally on server and return the store path.
    /// </summary>
    /// <param name="file">File to be stored on local server.</param>
    /// <returns>stored file path.</returns>
    public string saveFile(HttpPostedFileBase file)
    {
      string fileName = Path.GetFileNameWithoutExtension(file.FileName);
      string FileExtension = Path.GetExtension(file.FileName);
      fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + fileName.Trim() + FileExtension;
      string UploadPath = ConfigurationManager.AppSettings["TicketAttachmentPath"].ToString();
      string filePath = UploadPath + fileName;
      file.SaveAs(filePath);
      return filePath;
    }

    /// <summary>
    /// Gives ticket for specified ticketId.
    /// </summary>
    /// <param name="ticketId">for Ticket to be return.</param>
    /// <returns>Ticket object</returns>
    public Ticket GetTicketFromId(int ticketId)
    {
      Ticket ticket = db.Tickets.Find(ticketId);
      return ticket;
    }

    public void SubmitterUpdateTicket(TicketEditFormViewModel viewModel)
    {
      DateTime updateTime = DateTime.Now;
      Ticket ticketInDb = GetTicketFromId(viewModel.Id);
      ticketInDb.Updated = updateTime;
      if (ticketInDb.Title != viewModel.Title)
      {
        AddTicketHistory("Title", ticketInDb, viewModel, ticketInDb.OwnerUserId);
        ticketInDb.Title = viewModel.Title;
      }

      if (ticketInDb.Description != viewModel.Description)
      {
        AddTicketHistory("Description", ticketInDb, viewModel, ticketInDb.OwnerUserId);
        ticketInDb.Description = viewModel.Description;
      }

      if (ticketInDb.TicketPrioritiesId != viewModel.TicketPrioritiesId)
      {
        AddTicketHistory("TicketPrioritiesId", ticketInDb, viewModel, ticketInDb.OwnerUserId);
        ticketInDb.TicketPrioritiesId = viewModel.TicketPrioritiesId;
      }

      if (ticketInDb.ProjectId != viewModel.ProjectId)
      {
        AddTicketHistory("ProjectId", ticketInDb, viewModel, ticketInDb.OwnerUserId);
        ticketInDb.ProjectId = viewModel.ProjectId;
      }

      if (ticketInDb.ProjectId != viewModel.ProjectId)
      {
        AddTicketHistory("TicketTypeId", ticketInDb, viewModel, ticketInDb.OwnerUserId);
        ticketInDb.TicketTypeId = viewModel.TicketTypeId;
      }
      db.SaveChanges();
    }

    public void AddTicketHistory(string editedProperty, Ticket ticket, TicketEditFormViewModel viewModel, string userId)
    {
      var x = viewModel.GetType().GetProperty(editedProperty).GetValue(viewModel).ToString();
      ticket.TicketHistories.Add(new TicketHistories()
      {
        Changed = ticket.Updated.Value,
        TicketId = ticket.Id,
        OldValue = ticket.GetType().GetProperty(editedProperty).GetValue(ticket).ToString(),
        NewValue = viewModel.GetType().GetProperty(editedProperty).GetValue(viewModel).ToString(),
        Property = editedProperty,
        UserId = userId,
      });
    }

    public void UpdateStatus(int ticketId, int statusId, string userId)
    {
      Ticket ticket = GetTicketFromId(ticketId);
      string oldValue = ticket.TicketStatusId.ToString() == "" ? "N/A" : ticket.TicketStatusId.ToString();
      ticket.TicketStatusId = statusId;
      ticket.TicketHistories.Add(new TicketHistories()
      {
        Changed = DateTime.Now,
        NewValue = statusId.ToString(),
        Property = "TicketStatusId",
        OldValue = oldValue,
        TicketId = ticketId,
        UserId = userId,
      });
      db.SaveChanges();
    }

    public void AssignUser(AssignUserViewModel viewModel)
    {
    }
  }
}