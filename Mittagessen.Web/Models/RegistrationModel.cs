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
        public RegistrationModel()
        {
            CommonRegistrationPassword = ConfigurationManager.AppSettings["UserPassword"];
        }

        [DataType(DataType.Password)]
        [Compare("CommonRegistrationPassword")]
        public string RegistrationPassword { get; set; }
        [Required]
        public string RegistrationName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }

        [ScaffoldColumn(false)]
        public string CommonRegistrationPassword { get; set; }
    }
}