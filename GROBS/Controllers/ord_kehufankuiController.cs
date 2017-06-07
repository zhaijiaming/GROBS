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
    public class ord_kehufankuiController : Controller
    {
        private Iord_kehufankuiService ob_ord_kehufankuiservice = ServiceFactory.ord_kehufankuiservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_kehufankui_index";
            Expression<Func<ord_kehufankui, bool>> where = PredicateExtensionses.True<ord_kehufankui>();
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
                                        where = where.And(ord_kehufankui => ord_kehufankui.Leixing == int.Parse(leixing));
                                    else
                                        where = where.Or(ord_kehufankui => ord_kehufankui.Leixing == int.Parse(leixing));
                                }
                                if (leixingequal.Equals(">"))
                                {
                                    if (leixingand.Equals("and"))
                                        where = where.And(ord_kehufankui => ord_kehufankui.Leixing > int.Parse(leixing));
                                    else
                                        where = where.Or(ord_kehufankui => ord_kehufankui.Leixing > int.Parse(leixing));
                                }
                                if (leixingequal.Equals("<"))
                                {
                                    if (leixingand.Equals("and"))
                                        where = where.And(ord_kehufankui => ord_kehufankui.Leixing < int.Parse(leixing));
                                    else
                                        where = where.Or(ord_kehufankui => ord_kehufankui.Leixing < int.Parse(leixing));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_kehufankui => ord_kehufankui.IsDelete == false);

            var tempData = ob_ord_kehufankuiservice.LoadSortEntities(where.Compile(), false, ord_kehufankui => ord_kehufankui.ID).ToPagedList<ord_kehufankui>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_kehufankui = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_kehufankui_index";
            string page = "1";
            string leixing = Request["leixing"] ?? "";
            string leixingequal = Request["leixingequal"] ?? "";
            string leixingand = Request["leixingand"] ?? "";
            Expression<Func<ord_kehufankui, bool>> where = PredicateExtensionses.True<ord_kehufankui>();
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
                            where = where.And(ord_kehufankui => ord_kehufankui.Leixing == int.Parse(leixing));
                        else
                            where = where.Or(ord_kehufankui => ord_kehufankui.Leixing == int.Parse(leixing));
                    }
                    if (leixingequal.Equals(">"))
                    {
                        if (leixingand.Equals("and"))
                            where = where.And(ord_kehufankui => ord_kehufankui.Leixing > int.Parse(leixing));
                        else
                            where = where.Or(ord_kehufankui => ord_kehufankui.Leixing > int.Parse(leixing));
                    }
                    if (leixingequal.Equals("<"))
                    {
                        if (leixingand.Equals("and"))
                            where = where.And(ord_kehufankui => ord_kehufankui.Leixing < int.Parse(leixing));
                        else
                            where = where.Or(ord_kehufankui => ord_kehufankui.Leixing < int.Parse(leixing));
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
                            where = where.And(ord_kehufankui => ord_kehufankui.Leixing == int.Parse(leixing));
                        else
                            where = where.Or(ord_kehufankui => ord_kehufankui.Leixing == int.Parse(leixing));
                    }
                    if (leixingequal.Equals(">"))
                    {
                        if (leixingand.Equals("and"))
                            where = where.And(ord_kehufankui => ord_kehufankui.Leixing > int.Parse(leixing));
                        else
                            where = where.Or(ord_kehufankui => ord_kehufankui.Leixing > int.Parse(leixing));
                    }
                    if (leixingequal.Equals("<"))
                    {
                        if (leixingand.Equals("and"))
                            where = where.And(ord_kehufankui => ord_kehufankui.Leixing < int.Parse(leixing));
                        else
                            where = where.Or(ord_kehufankui => ord_kehufankui.Leixing < int.Parse(leixing));
                    }
                }
                if (!string.IsNullOrEmpty(leixing))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leixing", leixing, leixingequal, leixingand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leixing", "", leixingequal, leixingand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_kehufankui => ord_kehufankui.IsDelete == false);

            var tempData = ob_ord_kehufankuiservice.LoadSortEntities(where.Compile(), false, ord_kehufankui => ord_kehufankui.ID).ToPagedList<ord_kehufankui>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_kehufankui = tempData;
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
            string memo = Request["memo"] ?? "";
            string lianxi = Request["lianxi"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                ord_kehufankui ob_ord_kehufankui = new ord_kehufankui();
                ob_ord_kehufankui.Leixing = leixing == "" ? 0 : int.Parse(leixing);
                ob_ord_kehufankui.Memo = memo.Trim();
                ob_ord_kehufankui.Lianxi = lianxi.Trim();
                ob_ord_kehufankui.Col1 = col1.Trim();
                ob_ord_kehufankui.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_kehufankui.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_kehufankui = ob_ord_kehufankuiservice.AddEntity(ob_ord_kehufankui);
                ViewBag.ord_kehufankui = ob_ord_kehufankui;
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
            ord_kehufankui tempData = ob_ord_kehufankuiservice.GetEntityById(ord_kehufankui => ord_kehufankui.ID == id && ord_kehufankui.IsDelete == false);
            ViewBag.ord_kehufankui = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_kehufankuiViewModel ord_kehufankuiviewmodel = new ord_kehufankuiViewModel();
                ord_kehufankuiviewmodel.ID = tempData.ID;
                ord_kehufankuiviewmodel.Leixing = tempData.Leixing;
                ord_kehufankuiviewmodel.Memo = tempData.Memo;
                ord_kehufankuiviewmodel.Lianxi = tempData.Lianxi;
                ord_kehufankuiviewmodel.Col1 = tempData.Col1;
                ord_kehufankuiviewmodel.MakeDate = tempData.MakeDate;
                ord_kehufankuiviewmodel.MakeMan = tempData.MakeMan;
                return View(ord_kehufankuiviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string leixing = Request["leixing"] ?? "";
            string memo = Request["memo"] ?? "";
            string lianxi = Request["lianxi"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                ord_kehufankui p = ob_ord_kehufankuiservice.GetEntityById(ord_kehufankui => ord_kehufankui.ID == uid);
                p.Leixing = leixing == "" ? 0 : int.Parse(leixing);
                p.Memo = memo.Trim();
                p.Lianxi = lianxi.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_kehufankuiservice.UpdateEntity(p);
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
            ord_kehufankui ob_ord_kehufankui;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_kehufankui = ob_ord_kehufankuiservice.GetEntityById(ord_kehufankui => ord_kehufankui.ID == id && ord_kehufankui.IsDelete == false);
                    ob_ord_kehufankui.IsDelete = true;
                    ob_ord_kehufankuiservice.UpdateEntity(ob_ord_kehufankui);
                }
            }
            return RedirectToAction("Index");
        }
        public JsonResult AddFeedback()
        {
            int _userid = (int)Session["user_id"];
            int _custid = (int)Session["customer_id"];
            string _custname = (string)Session["customer_name"];
            var _lxr = Request["lxr"] ?? "";
            var _memo = Request["memo"] ?? "";
            var _lx = Request["lx"] ?? "0";

            if (string.IsNullOrEmpty(_memo))
                return Json(-1);
            ord_kehufankui _khfk = new ord_kehufankui();
            _khfk.Leixing = int.Parse(_lx);
            _khfk.Lianxi = _lxr;
            _khfk.Memo = _memo;
            _khfk.MakeMan = _custid;
            _khfk=ob_ord_kehufankuiservice.AddEntity(_khfk);
            if (_khfk == null)
                return Json(-2);
            return Json(1);
        }
    }
}

