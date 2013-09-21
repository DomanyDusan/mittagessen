using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;

namespace Mittagessen.Web.Models
{
    public class UserModel
    {
        [HiddenInput(DisplayValue=false)]
        public Guid UserId { get; set; }
        [DisplayName("Benutzername")]
        public string UserName { get; set; }
        [DisplayName("E-Mail-Adresse")]
        public string Email { get; set; }
    }
}