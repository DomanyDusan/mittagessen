using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Configuration;
using System.ComponentModel;

namespace Mittagessen.Web.Models
{
    public class RegistrationModel
    {
        [DisplayName("Das geheime Kennwort für Registrierung")]
        [DataType(DataType.Password)]
        public string RegistrationPassword { get; set; }
        [DisplayName("Benutzername")]
        [Remote("UserNameExists", "Account", "", ErrorMessage = "Der Benutzername wird schon benutzt")]
        public string RegistrationName { get; set; }
        [DisplayName("E-Mail-Adresse")]
        public string Email { get; set; }
        [DisplayName("Neues Passwort")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DisplayName("Neues Passwort Bestätigung")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}