using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Mittagessen.Web.Infrastructure
{
    public class Authentication : IAuthentication
    {
        public void SaveAuthentication(string userName, bool persist)
        {
            FormsAuthentication.SetAuthCookie(userName, false);
        }

        public void RemoveAuthentication()
        {
            FormsAuthentication.SignOut();
        }
    }
}