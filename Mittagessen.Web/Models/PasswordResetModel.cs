using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mittagessen.Web.Models
{
    public class PasswordResetModel
    {
        [HiddenInput(DisplayValue = false)]
        public string PasswordResetString { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Neues Passwort")]
        public string NewPassword { get; set; }
    }
}