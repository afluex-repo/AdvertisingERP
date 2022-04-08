using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvertisingERP.Models
{
    public class Home
    {
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string Pk_AdminID { get; set; }
        public string Pk_UserID { get; set; }
        public string Result { get; set; }
        public string Email { get; set; }
    }
}