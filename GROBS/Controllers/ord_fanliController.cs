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
    public class ord_fanliController : Controller
    {
        private Iord_fanliService ob_ord_fanliservice = ServiceFactory.ord_fanliservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_fanli_index";
            Expression<Func<ord_fanli, bool>> where = PredicateExtensionses.True<ord_fanli>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "khid":
                            string khid = scld[1];
                            string khidequal = scld[2];
                            string khidand = scld[3];
                            if (!string.IsNullOrEmpty(khid))
                            {
                                if (khidequal.Equals("="))
                                {
                                    if (khidand.Equals("and"))
                                        where = where.And(ord_fanli => ord_fanli.KHID == int.Parse(khid));
                                    else
                                        where = where.Or(ord_fanli => ord_fanli.KHID == int.Parse(khid));
                                }
                                if (khidequal.Equals(">"))
                                {
                                    if (khidand.Equals("and"))
                                        where = where.And(ord_fanli => ord_fanli.KHID > int.Parse(khid));
                                    else
                                        where = where.Or(ord_fanli => ord_fanli.KHID > int.Parse(khid));
                                }
                                if (khidequal.Equals("<"))
                                {
                                    if (khidand.Equals("and"))
                                        where = where.And(ord_fanli => ord_fanli.KHID < int.Parse(khid));
                                    else
                                        where = where.Or(ord_fanli => ord_fanli.KHID < int.Parse(khid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_fanli => ord_fanli.IsDelete == false);

            var tempData = ob_ord_fanliservice.LoadSortEntities(where.Compile(), false, ord_fanli => ord_fanli.ID).ToPagedList<ord_fanli>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_fanli = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_fanli_index";
            string page = "1";
            string khid = Request["khid"] ?? "";
            string khidequal = Request["khidequal"] ?? "";
            string khidand = Request["khidand"] ?? "";
            Expression<Func<ord_fanli, bool>> where = PredicateExtensionses.True<ord_fanli>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(khid))
                {
                    if (khidequal.Equals("="))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_fanli => ord_fanli.KHID == int.Parse(khid));
                        else
                            where = where.Or(ord_fanli => ord_fanli.KHID == int.Parse(khid));
                    }
                    if (khidequal.Equals(">"))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_fanli => ord_fanli.KHID > int.Parse(khid));
                        else
                            where = where.Or(ord_fanli => ord_fanli.KHID > int.Parse(khid));
                    }
                    if (khidequal.Equals("<"))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_fanli => ord_fanli.KHID < int.Parse(khid));
                        else
                            where = where.Or(ord_fanli => ord_fanli.KHID < int.Parse(khid));
                    }
                }
                if (!string.IsNullOrEmpty(khid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", khid, khidequal, khidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", "", khidequal, khidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(khid))
                {
                    if (khidequal.Equals("="))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_fanli => ord_fanli.KHID == int.Parse(khid));
                        else
                            where = where.Or(ord_fanli => ord_fanli.KHID == int.Parse(khid));
                    }
                    if (khidequal.Equals(">"))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_fanli => ord_fanli.KHID > int.Parse(khid));
                        else
                            where = where.Or(ord_fanli => ord_fanli.KHID > int.Parse(khid));
                    }
                    if (khidequal.Equals("<"))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_fanli => ord_fanli.KHID < int.Parse(khid));
                        else
                            where = where.Or(ord_fanli => ord_fanli.KHID < int.Parse(khid));
                    }
                }
                if (!string.IsNullOrEmpty(khid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", khid, khidequal, khidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", "", khidequal, khidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_fanli => ord_fanli.IsDelete == false);

            var tempData = ob_ord_fanliservice.LoadSortEntities(where.Compile(), false, ord_fanli => ord_fanli.ID).ToPagedList<ord_fanli>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_fanli = tempData;
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
            string khid = Request["khid"] ?? "";
            string zonge = Request["zonge"] ?? "";
            string keyong = Request["keyong"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                ord_fanli ob_ord_fanli = new ord_fanli();
                ob_ord_fanli.KHID = khid == "" ? 0 : int.Parse(khid);
                ob_ord_fanli.Zonge = zonge == "" ? 0 : float.Parse(zonge);
                ob_ord_fanli.Keyong = keyong == "" ? 0 : float.Parse(keyong);
                ob_ord_fanli.Col1 = col1.Trim();
                ob_ord_fanli.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_fanli.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_fanli = ob_ord_fanliservice.AddEntity(ob_ord_fanli);
                ViewBag.ord_fanli = ob_ord_fanli;
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
            ord_fanli tempData = ob_ord_fanliservice.GetEntityById(ord_fanli => ord_fanli.ID == id && ord_fanli.IsDelete == false);
            ViewBag.ord_fanli = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_fanliViewModel ord_fanliviewmodel = new ord_fanliViewModel();
                ord_fanliviewmodel.ID = tempData.ID;
                ord_fanliviewmodel.KHID = tempData.KHID;
                ord_fanliviewmodel.Zonge = tempData.Zonge;
                ord_fanliviewmodel.Keyong = tempData.Keyong;
                ord_fanliviewmodel.Col1 = tempData.Col1;
                ord_fanliviewmodel.MakeDate = tempData.MakeDate;
                ord_fanliviewmodel.MakeMan = tempData.MakeMan;
                return View(ord_fanliviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string khid = Request["khid"] ?? "";
            string zonge = Request["zonge"] ?? "";
            string keyong = Request["keyong"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                ord_fanli p = ob_ord_fanliservice.GetEntityById(ord_fanli => ord_fanli.ID == uid);
                p.KHID = khid == "" ? 0 : int.Parse(khid);
                p.Zonge = zonge == "" ? 0 : float.Parse(zonge);
                p.Keyong = keyong == "" ? 0 : float.Parse(keyong);
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_fanliservice.UpdateEntity(p);
                ViewBag.saveok = ViewAddTag.ModifyOk;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.saveok = ViewAddTag.ModifyNo;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete()
        {
            string sdel = Request["del"] ?? "";
            int id;
            ord_fanli ob_ord_fanli;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_fanli = ob_ord_fanliservice.GetEntityById(ord_fanli => ord_fanli.ID == id && ord_fanli.IsDelete == false);
                    ob_ord_fanli.IsDelete = true;
                    ob_ord_fanliservice.UpdateEntity(ob_ord_fanli);
                }
            }
            return RedirectToAction("Index");
        }
        [OutputCache(Duration =30)]
        public ActionResult CustomerProfit(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_fanli_customerprofit";
            Expression<Func<ord_fanli, bool>> where = PredicateExtensionses.True<ord_fanli>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "khid":
                            string khid = scld[1];
                            string khidequal = scld[2];
                            string khidand = scld[3];
                            if (!string.IsNullOrEmpty(khid))
                            {
                                if (khidequal.Equals("="))
                                {
                                    if (khidand.Equals("and"))
                                        where = where.And(ord_fanli => ord_fanli.KHID == int.Parse(khid));
                                    else
                                        where = where.Or(ord_fanli => ord_fanli.KHID == int.Parse(khid));
                                }
                                if (khidequal.Equals(">"))
                                {
                                    if (khidand.Equals("and"))
                                        where = where.And(ord_fanli => ord_fanli.KHID > int.Parse(khid));
                                    else
                                        where = where.Or(ord_fanli => ord_fanli.KHID > int.Parse(khid));
                                }
                                if (khidequal.Equals("<"))
                                {
                                    if (khidand.Equals("and"))
                                        where = where.And(ord_fanli => ord_fanli.KHID < int.Parse(khid));
                                    else
                                        where = where.Or(ord_fanli => ord_fanli.KHID < int.Parse(khid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_fanli => ord_fanli.IsDelete == false);

            var tempData = ob_ord_fanliservice.LoadSortEntities(where.Compile(), false, ord_fanli => ord_fanli.ID).ToPagedList<ord_fanli>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_fanli = tempData;
            return View(tempData);
        }
        [HttpPost]
        [OutputCache(Duration =30)]
        public ActionResult CustomerProfit()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_fanli_customerprofit";
            string page = "1";
            string khid = Request["khid"] ?? "";
            string khidequal = Request["khidequal"] ?? "";
            string khidand = Request["khidand"] ?? "";
            Expression<Func<ord_fanli, bool>> where = PredicateExtensionses.True<ord_fanli>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(khid))
                {
                    if (khidequal.Equals("="))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_fanli => ord_fanli.KHID == int.Parse(khid));
                        else
                            where = where.Or(ord_fanli => ord_fanli.KHID == int.Parse(khid));
                    }
                    if (khidequal.Equals(">"))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_fanli => ord_fanli.KHID > int.Parse(khid));
                        else
                            where = where.Or(ord_fanli => ord_fanli.KHID > int.Parse(khid));
                    }
                    if (khidequal.Equals("<"))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_fanli => ord_fanli.KHID < int.Parse(khid));
                        else
                            where = where.Or(ord_fanli => ord_fanli.KHID < int.Parse(khid));
                    }
                }
                if (!string.IsNullOrEmpty(khid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", khid, khidequal, khidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", "", khidequal, khidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(khid))
                {
                    if (khidequal.Equals("="))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_fanli => ord_fanli.KHID == int.Parse(khid));
                        else
                            where = where.Or(ord_fanli => ord_fanli.KHID == int.Parse(khid));
                    }
                    if (khidequal.Equals(">"))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_fanli => ord_fanli.KHID > int.Parse(khid));
                        else
                            where = where.Or(ord_fanli => ord_fanli.KHID > int.Parse(khid));
                    }
                    if (khidequal.Equals("<"))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_fanli => ord_fanli.KHID < int.Parse(khid));
                        else
                            where = where.Or(ord_fanli => ord_fanli.KHID < int.Parse(khid));
                    }
                }
                if (!string.IsNullOrEmpty(khid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", khid, khidequal, khidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", "", khidequal, khidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_fanli => ord_fanli.IsDelete == false);

            var tempData = ob_ord_fanliservice.LoadSortEntities(where.Compile(), false, ord_fanli => ord_fanli.ID).ToPagedList<ord_fanli>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_fanli = tempData;
            return View(tempData);
        }
    }
}

