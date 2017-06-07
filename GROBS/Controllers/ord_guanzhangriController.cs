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
    public class ord_guanzhangriController : Controller
    {
        private Iord_guanzhangriService ob_ord_guanzhangriservice = ServiceFactory.ord_guanzhangriservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_guanzhangri_index";
            Expression<Func<ord_guanzhangri, bool>> where = PredicateExtensionses.True<ord_guanzhangri>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "leixing":
                            string leixing = scld[1];
                            string leixingequal = scld[2];
                            string leixingand = scld[3];
                            if (!string.IsNullOrEmpty(leixing))
                            {
                                if (leixingequal.Equals("="))
                                {
                                    if (leixingand.Equals("and"))
                                        where = where.And(ord_guanzhangri => ord_guanzhangri.Leixing == int.Parse(leixing));
                                    else
                                        where = where.Or(ord_guanzhangri => ord_guanzhangri.Leixing == int.Parse(leixing));
                                }
                                if (leixingequal.Equals(">"))
                                {
                                    if (leixingand.Equals("and"))
                                        where = where.And(ord_guanzhangri => ord_guanzhangri.Leixing > int.Parse(leixing));
                                    else
                                        where = where.Or(ord_guanzhangri => ord_guanzhangri.Leixing > int.Parse(leixing));
                                }
                                if (leixingequal.Equals("<"))
                                {
                                    if (leixingand.Equals("and"))
                                        where = where.And(ord_guanzhangri => ord_guanzhangri.Leixing < int.Parse(leixing));
                                    else
                                        where = where.Or(ord_guanzhangri => ord_guanzhangri.Leixing < int.Parse(leixing));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_guanzhangri => ord_guanzhangri.IsDelete == false);

            var tempData = ob_ord_guanzhangriservice.LoadSortEntities(where.Compile(), false, ord_guanzhangri => ord_guanzhangri.ID).ToPagedList<ord_guanzhangri>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_guanzhangri = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_guanzhangri_index";
            string page = "1";
            string leixing = Request["leixing"] ?? "";
            string leixingequal = Request["leixingequal"] ?? "";
            string leixingand = Request["leixingand"] ?? "";
            Expression<Func<ord_guanzhangri, bool>> where = PredicateExtensionses.True<ord_guanzhangri>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(leixing))
                {
                    if (leixingequal.Equals("="))
                    {
                        if (leixingand.Equals("and"))
                            where = where.And(ord_guanzhangri => ord_guanzhangri.Leixing == int.Parse(leixing));
                        else
                            where = where.Or(ord_guanzhangri => ord_guanzhangri.Leixing == int.Parse(leixing));
                    }
                    if (leixingequal.Equals(">"))
                    {
                        if (leixingand.Equals("and"))
                            where = where.And(ord_guanzhangri => ord_guanzhangri.Leixing > int.Parse(leixing));
                        else
                            where = where.Or(ord_guanzhangri => ord_guanzhangri.Leixing > int.Parse(leixing));
                    }
                    if (leixingequal.Equals("<"))
                    {
                        if (leixingand.Equals("and"))
                            where = where.And(ord_guanzhangri => ord_guanzhangri.Leixing < int.Parse(leixing));
                        else
                            where = where.Or(ord_guanzhangri => ord_guanzhangri.Leixing < int.Parse(leixing));
                    }
                }
                if (!string.IsNullOrEmpty(leixing))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leixing", leixing, leixingequal, leixingand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leixing", "", leixingequal, leixingand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(leixing))
                {
                    if (leixingequal.Equals("="))
                    {
                        if (leixingand.Equals("and"))
                            where = where.And(ord_guanzhangri => ord_guanzhangri.Leixing == int.Parse(leixing));
                        else
                            where = where.Or(ord_guanzhangri => ord_guanzhangri.Leixing == int.Parse(leixing));
                    }
                    if (leixingequal.Equals(">"))
                    {
                        if (leixingand.Equals("and"))
                            where = where.And(ord_guanzhangri => ord_guanzhangri.Leixing > int.Parse(leixing));
                        else
                            where = where.Or(ord_guanzhangri => ord_guanzhangri.Leixing > int.Parse(leixing));
                    }
                    if (leixingequal.Equals("<"))
                    {
                        if (leixingand.Equals("and"))
                            where = where.And(ord_guanzhangri => ord_guanzhangri.Leixing < int.Parse(leixing));
                        else
                            where = where.Or(ord_guanzhangri => ord_guanzhangri.Leixing < int.Parse(leixing));
                    }
                }
                if (!string.IsNullOrEmpty(leixing))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leixing", leixing, leixingequal, leixingand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leixing", "", leixingequal, leixingand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_guanzhangri => ord_guanzhangri.IsDelete == false);

            var tempData = ob_ord_guanzhangriservice.LoadSortEntities(where.Compile(), false, ord_guanzhangri => ord_guanzhangri.ID).ToPagedList<ord_guanzhangri>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_guanzhangri = tempData;
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
            string leixing = Request["leixing"] ?? "";
            string guanzhangri = Request["guanzhangri"] ?? "";
            string memo = Request["memo"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                ord_guanzhangri ob_ord_guanzhangri = new ord_guanzhangri();
                ob_ord_guanzhangri.Leixing = leixing == "" ? 0 : int.Parse(leixing);
                ob_ord_guanzhangri.Guanzhangri = guanzhangri == "" ? DateTime.Now : DateTime.Parse(guanzhangri);
                ob_ord_guanzhangri.Memo = memo.Trim();
                ob_ord_guanzhangri.Col1 = col1.Trim();
                ob_ord_guanzhangri.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_guanzhangri.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_guanzhangri = ob_ord_guanzhangriservice.AddEntity(ob_ord_guanzhangri);
                ViewBag.ord_guanzhangri = ob_ord_guanzhangri;
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
            ord_guanzhangri tempData = ob_ord_guanzhangriservice.GetEntityById(ord_guanzhangri => ord_guanzhangri.ID == id && ord_guanzhangri.IsDelete == false);
            ViewBag.ord_guanzhangri = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_guanzhangriViewModel ord_guanzhangriviewmodel = new ord_guanzhangriViewModel();
                ord_guanzhangriviewmodel.ID = tempData.ID;
                ord_guanzhangriviewmodel.Leixing = tempData.Leixing;
                ord_guanzhangriviewmodel.Guanzhangri = tempData.Guanzhangri;
                ord_guanzhangriviewmodel.Memo = tempData.Memo;
                ord_guanzhangriviewmodel.Col1 = tempData.Col1;
                ord_guanzhangriviewmodel.MakeDate = tempData.MakeDate;
                ord_guanzhangriviewmodel.MakeMan = tempData.MakeMan;
                return View(ord_guanzhangriviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string leixing = Request["leixing"] ?? "";
            string guanzhangri = Request["guanzhangri"] ?? "";
            string memo = Request["memo"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                ord_guanzhangri p = ob_ord_guanzhangriservice.GetEntityById(ord_guanzhangri => ord_guanzhangri.ID == uid);
                p.Leixing = leixing == "" ? 0 : int.Parse(leixing);
                p.Guanzhangri = guanzhangri == "" ? DateTime.Now : DateTime.Parse(guanzhangri);
                p.Memo = memo.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_guanzhangriservice.UpdateEntity(p);
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
            ord_guanzhangri ob_ord_guanzhangri;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_guanzhangri = ob_ord_guanzhangriservice.GetEntityById(ord_guanzhangri => ord_guanzhangri.ID == id && ord_guanzhangri.IsDelete == false);
                    ob_ord_guanzhangri.IsDelete = true;
                    ob_ord_guanzhangriservice.UpdateEntity(ob_ord_guanzhangri);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

