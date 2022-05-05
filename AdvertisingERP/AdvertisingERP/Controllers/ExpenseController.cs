using AdvertisingERP.Filter;
using AdvertisingERP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AdvertisingERP.Controllers
{
    public class ExpenseController : Controller
    {
        // GET: Expense
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExpenseTypeMaster()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ExpenseTypeMaster")]
        [OnAction(ButtonName = "btnsave")]
        public ActionResult ExpenseTypeMaster(Expense model)
        {
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.SaveExpenseType();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["msg"] = "Expense type save successfully";
                    }
                    else
                    {
                        TempData["msgerror"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msgerror"] = ex.Message;
            }
            return RedirectToAction("ExpenseTypeMaster", "Expense");
        }

        public ActionResult ExpenseTypeList()
        {
            Expense model = new Expense();
            try
            {
                List<Expense> lst = new List<Expense>();
                DataSet ds = model.GetExpenseTypeList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Expense obj = new Expense();
                        obj.ExpenseTypeId = dr["PK_ExpenseTypeId"].ToString();
                        obj.ExpenseType = dr["ExpenseTypeName"].ToString();
                        lst.Add(obj);
                    }
                    model.lstExpenseType = lst;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        public ActionResult DeleteExpenseType(Expense model, string Id)
        {
            try
            {
                model.ExpenseTypeId = Id;
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.DeleteExpenseType();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["msg"] = "Expense type deleted successfully";
                    }
                    else
                    {
                        TempData["msgerror"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msgerror"] = ex.Message;
            }
            return RedirectToAction("ExpenseTypeList", "Expense");
        }


        public ActionResult ExpenseType(string Id)
        {
            Expense model = new Expense();
            List<SelectListItem> ddlExpenseType = new List<SelectListItem>();
            if (Id != null)
            {
                model.ExpenseId = Id;
                DataSet ds = model.GetExpenseList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.ExpenseId = ds.Tables[0].Rows[0]["PK_ExpenseId"].ToString();
                    model.FK_ExpenseTypeId = ds.Tables[0].Rows[0]["FK_ExpenseTypeId"].ToString();
                    model.ExpenseName = ds.Tables[0].Rows[0]["ExpenseName"].ToString();
                }

                int count1 = 0;
                DataSet ds1 = model.GetExpenseTypeList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count1 == 0)
                        {
                            ddlExpenseType.Add(new SelectListItem { Text = "Select", Value = "0" });
                        }
                        ddlExpenseType.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["PK_ExpenseTypeId"].ToString() });
                        count1 = count1 + 1;
                    }
                }
                model.ddlExpenseType = ddlExpenseType;

            }

            int count4 = 0;
            //List<SelectListItem> ddlExpenseType = new List<SelectListItem>();
            DataSet dsTemplate = model.GetExpenseTypeList();
            if (dsTemplate != null && dsTemplate.Tables.Count > 0 && dsTemplate.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsTemplate.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlExpenseType.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlExpenseType.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["PK_ExpenseTypeId"].ToString() });
                    count4 = count4 + 1;
                }
            }
            ViewBag.ddlExpenseType = ddlExpenseType;

            return View(model);
        }


        public ActionResult ExpenseList()
        {
            Expense model = new Expense();
            try
            {
                List<Expense> lst = new List<Expense>();
                DataSet ds = model.GetExpenseList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Expense obj = new Expense();
                        obj.ExpenseId = dr["PK_ExpenseId"].ToString();
                        obj.ExpenseType = dr["ExpenseTypeName"].ToString();
                        obj.ExpenseName = dr["ExpenseName"].ToString();
                        lst.Add(obj);
                    }
                    model.lstExpense = lst;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        [HttpPost]
        [ActionName("ExpenseType")]
        [OnAction(ButtonName = "btnsave")]
        public ActionResult ExpenseType(Expense model)
        {
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.SaveExpense();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["msg"] = "Expense type save successfully";
                    }
                    else
                    {
                        TempData["msgerror"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msgerror"] = ex.Message;
            }
            return RedirectToAction("ExpenseType", "Expense");
        }


        [HttpPost]
        [ActionName("ExpenseType")]
        [OnAction(ButtonName = "btnupdate")]
        public ActionResult UpdateExpenseType(Expense model)
        {
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.UpdateExpense();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["msg"] = "Expense type updated successfully";
                    }
                    else
                    {
                        TempData["msgerror"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msgerror"] = ex.Message;
            }
            return RedirectToAction("ExpenseList", "Expense");
        }


        public ActionResult DeleteExpense(Expense model, string Id)
        {
            try
            {
                model.ExpenseId = Id;
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.DeleteExpense();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["msg"] = "Expense deleted successfully";
                    }
                    else
                    {
                        TempData["msgerror"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msgerror"] = ex.Message;
            }
            return RedirectToAction("ExpenseList", "Expense");
        }

        public ActionResult DrExpense()
        {
            Expense model = new Expense();
            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
            DataSet dsTemplate = model.GetPaymentMode();
            int count1 = 0;
            if (dsTemplate != null && dsTemplate.Tables.Count > 0 && dsTemplate.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsTemplate.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlpaymentmode.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            #region  ddlExpenseType
            int count4 = 0;
            List<SelectListItem> ddlExpenseType = new List<SelectListItem>();
            DataSet dsTemp = model.GetExpenseTypeList();
            if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsTemp.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlExpenseType.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlExpenseType.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["PK_ExpenseTypeId"].ToString() });
                    count4 = count4 + 1;
                }
            }
            ViewBag.ddlExpenseType = ddlExpenseType;
            #endregion
            #region company name
            int count2 = 0;
            List<SelectListItem> ddlcompany = new List<SelectListItem>();
            DataSet dscom = model.getCompany();
            if (dscom != null && dscom.Tables.Count > 0 && dscom.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dscom.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlcompany.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlcompany.Add(new SelectListItem { Text = r["CompanyName"].ToString(), Value = r["PK_CompanyID"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlcompany = ddlcompany;
            #endregion
            List<SelectListItem> ddlExpenseName = new List<SelectListItem>();
            ViewBag.ddlExpenseName = ddlExpenseName;
            return View(model);

        }


        public ActionResult GetExpenseTypeName(string FK_ExpenseTypeId)
        {
            Expense model = new Expense();
            List<SelectListItem> ddlExpenseName = new List<SelectListItem>();

            model.ExpenseId = FK_ExpenseTypeId;
            DataSet dsTemp = model.GetExpenseTypeName();
            if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsTemp.Tables[0].Rows)
                {

                    ddlExpenseName.Add(new SelectListItem { Text = r["ExpenseName"].ToString(), Value = r["PK_ExpenseId"].ToString() });

                }
                model.ddlExpenseName = ddlExpenseName;


            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveDrExpense(Expense model)
        {

            //dt.Cr = " ";
            model.EntryType = "Dr";
            model.AddedBy = Session["UserID"].ToString();
              var jss = new JavaScriptSerializer();
            var jdv = jss.Deserialize<dynamic>(Request["dataValue"]);
            DataTable VisitorDetails = new DataTable();
            VisitorDetails = JsonConvert.DeserializeObject<DataTable>(jdv["AddData"]);
            model.dt = VisitorDetails;
            DataSet ds = new DataSet();
            ds = model.SaveData();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["msg"] = "Dr Expense  Save successfully";
                }
                else
                {
                    TempData["msgerror"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                TempData["msgerror"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }

            return Json(model, JsonRequestBehavior.AllowGet);

        }


        public ActionResult CrExpense()
        {
            Expense model = new Expense();
            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
            DataSet dsTemplate = model.GetPaymentMode();
            int count1 = 0;
            if (dsTemplate != null && dsTemplate.Tables.Count > 0 && dsTemplate.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsTemplate.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlpaymentmode.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            #region  ddlExpenseType
            int count4 = 0;
            List<SelectListItem> ddlExpenseType = new List<SelectListItem>();
            DataSet dsTemp = model.GetExpenseTypeList();
            if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsTemp.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlExpenseType.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlExpenseType.Add(new SelectListItem { Text = r["ExpenseTypeName"].ToString(), Value = r["PK_ExpenseTypeId"].ToString() });
                    count4 = count4 + 1;
                }
            }
            ViewBag.ddlExpenseType = ddlExpenseType;
            #endregion
            #region company name
            int count2 = 0;
            List<SelectListItem> ddlcompany = new List<SelectListItem>();
            DataSet dscom = model.getCompany();
            if (dscom != null && dscom.Tables.Count > 0 && dscom.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dscom.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlcompany.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlcompany.Add(new SelectListItem { Text = r["CompanyName"].ToString(), Value = r["PK_CompanyID"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlcompany = ddlcompany;
            #endregion
            List<SelectListItem> ddlExpenseName = new List<SelectListItem>();
            ViewBag.ddlExpenseName = ddlExpenseName;
            return View(model);

        }
        public ActionResult CrExpenseList()
        {
            return View();
        }
    }
}