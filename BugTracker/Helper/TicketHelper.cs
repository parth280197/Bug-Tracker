using BugTracker.Models;
using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace BugTracker.Helper
{
  public class TicketHelper
  {
    private ApplicationDbContext db;
    public TicketHelper(ApplicationDbContext db)
    {
      this.db = db;
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
  }
}