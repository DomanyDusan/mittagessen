using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mittagessen.Web.Models
{
    public class PasswordChangeModel
    {
        [DisplayName("Das alte Passwort")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [DisplayName("Neues Passwort")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DisplayName("Neues Passwort Bestätigung")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}