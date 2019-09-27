using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BugTracker.Models
{
  // You can add profile data for the user by adding more properties to your User class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
  public class User : IdentityUser
  {
    [InverseProperty("AssignedToUser")]
    public virtual ICollection<Ticket> AssignedTickets { get; set; }
    [InverseProperty("OwnersUser")]
    public virtual ICollection<Ticket> CreatedTickets { get; set; }
    public virtual ICollection<TicketAttachments> TicketAttachments { get; set; }
    public virtual ICollection<TicketComments> TicketComments { get; set; }
    public virtual ICollection<TicketHistories> TicketHistories { get; set; }
    public virtual ICollection<TicketNotificatioin> TicketNotificatioins { get; set; }
    public virtual ICollection<Project> Projects { get; set; }
    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
    {
      // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
      var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
      // Add custom user claims here
      return userIdentity;
    }
  }

  public class Ticket
  {
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    [DisplayFormat(DataFormatString = "{0:d MMM yyyy}")]
    public DateTime Created { get; set; }
    [DisplayFormat(DataFormatString = "{0:d MMM yyyy}")]
    public DateTime? Updated { get; set; }
    [Required]
    public int ProjectId { get; set; }
    public virtual Project Project { get; set; }
    [Required]
    public int TicketTypeId { get; set; }
    public virtual TicketType TicketType { get; set; }
    [Required]
    public int TicketPrioritiesId { get; set; }
    public virtual TicketPriorities TicketPriorities { get; set; }
    public int? TicketStatusId { get; set; }
    public virtual TicketStatuses TicketStatus { get; set; }
    public string OwnerUserId { get; set; }
    [ForeignKey("OwnerUserId")]
    public virtual User OwnersUser { get; set; }
    public string AssignedToUserId { get; set; }
    [ForeignKey("AssignedToUserId")]
    public virtual User AssignedToUser { get; set; }


    public virtual ICollection<TicketAttachments> TicketAttachments { get; set; }
    public virtual ICollection<TicketComments> TicketComments { get; set; }
    public virtual ICollection<TicketHistories> TicketHistories { get; set; }
    public virtual ICollection<TicketNotificatioin> TicketNotificatioins { get; set; }
  }

  public class TicketStatuses
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
  }

  public class TicketPriorities
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
  }

  public class TicketType
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
  }
  public class TicketAttachments
  {
    public int Id { get; set; }
    [Required]
    public int TicketId { get; set; }
    public virtual Ticket Ticket { get; set; }
    [Required]
    public string FilePath { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public DateTime Created { get; set; }
    [Required]
    public string UserId { get; set; }
    public virtual User User { get; set; }
    public string FileUrl { get; set; }
  }
  public class TicketComments
  {
    public int Id { get; set; }
    [Required]
    public string Comment { get; set; }
    [Required]
    public DateTime Created { get; set; }
    [Required]
    public int TicketId { get; set; }
    public Ticket Ticket { get; set; }
    [Required]
    public string UserId { get; set; }
    public User User { get; set; }
  }
  public class TicketHistories
  {
    public int Id { get; set; }
    [Required]
    public int TicketId { get; set; }
    public Ticket Ticket { get; set; }
    [Required]
    public string Property { get; set; }
    [Required]
    public string OldValue { get; set; }
    [Required]
    public string NewValue { get; set; }
    [Required]
    public DateTime Changed { get; set; }
    [Required]
    public string UserId { get; set; }
    public User User { get; set; }
  }
  public class TicketNotificatioin
  {
    public int Id { get; set; }
    [Required]
    public int TicketId { get; set; }
    public Ticket Ticket { get; set; }
    [Required]
    public int UserId { get; set; }
    public User User { get; set; }
  }
  public class Project
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    //relations
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; }
  }

  public class ApplicationDbContext : IdentityDbContext<User>
  {
    //relations
    public DbSet<Project> Projects { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketAttachments> TicketAttachments { get; set; }
    public DbSet<TicketComments> TicketComments { get; set; }
    public DbSet<TicketHistories> TicketHistories { get; set; }
    public DbSet<TicketNotificatioin> TicketNotificatioins { get; set; }
    public DbSet<TicketPriorities> TicketPriorities { get; set; }
    public DbSet<TicketStatuses> TicketStatuses { get; set; }
    public DbSet<TicketType> TicketTypes { get; set; }

    public ApplicationDbContext()
        : base("DefaultConnection", throwIfV1Schema: false)
    {
    }

    public static ApplicationDbContext Create()
    {
      return new ApplicationDbContext();
    }

  }
}