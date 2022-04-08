using AdvertisingERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdvertisingERP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Login()
        {
            Session.Abandon();
            if (TempData["Login"] == null)
            {
                ViewBag.errormsg = "none";
            }
            return View();
        }
        public ActionResult LoginAction(Home obj)
        {
            if (obj.LoginId == null)
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter LoginId";
                return RedirectToAction("Login");

            }
            if (obj.Password == null)
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter Password";
                return RedirectToAction("Login");
            }
            if (obj.LoginId.Trim() == "")
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter LoginId";
                return RedirectToAction("Login");
               
            }
            if (obj.Password.Trim() == "")
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter Password";
                return RedirectToAction("Login");

            }
          
            try
            {
                Home Modal = new Home();
                DataSet ds = obj.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        ViewBag.errormsg = "";
                        Session["UserID"] = ds.Tables[0].Rows[0]["Pk_Id"].ToString();
                        Session["LoginID"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Username"] = ds.Tables[0].Rows[0]["Name"].ToString();
                       
                        return RedirectToAction("DashBoard","Admin");
                      
                    }
                    else
                    {
                        ViewBag.errormsg = "";
                        TempData["Login"] = "Incorrect LoginId Or Password";
                        return RedirectToAction("Login");
                      
                    }
                    
                    


                }
                else
                {
                    ViewBag.errormsg = "";
                    TempData["Login"] = "Incorrect LoginId Or Password";
                    return RedirectToAction("Login");
                   

                }
            }
            catch (Exception ex)
            {
                ViewBag.errormsg = "";
                TempData["Login"] = ex.Message;
                return RedirectToAction("Login");
               
            }

          



        }

    }
}
