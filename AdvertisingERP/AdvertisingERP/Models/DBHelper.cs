﻿    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdvertisingERP.Models
{
    public class DBHelper
    {
        public static string connectionString = string.Empty;

        static DBHelper()
        {
            try
            {
                //connectionString = "Data Source=DESKTOP-UDD0G1A\\SQLEXPRESS; Initial Catalog=CitiAdDB; User Id=sa; Password=1234; Integrated Security=false; ";
                //connectionString = "Data Source=184.95.53.181;Initial Catalog=CitiAdDemo;User Id=citiusertest;Password=citi123!@#;Integrated Security=false;";

                connectionString = "Data Source=23.111.171.42;Initial Catalog=AdvertisingERP;User Id=AdvertisingERP;Password=AfluexERP123!@#;Integrated Security=false;";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int ExecuteNonQuery(string commandText, params SqlParameter[] commandParameters)
        {
            int k = 0;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(commandParameters);
                    connection.Open();
                    k = command.ExecuteNonQuery();
                }
                return k;
            }
            catch
            {
                return k;
            }
        }

        public static DataSet ExecuteQuery(string commandText, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Msg");
                dt.Columns.Add("ErrorMessage");

                DataRow dr = dt.NewRow();
                dr["Msg"] = "0";
                dr["ErrorMessage"] = ex.Message;
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);

            }
            return ds;
        }
    }
}



