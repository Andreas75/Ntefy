namespace NtefyWeb.RequestContextMigrations
{
    using NtefyWeb.DAL.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NtefyWeb.DAL.RequestContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"RequestContextMigrations";
        }

        protected override void Seed(NtefyWeb.DAL.RequestContext context)
        {
            var user = new User
            {
                Email = "carlsson75@gmail.com",
                Country = "Se",
                UserName = "Andy"
            };
            context.Users.Add(user);
            context.SaveChanges();

            //var record = new Record
            //{
            //    Artist = "Jamie xx",
            //    Title = "In Colour"
            //};

            //context.Records.Add(record);
            //context.SaveChanges();

            //var recId = context.Records.FirstOrDefault().RecordId;
            //var uId = context.Users.FirstOrDefault().Id;
            //var request = new Request
            //{
            //    RecordId = recId,
            //    UserId = uId,
            //    RequestDate = DateTime.Now
            //};

            //context.Requests.Add(request);
            //context.SaveChanges();
        }
    }
}
