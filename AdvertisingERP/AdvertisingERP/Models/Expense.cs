using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdvertisingERP.Models
{
    public class Expense
    {
        public string ExpenseTypeId { get; set; }
        public string FK_ExpenseTypeId { get; set; }
        public string ExpenseId { get; set; }
        public string ExpenseType { get; set; }
        public string ExpenseName { get; set; }
        public string AddedBy { get; set; }
        public string PK_CompanyID { get; set; }
        public string Remark { get; set; }
        public string Transactiondt { get; set; }
        public string PK_DrExpenseID { get; set; }
        public string PF_ExpenseNameID { get; set; }
        public string PK_PaymentmodeId { get; set; }
        public List<Expense> lstExpenseType { get; set; }
        public List<Expense> lstExpense { get; set; }
        public List<SelectListItem> ddlExpenseType { get; set; }
        public List<SelectListItem> ddlExpenseName { get; set; }


        


        public DataSet SaveExpenseType()
        {
            SqlParameter[] para = { new SqlParameter("@ExpenseTypeName",ExpenseType ),
                                       new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("SaveExpenseType", para);
            return ds;
        }

        public DataSet GetExpenseTypeList()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetExpenseTypeList");
            return ds;
        }

        public DataSet DeleteExpenseType()
        {
            SqlParameter[] para = { new SqlParameter("@ExpenseTypeId",ExpenseTypeId ),
                                       new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("DeleteExpenseType", para);
            return ds;
        }
        public DataSet SaveExpense()
        {
            SqlParameter[] para = {
                new SqlParameter("@FK_ExpenseTypeId",FK_ExpenseTypeId ),
                   new SqlParameter("@ExpenseName",ExpenseName ),
                                       new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("SaveExpense", para);
            return ds;
        }


        public DataSet GetExpenseList()
        {

            SqlParameter[] para = {
                new SqlParameter("@PK_ExpenseId",ExpenseId)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetExpenseList", para);
            return ds;
        }

        public DataSet UpdateExpense()
        {
            SqlParameter[] para = {
                  new SqlParameter("@ExpenseId",ExpenseId),
              new SqlParameter("@FK_ExpenseTypeId",FK_ExpenseTypeId ),
                   new SqlParameter("@ExpenseName",ExpenseName ),
                                       new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateExpense", para);
            return ds;
        }


        public DataSet DeleteExpense()
        {
            SqlParameter[] para = { new SqlParameter("@PK_ExpenseId",ExpenseId ),
                                       new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("DeleteExpense", para);
            return ds;
        }

        public DataSet GetPaymentMode()
        {
            DataSet ds = DBHelper.ExecuteQuery("getpaymentmode");
            return ds;
        }
         public DataSet getCompany()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetCompany");
            return ds;
        }
        public DataSet GetExpenseTypeName()
        {
            SqlParameter[] para = { new SqlParameter("@FK_ExpenseTypeId",ExpenseId ),
                                      
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetExpenseTypeName", para);
            return ds;
        }
    }
}