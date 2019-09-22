using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BugTracker.Models.ViewModel
{
  public class ProjectFormViewModel
  {
    public Project Project { get; set; }
    [Display(Name = "Select users for the project.")]
    public IEnumerable<SelectListItem> UsersList { get; set; }

    [Required(ErrorMessage = "*Please select user.")]
    public string[] SelectedId { get; set; }
  }
}