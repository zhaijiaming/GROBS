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
    public class ord_dingdanmxController : Controller
    {
        private Iord_dingdanmxService ob_ord_dingdanmxservice = ServiceFactory.ord_dingdanmxservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_dingdanmx_index";
            Expression<Func<ord_dingdanmx, bool>> where = PredicateExtensionses.True<ord_dingdanmx>();
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
                                        where = where.And(ord_dingdanmx => ord_dingdanmx.DDID == int.Parse(ddid));
                                    else
                                        where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID == int.Parse(ddid));
                                }
                                if (ddidequal.Equals(">"))
                                {
                                    if (ddidand.Equals("and"))
                                        where = where.And(ord_dingdanmx => ord_dingdanmx.DDID > int.Parse(ddid));
                                    else
                                        where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID > int.Parse(ddid));
                                }
                                if (ddidequal.Equals("<"))
                                {
                                    if (ddidand.Equals("and"))
                                        where = where.And(ord_dingdanmx => ord_dingdanmx.DDID < int.Parse(ddid));
                                    else
                                        where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID < int.Parse(ddid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_dingdanmx => ord_dingdanmx.IsDelete == false);

            var tempData = ob_ord_dingdanmxservice.LoadSortEntities(where.Compile(), false, ord_dingdanmx => ord_dingdanmx.ID).ToPagedList<ord_dingdanmx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_dingdanmx = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_dingdanmx_index";
            string page = "1";
            string ddid = Request["ddid"] ?? "";
            string ddidequal = Request["ddidequal"] ?? "";
            string ddidand = Request["ddidand"] ?? "";
            Expression<Func<ord_dingdanmx, bool>> where = PredicateExtensionses.True<ord_dingdanmx>();
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
                            where = where.And(ord_dingdanmx => ord_dingdanmx.DDID == int.Parse(ddid));
                        else
                            where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID == int.Parse(ddid));
                    }
                    if (ddidequal.Equals(">"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_dingdanmx => ord_dingdanmx.DDID > int.Parse(ddid));
                        else
                            where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID > int.Parse(ddid));
                    }
                    if (ddidequal.Equals("<"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_dingdanmx => ord_dingdanmx.DDID < int.Parse(ddid));
                        else
                            where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID < int.Parse(ddid));
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
                            where = where.And(ord_dingdanmx => ord_dingdanmx.DDID == int.Parse(ddid));
                        else
                            where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID == int.Parse(ddid));
                    }
                    if (ddidequal.Equals(">"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_dingdanmx => ord_dingdanmx.DDID > int.Parse(ddid));
                        else
                            where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID > int.Parse(ddid));
                    }
                    if (ddidequal.Equals("<"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_dingdanmx => ord_dingdanmx.DDID < int.Parse(ddid));
                        else
                            where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID < int.Parse(ddid));
                    }
                }
                if (!string.IsNullOrEmpty(ddid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddid", ddid, ddidequal, ddidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddid", "", ddidequal, ddidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_dingdanmx => ord_dingdanmx.IsDelete == false);

            var tempData = ob_ord_dingdanmxservice.LoadSortEntities(where.Compile(), false, ord_dingdanmx => ord_dingdanmx.ID).ToPagedList<ord_dingdanmx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_dingdanmx = tempData;
            return View(tempData);
        }

        public ActionResult Add()
        {
            string ddid = Request["ddid"] ?? "";
            ViewBag.ddid = ddid;

            ViewBag.userid = (int)Session["user_id"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string ddid = Request["ddid"] ?? "";
            string spid = Request["spid"] ?? "";
            string spbm = Request["spbm"] ?? "";
            string spmc = Request["spmc"] ?? "";
            string guige = Request["guige"] ?? "";
            string cgsl = Request["cgsl"] ?? "";
            string fhsl = Request["fhsl"] ?? "";
            string xsbj = Request["xsbj"] ?? "";
            string xsdj = Request["xsdj"] ?? "";
            string jine = Request["jine"] ?? "";
            string zhekou = Request["zhekou"] ?? "";
            string zhekoulv = Request["zhekoulv"] ?? "";
            string hsl = Request["hsl"] ?? "";
            string hsbm = Request["hsbm"] ?? "";
            string jbdw = Request["jbdw"] ?? "";
            string xsdw = Request["xsdw"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int _id = int.Parse(ddid);
            try
            {
                ord_dingdanmx ob_ord_dingdanmx = new ord_dingdanmx();
                ob_ord_dingdanmx.DDID = ddid == "" ? 0 : int.Parse(ddid);
                ob_ord_dingdanmx.SPID = spid == "" ? 0 : int.Parse(spid);
                ob_ord_dingdanmx.SPBM = spbm.Trim();
                ob_ord_dingdanmx.SPMC = spmc.Trim();
                ob_ord_dingdanmx.Guige = guige.Trim();
                ob_ord_dingdanmx.CGSL = cgsl == "" ? 0 : float.Parse(cgsl);
                ob_ord_dingdanmx.FHSL = fhsl == "" ? 0 : float.Parse(fhsl);
                ob_ord_dingdanmx.XSBJ = xsbj == "" ? 0 : float.Parse(xsbj);
                ob_ord_dingdanmx.XSDJ = xsdj == "" ? 0 : float.Parse(xsdj);
                ob_ord_dingdanmx.Jine = jine == "" ? 0 : float.Parse(jine);
                ob_ord_dingdanmx.Zhekou = zhekou == "" ? 0 : float.Parse(zhekou);
                ob_ord_dingdanmx.Zhekoulv = zhekoulv == "" ? 0 : float.Parse(zhekoulv);
                ob_ord_dingdanmx.HSL = hsl == "" ? 0 : float.Parse(hsl);
                ob_ord_dingdanmx.HSBM = hsbm.Trim();
                ob_ord_dingdanmx.JBDW = jbdw.Trim();
                ob_ord_dingdanmx.XSDW = xsdw.Trim();
                ob_ord_dingdanmx.Col1 = col1.Trim();
                ob_ord_dingdanmx.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_dingdanmx.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_dingdanmx = ob_ord_dingdanmxservice.AddEntity(ob_ord_dingdanmx);
                ViewBag.ord_dingdanmx = ob_ord_dingdanmx;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Edit","ord_dingdan",new { id = _id});
        }

        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            ord_dingdanmx tempData = ob_ord_dingdanmxservice.GetEntityById(ord_dingdanmx => ord_dingdanmx.ID == id && ord_dingdanmx.IsDelete == false);
            ViewBag.ord_dingdanmx = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_dingdanmxViewModel ord_dingdanmxviewmodel = new ord_dingdanmxViewModel();
                ord_dingdanmxviewmodel.ID = tempData.ID;
                ord_dingdanmxviewmodel.DDID = tempData.DDID;
                ord_dingdanmxviewmodel.SPID = tempData.SPID;
                ord_dingdanmxviewmodel.SPBM = tempData.SPBM;
                ord_dingdanmxviewmodel.SPMC = tempData.SPMC;
                ord_dingdanmxviewmodel.Guige = tempData.Guige;
                ord_dingdanmxviewmodel.CGSL = tempData.CGSL;
                ord_dingdanmxviewmodel.FHSL = tempData.FHSL;
                ord_dingdanmxviewmodel.XSBJ = tempData.XSBJ;
                ord_dingdanmxviewmodel.XSDJ = tempData.XSDJ;
                ord_dingdanmxviewmodel.Jine = tempData.Jine;
                ord_dingdanmxviewmodel.Zhekou = tempData.Zhekou;
                ord_dingdanmxviewmodel.Zhekoulv = tempData.Zhekoulv;
                ord_dingdanmxviewmodel.HSL = tempData.HSL;
                ord_dingdanmxviewmodel.HSBM = tempData.HSBM;
                ord_dingdanmxviewmodel.JBDW = tempData.JBDW;
                ord_dingdanmxviewmodel.XSDW = tempData.XSDW;
                ord_dingdanmxviewmodel.Col1 = tempData.Col1;
                ord_dingdanmxviewmodel.MakeDate = tempData.MakeDate;
                ord_dingdanmxviewmodel.MakeMan = tempData.MakeMan;
                return View(ord_dingdanmxviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string ddid = Request["ddid"] ?? "";
            string spid = Request["spid"] ?? "";
            string spbm = Request["spbm"] ?? "";
            string spmc = Request["spmc"] ?? "";
            string guige = Request["guige"] ?? "";
            string cgsl = Request["cgsl"] ?? "";
            string fhsl = Request["fhsl"] ?? "";
            string xsbj = Request["xsbj"] ?? "";
            string xsdj = Request["xsdj"] ?? "";
            string jine = Request["jine"] ?? "";
            string zhekou = Request["zhekou"] ?? "";
            string zhekoulv = Request["zhekoulv"] ?? "";
            string hsl = Request["hsl"] ?? "";
            string hsbm = Request["hsbm"] ?? "";
            string jbdw = Request["jbdw"] ?? "";
            string xsdw = Request["xsdw"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            int _id = int.Parse(ddid);
            try
            {
                ord_dingdanmx p = ob_ord_dingdanmxservice.GetEntityById(ord_dingdanmx => ord_dingdanmx.ID == uid);
                p.DDID = ddid == "" ? 0 : int.Parse(ddid);
                p.SPID = spid == "" ? 0 : int.Parse(spid);
                p.SPBM = spbm.Trim();
                p.SPMC = spmc.Trim();
                p.Guige = guige.Trim();
                p.CGSL = cgsl == "" ? 0 : float.Parse(cgsl);
                p.FHSL = fhsl == "" ? 0 : float.Parse(fhsl);
                p.XSBJ = xsbj == "" ? 0 : float.Parse(xsbj);
                p.XSDJ = xsdj == "" ? 0 : float.Parse(xsdj);
                p.Jine = jine == "" ? 0 : float.Parse(jine);
                p.Zhekou = zhekou == "" ? 0 : float.Parse(zhekou);
                p.Zhekoulv = zhekoulv == "" ? 0 : float.Parse(zhekoulv);
                p.HSL = hsl == "" ? 0 : float.Parse(hsl);
                p.HSBM = hsbm.Trim();
                p.JBDW = jbdw.Trim();
                p.XSDW = xsdw.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_dingdanmxservice.UpdateEntity(p);
                ViewBag.saveok = ViewAddTag.ModifyOk;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.saveok = ViewAddTag.ModifyNo;
            }
            return RedirectToAction("Edit","ord_dingdan", new { id = _id });
        }
        public ActionResult Delete()
        {
            string sdel = Request["del"] ?? "";
            int id;
            ord_dingdanmx ob_ord_dingdanmx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_dingdanmx = ob_ord_dingdanmxservice.GetEntityById(ord_dingdanmx => ord_dingdanmx.ID == id && ord_dingdanmx.IsDelete == false);
                    ob_ord_dingdanmx.IsDelete = true;
                    ob_ord_dingdanmxservice.UpdateEntity(ob_ord_dingdanmx);
                }
            }
            return RedirectToAction("Index");
        }

        public JsonResult DeleteFromDingdan()
        {
            string sdel = Request["del"] ?? "";
            int id;
            ord_dingdanmx ob_ord_dingdanmx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_dingdanmx = ob_ord_dingdanmxservice.GetEntityById(ord_dingdanmx => ord_dingdanmx.ID == id && ord_dingdanmx.IsDelete == false);
                    ob_ord_dingdanmx.IsDelete = true;
                    ob_ord_dingdanmxservice.UpdateEntity(ob_ord_dingdanmx);
                }
            }
            return Json(1);
        }
        public JsonResult getDingdanMingXiWithDDID()
        {
            string _ddid = Request["ddid"] ?? "";
            if (!string.IsNullOrEmpty(_ddid))
            {
                var tempdata = ServiceFactory.ord_dingdanmxservice.LoadSortEntities(p => p.DDID == int.Parse(_ddid) && p.IsDelete == false, true, p => p.DDID).ToList<ord_dingdanmx>();
                if(tempdata != null)
                {
                    return Json(tempdata);
                }
                else
                {
                    return Json(-1);
                }
            }
            else
            {
                return Json(-1);
            }
        }
        public JsonResult GetCommodityPrice()
        {
            var _cpx = Request["cpx"] ?? "";
            var _sp = Request["sp"] ?? "";

            if (string.IsNullOrEmpty(_cpx) || string.IsNullOrEmpty(_sp))
                return Json(-1);

            var _spjg = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.Daima == _sp && p.Chanpinxian==int.Parse(_cpx) && p.IsDelete == false);
            if (_spjg == null)
                return Json(-2);
            return Json(_spjg);
        }
    }
}

