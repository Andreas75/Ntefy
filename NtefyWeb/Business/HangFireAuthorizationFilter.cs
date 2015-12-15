using Hangfire.Dashboard;
using NtefyWeb.DAL.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NtefyWeb.Business
{
    public class HangFireAuthorizationFilter : IAuthorizationFilter
    {
        public bool Authorize(IDictionary<string, object> owinEnvironment)
        {
            bool boolAuthorizeCurrentUserToAccessHangFireDashboard = false;

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {                
                var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId<string>(HttpContext.Current.User.Identity);
                var userRepo = new UserRepository();
                var userList = new List<string>();
                userList.Add(userId);
                var email = userRepo.GetUsersEmail(userList);
                if (email == "carlsson75@gmail.com")
                {
                    boolAuthorizeCurrentUserToAccessHangFireDashboard = true;
                }
            }

            return boolAuthorizeCurrentUserToAccessHangFireDashboard;
        }
    }
}