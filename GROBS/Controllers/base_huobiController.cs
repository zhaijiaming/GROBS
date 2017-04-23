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
    public class base_huobiController : Controller
    {
        private Ibase_huobiService ob_base_huobiservice = ServiceFactory.base_huobiservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_huobi_index";
            PageMenu.Set("Index", "base_huobi", "基础数据");
            Expression<Func<base_huobi, bool>> where = PredicateExtensionses.True<base_huobi>();
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
                                        where = where.And(base_huobi => base_huobi.Bianhao == bianhao);
                                    else
                                        where = where.Or(base_huobi => base_huobi.Bianhao == bianhao);
                                }
                                if (bianhaoequal.Equals("like"))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(base_huobi => base_huobi.Bianhao.Contains(bianhao));
                                    else
                                        where = where.Or(base_huobi => base_huobi.Bianhao.Contains(bianhao));
                                }
                            }
                            break;
                        case "mingcheng":
                            string mingcheng = scld[1];
                            string mingchengequal = scld[2];
                            string mingchengand = scld[3];
                            if (!string.IsNullOrEmpty(mingcheng))
                            {
                                if (mingchengequal.Equals("="))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_huobi => base_huobi.Mingcheng == mingcheng);
                                    else
                                        where = where.Or(base_huobi => base_huobi.Mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_huobi => base_huobi.Mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(base_huobi => base_huobi.Mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_huobi => base_huobi.IsDelete == false);

            var tempData = ob_base_huobiservice.LoadSortEntities(where.Compile(), false, base_huobi => base_huobi.ID).ToPagedList<base_huobi>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_huobi = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_huobi_index";
            string page = "1";
            //bianhao
            string bianhao = Request["bianhao"] ?? "";
            string bianhaoequal = Request["bianhaoequal"] ?? "";
            string bianhaoand = Request["bianhaoand"] ?? "";
            //mingcheng
            string mingcheng = Request["mingcheng"] ?? "";
            string mingchengequal = Request["mingchengequal"] ?? "";
            string mingchengand = Request["mingchengand"] ?? "";
            PageMenu.Set("Index", "base_huobi", "基础数据");
            Expression<Func<base_huobi, bool>> where = PredicateExtensionses.True<base_huobi>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //bianhao
                if (!string.IsNullOrEmpty(bianhao))
                {
                    if (bianhaoequal.Equals("="))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_huobi => base_huobi.Bianhao == bianhao);
                        else
                            where = where.Or(base_huobi => base_huobi.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_huobi => base_huobi.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_huobi => base_huobi.Bianhao.Contains(bianhao));
                    }
                }
                if (!string.IsNullOrEmpty(bianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", bianhao, bianhaoequal, bianhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", "", bianhaoequal, bianhaoand);
                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_huobi => base_huobi.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_huobi => base_huobi.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_huobi => base_huobi.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_huobi => base_huobi.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //bianhao
                if (!string.IsNullOrEmpty(bianhao))
                {
                    if (bianhaoequal.Equals("="))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_huobi => base_huobi.Bianhao == bianhao);
                        else
                            where = where.Or(base_huobi => base_huobi.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_huobi => base_huobi.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_huobi => base_huobi.Bianhao.Contains(bianhao));
                    }
                }
                if (!string.IsNullOrEmpty(bianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", bianhao, bianhaoequal, bianhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", "", bianhaoequal, bianhaoand);
                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_huobi => base_huobi.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_huobi => base_huobi.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_huobi => base_huobi.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_huobi => base_huobi.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_huobi => base_huobi.IsDelete == false);

            var tempData = ob_base_huobiservice.LoadSortEntities(where.Compile(), false, base_huobi => base_huobi.ID).ToPagedList<base_huobi>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_huobi = tempData;
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
                base_huobi ob_base_huobi = new base_huobi();
                ob_base_huobi.Bianhao = bianhao.Trim();
                ob_base_huobi.Mingcheng = mingcheng.Trim();
                ob_base_huobi.Miaoshu = miaoshu.Trim();
                ob_base_huobi.Col1 = col1.Trim();
                ob_base_huobi.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_huobi.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_huobi = ob_base_huobiservice.AddEntity(ob_base_huobi);
                ViewBag.base_huobi = ob_base_huobi;
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
            base_huobi tempData = ob_base_huobiservice.GetEntityById(base_huobi => base_huobi.ID == id && base_huobi.IsDelete == false);
            ViewBag.base_huobi = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_huobiViewModel base_huobiviewmodel = new base_huobiViewModel();
                base_huobiviewmodel.ID = tempData.ID;
                base_huobiviewmodel.Bianhao = tempData.Bianhao;
                base_huobiviewmodel.Mingcheng = tempData.Mingcheng;
                base_huobiviewmodel.Miaoshu = tempData.Miaoshu;
                base_huobiviewmodel.Col1 = tempData.Col1;
                base_huobiviewmodel.MakeDate = tempData.MakeDate;
                base_huobiviewmodel.MakeMan = tempData.MakeMan;
                return View(base_huobiviewmodel);
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
                base_huobi p = ob_base_huobiservice.GetEntityById(base_huobi => base_huobi.ID == uid);
                p.Bianhao = bianhao.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.Miaoshu = miaoshu.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_huobiservice.UpdateEntity(p);
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
            base_huobi ob_base_huobi;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_huobi = ob_base_huobiservice.GetEntityById(base_huobi => base_huobi.ID == id && base_huobi.IsDelete == false);
                    ob_base_huobi.IsDelete = true;
                    ob_base_huobiservice.UpdateEntity(ob_base_huobi);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

