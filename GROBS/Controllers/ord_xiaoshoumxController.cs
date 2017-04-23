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
    public class ord_xiaoshoumxController : Controller
    {
        private Iord_xiaoshoumxService ob_ord_xiaoshoumxservice = ServiceFactory.ord_xiaoshoumxservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_xiaoshoumx_index";
            Expression<Func<ord_xiaoshoumx, bool>> where = PredicateExtensionses.True<ord_xiaoshoumx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "xsid":
                            string xsid = scld[1];
                            string xsidequal = scld[2];
                            string xsidand = scld[3];
                            if (!string.IsNullOrEmpty(xsid))
                            {
                                if (xsidequal.Equals("="))
                                {
                                    if (xsidand.Equals("and"))
                                        where = where.And(ord_xiaoshoumx => ord_xiaoshoumx.XSID == int.Parse(xsid));
                                    else
                                        where = where.Or(ord_xiaoshoumx => ord_xiaoshoumx.XSID == int.Parse(xsid));
                                }
                                if (xsidequal.Equals(">"))
                                {
                                    if (xsidand.Equals("and"))
                                        where = where.And(ord_xiaoshoumx => ord_xiaoshoumx.XSID > int.Parse(xsid));
                                    else
                                        where = where.Or(ord_xiaoshoumx => ord_xiaoshoumx.XSID > int.Parse(xsid));
                                }
                                if (xsidequal.Equals("<"))
                                {
                                    if (xsidand.Equals("and"))
                                        where = where.And(ord_xiaoshoumx => ord_xiaoshoumx.XSID < int.Parse(xsid));
                                    else
                                        where = where.Or(ord_xiaoshoumx => ord_xiaoshoumx.XSID < int.Parse(xsid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_xiaoshoumx => ord_xiaoshoumx.IsDelete == false);

            var tempData = ob_ord_xiaoshoumxservice.LoadSortEntities(where.Compile(), false, ord_xiaoshoumx => ord_xiaoshoumx.ID).ToPagedList<ord_xiaoshoumx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_xiaoshoumx = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_xiaoshoumx_index";
            string page = "1";
            string xsid = Request["xsid"] ?? "";
            string xsidequal = Request["xsidequal"] ?? "";
            string xsidand = Request["xsidand"] ?? "";
            Expression<Func<ord_xiaoshoumx, bool>> where = PredicateExtensionses.True<ord_xiaoshoumx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(xsid))
                {
                    if (xsidequal.Equals("="))
                    {
                        if (xsidand.Equals("and"))
                            where = where.And(ord_xiaoshoumx => ord_xiaoshoumx.XSID == int.Parse(xsid));
                        else
                            where = where.Or(ord_xiaoshoumx => ord_xiaoshoumx.XSID == int.Parse(xsid));
                    }
                    if (xsidequal.Equals(">"))
                    {
                        if (xsidand.Equals("and"))
                            where = where.And(ord_xiaoshoumx => ord_xiaoshoumx.XSID > int.Parse(xsid));
                        else
                            where = where.Or(ord_xiaoshoumx => ord_xiaoshoumx.XSID > int.Parse(xsid));
                    }
                    if (xsidequal.Equals("<"))
                    {
                        if (xsidand.Equals("and"))
                            where = where.And(ord_xiaoshoumx => ord_xiaoshoumx.XSID < int.Parse(xsid));
                        else
                            where = where.Or(ord_xiaoshoumx => ord_xiaoshoumx.XSID < int.Parse(xsid));
                    }
                }
                if (!string.IsNullOrEmpty(xsid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xsid", xsid, xsidequal, xsidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xsid", "", xsidequal, xsidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(xsid))
                {
                    if (xsidequal.Equals("="))
                    {
                        if (xsidand.Equals("and"))
                            where = where.And(ord_xiaoshoumx => ord_xiaoshoumx.XSID == int.Parse(xsid));
                        else
                            where = where.Or(ord_xiaoshoumx => ord_xiaoshoumx.XSID == int.Parse(xsid));
                    }
                    if (xsidequal.Equals(">"))
                    {
                        if (xsidand.Equals("and"))
                            where = where.And(ord_xiaoshoumx => ord_xiaoshoumx.XSID > int.Parse(xsid));
                        else
                            where = where.Or(ord_xiaoshoumx => ord_xiaoshoumx.XSID > int.Parse(xsid));
                    }
                    if (xsidequal.Equals("<"))
                    {
                        if (xsidand.Equals("and"))
                            where = where.And(ord_xiaoshoumx => ord_xiaoshoumx.XSID < int.Parse(xsid));
                        else
                            where = where.Or(ord_xiaoshoumx => ord_xiaoshoumx.XSID < int.Parse(xsid));
                    }
                }
                if (!string.IsNullOrEmpty(xsid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xsid", xsid, xsidequal, xsidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xsid", "", xsidequal, xsidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_xiaoshoumx => ord_xiaoshoumx.IsDelete == false);

            var tempData = ob_ord_xiaoshoumxservice.LoadSortEntities(where.Compile(), false, ord_xiaoshoumx => ord_xiaoshoumx.ID).ToPagedList<ord_xiaoshoumx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_xiaoshoumx = tempData;
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
            string xsid = Request["xsid"] ?? "";
            string spdm = Request["spdm"] ?? "";
            string spmc = Request["spmc"] ?? "";
            string guige = Request["guige"] ?? "";
            string shuliang = Request["shuliang"] ?? "";
            string hsl = Request["hsl"] ?? "";
            string jbdw = Request["jbdw"] ?? "";
            string xsdw = Request["xsdw"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                ord_xiaoshoumx ob_ord_xiaoshoumx = new ord_xiaoshoumx();
                ob_ord_xiaoshoumx.XSID = xsid == "" ? 0 : int.Parse(xsid);
                ob_ord_xiaoshoumx.SPDM = spdm.Trim();
                ob_ord_xiaoshoumx.SPMC = spmc.Trim();
                ob_ord_xiaoshoumx.Guige = guige.Trim();
                ob_ord_xiaoshoumx.Shuliang = shuliang == "" ? 0 : float.Parse(shuliang);
                ob_ord_xiaoshoumx.HSL = hsl == "" ? 0 : float.Parse(hsl);
                ob_ord_xiaoshoumx.JBDW = jbdw.Trim();
                ob_ord_xiaoshoumx.XSDW = xsdw.Trim();
                ob_ord_xiaoshoumx.Col1 = col1.Trim();
                ob_ord_xiaoshoumx.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_xiaoshoumx.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_xiaoshoumx = ob_ord_xiaoshoumxservice.AddEntity(ob_ord_xiaoshoumx);
                ViewBag.ord_xiaoshoumx = ob_ord_xiaoshoumx;
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
            ord_xiaoshoumx tempData = ob_ord_xiaoshoumxservice.GetEntityById(ord_xiaoshoumx => ord_xiaoshoumx.ID == id && ord_xiaoshoumx.IsDelete == false);
            ViewBag.ord_xiaoshoumx = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_xiaoshoumxViewModel ord_xiaoshoumxviewmodel = new ord_xiaoshoumxViewModel();
                ord_xiaoshoumxviewmodel.ID = tempData.ID;
                ord_xiaoshoumxviewmodel.XSID = tempData.XSID;
                ord_xiaoshoumxviewmodel.SPDM = tempData.SPDM;
                ord_xiaoshoumxviewmodel.SPMC = tempData.SPMC;
                ord_xiaoshoumxviewmodel.Guige = tempData.Guige;
                ord_xiaoshoumxviewmodel.Shuliang = tempData.Shuliang;
                ord_xiaoshoumxviewmodel.HSL = tempData.HSL;
                ord_xiaoshoumxviewmodel.JBDW = tempData.JBDW;
                ord_xiaoshoumxviewmodel.XSDW = tempData.XSDW;
                ord_xiaoshoumxviewmodel.Col1 = tempData.Col1;
                ord_xiaoshoumxviewmodel.MakeDate = tempData.MakeDate;
                ord_xiaoshoumxviewmodel.MakeMan = tempData.MakeMan;
                return View(ord_xiaoshoumxviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string xsid = Request["xsid"] ?? "";
            string spdm = Request["spdm"] ?? "";
            string spmc = Request["spmc"] ?? "";
            string guige = Request["guige"] ?? "";
            string shuliang = Request["shuliang"] ?? "";
            string hsl = Request["hsl"] ?? "";
            string jbdw = Request["jbdw"] ?? "";
            string xsdw = Request["xsdw"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                ord_xiaoshoumx p = ob_ord_xiaoshoumxservice.GetEntityById(ord_xiaoshoumx => ord_xiaoshoumx.ID == uid);
                p.XSID = xsid == "" ? 0 : int.Parse(xsid);
                p.SPDM = spdm.Trim();
                p.SPMC = spmc.Trim();
                p.Guige = guige.Trim();
                p.Shuliang = shuliang == "" ? 0 : float.Parse(shuliang);
                p.HSL = hsl == "" ? 0 : float.Parse(hsl);
                p.JBDW = jbdw.Trim();
                p.XSDW = xsdw.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_xiaoshoumxservice.UpdateEntity(p);
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
            ord_xiaoshoumx ob_ord_xiaoshoumx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_xiaoshoumx = ob_ord_xiaoshoumxservice.GetEntityById(ord_xiaoshoumx => ord_xiaoshoumx.ID == id && ord_xiaoshoumx.IsDelete == false);
                    ob_ord_xiaoshoumx.IsDelete = true;
                    ob_ord_xiaoshoumxservice.UpdateEntity(ob_ord_xiaoshoumx);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

