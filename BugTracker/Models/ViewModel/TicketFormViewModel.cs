using System.Web;
using System.Web.Mvc;

namespace BugTracker.Models.ViewModel
{
  public class TicketFormViewModel
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public int ProjectId { get; set; }
    public SelectList Projects { get; set; }

    public int TicketTypeId { get; set; }
    public SelectList TicketTypes { get; set; }

    public int TicketPriorityId { get; set; }
    public SelectList TicketPriorities { get; set; }

    public string FilePath { get; set; }
    public HttpPostedFile File { get; set; }
  }
}
