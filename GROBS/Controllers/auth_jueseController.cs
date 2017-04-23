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
    public class auth_jueseController : Controller
    {
        private Iauth_jueseService ob_auth_jueseservice = ServiceFactory.auth_jueseservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "auth_juese_index";
            Expression<Func<auth_juese, bool>> where = PredicateExtensionses.True<auth_juese>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "rolename":
                            string rolename = scld[1];
                            string rolenameequal = scld[2];
                            string rolenameand = scld[3];
                            if (!string.IsNullOrEmpty(rolename))
                            {
                                if (rolenameequal.Equals("="))
                                {
                                    if (rolenameand.Equals("and"))
                                        where = where.And(auth_juese => auth_juese.RoleName == rolename);
                                    else
                                        where = where.Or(auth_juese => auth_juese.RoleName == rolename);
                                }
                                if (rolenameequal.Equals("like"))
                                {
                                    if (rolenameand.Equals("and"))
                                        where = where.And(auth_juese => auth_juese.RoleName.Contains(rolename));
                                    else
                                        where = where.Or(auth_juese => auth_juese.RoleName.Contains(rolename));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(auth_juese => auth_juese.IsDelete == false);

            var tempData = ob_auth_jueseservice.LoadSortEntities(where.Compile(), false, auth_juese => auth_juese.ID).ToPagedList<auth_juese>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.auth_juese = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "auth_juese_index";
            string page = "1";
            string rolename = Request["rolename"] ?? "";
            string rolenameequal = Request["rolenameequal"] ?? "";
            string rolenameand = Request["rolenameand"] ?? "";
            Expression<Func<auth_juese, bool>> where = PredicateExtensionses.True<auth_juese>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(rolename))
                {
                    if (rolenameequal.Equals("="))
                    {
                        if (rolenameand.Equals("and"))
                            where = where.And(auth_juese => auth_juese.RoleName == rolename);
                        else
                            where = where.Or(auth_juese => auth_juese.RoleName == rolename);
                    }
                    if (rolenameequal.Equals("like"))
                    {
                        if (rolenameand.Equals("and"))
                            where = where.And(auth_juese => auth_juese.RoleName.Contains(rolename));
                        else
                            where = where.Or(auth_juese => auth_juese.RoleName.Contains(rolename));
                    }
                }
                if (!string.IsNullOrEmpty(rolename))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rolename", rolename, rolenameequal, rolenameand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rolename", "", rolenameequal, rolenameand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(rolename))
                {
                    if (rolenameequal.Equals("="))
                    {
                        if (rolenameand.Equals("and"))
                            where = where.And(auth_juese => auth_juese.RoleName == rolename);
                        else
                            where = where.Or(auth_juese => auth_juese.RoleName == rolename);
                    }
                    if (rolenameequal.Equals("like"))
                    {
                        if (rolenameand.Equals("and"))
                            where = where.And(auth_juese => auth_juese.RoleName.Contains(rolename));
                        else
                            where = where.Or(auth_juese => auth_juese.RoleName.Contains(rolename));
                    }
                }
                if (!string.IsNullOrEmpty(rolename))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rolename", rolename, rolenameequal, rolenameand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "rolename", "", rolenameequal, rolenameand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(auth_juese => auth_juese.IsDelete == false);

            var tempData = ob_auth_jueseservice.LoadSortEntities(where.Compile(), false, auth_juese => auth_juese.ID).ToPagedList<auth_juese>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.auth_juese = tempData;
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
            string rolename = Request["rolename"] ?? "";
            string roledescripe = Request["roledescripe"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string isclose = Request["isclose"] ?? "";
            try
            {
                auth_juese ob_auth_juese = new auth_juese();
                ob_auth_juese.RoleName = rolename.Trim();
                ob_auth_juese.RoleDescripe = roledescripe.Trim();
                ob_auth_juese.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_auth_juese.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_auth_juese.IsClose = isclose == "" ? false : Boolean.Parse(isclose);
                ob_auth_juese = ob_auth_jueseservice.AddEntity(ob_auth_juese);
                ViewBag.auth_juese = ob_auth_juese;
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
            auth_juese tempData = ob_auth_jueseservice.GetEntityById(auth_juese => auth_juese.ID == id && auth_juese.IsDelete == false);
            ViewBag.auth_juese = tempData;
            if (tempData == null)
                return View();
            else
            {
                auth_jueseViewModel auth_jueseviewmodel = new auth_jueseViewModel();
                auth_jueseviewmodel.ID = tempData.ID;
                auth_jueseviewmodel.RoleName = tempData.RoleName;
                auth_jueseviewmodel.RoleDescripe = tempData.RoleDescripe;
                auth_jueseviewmodel.MakeDate = tempData.MakeDate;
                auth_jueseviewmodel.MakeMan = tempData.MakeMan;
                auth_jueseviewmodel.IsClose = tempData.IsClose;
                return View(auth_jueseviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string rolename = Request["rolename"] ?? "";
            string roledescripe = Request["roledescripe"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string isclose = Request["isclose"] ?? "";
            int uid = int.Parse(id);
            try
            {
                auth_juese p = ob_auth_jueseservice.GetEntityById(auth_juese => auth_juese.ID == uid);
                p.RoleName = rolename.Trim();
                p.RoleDescripe = roledescripe.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.IsClose = isclose == "" ? false : Boolean.Parse(isclose);
                ob_auth_jueseservice.UpdateEntity(p);
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
            auth_juese ob_auth_juese;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_auth_juese = ob_auth_jueseservice.GetEntityById(auth_juese => auth_juese.ID == id && auth_juese.IsDelete == false);
                    ob_auth_juese.IsDelete = true;
                    ob_auth_jueseservice.UpdateEntity(ob_auth_juese);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

