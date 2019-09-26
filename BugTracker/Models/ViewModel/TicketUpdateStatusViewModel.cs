using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BugTracker.Models.ViewModel
{
  public class TicketUpdateStatusViewModel
  {
    [Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }
    [Required(ErrorMessage = "Please select ticket status.")]
    [Display(Name = "Select ticket status.")]
    public int TicketStatusId { get; set; }
    public SelectList TicketStatus { get; set; }
  }
}