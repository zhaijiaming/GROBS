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
    public class operatelogController : Controller
    {
        private IoperatelogService ob_operatelogservice = ServiceFactory.operatelogservice;
        //private IoperatelogService ob_operatelogservice =operatelogService.GetInstance();
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "operatelog_index";
            Expression<Func<operatelog, bool>> where = PredicateExtensionses.True<operatelog>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "modelid":
                            string modelid = scld[1];
                            string modelidequal = scld[2];
                            string modelidand = scld[3];
                            if (!string.IsNullOrEmpty(modelid))
                            {
                                if (modelidequal.Equals("="))
                                {
                                    if (modelidand.Equals("and"))
                                        where = where.And(operatelog => operatelog.ModelID == modelid);
                                    else
                                        where = where.Or(operatelog => operatelog.ModelID == modelid);
                                }
                                if (modelidequal.Equals("like"))
                                {
                                    if (modelidand.Equals("and"))
                                        where = where.And(operatelog => operatelog.ModelID.Contains(modelid));
                                    else
                                        where = where.Or(operatelog => operatelog.ModelID.Contains(modelid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(operatelog => operatelog.IsDelete == false);

            var tempData = ob_operatelogservice.LoadSortEntities(where.Compile(), false, operatelog => operatelog.ID).ToPagedList<operatelog>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.operatelog = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "operatelog_index";
            string page = "1";
            string modelid = Request["modelid"] ?? "";
            string modelidequal = Request["modelidequal"] ?? "";
            string modelidand = Request["modelidand"] ?? "";
            Expression<Func<operatelog, bool>> where = PredicateExtensionses.True<operatelog>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(modelid))
                {
                    if (modelidequal.Equals("="))
                    {
                        if (modelidand.Equals("and"))
                            where = where.And(operatelog => operatelog.ModelID == modelid);
                        else
                            where = where.Or(operatelog => operatelog.ModelID == modelid);
                    }
                    if (modelidequal.Equals("like"))
                    {
                        if (modelidand.Equals("and"))
                            where = where.And(operatelog => operatelog.ModelID.Contains(modelid));
                        else
                            where = where.Or(operatelog => operatelog.ModelID.Contains(modelid));
                    }
                }
                if (!string.IsNullOrEmpty(modelid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "modelid", modelid, modelidequal, modelidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "modelid", "", modelidequal, modelidand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(modelid))
                {
                    if (modelidequal.Equals("="))
                    {
                        if (modelidand.Equals("and"))
                            where = where.And(operatelog => operatelog.ModelID == modelid);
                        else
                            where = where.Or(operatelog => operatelog.ModelID == modelid);
                    }
                    if (modelidequal.Equals("like"))
                    {
                        if (modelidand.Equals("and"))
                            where = where.And(operatelog => operatelog.ModelID.Contains(modelid));
                        else
                            where = where.Or(operatelog => operatelog.ModelID.Contains(modelid));
                    }
                }
                if (!string.IsNullOrEmpty(modelid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "modelid", modelid, modelidequal, modelidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "modelid", "", modelidequal, modelidand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(operatelog => operatelog.IsDelete == false);

            var tempData = ob_operatelogservice.LoadSortEntities(where.Compile(), false, operatelog => operatelog.ID).ToPagedList<operatelog>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.operatelog = tempData;
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
            string modelid = Request["modelid"] ?? "";
            string opinfo = Request["opinfo"] ?? "";
            string opman = Request["opman"] ?? "";
            string opdate = Request["opdate"] ?? "";
            try
            {
                operatelog ob_operatelog = new operatelog();
                ob_operatelog.ModelID = modelid.Trim();
                ob_operatelog.OpInfo = opinfo.Trim();
                ob_operatelog.OpMan = opman.Trim();
                ob_operatelog.OpDate = opdate == "" ? DateTime.Now : DateTime.Parse(opdate);
                ob_operatelog = ob_operatelogservice.AddEntity(ob_operatelog);
                ViewBag.operatelog = ob_operatelog;
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
            operatelog tempData = ob_operatelogservice.GetEntityById(operatelog => operatelog.ID == id && operatelog.IsDelete == false);
            ViewBag.operatelog = tempData;
            if (tempData == null)
                return View();
            else
            {
                operatelogViewModel operatelogviewmodel = new operatelogViewModel();
                operatelogviewmodel.ID = tempData.ID;
                operatelogviewmodel.ModelID = tempData.ModelID;
                operatelogviewmodel.OpInfo = tempData.OpInfo;
                operatelogviewmodel.OpMan = tempData.OpMan;
                operatelogviewmodel.OpDate = tempData.OpDate;
                return View(operatelogviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string modelid = Request["modelid"] ?? "";
            string opinfo = Request["opinfo"] ?? "";
            string opman = Request["opman"] ?? "";
            string opdate = Request["opdate"] ?? "";
            int uid = int.Parse(id);
            try
            {
                operatelog p = ob_operatelogservice.GetEntityById(operatelog => operatelog.ID == uid);
                p.ModelID = modelid.Trim();
                p.OpInfo = opinfo.Trim();
                p.OpMan = opman.Trim();
                p.OpDate = opdate == "" ? DateTime.Now : DateTime.Parse(opdate);
                ob_operatelogservice.UpdateEntity(p);
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
            operatelog ob_operatelog;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_operatelog = ob_operatelogservice.GetEntityById(operatelog => operatelog.ID == id && operatelog.IsDelete == false);
                    ob_operatelog.IsDelete = true;
                    ob_operatelogservice.UpdateEntity(ob_operatelog);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

