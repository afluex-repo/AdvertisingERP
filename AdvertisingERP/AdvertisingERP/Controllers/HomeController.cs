using AdvertisingERP.Filter;
using AdvertisingERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace AdvertisingERP.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Login")]
        [OnAction(ButtonName = "btnlogin")]
        public ActionResult LoginPanel(Home model)
        {
            if (model.LoginID == null)
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter LoginId";
                return RedirectToAction("Login");

            }
            if (model.Password == null)
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter Password";
                return RedirectToAction("Login");
            }
            if (model.LoginID.Trim() == "")
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter LoginId";
                return RedirectToAction("Login");

            }
            if (model.Password.Trim() == "")
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter Password";
                return RedirectToAction("Login");
            }
            try
            {
                DataSet ds = model.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["Fk_UserTypeId"].ToString() == "1")
                    {
                        ViewBag.errormsg = "";
                        Session["AdminID"] = ds.Tables[0].Rows[0]["Pk_Id"].ToString();
                        Session["LoginID"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Username"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        Session["EmailID"] = ds.Tables[0].Rows[0]["EmailId"].ToString();
                        Session["FK_UserTypeID"] = ds.Tables[0].Rows[0]["Fk_UserTypeId"].ToString();

                        return RedirectToAction("DashBoard", "Home");

                    }

                    if (ds.Tables[0].Rows[0]["Fk_UserTypeId"].ToString() != "1")
                    {
                        ViewBag.errormsg = "";
                        Session["UserID"] = ds.Tables[0].Rows[0]["Pk_Id"].ToString();
                        Session["LoginID"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Username"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        Session["FK_UserTypeID"] = ds.Tables[0].Rows[0]["Fk_UserTypeId"].ToString();
                        return RedirectToAction("EmployeeDashBoard", "Employee");

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

       
        [HttpPost]
        [ActionName("ForgetPassword")]
        [OnAction(ButtonName = "btnforgetpassword")]
        public ActionResult ChangePassword(Home model)
        {
            if (model.LoginID == null)
            {
                ViewBag.errormsg = "";
                TempData["Error"] = "Please Enter LoginId";
                return RedirectToAction("ForgetPassword");

            }
            if (model.Email == null)
            {
                ViewBag.errormsg = "";
                TempData["Error"] = "Please Enter EmailId";
                return RedirectToAction("ForgetPassword");
            }
            DataSet ds = model.PasswordForget();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    try
                    {
                        if (model.Email != null)
                        {
                            string mailbody = "";
                            mailbody = "Dear Member,<br> your Passoword is : " + ds.Tables[0].Rows[0]["Password"].ToString();
                            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                            {
                                Host = "smtp.gmail.com",
                                Port = 587,
                                EnableSsl = true,
                                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = true,
                                Credentials = new NetworkCredential("developer5.afluex@gmail.com", "Afluex@123")
                            };
                            using (var message = new MailMessage("developer5.afluex@gmail.com", model.Email)
                            {
                                IsBodyHtml = true,
                                Subject = "Recover Password",
                                Body = mailbody
                            })
                                smtp.Send(message);
                            TempData["Success"] = "Your Password Has Been Send On your EmailId";

                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                else
                {
                    TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }

            return RedirectToAction("ForgetPassword", "Home");
        }
    }
}
   