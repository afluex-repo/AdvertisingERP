using AdvertisingERP.Filter;
using AdvertisingERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdvertisingERP.Controllers
{
    public class ExpenseController : Controller
    {
        // GET: Expense
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExpenseTypeMaster(string Id)
        {
            Expense model = new Expense();
            if(Id!=null)
            {
                model.ExpenseTypeId = Id;
                DataSet ds = model.GetExpenseTypeList();
                if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
                {
                    model.ExpenseType = ds.Tables[0].Rows[0]["ExpenseTypeName"].ToString();
                }
            }
            return View(model);
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

        [HttpPost]
        [OnAction(ButtonName="btnupdate")]
        [ActionName("ExpenseTypeMaster")]
        public ActionResult UpdateExpenseTypeMaster(Expense model)
        {
            try
            {
                model.AddedBy= Session["UserID"].ToString();
                DataSet ds = model.UpdateExpenseType();
                if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
                {
                    if(ds.Tables[0].Rows[0][0].ToString()=="1")
                    {
                        TempData["msg"] = "Expense type updated successfully";
                    }
                    else if(ds.Tables[0].Rows[0][0].ToString()=="0")
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch(Exception ex)
            {
                TempData["msg"] = ex.Message;
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
            if (Id!=null)
            {
                model.ExpenseId = Id;
                DataSet ds = model.GetExpenseList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.ExpenseId = ds.Tables[0].Rows[0]["PK_ExpenseId"].ToString();
                    model.FK_ExpenseTypeId = ds.Tables[0].Rows[0]["FK_ExpenseTypeId"].ToString();
                    model.ExpenseName = ds.Tables[0].Rows[0]["ExpenseName"].ToString();
                }
                int count4 = 0;
                List<SelectListItem> ddlExpenseType = new List<SelectListItem>();
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
            }
            else
            {
                int count4 = 0;
                List<SelectListItem> ddlExpenseType = new List<SelectListItem>();
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
            }
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
                        TempData["msg"] = "Expense save successfully";
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
                        TempData["msg"] = "Expense updated successfully";
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


    }
}