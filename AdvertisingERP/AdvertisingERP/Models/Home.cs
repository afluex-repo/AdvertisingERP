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
        public string LoginId { get; set; }
        public string Password { get; set; }

        public DataSet Login()
        {
            //LoginProc
            SqlParameter[] para ={new SqlParameter ("@UserName",LoginId),
                                new SqlParameter("@Password",Password)};
            DataSet ds = DBHelper.ExecuteQuery("LoginProc", para);
            return ds;
        }
    }
}