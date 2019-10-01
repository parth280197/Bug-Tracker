using System.Web;

namespace BugTracker.Models.ViewModel
{
  public class AttachmentFormViewModel
  {
    public int Id { get; set; }
    public int TicketId { get; set; }
    public string Description { get; set; }
    public string FilePath { get; set; }
    public HttpPostedFileBase File { get; set; }
  }
}