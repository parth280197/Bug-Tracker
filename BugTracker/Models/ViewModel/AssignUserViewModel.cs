using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BugTracker.Models.ViewModel
{
  public class AssignUserViewModel
  {
    public int Id { get; set; }
    public String Title { get; set; }
    public string Description { get; set; }
    [Display(Name = "Select user to assign task")]
    public IEnumerable<SelectListItem> UsersList { get; set; }

    [Required(ErrorMessage = "*Please select user.", AllowEmptyStrings = false)]
    public string SelectedId { get; set; }
  }
}