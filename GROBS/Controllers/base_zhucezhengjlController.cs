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
    public class base_zhucezhengjlController : Controller
    {
        private Ibase_zhucezhengjlService ob_base_zhucezhengjlservice = ServiceFactory.base_zhucezhengjlservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_zhucezhengjl_index";
            Expression<Func<base_zhucezhengjl, bool>> where = PredicateExtensionses.True<base_zhucezhengjl>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "zczid":
                            string zczid = scld[1];
                            string zczidequal = scld[2];
                            string zczidand = scld[3];
                            if (!string.IsNullOrEmpty(zczid))
                            {
                                if (zczidequal.Equals("="))
                                {
                                    if (zczidand.Equals("and"))
                                        where = where.And(base_zhucezhengjl => base_zhucezhengjl.ZCZID == int.Parse(zczid));
                                    else
                                        where = where.Or(base_zhucezhengjl => base_zhucezhengjl.ZCZID == int.Parse(zczid));
                                }
                                if (zczidequal.Equals(">"))
                                {
                                    if (zczidand.Equals("and"))
                                        where = where.And(base_zhucezhengjl => base_zhucezhengjl.ZCZID > int.Parse(zczid));
                                    else
                                        where = where.Or(base_zhucezhengjl => base_zhucezhengjl.ZCZID > int.Parse(zczid));
                                }
                                if (zczidequal.Equals("<"))
                                {
                                    if (zczidand.Equals("and"))
                                        where = where.And(base_zhucezhengjl => base_zhucezhengjl.ZCZID < int.Parse(zczid));
                                    else
                                        where = where.Or(base_zhucezhengjl => base_zhucezhengjl.ZCZID < int.Parse(zczid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_zhucezhengjl => base_zhucezhengjl.IsDelete == false);

            var tempData = ob_base_zhucezhengjlservice.LoadSortEntities(where.Compile(), false, base_zhucezhengjl => base_zhucezhengjl.ID).ToPagedList<base_zhucezhengjl>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_zhucezhengjl = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_zhucezhengjl_index";
            string page = "1";
            string zczid = Request["zczid"] ?? "";
            string zczidequal = Request["zczidequal"] ?? "";
            string zczidand = Request["zczidand"] ?? "";
            Expression<Func<base_zhucezhengjl, bool>> where = PredicateExtensionses.True<base_zhucezhengjl>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(zczid))
                {
                    if (zczidequal.Equals("="))
                    {
                        if (zczidand.Equals("and"))
                            where = where.And(base_zhucezhengjl => base_zhucezhengjl.ZCZID == int.Parse(zczid));
                        else
                            where = where.Or(base_zhucezhengjl => base_zhucezhengjl.ZCZID == int.Parse(zczid));
                    }
                    if (zczidequal.Equals(">"))
                    {
                        if (zczidand.Equals("and"))
                            where = where.And(base_zhucezhengjl => base_zhucezhengjl.ZCZID > int.Parse(zczid));
                        else
                            where = where.Or(base_zhucezhengjl => base_zhucezhengjl.ZCZID > int.Parse(zczid));
                    }
                    if (zczidequal.Equals("<"))
                    {
                        if (zczidand.Equals("and"))
                            where = where.And(base_zhucezhengjl => base_zhucezhengjl.ZCZID < int.Parse(zczid));
                        else
                            where = where.Or(base_zhucezhengjl => base_zhucezhengjl.ZCZID < int.Parse(zczid));
                    }
                }
                if (!string.IsNullOrEmpty(zczid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "zczid", zczid, zczidequal, zczidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "zczid", "", zczidequal, zczidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(zczid))
                {
                    if (zczidequal.Equals("="))
                    {
                        if (zczidand.Equals("and"))
                            where = where.And(base_zhucezhengjl => base_zhucezhengjl.ZCZID == int.Parse(zczid));
                        else
                            where = where.Or(base_zhucezhengjl => base_zhucezhengjl.ZCZID == int.Parse(zczid));
                    }
                    if (zczidequal.Equals(">"))
                    {
                        if (zczidand.Equals("and"))
                            where = where.And(base_zhucezhengjl => base_zhucezhengjl.ZCZID > int.Parse(zczid));
                        else
                            where = where.Or(base_zhucezhengjl => base_zhucezhengjl.ZCZID > int.Parse(zczid));
                    }
                    if (zczidequal.Equals("<"))
                    {
                        if (zczidand.Equals("and"))
                            where = where.And(base_zhucezhengjl => base_zhucezhengjl.ZCZID < int.Parse(zczid));
                        else
                            where = where.Or(base_zhucezhengjl => base_zhucezhengjl.ZCZID < int.Parse(zczid));
                    }
                }
                if (!string.IsNullOrEmpty(zczid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "zczid", zczid, zczidequal, zczidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "zczid", "", zczidequal, zczidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_zhucezhengjl => base_zhucezhengjl.IsDelete == false);

            var tempData = ob_base_zhucezhengjlservice.LoadSortEntities(where.Compile(), false, base_zhucezhengjl => base_zhucezhengjl.ID).ToPagedList<base_zhucezhengjl>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_zhucezhengjl = tempData;
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
            string zczid = Request["zczid"] ?? "";
            string spid = Request["spid"] ?? "";
            string memo = Request["memo"] ?? "";
            string gbsf = Request["gbsf"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                base_zhucezhengjl ob_base_zhucezhengjl = new base_zhucezhengjl();
                ob_base_zhucezhengjl.ZCZID = zczid == "" ? 0 : int.Parse(zczid);
                ob_base_zhucezhengjl.SPID = spid == "" ? 0 : int.Parse(spid);
                ob_base_zhucezhengjl.Memo = memo.Trim();
                ob_base_zhucezhengjl.GBSF = gbsf == "" ? false : Boolean.Parse(gbsf);
                ob_base_zhucezhengjl.Col1 = col1.Trim();
                ob_base_zhucezhengjl.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_zhucezhengjl.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_zhucezhengjl = ob_base_zhucezhengjlservice.AddEntity(ob_base_zhucezhengjl);
                ViewBag.base_zhucezhengjl = ob_base_zhucezhengjl;
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
            base_zhucezhengjl tempData = ob_base_zhucezhengjlservice.GetEntityById(base_zhucezhengjl => base_zhucezhengjl.ID == id && base_zhucezhengjl.IsDelete == false);
            ViewBag.base_zhucezhengjl = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_zhucezhengjlViewModel base_zhucezhengjlviewmodel = new base_zhucezhengjlViewModel();
                base_zhucezhengjlviewmodel.ID = tempData.ID;
                base_zhucezhengjlviewmodel.ZCZID = tempData.ZCZID;
                base_zhucezhengjlviewmodel.SPID = tempData.SPID;
                base_zhucezhengjlviewmodel.Memo = tempData.Memo;
                base_zhucezhengjlviewmodel.GBSF = tempData.GBSF;
                base_zhucezhengjlviewmodel.Col1 = tempData.Col1;
                base_zhucezhengjlviewmodel.MakeDate = tempData.MakeDate;
                base_zhucezhengjlviewmodel.MakeMan = tempData.MakeMan;
                return View(base_zhucezhengjlviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string zczid = Request["zczid"] ?? "";
            string spid = Request["spid"] ?? "";
            string memo = Request["memo"] ?? "";
            string gbsf = Request["gbsf"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_zhucezhengjl p = ob_base_zhucezhengjlservice.GetEntityById(base_zhucezhengjl => base_zhucezhengjl.ID == uid);
                p.ZCZID = zczid == "" ? 0 : int.Parse(zczid);
                p.SPID = spid == "" ? 0 : int.Parse(spid);
                p.Memo = memo.Trim();
                p.GBSF = gbsf == "" ? false : Boolean.Parse(gbsf);
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_zhucezhengjlservice.UpdateEntity(p);
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
            base_zhucezhengjl ob_base_zhucezhengjl;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_zhucezhengjl = ob_base_zhucezhengjlservice.GetEntityById(base_zhucezhengjl => base_zhucezhengjl.ID == id && base_zhucezhengjl.IsDelete == false);
                    ob_base_zhucezhengjl.IsDelete = true;
                    ob_base_zhucezhengjlservice.UpdateEntity(ob_base_zhucezhengjl);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

