using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularCRUDOperation.Models
{
    public class UserModel
    {
        [Required(ErrorMessage ="Username is required")]
        public string user_id { get; set; }
        [Required(ErrorMessage ="Password is required")]
        public string user_pass { get; set; }
        public string user_name { get; set; }
        public string user_role { get; set; }
    }
}