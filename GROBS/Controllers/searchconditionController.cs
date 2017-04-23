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
    public class searchconditionController : Controller
    {
        //private IsearchconditionService ob_searchconditionservice = ServiceFactory.searchconditionservice;
        private IsearchconditionService ob_searchconditionservice = searchconditionService.GetInstance();
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int _userid = (int)Session["user_id"];
            string pagetag = "searchcondition_index";
            Expression<Func<searchcondition, bool>> where = PredicateExtensionses.True<searchcondition>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == _userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "pagebrief":
                            string pagebrief = scld[1];
                            string pagebriefequal = scld[2];
                            string pagebriefand = scld[3];
                            if (!string.IsNullOrEmpty(pagebrief))
                            {
                                if (pagebriefequal.Equals("="))
                                {
                                    if (pagebriefand.Equals("and"))
                                        where = where.And(p => p.PageBrief == pagebrief);
                                    else
                                        where = where.Or(p => p.PageBrief == pagebrief);
                                }
                                if (pagebriefequal.Equals("like"))
                                {
                                    if (pagebriefand.Equals("and"))
                                        where = where.And(p => p.PageBrief.Contains(pagebrief));
                                    else
                                        where = where.Or(p => p.PageBrief.Contains(pagebrief));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition0 = sc.ConditionInfo;
            }

            where = where.And(searchcondition => searchcondition.IsDelete == false);

            var tempData = ob_searchconditionservice.LoadSortEntities(where.Compile(), false, searchcondition => searchcondition.ID).ToPagedList<searchcondition>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.searchcondition = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int _userid = (int)Session["user_id"];
            string pagetag = "searchcondition_index";
            string page = "1";
            //pagebrief
            string pagebrief = Request["pagebrief"] ?? "";
            string pagebriefequal = Request["pagebriefequal"] ?? "";
            string pagebriefand = Request["pagebriefand"] ?? "";
            Expression<Func<searchcondition, bool>> where = PredicateExtensionses.True<searchcondition>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == _userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = _userid;
                sc.PageBrief = pagetag;
                //pagebrief
                if (!string.IsNullOrEmpty(pagebrief))
                {
                    if (pagebriefequal.Equals("="))
                    {
                        if (pagebriefand.Equals("and"))
                            where = where.And(p => p.PageBrief == pagebrief);
                        else
                            where = where.Or(p => p.PageBrief == pagebrief);
                    }
                    if (pagebriefequal.Equals("like"))
                    {
                        if (pagebriefand.Equals("and"))
                            where = where.And(p => p.PageBrief.Contains(pagebrief));
                        else
                            where = where.Or(p => p.PageBrief.Contains(pagebrief));
                    }
                }
                if (!string.IsNullOrEmpty(pagebrief))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pagebrief", pagebrief, pagebriefequal, pagebriefand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pagebrief", "", pagebriefequal, pagebriefand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //pagebrief
                if (!string.IsNullOrEmpty(pagebrief))
                {
                    if (pagebriefequal.Equals("="))
                    {
                        if (pagebriefand.Equals("and"))
                            where = where.And(p => p.PageBrief == pagebrief);
                        else
                            where = where.Or(p => p.PageBrief == pagebrief);
                    }
                    if (pagebriefequal.Equals("like"))
                    {
                        if (pagebriefand.Equals("and"))
                            where = where.And(p => p.PageBrief.Contains(pagebrief));
                        else
                            where = where.Or(p => p.PageBrief.Contains(pagebrief));
                    }
                }
                if (!string.IsNullOrEmpty(pagebrief))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pagebrief", pagebrief, pagebriefequal, pagebriefand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "pagebrief", "", pagebriefequal, pagebriefand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition0 = sc.ConditionInfo;
            where = where.And(searchcondition => searchcondition.IsDelete == false);

            var tempData = ob_searchconditionservice.LoadSortEntities(where.Compile(), false, searchcondition => searchcondition.ID).ToPagedList<searchcondition>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.searchcondition = tempData;
            return View(tempData);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string userid = Request["userid"] ?? "";
            string conditioninfo = Request["conditioninfo"] ?? "";
            string pagebrief = Request["pagebrief"] ?? "";
            string modifydate = Request["modifydate"] ?? "";
            string conditiontitle = Request["conditiontitle"] ?? "";
            try
            {
                searchcondition ob_searchcondition = new searchcondition();
                ob_searchcondition.UserID = userid == "" ? 0 : int.Parse(userid);
                ob_searchcondition.ConditionInfo = conditioninfo.Trim();
                ob_searchcondition.PageBrief = pagebrief.Trim();
                ob_searchcondition.ModifyDate = modifydate == "" ? DateTime.Now : DateTime.Parse(modifydate);
                ob_searchcondition.ConditionTitle = conditiontitle.Trim();
                ob_searchcondition = ob_searchconditionservice.AddEntity(ob_searchcondition);
                ViewBag.searchcondition = ob_searchcondition;
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
            searchcondition tempData = ob_searchconditionservice.GetEntityById(searchcondition => searchcondition.ID == id && searchcondition.IsDelete == false);
            ViewBag.searchcondition = tempData;
            if (tempData == null)
                return View();
            else
            {
                searchconditionViewModel searchconditionviewmodel = new searchconditionViewModel();
                searchconditionviewmodel.ID = tempData.ID;
                searchconditionviewmodel.UserID = tempData.UserID;
                searchconditionviewmodel.ConditionInfo = tempData.ConditionInfo;
                searchconditionviewmodel.PageBrief = tempData.PageBrief;
                searchconditionviewmodel.ModifyDate = tempData.ModifyDate;
                searchconditionviewmodel.ConditionTitle = tempData.ConditionTitle;
                return View(searchconditionviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string userid = Request["userid"] ?? "";
            string conditioninfo = Request["conditioninfo"] ?? "";
            string pagebrief = Request["pagebrief"] ?? "";
            string modifydate = Request["modifydate"] ?? "";
            string conditiontitle = Request["conditiontitle"] ?? "";
            int uid = int.Parse(id);
            try
            {
                searchcondition p = ob_searchconditionservice.GetEntityById(searchcondition => searchcondition.ID == uid);
                p.UserID = userid == "" ? 0 : int.Parse(userid);
                p.ConditionInfo = conditioninfo.Trim();
                p.PageBrief = pagebrief.Trim();
                p.ModifyDate = modifydate == "" ? DateTime.Now : DateTime.Parse(modifydate);
                p.ConditionTitle = conditiontitle.Trim();
                ob_searchconditionservice.UpdateEntity(p);
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
            searchcondition ob_searchcondition;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_searchcondition = ob_searchconditionservice.GetEntityById(searchcondition => searchcondition.ID == id && searchcondition.IsDelete == false);
                    if (ob_searchcondition != null)
                        ob_searchconditionservice.DeleteEntity(ob_searchcondition);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

