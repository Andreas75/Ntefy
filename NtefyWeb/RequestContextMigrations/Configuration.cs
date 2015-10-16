namespace NtefyWeb.RequestContextMigrations
{
    using NtefyWeb.DAL;
    using NtefyWeb.DAL.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RequestContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"RequestContextMigrations";
        }

        protected override void Seed(RequestContext context)
        {
            var user1 = new User
            {
                Email = "carlsson75@gmail.com",
                Country = "Se",
                UserName = "Andy"
            };
           
            context.Users.Add(user1);
            context.SaveChanges();

            var user2 = new User
            {
                Email = "andreas.carlsson@hiq.se",
                Country = "Se",
                UserName = "Andreas"
            };

            context.Users.Add(user2);
            context.SaveChanges();

            var user3 = new User
            {
                Email = "andeca_75@hotmail.com",
                Country = "Se",
                UserName = "Andeca"
            };

            context.Users.Add(user3);
            context.SaveChanges();

            var jamieXX = new Record
            {
                Artist = "Jamie xx",
                Title = "In Colour"
            };

            context.Records.Add(jamieXX);
            context.SaveChanges();

            var deafHeaven = new Record
            {
                Artist = "Deafheaven",
                Title = "New Bermuda"
            };

            context.Records.Add(deafHeaven);
            context.SaveChanges();

            var lana = new Record
            {
                Artist = "Lana Del Rey",
                Title = "Honeymoon"
            };

            context.Records.Add(lana);
            context.SaveChanges();

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
