using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BugTracker.Models.ViewModel
{
  public class TicketEditFormViewModel
  {
    [Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required(ErrorMessage = "Please select project.")]
    [Display(Name = "Select Project")]
    public int ProjectId { get; set; }
    public SelectList Projects { get; set; }

    [Required(ErrorMessage = "Please select ticket type.")]
    [Display(Name = "Select ticket type")]
    public int TicketTypeId { get; set; }
    public SelectList TicketTypes { get; set; }

    [Required(ErrorMessage = "Please select priority.")]
    [Display(Name = "Select priority")]
    public int TicketPrioritiesId { get; set; }
    public SelectList TicketPriorities { get; set; }
  }
}