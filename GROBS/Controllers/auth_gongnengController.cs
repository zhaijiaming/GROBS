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
    public class auth_gongnengController : Controller
    {
        private Iauth_gongnengService ob_auth_gongnengservice = ServiceFactory.auth_gongnengservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "auth_gongneng_index";
            Expression<Func<auth_gongneng, bool>> where = PredicateExtensionses.True<auth_gongneng>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "module":
                            string module = scld[1];
                            string moduleequal = scld[2];
                            string moduleand = scld[3];
                            if (!string.IsNullOrEmpty(module))
                            {
                                if (moduleequal.Equals("="))
                                {
                                    if (moduleand.Equals("and"))
                                        where = where.And(auth_gongneng => auth_gongneng.Module == module);
                                    else
                                        where = where.Or(auth_gongneng => auth_gongneng.Module == module);
                                }
                                if (moduleequal.Equals("like"))
                                {
                                    if (moduleand.Equals("and"))
                                        where = where.And(auth_gongneng => auth_gongneng.Module.Contains(module));
                                    else
                                        where = where.Or(auth_gongneng => auth_gongneng.Module.Contains(module));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(auth_gongneng => auth_gongneng.IsDelete == false);

            var tempData = ob_auth_gongnengservice.LoadSortEntities(where.Compile(), false, auth_gongneng => auth_gongneng.ID).ToPagedList<auth_gongneng>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.auth_gongneng = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "auth_gongneng_index";
            string page = "1";
            string module = Request["module"] ?? "";
            string moduleequal = Request["moduleequal"] ?? "";
            string moduleand = Request["moduleand"] ?? "";
            Expression<Func<auth_gongneng, bool>> where = PredicateExtensionses.True<auth_gongneng>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(module))
                {
                    if (moduleequal.Equals("="))
                    {
                        if (moduleand.Equals("and"))
                            where = where.And(auth_gongneng => auth_gongneng.Module == module);
                        else
                            where = where.Or(auth_gongneng => auth_gongneng.Module == module);
                    }
                    if (moduleequal.Equals("like"))
                    {
                        if (moduleand.Equals("and"))
                            where = where.And(auth_gongneng => auth_gongneng.Module.Contains(module));
                        else
                            where = where.Or(auth_gongneng => auth_gongneng.Module.Contains(module));
                    }
                }
                if (!string.IsNullOrEmpty(module))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "module", module, moduleequal, moduleand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "module", "", moduleequal, moduleand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(module))
                {
                    if (moduleequal.Equals("="))
                    {
                        if (moduleand.Equals("and"))
                            where = where.And(auth_gongneng => auth_gongneng.Module == module);
                        else
                            where = where.Or(auth_gongneng => auth_gongneng.Module == module);
                    }
                    if (moduleequal.Equals("like"))
                    {
                        if (moduleand.Equals("and"))
                            where = where.And(auth_gongneng => auth_gongneng.Module.Contains(module));
                        else
                            where = where.Or(auth_gongneng => auth_gongneng.Module.Contains(module));
                    }
                }
                if (!string.IsNullOrEmpty(module))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "module", module, moduleequal, moduleand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "module", "", moduleequal, moduleand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(auth_gongneng => auth_gongneng.IsDelete == false);

            var tempData = ob_auth_gongnengservice.LoadSortEntities(where.Compile(), false, auth_gongneng => auth_gongneng.ID).ToPagedList<auth_gongneng>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.auth_gongneng = tempData;
            return View(tempData);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string module = Request["module"] ?? "";
            string controller = Request["controller"] ?? "";
            string function = Request["function"] ?? "";
            string name = Request["name"] ?? "";
            string grade = Request["grade"] ?? "";
            string forbid = Request["forbid"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                auth_gongneng ob_auth_gongneng = new auth_gongneng();
                ob_auth_gongneng.Module = module.Trim();
                ob_auth_gongneng.Controller = controller.Trim();
                ob_auth_gongneng.Function = function.Trim();
                ob_auth_gongneng.Name = name.Trim();
                ob_auth_gongneng.Grade = grade == "" ? 0 : int.Parse(grade);
                ob_auth_gongneng.Forbid = forbid == "" ? false : Boolean.Parse(forbid);
                ob_auth_gongneng.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_auth_gongneng.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_auth_gongneng = ob_auth_gongnengservice.AddEntity(ob_auth_gongneng);
                ViewBag.auth_gongneng = ob_auth_gongneng;
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
            auth_gongneng tempData = ob_auth_gongnengservice.GetEntityById(auth_gongneng => auth_gongneng.ID == id && auth_gongneng.IsDelete == false);
            ViewBag.auth_gongneng = tempData;
            if (tempData == null)
                return View();
            else
            {
                auth_gongnengViewModel auth_gongnengviewmodel = new auth_gongnengViewModel();
                auth_gongnengviewmodel.ID = tempData.ID;
                auth_gongnengviewmodel.Module = tempData.Module;
                auth_gongnengviewmodel.Controller = tempData.Controller;
                auth_gongnengviewmodel.Function = tempData.Function;
                auth_gongnengviewmodel.Name = tempData.Name;
                auth_gongnengviewmodel.Grade = tempData.Grade;
                auth_gongnengviewmodel.Forbid = tempData.Forbid;
                auth_gongnengviewmodel.MakeDate = tempData.MakeDate;
                auth_gongnengviewmodel.MakeMan = tempData.MakeMan;
                return View(auth_gongnengviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string module = Request["module"] ?? "";
            string controller = Request["controller"] ?? "";
            string function = Request["function"] ?? "";
            string name = Request["name"] ?? "";
            string grade = Request["grade"] ?? "";
            string forbid = Request["forbid"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                auth_gongneng p = ob_auth_gongnengservice.GetEntityById(auth_gongneng => auth_gongneng.ID == uid);
                p.Module = module.Trim();
                p.Controller = controller.Trim();
                p.Function = function.Trim();
                p.Name = name.Trim();
                p.Grade = grade == "" ? 0 : int.Parse(grade);
                p.Forbid = forbid == "" ? false : Boolean.Parse(forbid);
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_auth_gongnengservice.UpdateEntity(p);
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
            auth_gongneng ob_auth_gongneng;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_auth_gongneng = ob_auth_gongnengservice.GetEntityById(auth_gongneng => auth_gongneng.ID == id && auth_gongneng.IsDelete == false);
                    ob_auth_gongneng.IsDelete = true;
                    ob_auth_gongnengservice.UpdateEntity(ob_auth_gongneng);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

