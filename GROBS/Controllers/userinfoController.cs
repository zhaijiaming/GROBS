using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using System.Diagnostics;
using GROBS.EFModels;
using GROBS.IBSL;
using GROBS.BSL;
using GROBS.Common;
using GROBS.Models;
using GROBS.Filters;
using System.Linq.Expressions;
using X.PagedList;
namespace GROBS.Controllers
{
    public class User
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public int? EmployeeID { get; set; }
    }
    public class userinfoController : Controller
    {
        private IuserinfoService ob_userinfoservice = ServiceFactory.userinfoservice;
        private List<SearchConditionModel> _searchconditions;
        //public ActionResult Index()
        //{
        //    var tempData = ob_userinfoservice.LoadSortEntities(userinfo => userinfo.IsDelete == false && userinfo.FullName.Contains("dav"), false, userinfo => userinfo.ID);
        //    //var tempdata=ob_userinfoservice.LoadPageEntities(1, 20,out itotal, userinfo => userinfo.IsDelete == false, false, userinfo => userinfo.ID);
        //    ViewBag.userinfo = tempData;
        //    return View();
        //}
        private void MakeSearch()
        {
            _searchconditions = new List<SearchConditionModel>();
            SearchConditionModel scm = new SearchConditionModel();
            scm.ItemCode = "Account";
            scm.ItemTitle = "ÕËºÅ";
            scm.ItemType = "System.String";
            //scm.ItemJion = "";
            //scm.ItemValue = "";
            //scm.ItemOpValue = "";
            _searchconditions.Add(scm);
            scm = new SearchConditionModel();
            scm.ItemCode = "FullName";
            scm.ItemTitle = "ÐÕÃû";
            scm.ItemType = "System.String";
            _searchconditions.Add(scm);
            scm = new SearchConditionModel();
            scm.ItemCode = "EmployeeID";
            scm.ItemTitle = "Ô±¹¤ºÅ";
            scm.ItemType = "System.Int";
            _searchconditions.Add(scm);
            scm = new SearchConditionModel();
            scm.ItemCode = "Status";
            scm.ItemTitle = "×´Ì¬";
            scm.ItemType = "System.Int";
            _searchconditions.Add(scm);
        }
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            try
            {
                if (string.IsNullOrEmpty(page))
                {
                    page = "1";
                }
                int userid = (int)Session["user_id"];
                string pagetag = "userinfo_index";
                MakeSearch();
                Expression<Func<userinfo, bool>> where = PredicateExtensionses.True<userinfo>();
                searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
                if (sc != null)
                {
                    string[] sclist = sc.ConditionInfo.Split(';');
                    foreach (string scl in sclist)
                    {
                        string[] scld = scl.Split(',');
                        switch (scld[0])
                        {
                            case "account":
                                string account = scld[1];// Request["account"] ?? "";
                                string accountequal = scld[2];// = Request["accountequal"] ?? "";
                                string accountand = scld[3]; //Request["accountand"] ?? "";
                                if (!string.IsNullOrEmpty(account))
                                {
                                    if (accountequal.Equals("="))
                                    {
                                        if (accountand.Equals("and"))
                                            where = where.And(userinfo => userinfo.Account == account);
                                        else
                                            where = where.Or(userinfo => userinfo.Account == account);
                                    }
                                    if (accountequal.Equals("like"))
                                    {
                                        if (accountand.Equals("and"))
                                            where = where.And(userinfo => userinfo.Account.Contains(account));
                                        else
                                            where = where.Or(userinfo => userinfo.Account.Contains(account));
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    ViewBag.SearchCondition = sc.ConditionInfo;
                }

                where = where.And(userinfo => userinfo.IsDelete == false);

                var tempData = ob_userinfoservice.LoadSortEntities(where.Compile(), false, userinfo => userinfo.ID).ToPagedList<userinfo>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                //var tempData = ob_userinfoservice.LoadPageEntities(int.Parse(index),int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]), out itotal, where.Compile(), false, userinfo => userinfo.ID);
                //ViewBag.userinfo = tempData;
                //log4net.LogManager.GetLogger(Session["user_id"].ToString()).Error("user list now");
                //ViewBag.SearchCondition = _searchconditions;
                return View(tempData);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("AppLog").Error(string.Format("userinfo,{0}", ex.Message));
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            try
            {
                int userid = (int)Session["user_id"];
                string pagetag = "userinfo_index";
                string page = "1";

                string account = Request["account"] ?? "";
                string accountequal = Request["accountequal"] ?? "";
                string accountand = Request["accountand"] ?? "";

                //MakeSearch();
                Expression<Func<userinfo, bool>> where = PredicateExtensionses.True<userinfo>();

                searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
                if (sc == null)
                {
                    sc = new searchcondition();
                    sc.UserID = userid;
                    sc.PageBrief = pagetag;
                    if (!string.IsNullOrEmpty(account))
                    {
                        if (accountequal.Equals("="))
                        {
                            if (accountand.Equals("and"))
                                where = where.And(userinfo => userinfo.Account == account);
                            else
                                where = where.Or(userinfo => userinfo.Account == account);
                        }
                        if (accountequal.Equals("like"))
                        {
                            if (accountand.Equals("and"))
                                where = where.And(userinfo => userinfo.Account.Contains(account));
                            else
                                where = where.Or(userinfo => userinfo.Account.Contains(account));
                        }
                        sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "account", account, accountequal, accountand);
                    }
                    searchconditionService.GetInstance().AddEntity(sc);
                }
                else
                {
                    sc.ConditionInfo = "";
                    if (!string.IsNullOrEmpty(account))
                    {
                        if (accountequal.Equals("="))
                        {
                            if (accountand.Equals("and"))
                                where = where.And(userinfo => userinfo.Account == account);
                            else
                                where = where.Or(userinfo => userinfo.Account == account);
                        }
                        if (accountequal.Equals("like"))
                        {
                            if (accountand.Equals("and"))
                                where = where.And(userinfo => userinfo.Account.Contains(account));
                            else
                                where = where.Or(userinfo => userinfo.Account.Contains(account));
                        }
                        sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "account", account, accountequal, accountand);
                    }
                    searchconditionService.GetInstance().UpdateEntity(sc);
                }
                ViewBag.SearchCondition = sc.ConditionInfo;

                where = where.And(userinfo => userinfo.IsDelete == false);
                //var ulog = Session["User_ID"];
                //int itotal = 0;
                var tempData = ob_userinfoservice.LoadSortEntities(where.Compile(), false, userinfo => userinfo.ID).ToPagedList<userinfo>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                //var tempData = ob_userinfoservice.LoadPageEntities(int.Parse(index),int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]), out itotal, where.Compile(), false, userinfo => userinfo.ID);
                //ViewBag.userinfo = tempData;
                //log4net.LogManager.GetLogger(Session["user_id"].ToString()).Error("user list now");
                log4net.LogManager.GetLogger(string.Format("{0}", Session["user_id"])).Info("list user info");
                return View(tempData);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("AppLog").Error(string.Format("userinfo_index,{0}", ex.Message));
                return RedirectToAction("Home");
            }
        }
        public ActionResult PageList()
        {
            string account = Request["account"] ?? "";
            string fullname = Request["fullname"] ?? "";
            string employeeid = Request["employeeid"] ?? "";

            string accountequal = Request["accountequal"] ?? "";
            string fullnameequal = Request["fullnameequal"] ?? "";
            string employeeidequal = Request["employeeidequal"] ?? "";

            string accountand = Request["accountand"] ?? "";
            string fullnameand = Request["fullnameand"] ?? "";
            string employeeidand = Request["employeeidand"] ?? "";

            Expression<Func<userinfo, bool>> where = PredicateExtensionses.True<userinfo>();// = userinfo => userinfo.IsDelete == false;

            if (!string.IsNullOrEmpty(account))
            {
                if (accountequal.Equals("="))
                {
                    if (accountand.Equals("and"))
                        where = where.And(userinfo => userinfo.Account == account);
                    else
                        where = where.Or(userinfo => userinfo.Account == account);
                }
                if (accountequal.Equals("like"))
                {
                    if (accountand.Equals("and"))
                        where = where.And(userinfo => userinfo.Account.Contains(account));
                    else
                        where = where.Or(userinfo => userinfo.Account.Contains(account));
                }
            }
            //where = where.And(userinfo => userinfo.Account == account);
            if (!string.IsNullOrEmpty(fullname))
            {
                if (fullnameequal.Equals("="))
                {
                    if (fullnameand.Equals("and"))
                        where = where.And(userinfo => userinfo.FullName == fullname);
                    else
                        where = where.Or(userinfo => userinfo.FullName == fullname);
                }
                if (fullnameequal.Equals("like"))
                    if (fullnameand.Equals("and"))
                        where = where.And(userinfo => userinfo.FullName.Contains(fullname));
                    else
                        where = where.Or(userinfo => userinfo.FullName.Contains(fullname));
            }
            //where = where.And(userinfo => userinfo.FullName == fullname);
            if (!string.IsNullOrEmpty(employeeid))
            {
                if (employeeidequal.Equals("="))
                {
                    if (employeeidand.Equals("and"))
                        where = where.And(userinfo => userinfo.EmployeeID == int.Parse(employeeid));
                    else
                        where = where.Or(userinfo => userinfo.EmployeeID == int.Parse(employeeid));
                }
                if (employeeidequal.Equals(">"))
                {
                    if (employeeidand.Equals("and"))
                        where = where.And(userinfo => userinfo.EmployeeID > int.Parse(employeeid));
                    else
                        where = where.Or(userinfo => userinfo.EmployeeID > int.Parse(employeeid));
                }
                if (employeeidequal.Equals("<"))
                {
                    if (employeeidand.Equals("and"))
                        where = where.And(userinfo => userinfo.EmployeeID < int.Parse(employeeid));
                    else
                        where = where.Or(userinfo => userinfo.EmployeeID < int.Parse(employeeid));
                }
            }
            where = where.And(userinfo => userinfo.IsDelete == false);
            //where = where.And(userinfo => userinfo.EmployeeID.Equals(employeeid));
            var tempData = ob_userinfoservice.LoadEntities(where.Compile());
            ViewBag.userinfo = tempData;
            return View("Index");
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            //string id = Request["ob_userinfo_id"] ?? "";
            //string account = Request["ob_userinfo_account"] ?? "";
            //string password = Request["ob_userinfo_password"] ?? "";
            //string fullname = Request["ob_userinfo_fullname"] ?? "";
            //string employeeid = Request["ob_userinfo_employeeid"] ?? "";
            //string accounttype = Request["ob_userinfo_accounttype"] ?? "";
            //string remark = Request["ob_userinfo_remark"] ?? "";
            //string status = Request["ob_userinfo_status"] ?? "";
            //string inputman = Request["ob_userinfo_inputman"] ?? "";
            //string inputdate = Request["ob_userinfo_inputdate"] ?? "";
            //string modifyman = Request["ob_userinfo_modifyman"] ?? "";
            //string modifydate = Request["ob_userinfo_modifydate"] ?? "";
            string id = Request["id"] ?? "";
            string account = Request["account"] ?? "";
            string password = Request["password"] ?? "";
            string fullname = Request["fullname"] ?? "";
            string employeeid = Request["employeeid"] ?? "";
            string accounttype = Request["accounttype"] ?? "";
            string remark = Request["remark"] ?? "";
            string status = Request["status"] ?? "";
            string inputman = Request["inputman"] ?? "";
            string inputdate = Request["inputdate"] ?? "";
            string modifyman = Request["modifyman"] ?? "";
            string modifydate = Request["modifydate"] ?? "";
            try
            {
                userinfo ob_userinfo = new userinfo();
                ob_userinfo.Account = account.Trim();
                ob_userinfo.Password = password.Trim();
                ob_userinfo.FullName = fullname.Trim();
                ob_userinfo.EmployeeID = employeeid == "" ? 0 : int.Parse(employeeid);
                ob_userinfo.AccountType = accounttype == "" ? 0 : int.Parse(accounttype);
                ob_userinfo.Remark = remark.Trim();
                ob_userinfo.Status = status == "" ? 0 : int.Parse(status);
                ob_userinfo.InputMan = inputman.Trim();
                ob_userinfo.InputDate = inputdate == "" ? DateTime.Now : DateTime.Parse(inputdate);
                ob_userinfo.ModifyMan = modifyman.Trim();
                ob_userinfo.ModifyDate = modifydate == "" ? DateTime.Now : DateTime.Parse(modifydate);
                ob_userinfo = ob_userinfoservice.AddEntity(ob_userinfo);
                ViewBag.userinfo = ob_userinfo;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                log4net.LogManager.GetLogger("AppLog").Error(string.Format("Add userinfo fail,{0}", ex.Message));
            }
            return RedirectToAction("Index");
        }
        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            userinfo tempData = ob_userinfoservice.GetEntityById(userinfo => userinfo.ID == id && userinfo.IsDelete == false);
            ViewBag.userinfo = tempData;
            if (tempData == null)
                return View();
            else
            {
                userinfoViewModel userinfoviewmodel = new userinfoViewModel();
                userinfoviewmodel.ID = tempData.ID;
                userinfoviewmodel.Account = tempData.Account;
                userinfoviewmodel.AccountType = tempData.AccountType;
                userinfoviewmodel.EmployeeID = tempData.EmployeeID;
                userinfoviewmodel.FullName = tempData.FullName;
                userinfoviewmodel.InputDate = tempData.InputDate;
                userinfoviewmodel.InputMan = tempData.InputMan;
                userinfoviewmodel.IsDelete = tempData.IsDelete;
                userinfoviewmodel.ModifyDate = tempData.ModifyDate;
                userinfoviewmodel.ModifyMan = tempData.ModifyMan;
                userinfoviewmodel.Password = tempData.Password;
                userinfoviewmodel.Remark = tempData.Remark;
                userinfoviewmodel.Status = tempData.Status;
                return View(userinfoviewmodel);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            //string id = Request["ob_userinfo_id"] ?? "";
            //string account = Request["ob_userinfo_account"] ?? "";
            //string password = Request["ob_userinfo_password"] ?? "";
            //string fullname = Request["ob_userinfo_fullname"] ?? "";
            //string employeeid = Request["ob_userinfo_employeeid"] ?? "";
            //string accounttype = Request["ob_userinfo_accounttype"] ?? "";
            //string remark = Request["ob_userinfo_remark"] ?? "";
            //string status = Request["ob_userinfo_status"] ?? "";
            //string inputman = Request["ob_userinfo_inputman"] ?? "";
            //string inputdate = Request["ob_userinfo_inputdate"] ?? "";
            //string modifyman = Request["ob_userinfo_modifyman"] ?? "";
            //string modifydate = Request["ob_userinfo_modifydate"] ?? "";
            string id = Request["id"] ?? "";
            string account = Request["account"] ?? "";
            string password = Request["password"] ?? "";
            string fullname = Request["fullname"] ?? "";
            string employeeid = Request["employeeid"] ?? "";
            string accounttype = Request["accounttype"] ?? "";
            string remark = Request["remark"] ?? "";
            string status = Request["status"] ?? "";
            string inputman = Request["inputman"] ?? "";
            string inputdate = Request["inputdate"] ?? "";
            string modifyman = Request["modifyman"] ?? "";
            string modifydate = Request["modifydate"] ?? "";
            int uid = int.Parse(id);
            try
            {
                userinfo p = ob_userinfoservice.GetEntityById(userinfo => userinfo.ID == uid);
                p.Account = account.Trim();
                p.Password = password.Trim();
                p.FullName = fullname.Trim();
                p.EmployeeID = employeeid == "" ? 0 : int.Parse(employeeid);
                p.AccountType = accounttype == "" ? 0 : int.Parse(accounttype);
                p.Remark = remark.Trim();
                p.Status = status == "" ? 0 : int.Parse(status);
                p.InputMan = inputman.Trim();
                p.InputDate = inputdate == "" ? DateTime.Now : DateTime.Parse(inputdate);
                p.ModifyMan = modifyman.Trim();
                p.ModifyDate = modifydate == "" ? DateTime.Now : DateTime.Parse(modifydate);
                ob_userinfoservice.UpdateEntity(p);
                ViewBag.saveok = ViewAddTag.ModifyOk;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.saveok = ViewAddTag.ModifyNo;
            }
            return RedirectToAction("Edit", new { id = uid });
        }
        public ActionResult Delete()
        {
            string sdel = Request["del"] ?? "";
            int id;
            userinfo ob_userinfo;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_userinfo = ob_userinfoservice.GetEntityById(userinfo => userinfo.ID == id && userinfo.IsDelete == false);
                    ob_userinfo.IsDelete = true;
                    ob_userinfoservice.UpdateEntity(ob_userinfo);
                }
            }
            return RedirectToAction("Index");
        }

        public JsonResult GetUser()
        {
            var _usertemp = ob_userinfoservice.LoadSortEntities(p => p.IsDelete == false, true, s => s.FullName);
            List<User> _ulist = new List<User>();
            User _u;
            foreach (var u in _usertemp)
            {
                _u = new User();
                _u.ID = u.ID;
                _u.FullName = u.FullName;
                _u.EmployeeID = u.EmployeeID;
                _ulist.Add(_u);
            }
            return Json(_ulist);
        }
    }
}

