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
    public class rmd_myreminderController : Controller
    {
        private Irmd_myreminderService ob_rmd_myreminderservice = ServiceFactory.rmd_myreminderservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "rmd_myreminder_index";
            PageMenu.Set("Index", "rmd_myreminder", "信息提醒");
            Expression<Func<rmd_myreminder, bool>> where = PredicateExtensionses.True<rmd_myreminder>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "yonghuid":
                            string yonghuid = scld[1];
                            string yonghuidequal = scld[2];
                            string yonghuidand = scld[3];
                            if (!string.IsNullOrEmpty(yonghuid))
                            {
                                if (yonghuidequal.Equals("="))
                                {
                                    if (yonghuidand.Equals("and"))
                                        where = where.And(rmd_myreminder => rmd_myreminder.YonghuID == int.Parse(yonghuid));
                                    else
                                        where = where.Or(rmd_myreminder => rmd_myreminder.YonghuID == int.Parse(yonghuid));
                                }
                                if (yonghuidequal.Equals(">"))
                                {
                                    if (yonghuidand.Equals("and"))
                                        where = where.And(rmd_myreminder => rmd_myreminder.YonghuID > int.Parse(yonghuid));
                                    else
                                        where = where.Or(rmd_myreminder => rmd_myreminder.YonghuID > int.Parse(yonghuid));
                                }
                                if (yonghuidequal.Equals("<"))
                                {
                                    if (yonghuidand.Equals("and"))
                                        where = where.And(rmd_myreminder => rmd_myreminder.YonghuID < int.Parse(yonghuid));
                                    else
                                        where = where.Or(rmd_myreminder => rmd_myreminder.YonghuID < int.Parse(yonghuid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(rmd_myreminder => rmd_myreminder.IsDelete == false);

            var tempData = ob_rmd_myreminderservice.LoadSortEntities(where.Compile(), false, rmd_myreminder => rmd_myreminder.ID).ToPagedList<rmd_myreminder>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.rmd_myreminder = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "rmd_myreminder_index";
            string page = "1";
            string yonghuid = Request["yonghuid"] ?? "";
            string yonghuidequal = Request["yonghuidequal"] ?? "";
            string yonghuidand = Request["yonghuidand"] ?? "";
            PageMenu.Set("Index", "rmd_myreminder", "信息提醒");
            Expression<Func<rmd_myreminder, bool>> where = PredicateExtensionses.True<rmd_myreminder>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(yonghuid))
                {
                    if (yonghuidequal.Equals("="))
                    {
                        if (yonghuidand.Equals("and"))
                            where = where.And(rmd_myreminder => rmd_myreminder.YonghuID == int.Parse(yonghuid));
                        else
                            where = where.Or(rmd_myreminder => rmd_myreminder.YonghuID == int.Parse(yonghuid));
                    }
                    if (yonghuidequal.Equals(">"))
                    {
                        if (yonghuidand.Equals("and"))
                            where = where.And(rmd_myreminder => rmd_myreminder.YonghuID > int.Parse(yonghuid));
                        else
                            where = where.Or(rmd_myreminder => rmd_myreminder.YonghuID > int.Parse(yonghuid));
                    }
                    if (yonghuidequal.Equals("<"))
                    {
                        if (yonghuidand.Equals("and"))
                            where = where.And(rmd_myreminder => rmd_myreminder.YonghuID < int.Parse(yonghuid));
                        else
                            where = where.Or(rmd_myreminder => rmd_myreminder.YonghuID < int.Parse(yonghuid));
                    }
                }
                if (!string.IsNullOrEmpty(yonghuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "yonghuid", yonghuid, yonghuidequal, yonghuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "yonghuid", "", yonghuidequal, yonghuidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(yonghuid))
                {
                    if (yonghuidequal.Equals("="))
                    {
                        if (yonghuidand.Equals("and"))
                            where = where.And(rmd_myreminder => rmd_myreminder.YonghuID == int.Parse(yonghuid));
                        else
                            where = where.Or(rmd_myreminder => rmd_myreminder.YonghuID == int.Parse(yonghuid));
                    }
                    if (yonghuidequal.Equals(">"))
                    {
                        if (yonghuidand.Equals("and"))
                            where = where.And(rmd_myreminder => rmd_myreminder.YonghuID > int.Parse(yonghuid));
                        else
                            where = where.Or(rmd_myreminder => rmd_myreminder.YonghuID > int.Parse(yonghuid));
                    }
                    if (yonghuidequal.Equals("<"))
                    {
                        if (yonghuidand.Equals("and"))
                            where = where.And(rmd_myreminder => rmd_myreminder.YonghuID < int.Parse(yonghuid));
                        else
                            where = where.Or(rmd_myreminder => rmd_myreminder.YonghuID < int.Parse(yonghuid));
                    }
                }
                if (!string.IsNullOrEmpty(yonghuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "yonghuid", yonghuid, yonghuidequal, yonghuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "yonghuid", "", yonghuidequal, yonghuidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(rmd_myreminder => rmd_myreminder.IsDelete == false);

            var tempData = ob_rmd_myreminderservice.LoadSortEntities(where.Compile(), false, rmd_myreminder => rmd_myreminder.ID).ToPagedList<rmd_myreminder>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.rmd_myreminder = tempData;
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
            string yonghuid = Request["yonghuid"] ?? "";
            string tixingid = Request["tixingid"] ?? "";
            string tixingzq = Request["tixingzq"] ?? "";
            string yemianxs = Request["yemianxs"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            if (yemianxs.IndexOf("true") > -1)
                yemianxs = "true";

            try
            {
                rmd_myreminder ob_rmd_myreminder = new rmd_myreminder();
                ob_rmd_myreminder.YonghuID = yonghuid == "" ? 0 : int.Parse(yonghuid);
                ob_rmd_myreminder.TixingID = tixingid == "" ? 0 : int.Parse(tixingid);
                ob_rmd_myreminder.TixingZQ = tixingzq == "" ? 0 : int.Parse(tixingzq);
                ob_rmd_myreminder.YemianXS = yemianxs == "" ? false : Boolean.Parse(yemianxs);
                ob_rmd_myreminder.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_rmd_myreminder.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_rmd_myreminder = ob_rmd_myreminderservice.AddEntity(ob_rmd_myreminder);
                ViewBag.rmd_myreminder = ob_rmd_myreminder;
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
            rmd_myreminder tempData = ob_rmd_myreminderservice.GetEntityById(rmd_myreminder => rmd_myreminder.ID == id && rmd_myreminder.IsDelete == false);
            ViewBag.rmd_myreminder = tempData;
            if (tempData == null)
                return View();
            else
            {
                rmd_myreminderViewModel rmd_myreminderviewmodel = new rmd_myreminderViewModel();
                rmd_myreminderviewmodel.ID = tempData.ID;
                rmd_myreminderviewmodel.YonghuID = tempData.YonghuID;
                rmd_myreminderviewmodel.TixingID = tempData.TixingID;
                rmd_myreminderviewmodel.TixingZQ = tempData.TixingZQ;
                rmd_myreminderviewmodel.YemianXS = tempData.YemianXS;
                rmd_myreminderviewmodel.MakeDate = tempData.MakeDate;
                rmd_myreminderviewmodel.MakeMan = tempData.MakeMan;
                return View(rmd_myreminderviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string yonghuid = Request["yonghuid"] ?? "";
            string tixingid = Request["tixingid"] ?? "";
            string tixingzq = Request["tixingzq"] ?? "";
            string yemianxs = Request["yemianxs"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            if (yemianxs.IndexOf("true") > -1)
                yemianxs = "true";

            try
            {
                rmd_myreminder p = ob_rmd_myreminderservice.GetEntityById(rmd_myreminder => rmd_myreminder.ID == uid);
                p.YonghuID = yonghuid == "" ? 0 : int.Parse(yonghuid);
                p.TixingID = tixingid == "" ? 0 : int.Parse(tixingid);
                p.TixingZQ = tixingzq == "" ? 0 : int.Parse(tixingzq);
                p.YemianXS = yemianxs == "" ? false : Boolean.Parse(yemianxs);
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_rmd_myreminderservice.UpdateEntity(p);
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
            rmd_myreminder ob_rmd_myreminder;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_rmd_myreminder = ob_rmd_myreminderservice.GetEntityById(rmd_myreminder => rmd_myreminder.ID == id && rmd_myreminder.IsDelete == false);
                    ob_rmd_myreminder.IsDelete = true;
                    ob_rmd_myreminderservice.UpdateEntity(ob_rmd_myreminder);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult ReminderList()
        {
            int _userid = (int)Session["user_id"];
            PageMenu.Set("ReminderList", "rmd_myreminder", "信息提醒");

            List<rmd_remindlistViewModel> _reminds = new List<rmd_remindlistViewModel>();
            var _myremind = ob_rmd_myreminderservice.LoadEntities(p => p.YonghuID == _userid && p.IsDelete == false).ToList<rmd_myreminder>();
            if (_myremind.Count > 0)
            {
                foreach (var rmd in _myremind)
                {
                    rmd_remindlistViewModel _rmd = GetReminder((int)rmd.TixingID, (int)rmd.TixingZQ);
                    if (_rmd != null)
                        _reminds.Add(_rmd);
                }
            }

            ViewBag.RemindList = _reminds;
            return View();
        }
        protected rmd_remindlistViewModel GetReminder(int remindobject, int remindperiod)
        {
            rmd_remindlistViewModel _rmd = new rmd_remindlistViewModel();
            switch (remindobject)
            {
                case 2:
                    var _Zcz = ServiceFactory.base_shangpinzczservice.LoadEntities(p => p.IsDelete == false && p.ShixiaoSF == false && p.ZhucezhengYXQ <= DateTime.Now.AddDays(remindperiod)).ToList<base_shangpinzcz>();
                    var _ZczCount = (from s in _Zcz
                                     select s.ID).Count();
                    if (remindperiod == -1)
                        _rmd.Info = "已经超期的注册证有 " + _ZczCount.ToString() + "件";
                    else
                        _rmd.Info = "近效期 " + remindperiod.ToString() + "天的注册证有 " + _ZczCount.ToString() + "件";
                    _rmd.Ref = "/base_shangpinzcz/overdue?period=" + remindperiod.ToString();
                    break;
                case 3:
                    var _HZAuthor = ServiceFactory.base_huozhushouquanservice.LoadEntities(p => p.IsDelete == false && p.ShouquanshuYXQ <= DateTime.Now.AddDays(remindperiod)).ToList<base_huozhushouquan>();
                    var _HZACount = (from s in _HZAuthor select s.ID).Count();
                    if (remindperiod == -1)
                        _rmd.Info = "已经超期的授权有 " + _HZACount.ToString() + "条";
                    else
                        _rmd.Info = "近效期 " + remindperiod.ToString() + "天的授权,有 " + _HZACount.ToString() + "条";
                    _rmd.Ref = "/base_huozhushouquan/overdue?period=" + remindperiod.ToString();
                    break;
                case 4:
                    var _WTjy = ServiceFactory.base_weituokehuservice.LoadEntities(p => p.HezuoSF == true && p.IsDelete == false && p.YingyezhizhaoYXQ <= DateTime.Now.AddDays(remindperiod)).ToList<base_weituokehu>();
                    var _wtjyCount = (from s in _WTjy select s.ID).Count();
                    if (remindperiod == -1)
                        _rmd.Info = "货主营业执照已经超期的有 " + _wtjyCount.ToString() + "条";
                    else
                        _rmd.Info = "近效期 " + remindperiod.ToString() + "天的营业执照,有 " + _wtjyCount.ToString() + "条";
                    _rmd.Ref = "/base_weituokehu/yyzzoverdue?period=" + remindperiod.ToString();
                    break;
                case 5:
                    var _WTxk = ServiceFactory.base_weituokehuservice.LoadEntities(p => p.HezuoSF == true && p.IsDelete == false && p.JingyingxukeYXQ <= DateTime.Now.AddDays(remindperiod)).ToList<base_weituokehu>();
                    var _wtxkCount = (from s in _WTxk select s.ID).Count();
                    if (remindperiod == -1)
                        _rmd.Info = "货主经营许可已经超期的有 " + _wtxkCount.ToString() + "条";
                    else
                        _rmd.Info = "近效期 " + remindperiod.ToString() + "天的经营许可,有 " + _wtxkCount.ToString() + "条";
                    _rmd.Ref = "/base_weituokehu/jyxkoverdue?period=" + remindperiod.ToString();
                    break;
                case 0:
                default:
                    _rmd = null;
                    break;
            }
            return _rmd;
        }
        protected rmd_remindlistViewModel GetInventory(int remindperiod)
        {
            rmd_remindlistViewModel _rmd = new rmd_remindlistViewModel();
            switch (remindperiod)
            {
                case 1:
                    break;
                case 0:
                default:
                    break;
            }

            return _rmd;
        }
    }
}

