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
    public class ord_fanlixfController : Controller
    {
        private Iord_fanlixfService ob_ord_fanlixfservice = ServiceFactory.ord_fanlixfservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_fanlixf_index";
            Expression<Func<ord_fanlixf, bool>> where = PredicateExtensionses.True<ord_fanlixf>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "ddid":
                            string ddid = scld[1];
                            string ddidequal = scld[2];
                            string ddidand = scld[3];
                            if (!string.IsNullOrEmpty(ddid))
                            {
                                if (ddidequal.Equals("="))
                                {
                                    if (ddidand.Equals("and"))
                                        where = where.And(ord_fanlixf => ord_fanlixf.DDID == int.Parse(ddid));
                                    else
                                        where = where.Or(ord_fanlixf => ord_fanlixf.DDID == int.Parse(ddid));
                                }
                                if (ddidequal.Equals(">"))
                                {
                                    if (ddidand.Equals("and"))
                                        where = where.And(ord_fanlixf => ord_fanlixf.DDID > int.Parse(ddid));
                                    else
                                        where = where.Or(ord_fanlixf => ord_fanlixf.DDID > int.Parse(ddid));
                                }
                                if (ddidequal.Equals("<"))
                                {
                                    if (ddidand.Equals("and"))
                                        where = where.And(ord_fanlixf => ord_fanlixf.DDID < int.Parse(ddid));
                                    else
                                        where = where.Or(ord_fanlixf => ord_fanlixf.DDID < int.Parse(ddid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_fanlixf => ord_fanlixf.IsDelete == false);

            var tempData = ob_ord_fanlixfservice.LoadSortEntities(where.Compile(), false, ord_fanlixf => ord_fanlixf.ID).ToPagedList<ord_fanlixf>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_fanlixf = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_fanlixf_index";
            string page = "1";
            string ddid = Request["ddid"] ?? "";
            string ddidequal = Request["ddidequal"] ?? "";
            string ddidand = Request["ddidand"] ?? "";
            Expression<Func<ord_fanlixf, bool>> where = PredicateExtensionses.True<ord_fanlixf>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(ddid))
                {
                    if (ddidequal.Equals("="))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_fanlixf => ord_fanlixf.DDID == int.Parse(ddid));
                        else
                            where = where.Or(ord_fanlixf => ord_fanlixf.DDID == int.Parse(ddid));
                    }
                    if (ddidequal.Equals(">"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_fanlixf => ord_fanlixf.DDID > int.Parse(ddid));
                        else
                            where = where.Or(ord_fanlixf => ord_fanlixf.DDID > int.Parse(ddid));
                    }
                    if (ddidequal.Equals("<"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_fanlixf => ord_fanlixf.DDID < int.Parse(ddid));
                        else
                            where = where.Or(ord_fanlixf => ord_fanlixf.DDID < int.Parse(ddid));
                    }
                }
                if (!string.IsNullOrEmpty(ddid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddid", ddid, ddidequal, ddidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddid", "", ddidequal, ddidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(ddid))
                {
                    if (ddidequal.Equals("="))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_fanlixf => ord_fanlixf.DDID == int.Parse(ddid));
                        else
                            where = where.Or(ord_fanlixf => ord_fanlixf.DDID == int.Parse(ddid));
                    }
                    if (ddidequal.Equals(">"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_fanlixf => ord_fanlixf.DDID > int.Parse(ddid));
                        else
                            where = where.Or(ord_fanlixf => ord_fanlixf.DDID > int.Parse(ddid));
                    }
                    if (ddidequal.Equals("<"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_fanlixf => ord_fanlixf.DDID < int.Parse(ddid));
                        else
                            where = where.Or(ord_fanlixf => ord_fanlixf.DDID < int.Parse(ddid));
                    }
                }
                if (!string.IsNullOrEmpty(ddid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddid", ddid, ddidequal, ddidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddid", "", ddidequal, ddidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_fanlixf => ord_fanlixf.IsDelete == false);

            var tempData = ob_ord_fanlixfservice.LoadSortEntities(where.Compile(), false, ord_fanlixf => ord_fanlixf.ID).ToPagedList<ord_fanlixf>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_fanlixf = tempData;
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
            string ddid = Request["ddid"] ?? "";
            string khid = Request["khid"] ?? "";
            string xfje = Request["xfje"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                ord_fanlixf ob_ord_fanlixf = new ord_fanlixf();
                ob_ord_fanlixf.DDID = ddid == "" ? 0 : int.Parse(ddid);
                ob_ord_fanlixf.KHID = khid == "" ? 0 : int.Parse(khid);
                ob_ord_fanlixf.XFJE = xfje == "" ? 0 : float.Parse(xfje);
                ob_ord_fanlixf.Col1 = col1.Trim();
                ob_ord_fanlixf.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_fanlixf.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_fanlixf = ob_ord_fanlixfservice.AddEntity(ob_ord_fanlixf);
                ViewBag.ord_fanlixf = ob_ord_fanlixf;
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
            ord_fanlixf tempData = ob_ord_fanlixfservice.GetEntityById(ord_fanlixf => ord_fanlixf.ID == id && ord_fanlixf.IsDelete == false);
            ViewBag.ord_fanlixf = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_fanlixfViewModel ord_fanlixfviewmodel = new ord_fanlixfViewModel();
                ord_fanlixfviewmodel.ID = tempData.ID;
                ord_fanlixfviewmodel.DDID = tempData.DDID;
                ord_fanlixfviewmodel.KHID = tempData.KHID;
                ord_fanlixfviewmodel.XFJE = tempData.XFJE;
                ord_fanlixfviewmodel.Col1 = tempData.Col1;
                ord_fanlixfviewmodel.MakeDate = tempData.MakeDate;
                ord_fanlixfviewmodel.MakeMan = tempData.MakeMan;
                return View(ord_fanlixfviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string ddid = Request["ddid"] ?? "";
            string khid = Request["khid"] ?? "";
            string xfje = Request["xfje"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                ord_fanlixf p = ob_ord_fanlixfservice.GetEntityById(ord_fanlixf => ord_fanlixf.ID == uid);
                p.DDID = ddid == "" ? 0 : int.Parse(ddid);
                p.KHID = khid == "" ? 0 : int.Parse(khid);
                p.XFJE = xfje == "" ? 0 : float.Parse(xfje);
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_fanlixfservice.UpdateEntity(p);
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
            ord_fanlixf ob_ord_fanlixf;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_fanlixf = ob_ord_fanlixfservice.GetEntityById(ord_fanlixf => ord_fanlixf.ID == id && ord_fanlixf.IsDelete == false);
                    ob_ord_fanlixf.IsDelete = true;
                    ob_ord_fanlixfservice.UpdateEntity(ob_ord_fanlixf);
                }
            }
            return RedirectToAction("Index");
        }
        public JsonResult getFanliXiaoFeiWithQuery()
        {
            string khid = Request["khid"] ?? "";
            string req_ddid = Request["req_ddid"] ?? "";
            string req_xfje = Request["req_xfje"] ?? "";

            Expression<Func<ord_fanlixf, bool>> where = PredicateExtensionses.True<ord_fanlixf>();
            if (!string.IsNullOrEmpty(khid))
                where = where.And(p => p.KHID == int.Parse(khid));
            if (!string.IsNullOrEmpty(req_ddid))
                where = where.And(p => p.DDID == int.Parse(req_ddid));
            if (!string.IsNullOrEmpty(req_xfje))
                where = where.And(p => p.XFJE == int.Parse(req_xfje));
            where = where.And(p => p.IsDelete == false);

            var tempData = ServiceFactory.ord_fanlixfservice.LoadFanlixf(int.Parse(khid));
            if (tempData == null)
                return Json(-1);
            return Json(tempData);

        }
    }
}

