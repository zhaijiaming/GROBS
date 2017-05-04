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
    public class ord_qianhuoController : Controller
    {
        private Iord_qianhuoService ob_ord_qianhuoservice = ServiceFactory.ord_qianhuoservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_qianhuo_index";
            Expression<Func<ord_qianhuo, bool>> where = PredicateExtensionses.True<ord_qianhuo>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "mxid":
                            string mxid = scld[1];
                            string mxidequal = scld[2];
                            string mxidand = scld[3];
                            if (!string.IsNullOrEmpty(mxid))
                            {
                                if (mxidequal.Equals("="))
                                {
                                    if (mxidand.Equals("and"))
                                        where = where.And(ord_qianhuo => ord_qianhuo.MXID == int.Parse(mxid));
                                    else
                                        where = where.Or(ord_qianhuo => ord_qianhuo.MXID == int.Parse(mxid));
                                }
                                if (mxidequal.Equals(">"))
                                {
                                    if (mxidand.Equals("and"))
                                        where = where.And(ord_qianhuo => ord_qianhuo.MXID > int.Parse(mxid));
                                    else
                                        where = where.Or(ord_qianhuo => ord_qianhuo.MXID > int.Parse(mxid));
                                }
                                if (mxidequal.Equals("<"))
                                {
                                    if (mxidand.Equals("and"))
                                        where = where.And(ord_qianhuo => ord_qianhuo.MXID < int.Parse(mxid));
                                    else
                                        where = where.Or(ord_qianhuo => ord_qianhuo.MXID < int.Parse(mxid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_qianhuo => ord_qianhuo.IsDelete == false);

            var tempData = ob_ord_qianhuoservice.LoadSortEntities(where.Compile(), false, ord_qianhuo => ord_qianhuo.ID).ToPagedList<ord_qianhuo>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_qianhuo = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_qianhuo_index";
            string page = "1";
            string mxid = Request["mxid"] ?? "";
            string mxidequal = Request["mxidequal"] ?? "";
            string mxidand = Request["mxidand"] ?? "";
            Expression<Func<ord_qianhuo, bool>> where = PredicateExtensionses.True<ord_qianhuo>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(mxid))
                {
                    if (mxidequal.Equals("="))
                    {
                        if (mxidand.Equals("and"))
                            where = where.And(ord_qianhuo => ord_qianhuo.MXID == int.Parse(mxid));
                        else
                            where = where.Or(ord_qianhuo => ord_qianhuo.MXID == int.Parse(mxid));
                    }
                    if (mxidequal.Equals(">"))
                    {
                        if (mxidand.Equals("and"))
                            where = where.And(ord_qianhuo => ord_qianhuo.MXID > int.Parse(mxid));
                        else
                            where = where.Or(ord_qianhuo => ord_qianhuo.MXID > int.Parse(mxid));
                    }
                    if (mxidequal.Equals("<"))
                    {
                        if (mxidand.Equals("and"))
                            where = where.And(ord_qianhuo => ord_qianhuo.MXID < int.Parse(mxid));
                        else
                            where = where.Or(ord_qianhuo => ord_qianhuo.MXID < int.Parse(mxid));
                    }
                }
                if (!string.IsNullOrEmpty(mxid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mxid", mxid, mxidequal, mxidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mxid", "", mxidequal, mxidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(mxid))
                {
                    if (mxidequal.Equals("="))
                    {
                        if (mxidand.Equals("and"))
                            where = where.And(ord_qianhuo => ord_qianhuo.MXID == int.Parse(mxid));
                        else
                            where = where.Or(ord_qianhuo => ord_qianhuo.MXID == int.Parse(mxid));
                    }
                    if (mxidequal.Equals(">"))
                    {
                        if (mxidand.Equals("and"))
                            where = where.And(ord_qianhuo => ord_qianhuo.MXID > int.Parse(mxid));
                        else
                            where = where.Or(ord_qianhuo => ord_qianhuo.MXID > int.Parse(mxid));
                    }
                    if (mxidequal.Equals("<"))
                    {
                        if (mxidand.Equals("and"))
                            where = where.And(ord_qianhuo => ord_qianhuo.MXID < int.Parse(mxid));
                        else
                            where = where.Or(ord_qianhuo => ord_qianhuo.MXID < int.Parse(mxid));
                    }
                }
                if (!string.IsNullOrEmpty(mxid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mxid", mxid, mxidequal, mxidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mxid", "", mxidequal, mxidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_qianhuo => ord_qianhuo.IsDelete == false);

            var tempData = ob_ord_qianhuoservice.LoadSortEntities(where.Compile(), false, ord_qianhuo => ord_qianhuo.ID).ToPagedList<ord_qianhuo>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_qianhuo = tempData;
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
            string mxid = Request["mxid"] ?? "";
            string qhsl = Request["qhsl"] ?? "";
            string djrq = Request["djrq"] ?? "";
            string caigousf = Request["caigousf"] ?? "";
            string caigoubh = Request["caigoubh"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                ord_qianhuo ob_ord_qianhuo = new ord_qianhuo();
                ob_ord_qianhuo.MXID = mxid == "" ? 0 : int.Parse(mxid);
                ob_ord_qianhuo.QHSL = qhsl == "" ? 0 : float.Parse(qhsl);
                ob_ord_qianhuo.DJRQ = djrq == "" ? DateTime.Now : DateTime.Parse(djrq);
                ob_ord_qianhuo.CaigouSF = caigousf == "" ? false : Boolean.Parse(caigousf);
                ob_ord_qianhuo.CaigouBH = caigoubh.Trim();
                ob_ord_qianhuo.Col1 = col1.Trim();
                ob_ord_qianhuo.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_qianhuo.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_qianhuo = ob_ord_qianhuoservice.AddEntity(ob_ord_qianhuo);
                ViewBag.ord_qianhuo = ob_ord_qianhuo;
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
            ord_qianhuo tempData = ob_ord_qianhuoservice.GetEntityById(ord_qianhuo => ord_qianhuo.ID == id && ord_qianhuo.IsDelete == false);
            ViewBag.ord_qianhuo = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_qianhuoViewModel ord_qianhuoviewmodel = new ord_qianhuoViewModel();
                ord_qianhuoviewmodel.ID = tempData.ID;
                ord_qianhuoviewmodel.MXID = tempData.MXID;
                ord_qianhuoviewmodel.QHSL = tempData.QHSL;
                ord_qianhuoviewmodel.DJRQ = tempData.DJRQ;
                ord_qianhuoviewmodel.CaigouSF = tempData.CaigouSF;
                ord_qianhuoviewmodel.CaigouBH = tempData.CaigouBH;
                ord_qianhuoviewmodel.Col1 = tempData.Col1;
                ord_qianhuoviewmodel.MakeDate = tempData.MakeDate;
                ord_qianhuoviewmodel.MakeMan = tempData.MakeMan;
                return View(ord_qianhuoviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string mxid = Request["mxid"] ?? "";
            string qhsl = Request["qhsl"] ?? "";
            string djrq = Request["djrq"] ?? "";
            string caigousf = Request["caigousf"] ?? "";
            string caigoubh = Request["caigoubh"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                ord_qianhuo p = ob_ord_qianhuoservice.GetEntityById(ord_qianhuo => ord_qianhuo.ID == uid);
                p.MXID = mxid == "" ? 0 : int.Parse(mxid);
                p.QHSL = qhsl == "" ? 0 : float.Parse(qhsl);
                p.DJRQ = djrq == "" ? DateTime.Now : DateTime.Parse(djrq);
                p.CaigouSF = caigousf == "" ? false : Boolean.Parse(caigousf);
                p.CaigouBH = caigoubh.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_qianhuoservice.UpdateEntity(p);
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
            ord_qianhuo ob_ord_qianhuo;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_qianhuo = ob_ord_qianhuoservice.GetEntityById(ord_qianhuo => ord_qianhuo.ID == id && ord_qianhuo.IsDelete == false);
                    ob_ord_qianhuo.IsDelete = true;
                    ob_ord_qianhuoservice.UpdateEntity(ob_ord_qianhuo);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

