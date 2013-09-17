using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Mittagessen.Web.Models
{
    public class LoginModel
    {
        [Required]
        [DisplayName("Benutzername oder E-Mail-Adresse")]
        public string LoginName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Passwort")]
        public string LoginPassword { get; set; }
        [DisplayName("Login merken")]
        [ScaffoldColumn(false)]
        public bool RememberLogin { get; set; }
    }
}