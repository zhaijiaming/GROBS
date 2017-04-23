using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Collections.Generic;
using X.PagedList;
using GROBS.EFModels;
using GROBS.IBSL;
using GROBS.BSL;
using GROBS.Common;
using GROBS.Models;
using GROBS.Filters;

namespace GROBS.Controllers
{
    public class auth_quanxianController : Controller
    {
        private Iauth_quanxianService ob_auth_quanxianservice = ServiceFactory.auth_quanxianservice;
        //[OutputCache(Duration = 10)]
        //public ActionResult Authority1(string page)
        //{
        //    if (string.IsNullOrEmpty(page))
        //        page = "1";
        //    int userid = (int)Session["user_id"];
        //    string jsid = Request["jsid"] ?? "";
        //    if (jsid == "")
        //        jsid = "0";
        //    var tempData = ob_auth_quanxianservice.LoadSortEntities(auth_quanxian => auth_quanxian.JSID == int.Parse(jsid) && auth_quanxian.IsDelete == false, false, auth_quanxian => auth_quanxian.ID);
        //    ViewBag.auth_quanxian = tempData;
        //    return View(tempData);
        //}
        public ActionResult PersonRightList()
        {
            int user_id = (int)Session["user_id"];
            return View();
        }
        [OutputCache(Duration = 30)]
        public ActionResult Authority()
        {
            int userid = (int)Session["user_id"];
            string jsid = Request["jsid"] ?? "";
            if (jsid == "")
                jsid = "0";
            Iauth_jueseService jueseservice = ServiceFactory.auth_jueseservice;
            auth_juese juese = jueseservice.GetEntityById(auth_juese => auth_juese.ID == int.Parse(jsid) && auth_juese.IsDelete == false);
            if (juese == null)
                return View();
            auth_authorizeViewModel authorview = new auth_authorizeViewModel();
            authorview.RoleID = juese.ID;
            authorview.RoleName = juese.RoleName;
            IuserinfoService userservice = ServiceFactory.userinfoservice;
            IList<userinfo> users = userservice.LoadSortEntities(userinfo => userinfo.IsDelete == false, true, userinfo => userinfo.FullName).ToList<userinfo>();
            //Iauth_quanxianService qxservice = ServiceFactory.auth_quanxianservice;
            IList<auth_quanxian> qxs = ob_auth_quanxianservice.LoadSortEntities(auth_quanxian => auth_quanxian.IsDelete == false && auth_quanxian.JSID == int.Parse(jsid), true, auth_quanxian => auth_quanxian.RYID).ToList<auth_quanxian>();
            authorview.AllUser = users;
            authorview.AuthorizedUser = qxs;
            return View(authorview);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AuthorityNow()
        {
            var jsid = Request["jsid"] ?? "";
            var users = Request["users"] ?? "";
            int userid = (int)Session["user_id"];
            if (jsid == "")
                jsid = "0";
            else
            {
                IList<auth_quanxian> qxs = ob_auth_quanxianservice.LoadEntities(auth_quanxian => auth_quanxian.IsDelete == false && auth_quanxian.JSID == int.Parse(jsid)).ToList<auth_quanxian>();
                if (users == "")
                {
                    users = "0";
                    foreach (auth_quanxian qx in qxs)
                        ob_auth_quanxianservice.DeleteEntity(qx);
                }
                else
                {
                    string[] userlist = users.Split(',');
                    bool finduser = false;
                    //remove author
                    foreach (auth_quanxian qx in qxs)
                    {
                        finduser = false;
                        foreach (string us in userlist)
                        {
                            if (us.Length > 0)
                            {
                                if (qx.RYID == int.Parse(us))
                                {
                                    finduser = true;
                                    break;
                                }
                            }
                        }
                        if (!finduser)
                            ob_auth_quanxianservice.DeleteEntity(qx);
                    }
                    //add author
                    foreach (string us in userlist)
                    {
                        if (us.Length > 0)
                        {
                            finduser = false;
                            foreach (auth_quanxian qx in qxs)
                            {
                                if (qx.RYID == int.Parse(us))
                                {
                                    finduser = true;
                                    break;
                                }
                            }
                            if (!finduser)
                            {
                                auth_quanxian qxnew = new auth_quanxian();
                                qxnew.JSID = int.Parse(jsid);
                                qxnew.RYID = int.Parse(us);
                                qxnew.MakeMan = userid;
                                qxnew.SXDate = DateTime.Now.AddYears(1);
                                ob_auth_quanxianservice.AddEntity(qxnew);
                            }
                        }
                    }
                }
            }
            return RedirectToAction("Authority", new { jsid = jsid });
        }
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "auth_quanxian_index";
            Expression<Func<auth_quanxian, bool>> where = PredicateExtensionses.True<auth_quanxian>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "jsid":
                            string jsid = scld[1];
                            string jsidequal = scld[2];
                            string jsidand = scld[3];
                            if (!string.IsNullOrEmpty(jsid))
                            {
                                if (jsidequal.Equals("="))
                                {
                                    if (jsidand.Equals("and"))
                                        where = where.And(auth_quanxian => auth_quanxian.JSID == int.Parse(jsid));
                                    else
                                        where = where.Or(auth_quanxian => auth_quanxian.JSID == int.Parse(jsid));
                                }
                                if (jsidequal.Equals(">"))
                                {
                                    if (jsidand.Equals("and"))
                                        where = where.And(auth_quanxian => auth_quanxian.JSID > int.Parse(jsid));
                                    else
                                        where = where.Or(auth_quanxian => auth_quanxian.JSID > int.Parse(jsid));
                                }
                                if (jsidequal.Equals("<"))
                                {
                                    if (jsidand.Equals("and"))
                                        where = where.And(auth_quanxian => auth_quanxian.JSID < int.Parse(jsid));
                                    else
                                        where = where.Or(auth_quanxian => auth_quanxian.JSID < int.Parse(jsid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(auth_quanxian => auth_quanxian.IsDelete == false);

            var tempData = ob_auth_quanxianservice.LoadSortEntities(where.Compile(), false, auth_quanxian => auth_quanxian.ID).ToPagedList<auth_quanxian>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.auth_quanxian = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "auth_quanxian_index";
            string page = "1";
            string jsid = Request["jsid"] ?? "";
            string jsidequal = Request["jsidequal"] ?? "";
            string jsidand = Request["jsidand"] ?? "";
            Expression<Func<auth_quanxian, bool>> where = PredicateExtensionses.True<auth_quanxian>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(jsid))
                {
                    if (jsidequal.Equals("="))
                    {
                        if (jsidand.Equals("and"))
                            where = where.And(auth_quanxian => auth_quanxian.JSID == int.Parse(jsid));
                        else
                            where = where.Or(auth_quanxian => auth_quanxian.JSID == int.Parse(jsid));
                    }
                    if (jsidequal.Equals(">"))
                    {
                        if (jsidand.Equals("and"))
                            where = where.And(auth_quanxian => auth_quanxian.JSID > int.Parse(jsid));
                        else
                            where = where.Or(auth_quanxian => auth_quanxian.JSID > int.Parse(jsid));
                    }
                    if (jsidequal.Equals("<"))
                    {
                        if (jsidand.Equals("and"))
                            where = where.And(auth_quanxian => auth_quanxian.JSID < int.Parse(jsid));
                        else
                            where = where.Or(auth_quanxian => auth_quanxian.JSID < int.Parse(jsid));
                    }
                }
                if (!string.IsNullOrEmpty(jsid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jsid", jsid, jsidequal, jsidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jsid", "", jsidequal, jsidand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(jsid))
                {
                    if (jsidequal.Equals("="))
                    {
                        if (jsidand.Equals("and"))
                            where = where.And(auth_quanxian => auth_quanxian.JSID == int.Parse(jsid));
                        else
                            where = where.Or(auth_quanxian => auth_quanxian.JSID == int.Parse(jsid));
                    }
                    if (jsidequal.Equals(">"))
                    {
                        if (jsidand.Equals("and"))
                            where = where.And(auth_quanxian => auth_quanxian.JSID > int.Parse(jsid));
                        else
                            where = where.Or(auth_quanxian => auth_quanxian.JSID > int.Parse(jsid));
                    }
                    if (jsidequal.Equals("<"))
                    {
                        if (jsidand.Equals("and"))
                            where = where.And(auth_quanxian => auth_quanxian.JSID < int.Parse(jsid));
                        else
                            where = where.Or(auth_quanxian => auth_quanxian.JSID < int.Parse(jsid));
                    }
                }
                if (!string.IsNullOrEmpty(jsid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jsid", jsid, jsidequal, jsidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jsid", "", jsidequal, jsidand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(auth_quanxian => auth_quanxian.IsDelete == false);

            var tempData = ob_auth_quanxianservice.LoadSortEntities(where.Compile(), false, auth_quanxian => auth_quanxian.ID).ToPagedList<auth_quanxian>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.auth_quanxian = tempData;
            return View(tempData);
        }

        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string jsid = Request["jsid"] ?? "";
            string ryid = Request["ryid"] ?? "";
            string sxdate = Request["sxdate"] ?? "";
            string guanbisf = Request["guanbisf"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                auth_quanxian ob_auth_quanxian = new auth_quanxian();
                ob_auth_quanxian.JSID = jsid == "" ? 0 : int.Parse(jsid);
                ob_auth_quanxian.RYID = ryid == "" ? 0 : int.Parse(ryid);
                ob_auth_quanxian.SXDate = sxdate == "" ? DateTime.Now : DateTime.Parse(sxdate);
                ob_auth_quanxian.GuanbiSF = guanbisf == "" ? false : Boolean.Parse(guanbisf);
                ob_auth_quanxian.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_auth_quanxian.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_auth_quanxian = ob_auth_quanxianservice.AddEntity(ob_auth_quanxian);
                ViewBag.auth_quanxian = ob_auth_quanxian;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }

        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            auth_quanxian tempData = ob_auth_quanxianservice.GetEntityById(auth_quanxian => auth_quanxian.ID == id && auth_quanxian.IsDelete == false);
            ViewBag.auth_quanxian = tempData;
            if (tempData == null)
                return View();
            else
            {
                auth_quanxianViewModel auth_quanxianviewmodel = new auth_quanxianViewModel();
                auth_quanxianviewmodel.ID = tempData.ID;
                auth_quanxianviewmodel.JSID = tempData.JSID;
                auth_quanxianviewmodel.RYID = tempData.RYID;
                auth_quanxianviewmodel.SXDate = tempData.SXDate;
                auth_quanxianviewmodel.GuanbiSF = tempData.GuanbiSF;
                auth_quanxianviewmodel.MakeDate = tempData.MakeDate;
                auth_quanxianviewmodel.MakeMan = tempData.MakeMan;
                return View(auth_quanxianviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string jsid = Request["jsid"] ?? "";
            string ryid = Request["ryid"] ?? "";
            string sxdate = Request["sxdate"] ?? "";
            string guanbisf = Request["guanbisf"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                auth_quanxian p = ob_auth_quanxianservice.GetEntityById(auth_quanxian => auth_quanxian.ID == uid);
                p.JSID = jsid == "" ? 0 : int.Parse(jsid);
                p.RYID = ryid == "" ? 0 : int.Parse(ryid);
                p.SXDate = sxdate == "" ? DateTime.Now : DateTime.Parse(sxdate);
                p.GuanbiSF = guanbisf == "" ? false : Boolean.Parse(guanbisf);
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_auth_quanxianservice.UpdateEntity(p);
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
            auth_quanxian ob_auth_quanxian;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_auth_quanxian = ob_auth_quanxianservice.GetEntityById(auth_quanxian => auth_quanxian.ID == id && auth_quanxian.IsDelete == false);
                    ob_auth_quanxian.IsDelete = true;
                    ob_auth_quanxianservice.UpdateEntity(ob_auth_quanxian);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

