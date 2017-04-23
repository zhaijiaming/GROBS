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
    public class base_feiyongxmController : Controller
    {
        private Ibase_feiyongxmService ob_base_feiyongxmservice = ServiceFactory.base_feiyongxmservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_feiyongxm_index";
            PageMenu.Set("Index", "base_feiyongxm", "基础数据");
            Expression<Func<base_feiyongxm, bool>> where = PredicateExtensionses.True<base_feiyongxm>();
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
                                        where = where.And(base_feiyongxm => base_feiyongxm.Bianhao == bianhao);
                                    else
                                        where = where.Or(base_feiyongxm => base_feiyongxm.Bianhao == bianhao);
                                }
                                if (bianhaoequal.Equals("like"))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(base_feiyongxm => base_feiyongxm.Bianhao.Contains(bianhao));
                                    else
                                        where = where.Or(base_feiyongxm => base_feiyongxm.Bianhao.Contains(bianhao));
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
                                        where = where.And(base_feiyongxm => base_feiyongxm.Mingcheng == mingcheng);
                                    else
                                        where = where.Or(base_feiyongxm => base_feiyongxm.Mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_feiyongxm => base_feiyongxm.Mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(base_feiyongxm => base_feiyongxm.Mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_feiyongxm => base_feiyongxm.IsDelete == false);

            var tempData = ob_base_feiyongxmservice.LoadSortEntities(where.Compile(), false, base_feiyongxm => base_feiyongxm.ID).ToPagedList<base_feiyongxm>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_feiyongxm = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_feiyongxm_index";
            string page = "1";
            string bianhao = Request["bianhao"] ?? "";
            string bianhaoequal = Request["bianhaoequal"] ?? "";
            string bianhaoand = Request["bianhaoand"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string mingchengequal = Request["mingchengequal"] ?? "";
            string mingchengand = Request["mingchengand"] ?? "";
            PageMenu.Set("Index", "base_feiyongxm", "基础数据");
            Expression<Func<base_feiyongxm, bool>> where = PredicateExtensionses.True<base_feiyongxm>();
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
                            where = where.And(base_feiyongxm => base_feiyongxm.Bianhao == bianhao);
                        else
                            where = where.Or(base_feiyongxm => base_feiyongxm.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_feiyongxm => base_feiyongxm.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_feiyongxm => base_feiyongxm.Bianhao.Contains(bianhao));
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
                            where = where.And(base_feiyongxm => base_feiyongxm.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_feiyongxm => base_feiyongxm.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_feiyongxm => base_feiyongxm.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_feiyongxm => base_feiyongxm.Mingcheng.Contains(mingcheng));
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
                            where = where.And(base_feiyongxm => base_feiyongxm.Bianhao == bianhao);
                        else
                            where = where.Or(base_feiyongxm => base_feiyongxm.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_feiyongxm => base_feiyongxm.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_feiyongxm => base_feiyongxm.Bianhao.Contains(bianhao));
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
                            where = where.And(base_feiyongxm => base_feiyongxm.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_feiyongxm => base_feiyongxm.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_feiyongxm => base_feiyongxm.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_feiyongxm => base_feiyongxm.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_feiyongxm => base_feiyongxm.IsDelete == false);

            var tempData = ob_base_feiyongxmservice.LoadSortEntities(where.Compile(), false, base_feiyongxm => base_feiyongxm.ID).ToPagedList<base_feiyongxm>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_feiyongxm = tempData;
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
            string gongsi = Request["gongsi"] ?? "";
            string miaoshu = Request["miaoshu"] ?? "";
            string kemu = Request["kemu"] ?? "";
            string duima = Request["duima"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                base_feiyongxm ob_base_feiyongxm = new base_feiyongxm();
                ob_base_feiyongxm.Bianhao = bianhao.Trim();
                ob_base_feiyongxm.Mingcheng = mingcheng.Trim();
                ob_base_feiyongxm.Gongsi = gongsi == "" ? 0 : int.Parse(gongsi);
                ob_base_feiyongxm.Miaoshu = miaoshu.Trim();
                ob_base_feiyongxm.Kemu = kemu.Trim();
                ob_base_feiyongxm.Duima = duima.Trim();
                ob_base_feiyongxm.Col1 = col1.Trim();
                ob_base_feiyongxm.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_feiyongxm.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_feiyongxm = ob_base_feiyongxmservice.AddEntity(ob_base_feiyongxm);
                ViewBag.base_feiyongxm = ob_base_feiyongxm;
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
            base_feiyongxm tempData = ob_base_feiyongxmservice.GetEntityById(base_feiyongxm => base_feiyongxm.ID == id && base_feiyongxm.IsDelete == false);
            ViewBag.base_feiyongxm = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_feiyongxmViewModel base_feiyongxmviewmodel = new base_feiyongxmViewModel();
                base_feiyongxmviewmodel.ID = tempData.ID;
                base_feiyongxmviewmodel.Bianhao = tempData.Bianhao;
                base_feiyongxmviewmodel.Mingcheng = tempData.Mingcheng;
                base_feiyongxmviewmodel.Gongsi = tempData.Gongsi;
                base_feiyongxmviewmodel.Miaoshu = tempData.Miaoshu;
                base_feiyongxmviewmodel.Kemu = tempData.Kemu;
                base_feiyongxmviewmodel.Duima = tempData.Duima;
                base_feiyongxmviewmodel.Col1 = tempData.Col1;
                base_feiyongxmviewmodel.MakeDate = tempData.MakeDate;
                base_feiyongxmviewmodel.MakeMan = tempData.MakeMan;
                return View(base_feiyongxmviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string bianhao = Request["bianhao"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string gongsi = Request["gongsi"] ?? "";
            string miaoshu = Request["miaoshu"] ?? "";
            string kemu = Request["kemu"] ?? "";
            string duima = Request["duima"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_feiyongxm p = ob_base_feiyongxmservice.GetEntityById(base_feiyongxm => base_feiyongxm.ID == uid);
                p.Bianhao = bianhao.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.Gongsi = gongsi == "" ? 0 : int.Parse(gongsi);
                p.Miaoshu = miaoshu.Trim();
                p.Kemu = kemu.Trim();
                p.Duima = duima.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_feiyongxmservice.UpdateEntity(p);
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
            base_feiyongxm ob_base_feiyongxm;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_feiyongxm = ob_base_feiyongxmservice.GetEntityById(base_feiyongxm => base_feiyongxm.ID == id && base_feiyongxm.IsDelete == false);
                    ob_base_feiyongxm.IsDelete = true;
                    ob_base_feiyongxmservice.UpdateEntity(ob_base_feiyongxm);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

