using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Configuration;

namespace Mittagessen.Web.Models
{
    public class RegistrationModel
    {
        [DataType(DataType.Password)]
        public string RegistrationPassword { get; set; }
        [Remote("UserNameExists", "Account", "", ErrorMessage = "Der Benutzername wird schon benutzt")]
        public string RegistrationName { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}