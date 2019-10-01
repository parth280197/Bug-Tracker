using System.ComponentModel.DataAnnotations;
using System.Web;

namespace BugTracker.Models.ViewModel
{
  public class AttachmentFormViewModel
  {
    [Required]
    public int Id { get; set; }
    [Required]
    public int TicketId { get; set; }
    [Required]
    public string Description { get; set; }
    [Display(Name = "Upload file")]
    public string FilePath { get; set; }
    public HttpPostedFileBase File { get; set; }
  }
}