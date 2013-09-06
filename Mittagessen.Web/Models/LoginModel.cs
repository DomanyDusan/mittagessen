using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mittagessen.Web.Models
{
    public class LoginModel
    {
        [Required]
        public string LoginName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }
        public bool RememberLogin { get; set; }
    }
}