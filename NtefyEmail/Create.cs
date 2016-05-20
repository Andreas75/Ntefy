using NtefySpotify.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorEngine;
using Postal;
using System.Web.Mvc;
using System.Web.Hosting;
using NtefyEmail.Models;
using System.Net.Mail;
using System.Windows.Forms;
using System.Windows;

namespace NtefyEmail
{
    public static class Create
    {
        public static void CreateEmail(FullAlbum album, string recipitans)
        {
            try
            {
                var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
                var engines = new ViewEngineCollection();
                engines.Add(new FileSystemRazorViewEngine(viewsPath));

                var emailService = new EmailService(engines, createSmtpClient);
                //var emailService = new EmailService(engines);

                var email = new RequestEmail
                {
                    Artist = album.Artists.First().Name,
                    Title = album.Name,
                    Url = album.ExternalUrls["spotify"],
                    ImageUrl = album.Images.Last().Url,
                    Recipitans = recipitans,
                    From = "noteify@a-carlsson.se"
                };

                emailService.Send(email);
            }
            catch(Exception ex)
            {
                ex.Log().Display();
            }
            
        }

        internal static Exception Log(this Exception ex)
        {
            File.AppendAllText("CaughtExceptions" + DateTime.Now.ToString("yyyy-MM-dd") + ".log", DateTime.Now.ToString("HH:mm:ss") + ": " + ex.Message + "\n" + ex.ToString() + "\n");
            return ex;
        }

        internal static Exception Display(this Exception ex, string msg = null, MessageBoxIcon img = MessageBoxIcon.Error)
        {
            MessageBox.Show(msg ?? ex.Message, "", MessageBoxButtons.OK, img);
            return ex;
        }

        //public static void CreateTestEmail(string recipitans)
        //{
        //    var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
        //    var engines = new ViewEngineCollection();
        //    engines.Add(new FileSystemRazorViewEngine(viewsPath));
        //    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("azure_d3e3afce0847f68461c600f355688b5e@azure.com", "7N2Hly0r2uQhY5C");
        //    //SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
        //    //smtpClient.Credentials = credentials;
        //    SmtpClient smtpClient = new SmtpClient();
        //    var emailService = new EmailService(engines, createSmtpClient);

        //    var email = new RequestEmail
        //    {
        //        Artist = "Suede",
        //        Title = "Bloodsports",
        //        Url = "www.suede.com",
        //        //ImageUrl = album.Images.First().Url,
        //        Recipitans = recipitans
        //    };

        //    emailService.Send(email);
        //}

        public static SmtpClient createSmtpClient()
        {
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("noteify@a-carlsson.se", "Luftapa*12");
            SmtpClient smtpClient = new SmtpClient("smtp.a-carlsson.se", Convert.ToInt32(25));
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = false;
            smtpClient.Credentials = credentials;
            return smtpClient;

            //var smtpClient = new SmtpClient();
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            //var emailPickupDirectory = HostingEnvironment.MapPath("~/MailDrop");
            //if (!Directory.Exists(emailPickupDirectory))
            //{
            //    Directory.CreateDirectory(emailPickupDirectory);
            //}
            //smtpClient.PickupDirectoryLocation = emailPickupDirectory;
            //return smtpClient;
        }
    }
}
