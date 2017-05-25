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
    public class ord_qianhuosqController : Controller
    {
        private Iord_qianhuosqService ob_ord_qianhuosqservice = ServiceFactory.ord_qianhuosqservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_qianhuosq_index";
            Expression<Func<ord_qianhuosq, bool>> where = PredicateExtensionses.True<ord_qianhuosq>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "shenqinbh":
                            string shenqinbh = scld[1];
                            string shenqinbhequal = scld[2];
                            string shenqinbhand = scld[3];
                            if (!string.IsNullOrEmpty(shenqinbh))
                            {
                                if (shenqinbhequal.Equals("="))
                                {
                                    if (shenqinbhand.Equals("and"))
                                        where = where.And(ord_qianhuosq => ord_qianhuosq.ShenqinBH == shenqinbh);
                                    else
                                        where = where.Or(ord_qianhuosq => ord_qianhuosq.ShenqinBH == shenqinbh);
                                }
                                if (shenqinbhequal.Equals("like"))
                                {
                                    if (shenqinbhand.Equals("and"))
                                        where = where.And(ord_qianhuosq => ord_qianhuosq.ShenqinBH.Contains(shenqinbh));
                                    else
                                        where = where.Or(ord_qianhuosq => ord_qianhuosq.ShenqinBH.Contains(shenqinbh));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_qianhuosq => ord_qianhuosq.IsDelete == false);

            var tempData = ob_ord_qianhuosqservice.LoadSortEntities(where.Compile(), false, ord_qianhuosq => ord_qianhuosq.ID).ToPagedList<ord_qianhuosq>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_qianhuosq = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_qianhuosq_index";
            string page = "1";
            string shenqinbh = Request["shenqinbh"] ?? "";
            string shenqinbhequal = Request["shenqinbhequal"] ?? "";
            string shenqinbhand = Request["shenqinbhand"] ?? "";
            Expression<Func<ord_qianhuosq, bool>> where = PredicateExtensionses.True<ord_qianhuosq>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(shenqinbh))
                {
                    if (shenqinbhequal.Equals("="))
                    {
                        if (shenqinbhand.Equals("and"))
                            where = where.And(ord_qianhuosq => ord_qianhuosq.ShenqinBH == shenqinbh);
                        else
                            where = where.Or(ord_qianhuosq => ord_qianhuosq.ShenqinBH == shenqinbh);
                    }
                    if (shenqinbhequal.Equals("like"))
                    {
                        if (shenqinbhand.Equals("and"))
                            where = where.And(ord_qianhuosq => ord_qianhuosq.ShenqinBH.Contains(shenqinbh));
                        else
                            where = where.Or(ord_qianhuosq => ord_qianhuosq.ShenqinBH.Contains(shenqinbh));
                    }
                }
                if (!string.IsNullOrEmpty(shenqinbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shenqinbh", shenqinbh, shenqinbhequal, shenqinbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shenqinbh", "", shenqinbhequal, shenqinbhand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(shenqinbh))
                {
                    if (shenqinbhequal.Equals("="))
                    {
                        if (shenqinbhand.Equals("and"))
                            where = where.And(ord_qianhuosq => ord_qianhuosq.ShenqinBH == shenqinbh);
                        else
                            where = where.Or(ord_qianhuosq => ord_qianhuosq.ShenqinBH == shenqinbh);
                    }
                    if (shenqinbhequal.Equals("like"))
                    {
                        if (shenqinbhand.Equals("and"))
                            where = where.And(ord_qianhuosq => ord_qianhuosq.ShenqinBH.Contains(shenqinbh));
                        else
                            where = where.Or(ord_qianhuosq => ord_qianhuosq.ShenqinBH.Contains(shenqinbh));
                    }
                }
                if (!string.IsNullOrEmpty(shenqinbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shenqinbh", shenqinbh, shenqinbhequal, shenqinbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shenqinbh", "", shenqinbhequal, shenqinbhand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_qianhuosq => ord_qianhuosq.IsDelete == false);

            var tempData = ob_ord_qianhuosqservice.LoadSortEntities(where.Compile(), false, ord_qianhuosq => ord_qianhuosq.ID).ToPagedList<ord_qianhuosq>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_qianhuosq = tempData;
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
            string shenqinbh = Request["shenqinbh"] ?? "";
            string fasongsf = Request["fasongsf"] ?? "";
            string jieshousf = Request["jieshousf"] ?? "";
            string jieshouren = Request["jieshouren"] ?? "";
            string jieshousj = Request["jieshousj"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string makedate = Request["makedate"] ?? "";
            try
            {
                ord_qianhuosq ob_ord_qianhuosq = new ord_qianhuosq();
                ob_ord_qianhuosq.ShenqinBH = shenqinbh.Trim();
                ob_ord_qianhuosq.FasongSF = fasongsf == "" ? false : Boolean.Parse(fasongsf);
                ob_ord_qianhuosq.JieshouSF = jieshousf == "" ? false : Boolean.Parse(jieshousf);
                ob_ord_qianhuosq.Jieshouren = jieshouren == "" ? 0 : int.Parse(jieshouren);
                ob_ord_qianhuosq.JieshouSJ = jieshousj == "" ? DateTime.Now : DateTime.Parse(jieshousj);
                ob_ord_qianhuosq.Col1 = col1.Trim();
                ob_ord_qianhuosq.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_qianhuosq.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_qianhuosq = ob_ord_qianhuosqservice.AddEntity(ob_ord_qianhuosq);
                ViewBag.ord_qianhuosq = ob_ord_qianhuosq;
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
            ord_qianhuosq tempData = ob_ord_qianhuosqservice.GetEntityById(ord_qianhuosq => ord_qianhuosq.ID == id && ord_qianhuosq.IsDelete == false);
            ViewBag.ord_qianhuosq = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_qianhuosqViewModel ord_qianhuosqviewmodel = new ord_qianhuosqViewModel();
                ord_qianhuosqviewmodel.ID = tempData.ID;
                ord_qianhuosqviewmodel.ShenqinBH = tempData.ShenqinBH;
                ord_qianhuosqviewmodel.FasongSF = tempData.FasongSF;
                ord_qianhuosqviewmodel.JieshouSF = tempData.JieshouSF;
                ord_qianhuosqviewmodel.Jieshouren = tempData.Jieshouren;
                ord_qianhuosqviewmodel.JieshouSJ = tempData.JieshouSJ;
                ord_qianhuosqviewmodel.Col1 = tempData.Col1;
                ord_qianhuosqviewmodel.MakeMan = tempData.MakeMan;
                ord_qianhuosqviewmodel.MakeDate = tempData.MakeDate;
                return View(ord_qianhuosqviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string shenqinbh = Request["shenqinbh"] ?? "";
            string fasongsf = Request["fasongsf"] ?? "";
            string jieshousf = Request["jieshousf"] ?? "";
            string jieshouren = Request["jieshouren"] ?? "";
            string jieshousj = Request["jieshousj"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string makedate = Request["makedate"] ?? "";
            int uid = int.Parse(id);
            try
            {
                ord_qianhuosq p = ob_ord_qianhuosqservice.GetEntityById(ord_qianhuosq => ord_qianhuosq.ID == uid);
                p.ShenqinBH = shenqinbh.Trim();
                p.FasongSF = fasongsf == "" ? false : Boolean.Parse(fasongsf);
                p.JieshouSF = jieshousf == "" ? false : Boolean.Parse(jieshousf);
                p.Jieshouren = jieshouren == "" ? 0 : int.Parse(jieshouren);
                p.JieshouSJ = jieshousj == "" ? DateTime.Now : DateTime.Parse(jieshousj);
                p.Col1 = col1.Trim();
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_qianhuosqservice.UpdateEntity(p);
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
            ord_qianhuosq ob_ord_qianhuosq;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_qianhuosq = ob_ord_qianhuosqservice.GetEntityById(ord_qianhuosq => ord_qianhuosq.ID == id && ord_qianhuosq.IsDelete == false);
                    ob_ord_qianhuosq.IsDelete = true;
                    ob_ord_qianhuosqservice.UpdateEntity(ob_ord_qianhuosq);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

