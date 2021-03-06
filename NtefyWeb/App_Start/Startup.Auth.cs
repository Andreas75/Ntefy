﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin.Security.Providers.Instagram;
using Owin.Security.Providers.Spotify;
using Owin.Security.Providers.Reddit;
using Owin.Security.Providers.Flickr;
using Owin;
using NtefyWeb.Models;
using NtefyWeb.DAL.Models;
using NtefyWeb.DAL;
using System.Web.Configuration;

namespace NtefyWeb
{
    public partial class Startup
    {    

       
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(RequestContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, User>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });

           

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
           

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            app.UseFacebookAuthentication(               
               appId: WebConfigurationManager.AppSettings["FacebookAuthId"],
               appSecret: WebConfigurationManager.AppSettings["FacebookAuthSecret"]);            

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = WebConfigurationManager.AppSettings["GoogleAuthId"],
                ClientSecret = WebConfigurationManager.AppSettings["GoogleAuthSecret"]
            });

            app.UseInstagramInAuthentication(
                clientId: WebConfigurationManager.AppSettings["InstagramAuthId"],
                clientSecret: WebConfigurationManager.AppSettings["InstagramAuthSecret"]);

            app.UseSpotifyAuthentication(
                clientId: WebConfigurationManager.AppSettings["SpotifyAuthId"],
                clientSecret: WebConfigurationManager.AppSettings["SpotifyAuthSecret"]);

            

            //var spotifyOptions = new SpotifyAuthenticationOptions()
            //{
            //    ClientId = WebConfigurationManager.AppSettings["SpotifyAuthId"],
            //    ClientSecret = WebConfigurationManager.AppSettings["SpotifyAuthSecret"]
            //};
            //spotifyOptions.Scope.Add("DefaultUserName");
            //app.UseSpotifyAuthentication(spotifyOptions);

            app.UseRedditAuthentication(
                clientId: WebConfigurationManager.AppSettings["RedditAuthId"],
                clientSecret: WebConfigurationManager.AppSettings["RedditAuthSecret"]);

            app.UseFlickrAuthentication(
                appKey: WebConfigurationManager.AppSettings["FlickrAuthId"],
                appSecret: WebConfigurationManager.AppSettings["FlickrAuthSecret"]);
            
        }
    }
}