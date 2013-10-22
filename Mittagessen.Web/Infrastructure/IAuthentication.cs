using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mittagessen.Web.Infrastructure
{
    public interface IAuthentication
    {
        void SaveAuthentication(string userName, bool persist);
        void RemoveAuthentication();
    }
}
