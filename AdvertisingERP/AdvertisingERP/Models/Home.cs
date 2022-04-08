using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public DataSet PasswordForget()
        {
            SqlParameter[] para =
                {
                new SqlParameter("",LoginID),
                new SqlParameter("",Email)

            };
            DataSet ds = DBHelper.ExecuteQuery("", para);
            return ds;
        }
        public DataSet Login()
        {
            SqlParameter[] para =
            {
                new SqlParameter ("@UserName",LoginID),
                new SqlParameter("@Password",Password)
                };
            DataSet ds = DBHelper.ExecuteQuery("LoginProc", para);
            return ds;
        }
    }
}