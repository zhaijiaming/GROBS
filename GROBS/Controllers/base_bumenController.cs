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
    public class base_bumenController : Controller
    {
        private Ibase_bumenService ob_base_bumenservice = ServiceFactory.base_bumenservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_bumen_index";
            PageMenu.Set("Index", "base_bumen", "基础数据");
            Expression<Func<base_bumen, bool>> where = PredicateExtensionses.True<base_bumen>();
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
                                        where = where.And(base_bumen => base_bumen.Bianhao == bianhao);
                                    else
                                        where = where.Or(base_bumen => base_bumen.Bianhao == bianhao);
                                }
                                if (bianhaoequal.Equals("like"))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(base_bumen => base_bumen.Bianhao.Contains(bianhao));
                                    else
                                        where = where.Or(base_bumen => base_bumen.Bianhao.Contains(bianhao));
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
                                        where = where.And(base_bumen => base_bumen.Mingcheng == mingcheng);
                                    else
                                        where = where.Or(base_bumen => base_bumen.Mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_bumen => base_bumen.Mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(base_bumen => base_bumen.Mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_bumen => base_bumen.IsDelete == false);

            var tempData = ob_base_bumenservice.LoadSortEntities(where.Compile(), false, base_bumen => base_bumen.ID).ToPagedList<base_bumen>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_bumen = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_bumen_index";
            string page = "1";
            string bianhao = Request["bianhao"] ?? "";
            string bianhaoequal = Request["bianhaoequal"] ?? "";
            string bianhaoand = Request["bianhaoand"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string mingchengequal = Request["mingchengequal"] ?? "";
            string mingchengand = Request["mingchengand"] ?? "";
            PageMenu.Set("Index", "base_bumen", "基础数据");
            Expression<Func<base_bumen, bool>> where = PredicateExtensionses.True<base_bumen>();
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
                            where = where.And(base_bumen => base_bumen.Bianhao == bianhao);
                        else
                            where = where.Or(base_bumen => base_bumen.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_bumen => base_bumen.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_bumen => base_bumen.Bianhao.Contains(bianhao));
                    }
                }
                if (!string.IsNullOrEmpty(bianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", bianhao, bianhaoequal, bianhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", "", bianhaoequal, bianhaoand);

                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_bumen => base_bumen.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_bumen => base_bumen.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_bumen => base_bumen.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_bumen => base_bumen.Mingcheng.Contains(mingcheng));
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
                if (!string.IsNullOrEmpty(bianhao))
                {
                    if (bianhaoequal.Equals("="))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_bumen => base_bumen.Bianhao == bianhao);
                        else
                            where = where.Or(base_bumen => base_bumen.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_bumen => base_bumen.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_bumen => base_bumen.Bianhao.Contains(bianhao));
                    }
                }
                if (!string.IsNullOrEmpty(bianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", bianhao, bianhaoequal, bianhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", "", bianhaoequal, bianhaoand);

                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_bumen => base_bumen.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_bumen => base_bumen.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_bumen => base_bumen.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_bumen => base_bumen.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_bumen => base_bumen.IsDelete == false);

            var tempData = ob_base_bumenservice.LoadSortEntities(where.Compile(), false, base_bumen => base_bumen.ID).ToPagedList<base_bumen>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_bumen = tempData;
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
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                base_bumen ob_base_bumen = new base_bumen();
                ob_base_bumen.Bianhao = bianhao.Trim();
                ob_base_bumen.Mingcheng = mingcheng.Trim();
                ob_base_bumen.Gongsi = gongsi == "" ? 0 : int.Parse(gongsi);
                ob_base_bumen.Miaoshu = miaoshu.Trim();
                ob_base_bumen.Col1 = col1.Trim();
                ob_base_bumen.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_bumen.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_bumen = ob_base_bumenservice.AddEntity(ob_base_bumen);
                ViewBag.base_bumen = ob_base_bumen;
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
            base_bumen tempData = ob_base_bumenservice.GetEntityById(base_bumen => base_bumen.ID == id && base_bumen.IsDelete == false);
            ViewBag.base_bumen = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_bumenViewModel base_bumenviewmodel = new base_bumenViewModel();
                base_bumenviewmodel.ID = tempData.ID;
                base_bumenviewmodel.Bianhao = tempData.Bianhao;
                base_bumenviewmodel.Mingcheng = tempData.Mingcheng;
                base_bumenviewmodel.Gongsi = tempData.Gongsi;
                base_bumenviewmodel.Miaoshu = tempData.Miaoshu;
                base_bumenviewmodel.Col1 = tempData.Col1;
                base_bumenviewmodel.MakeDate = tempData.MakeDate;
                base_bumenviewmodel.MakeMan = tempData.MakeMan;
                return View(base_bumenviewmodel);
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
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_bumen p = ob_base_bumenservice.GetEntityById(base_bumen => base_bumen.ID == uid);
                p.Bianhao = bianhao.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.Gongsi = gongsi == "" ? 0 : int.Parse(gongsi);
                p.Miaoshu = miaoshu.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_bumenservice.UpdateEntity(p);
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
            base_bumen ob_base_bumen;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_bumen = ob_base_bumenservice.GetEntityById(base_bumen => base_bumen.ID == id && base_bumen.IsDelete == false);
                    ob_base_bumen.IsDelete = true;
                    ob_base_bumenservice.UpdateEntity(ob_base_bumen);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

