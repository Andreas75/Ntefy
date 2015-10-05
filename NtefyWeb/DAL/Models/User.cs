using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace NtefyWeb.DAL.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Country { get; set; }

        public ICollection<Request> Requests { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class Request
    {
        [Key]     
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }        
        public Guid RecordId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? FillDate { get; set; }
        
        public virtual Record Record { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }

    public class Record
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RecordId { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}