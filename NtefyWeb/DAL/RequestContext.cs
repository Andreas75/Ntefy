using Microsoft.AspNet.Identity.EntityFramework;
using NtefyWeb.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace NtefyWeb.DAL
{
    public class RequestContext : IdentityDbContext<User>
    {
        public RequestContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer<RequestContext>(new CreateDatabaseIfNotExists<RequestContext>());
        }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Record> Records { get; set; }
        


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public static RequestContext Create()
        {
            return new RequestContext();
        }
    }
}