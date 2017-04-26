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
    public class base_shangpinjgController : Controller
    {
        private Ibase_shangpinjgService ob_base_shangpinjgservice = ServiceFactory.base_shangpinjgservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_shangpinjg_index";
            Expression<Func<base_shangpinjg, bool>> where = PredicateExtensionses.True<base_shangpinjg>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "spid":
                            string spid = scld[1];
                            string spidequal = scld[2];
                            string spidand = scld[3];
                            if (!string.IsNullOrEmpty(spid))
                            {
                                if (spidequal.Equals("="))
                                {
                                    if (spidand.Equals("and"))
                                        where = where.And(base_shangpinjg => base_shangpinjg.SPID == int.Parse(spid));
                                    else
                                        where = where.Or(base_shangpinjg => base_shangpinjg.SPID == int.Parse(spid));
                                }
                                if (spidequal.Equals(">"))
                                {
                                    if (spidand.Equals("and"))
                                        where = where.And(base_shangpinjg => base_shangpinjg.SPID > int.Parse(spid));
                                    else
                                        where = where.Or(base_shangpinjg => base_shangpinjg.SPID > int.Parse(spid));
                                }
                                if (spidequal.Equals("<"))
                                {
                                    if (spidand.Equals("and"))
                                        where = where.And(base_shangpinjg => base_shangpinjg.SPID < int.Parse(spid));
                                    else
                                        where = where.Or(base_shangpinjg => base_shangpinjg.SPID < int.Parse(spid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_shangpinjg => base_shangpinjg.IsDelete == false);

            var tempData = ob_base_shangpinjgservice.LoadSortEntities(where.Compile(), false, base_shangpinjg => base_shangpinjg.ID).ToPagedList<base_shangpinjg>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shangpinjg = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_shangpinjg_index";
            string page = "1";
            string spid = Request["spid"] ?? "";
            string spidequal = Request["spidequal"] ?? "";
            string spidand = Request["spidand"] ?? "";
            Expression<Func<base_shangpinjg, bool>> where = PredicateExtensionses.True<base_shangpinjg>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(spid))
                {
                    if (spidequal.Equals("="))
                    {
                        if (spidand.Equals("and"))
                            where = where.And(base_shangpinjg => base_shangpinjg.SPID == int.Parse(spid));
                        else
                            where = where.Or(base_shangpinjg => base_shangpinjg.SPID == int.Parse(spid));
                    }
                    if (spidequal.Equals(">"))
                    {
                        if (spidand.Equals("and"))
                            where = where.And(base_shangpinjg => base_shangpinjg.SPID > int.Parse(spid));
                        else
                            where = where.Or(base_shangpinjg => base_shangpinjg.SPID > int.Parse(spid));
                    }
                    if (spidequal.Equals("<"))
                    {
                        if (spidand.Equals("and"))
                            where = where.And(base_shangpinjg => base_shangpinjg.SPID < int.Parse(spid));
                        else
                            where = where.Or(base_shangpinjg => base_shangpinjg.SPID < int.Parse(spid));
                    }
                }
                if (!string.IsNullOrEmpty(spid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "spid", spid, spidequal, spidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "spid", "", spidequal, spidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(spid))
                {
                    if (spidequal.Equals("="))
                    {
                        if (spidand.Equals("and"))
                            where = where.And(base_shangpinjg => base_shangpinjg.SPID == int.Parse(spid));
                        else
                            where = where.Or(base_shangpinjg => base_shangpinjg.SPID == int.Parse(spid));
                    }
                    if (spidequal.Equals(">"))
                    {
                        if (spidand.Equals("and"))
                            where = where.And(base_shangpinjg => base_shangpinjg.SPID > int.Parse(spid));
                        else
                            where = where.Or(base_shangpinjg => base_shangpinjg.SPID > int.Parse(spid));
                    }
                    if (spidequal.Equals("<"))
                    {
                        if (spidand.Equals("and"))
                            where = where.And(base_shangpinjg => base_shangpinjg.SPID < int.Parse(spid));
                        else
                            where = where.Or(base_shangpinjg => base_shangpinjg.SPID < int.Parse(spid));
                    }
                }
                if (!string.IsNullOrEmpty(spid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "spid", spid, spidequal, spidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "spid", "", spidequal, spidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_shangpinjg => base_shangpinjg.IsDelete == false);

            var tempData = ob_base_shangpinjgservice.LoadSortEntities(where.Compile(), false, base_shangpinjg => base_shangpinjg.ID).ToPagedList<base_shangpinjg>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shangpinjg = tempData;
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
            string spid = Request["spid"] ?? "";
            string jiaxs = Request["jiaxs"] ?? "";
            string jiacg = Request["jiacg"] ?? "";
            string jianb = Request["jianb"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                base_shangpinjg ob_base_shangpinjg = new base_shangpinjg();
                ob_base_shangpinjg.SPID = spid == "" ? 0 : int.Parse(spid);
                ob_base_shangpinjg.JiaXS = jiaxs == "" ? 0 : float.Parse(jiaxs);
                ob_base_shangpinjg.JiaCG = jiacg == "" ? 0 : float.Parse(jiacg);
                ob_base_shangpinjg.JiaNB = jianb == "" ? 0 : float.Parse(jianb);
                ob_base_shangpinjg.Col1 = col1.Trim();
                ob_base_shangpinjg.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_shangpinjg.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_shangpinjg = ob_base_shangpinjgservice.AddEntity(ob_base_shangpinjg);
                ViewBag.base_shangpinjg = ob_base_shangpinjg;
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
            base_shangpinjg tempData = ob_base_shangpinjgservice.GetEntityById(base_shangpinjg => base_shangpinjg.ID == id && base_shangpinjg.IsDelete == false);
            ViewBag.base_shangpinjg = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_shangpinjgViewModel base_shangpinjgviewmodel = new base_shangpinjgViewModel();
                base_shangpinjgviewmodel.ID = tempData.ID;
                base_shangpinjgviewmodel.SPID = tempData.SPID;
                base_shangpinjgviewmodel.JiaXS = tempData.JiaXS;
                base_shangpinjgviewmodel.JiaCG = tempData.JiaCG;
                base_shangpinjgviewmodel.JiaNB = tempData.JiaNB;
                base_shangpinjgviewmodel.Col1 = tempData.Col1;
                base_shangpinjgviewmodel.MakeDate = tempData.MakeDate;
                base_shangpinjgviewmodel.MakeMan = tempData.MakeMan;
                return View(base_shangpinjgviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string spid = Request["spid"] ?? "";
            string jiaxs = Request["jiaxs"] ?? "";
            string jiacg = Request["jiacg"] ?? "";
            string jianb = Request["jianb"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_shangpinjg p = ob_base_shangpinjgservice.GetEntityById(base_shangpinjg => base_shangpinjg.ID == uid);
                p.SPID = spid == "" ? 0 : int.Parse(spid);
                p.JiaXS = jiaxs == "" ? 0 : float.Parse(jiaxs);
                p.JiaCG = jiacg == "" ? 0 : float.Parse(jiacg);
                p.JiaNB = jianb == "" ? 0 : float.Parse(jianb);
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_shangpinjgservice.UpdateEntity(p);
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
            base_shangpinjg ob_base_shangpinjg;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_shangpinjg = ob_base_shangpinjgservice.GetEntityById(base_shangpinjg => base_shangpinjg.ID == id && base_shangpinjg.IsDelete == false);
                    ob_base_shangpinjg.IsDelete = true;
                    ob_base_shangpinjgservice.UpdateEntity(ob_base_shangpinjg);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

