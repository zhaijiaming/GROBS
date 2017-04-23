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
    public class base_quyuController : Controller
    {
        public class Quyu
        {
            public int ID { get; set; }
            public string Bianhao { get; set; }
            public string Mingcheng { get; set; }
            public string Miaoshu { get; set; }
        }
        private Ibase_quyuService ob_base_quyuservice = ServiceFactory.base_quyuservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_quyu_index";
            PageMenu.Set("Index", "base_quyu", "基础数据");
            Expression<Func<base_quyu, bool>> where = PredicateExtensionses.True<base_quyu>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "bianhao":
                            string bianhao = scld[1];
                            string bianhaoequal = scld[2];
                            string bianhaoand = scld[3];
                            if (!string.IsNullOrEmpty(bianhao))
                            {
                                if (bianhaoequal.Equals("="))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(base_quyu => base_quyu.Bianhao == bianhao);
                                    else
                                        where = where.Or(base_quyu => base_quyu.Bianhao == bianhao);
                                }
                                if (bianhaoequal.Equals("like"))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(base_quyu => base_quyu.Bianhao.Contains(bianhao));
                                    else
                                        where = where.Or(base_quyu => base_quyu.Bianhao.Contains(bianhao));
                                }
                            }
                            break;
                        case "miaoshu":
                            string miaoshu = scld[1];
                            string miaoshuequal = scld[2];
                            string miaoshuand = scld[3];
                            if (!string.IsNullOrEmpty(miaoshu))
                            {
                                if (miaoshuequal.Equals("="))
                                {
                                    if (miaoshuand.Equals("and"))
                                        where = where.And(base_quyu => base_quyu.Miaoshu == miaoshu);
                                    else
                                        where = where.Or(base_quyu => base_quyu.Miaoshu == miaoshu);
                                }
                                if (miaoshuequal.Equals("like"))
                                {
                                    if (miaoshuand.Equals("and"))
                                        where = where.And(base_quyu => base_quyu.Miaoshu.Contains(miaoshu));
                                    else
                                        where = where.Or(base_quyu => base_quyu.Miaoshu.Contains(miaoshu));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_quyu => base_quyu.IsDelete == false);

            var tempData = ob_base_quyuservice.LoadSortEntities(where.Compile(), false, base_quyu => base_quyu.ID).ToPagedList<base_quyu>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_quyu = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_quyu_index";
            string page = "1";
            string bianhao = Request["bianhao"] ?? "";
            string bianhaoequal = Request["bianhaoequal"] ?? "";
            string bianhaoand = Request["bianhaoand"] ?? "";
            string miaoshu = Request["miaoshu"] ?? "";
            string miaoshuequal = Request["miaoshuequal"] ?? "";
            string miaoshuand = Request["miaoshuand"] ?? "";
            PageMenu.Set("Index", "base_quyu", "基础数据");
            Expression<Func<base_quyu, bool>> where = PredicateExtensionses.True<base_quyu>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(bianhao))
                {
                    if (bianhaoequal.Equals("="))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_quyu => base_quyu.Bianhao == bianhao);
                        else
                            where = where.Or(base_quyu => base_quyu.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_quyu => base_quyu.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_quyu => base_quyu.Bianhao.Contains(bianhao));
                    }
                }
                if (!string.IsNullOrEmpty(bianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", bianhao, bianhaoequal, bianhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", "", bianhaoequal, bianhaoand);
                //miaoshu
                if (!string.IsNullOrEmpty(miaoshu))
                {
                    if (miaoshuequal.Equals("="))
                    {
                        if (miaoshuand.Equals("and"))
                            where = where.And(base_quyu => base_quyu.Miaoshu == miaoshu);
                        else
                            where = where.Or(base_quyu => base_quyu.Miaoshu == miaoshu);
                    }
                    if (miaoshuequal.Equals("like"))
                    {
                        if (miaoshuand.Equals("and"))
                            where = where.And(base_quyu => base_quyu.Miaoshu.Contains(miaoshu));
                        else
                            where = where.Or(base_quyu => base_quyu.Miaoshu.Contains(miaoshu));
                    }
                }
                if (!string.IsNullOrEmpty(miaoshu))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "miaoshu", miaoshu, miaoshuequal, miaoshuand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "miaoshu", "", miaoshuequal, miaoshuand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(bianhao))
                {
                    if (bianhaoequal.Equals("="))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_quyu => base_quyu.Bianhao == bianhao);
                        else
                            where = where.Or(base_quyu => base_quyu.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_quyu => base_quyu.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_quyu => base_quyu.Bianhao.Contains(bianhao));
                    }
                }
                if (!string.IsNullOrEmpty(bianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", bianhao, bianhaoequal, bianhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", "", bianhaoequal, bianhaoand);

                if (!string.IsNullOrEmpty(miaoshu))
                {
                    if (miaoshuequal.Equals("="))
                    {
                        if (miaoshuand.Equals("and"))
                            where = where.And(base_quyu => base_quyu.Miaoshu == miaoshu);
                        else
                            where = where.Or(base_quyu => base_quyu.Miaoshu == miaoshu);
                    }
                    if (miaoshuequal.Equals("like"))
                    {
                        if (miaoshuand.Equals("and"))
                            where = where.And(base_quyu => base_quyu.Miaoshu.Contains(miaoshu));
                        else
                            where = where.Or(base_quyu => base_quyu.Miaoshu.Contains(miaoshu));
                    }
                }
                if (!string.IsNullOrEmpty(miaoshu))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "miaoshu", miaoshu, miaoshuequal, miaoshuand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "miaoshu", "", miaoshuequal, miaoshuand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_quyu => base_quyu.IsDelete == false);

            var tempData = ob_base_quyuservice.LoadSortEntities(where.Compile(), false, base_quyu => base_quyu.ID).ToPagedList<base_quyu>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_quyu = tempData;
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
            string bianhao = Request["bianhao"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string miaoshu = Request["miaoshu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                base_quyu ob_base_quyu = new base_quyu();
                ob_base_quyu.Bianhao = bianhao.Trim();
                ob_base_quyu.Mingcheng = mingcheng.Trim();
                ob_base_quyu.Miaoshu = miaoshu.Trim();
                ob_base_quyu.Col1 = col1.Trim();
                ob_base_quyu.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_quyu.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_quyu = ob_base_quyuservice.AddEntity(ob_base_quyu);
                ViewBag.base_quyu = ob_base_quyu;
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
            base_quyu tempData = ob_base_quyuservice.GetEntityById(base_quyu => base_quyu.ID == id && base_quyu.IsDelete == false);
            ViewBag.base_quyu = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_quyuViewModel base_quyuviewmodel = new base_quyuViewModel();
                base_quyuviewmodel.ID = tempData.ID;
                base_quyuviewmodel.Bianhao = tempData.Bianhao;
                base_quyuviewmodel.Mingcheng = tempData.Mingcheng;
                base_quyuviewmodel.Miaoshu = tempData.Miaoshu;
                base_quyuviewmodel.Col1 = tempData.Col1;
                base_quyuviewmodel.MakeDate = tempData.MakeDate;
                base_quyuviewmodel.MakeMan = tempData.MakeMan;
                return View(base_quyuviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string bianhao = Request["bianhao"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string miaoshu = Request["miaoshu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_quyu p = ob_base_quyuservice.GetEntityById(base_quyu => base_quyu.ID == uid);
                p.Bianhao = bianhao.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.Miaoshu = miaoshu.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_quyuservice.UpdateEntity(p);
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
            base_quyu ob_base_quyu;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_quyu = ob_base_quyuservice.GetEntityById(base_quyu => base_quyu.ID == id && base_quyu.IsDelete == false);
                    ob_base_quyu.IsDelete = true;
                    ob_base_quyuservice.UpdateEntity(ob_base_quyu);
                }
            }
            return RedirectToAction("Index");
        }
        public JsonResult Getquyu()
        {
            var tempquyu = ob_base_quyuservice.LoadSortEntities(p => p.IsDelete == false, true, p => p.ID);
            List<Quyu> _quyulist = new List<Quyu>();
            Quyu _qy;
            foreach (var qy in tempquyu)
            {
                _qy = new Quyu();
                _qy.ID = qy.ID;
                _qy.Bianhao = qy.Bianhao;
                _qy.Mingcheng = qy.Mingcheng;
                _qy.Miaoshu = qy.Miaoshu;
                _quyulist.Add(_qy);
            }
            return Json(_quyulist);
        }
    }
}

