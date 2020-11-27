using AngularCRUDOperation.Models;
using AngularCRUDOperation.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularCRUDOperation.Controllers
{
    public class HomeController : Controller
    {
        DB db = new DB();
        public ActionResult Login()
        {
            return View();
        }

        public JsonResult userLogin(UserModel userModel)
        {
            UserModel modelss = new UserModel();
            string res = Convert.ToString(db.userlogin(userModel, out modelss));

            if (res == "1")
            {
                Session["user_id"] = modelss.user_id;
                Session["user_name"] = modelss.user_name;
                Session["user_pass"] = modelss.user_pass;
                Session["user_role"] = modelss.user_role;
            }
            else
            {
                res = "Failed";
            }

            return Json(res, JsonRequestBehavior.AllowGet);

        }
        // GET: Homme
        public ActionResult Index()
        {
            if ((Session["user_id"] != null) && (Session["user_pass"] != null))
            {
                if (Session["user_role"].ToString() == "admin")
                {

                    ViewBag.UserInfo = Session["user_name"].ToString();
                    ViewBag.UserRole = Session["user_role"].ToString();
                    return View();
                }
                if (Session["user_role"].ToString() == "user")
                {
                    ViewBag.UserInfo = Session["user_name"].ToString();
                    ViewBag.UserRole = Session["user_role"].ToString();
                    return View();
                }
            }
            
                return View("Login");
            
        }

        public ActionResult Add()
        {
            if ((Session["user_id"] != null) && (Session["user_pass"] != null))
            {
                if (Session["user_role"].ToString() == "admin")
                {

                    ViewBag.UserInfo = Session["user_name"].ToString();
                    ViewBag.UserRole = Session["user_role"].ToString();
                    return View();
                }
                if (Session["user_role"].ToString() == "user")
                {
                    ViewBag.UserInfo = Session["user_name"].ToString();
                    ViewBag.UserRole = Session["user_role"].ToString();
                    return View("Index");
                }
            }
            return View("Add");
        }

        public ActionResult Edit(int empid)
        {
            if ((Session["user_id"] != null) && (Session["user_pass"] != null))
            {
                if (Session["user_role"].ToString() == "admin")
                {

                    ViewBag.UserInfo = Session["user_name"].ToString();
                    ViewBag.UserRole = Session["user_role"].ToString();
                    return View();
                }
                if (Session["user_role"].ToString() == "user")
                {
                    ViewBag.UserInfo = Session["user_name"].ToString();
                    ViewBag.UserRole = Session["user_role"].ToString();
                    return View("Index");
                }
            }
            return View();
        }
        public JsonResult GetEmployeeById(int empid)
        {
            
            DataSet ds = db.SelectEmployeeById(empid);
            List<EmployeeModel> employeeModels = new List<EmployeeModel>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                employeeModels.Add(new EmployeeModel
                {
                    empid = Convert.ToInt32(dr["empid"]),
                    empname = dr["empname"].ToString(),
                    empaddress = dr["empaddress"].ToString(),
                    empcity = dr["empcity"].ToString()
                });
            }

            return Json(employeeModels, JsonRequestBehavior.AllowGet);
            
        }
        // Insert Records
        public JsonResult AddData(EmployeeModel employeeModel)
        {
            string res = string.Empty;
            try
            {
                db.InsertEmployee(employeeModel);
                res = "inserted";
                
            }
            catch
            {
                res = "failed"; ;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        
        // Update Records
        public JsonResult EditRecord(EmployeeModel employeeModel)
        {
            string res = string.Empty;
            try
            {
                db.UpdateEmployee(employeeModel);
                res = "updated";
            }
            catch
            {
                res = "failed";
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        // Delete Records
        public JsonResult Delete(int empid)
        {
            string res = string.Empty;
            try
            {
                db.DeleteEmployee(empid);
                res = "deleted";
            } 
            catch
            {
                res = "failed";
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        // Select All Records
        public JsonResult GetAll()
        {
            DataSet ds = db.SelectAllEmployee();
            List<EmployeeModel> employeeModels = new List<EmployeeModel>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                employeeModels.Add(new EmployeeModel
                {
                    empid = Convert.ToInt32(dr["empid"]),
                    empname = dr["empname"].ToString(),
                    empaddress = dr["empaddress"].ToString(),
                    empcity = dr["empcity"].ToString()
                });
            }
            return Json(employeeModels, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ImportData(List<EmployeeModel> employeeModels)
        {
            string res = string.Empty;
            try
            {
                db.InsertEmployee(employeeModels);
                res = "inserted";
            }
            catch
            {
                res = "failed";
            }
            return Json(res, JsonRequestBehavior.AllowGet);
           
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return View("Login");
        }
    }
}