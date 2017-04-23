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
    public class Danwei
    {
        public int ID { get; set; }
        public string Bianhao { get; set; }
        public string Mingcheng { get; set; }
        public string Miaoshu { get; set; }
    }
    public class base_jiliangdwController : Controller
    {
        private Ibase_jiliangdwService ob_base_jiliangdwservice = ServiceFactory.base_jiliangdwservice;
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_jiliangdw_index";
            PageMenu.Set("Index", "base_jiliangdw", "基础数据");
            Expression<Func<base_jiliangdw, bool>> where = PredicateExtensionses.True<base_jiliangdw>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
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
                                        where = where.And(base_jiliangdw => base_jiliangdw.Bianhao == bianhao);
                                    else
                                        where = where.Or(base_jiliangdw => base_jiliangdw.Bianhao == bianhao);
                                }
                                if (bianhaoequal.Equals("like"))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(base_jiliangdw => base_jiliangdw.Bianhao.Contains(bianhao));
                                    else
                                        where = where.Or(base_jiliangdw => base_jiliangdw.Bianhao.Contains(bianhao));
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
                                        where = where.And(base_jiliangdw => base_jiliangdw.Mingcheng == mingcheng);
                                    else
                                        where = where.Or(base_jiliangdw => base_jiliangdw.Mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_jiliangdw => base_jiliangdw.Mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(base_jiliangdw => base_jiliangdw.Mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_jiliangdw => base_jiliangdw.IsDelete == false);

            var tempData = ob_base_jiliangdwservice.LoadSortEntities(where.Compile(), false, base_jiliangdw => base_jiliangdw.ID).ToPagedList<base_jiliangdw>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_jiliangdw = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_jiliangdw_index";
            string page = "1";
            //bianhao
            string bianhao = Request["bianhao"] ?? "";
            string bianhaoequal = Request["bianhaoequal"] ?? "";
            string bianhaoand = Request["bianhaoand"] ?? "";
            //mingcheng
            string mingcheng = Request["mingcheng"] ?? "";
            string mingchengequal = Request["mingchengequal"] ?? "";
            string mingchengand = Request["mingchengand"] ?? "";
            PageMenu.Set("Index", "base_jiliangdw", "基础数据");
            Expression<Func<base_jiliangdw, bool>> where = PredicateExtensionses.True<base_jiliangdw>();
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
                            where = where.And(base_jiliangdw => base_jiliangdw.Bianhao == bianhao);
                        else
                            where = where.Or(base_jiliangdw => base_jiliangdw.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_jiliangdw => base_jiliangdw.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_jiliangdw => base_jiliangdw.Bianhao.Contains(bianhao));
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
                            where = where.And(base_jiliangdw => base_jiliangdw.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_jiliangdw => base_jiliangdw.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_jiliangdw => base_jiliangdw.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_jiliangdw => base_jiliangdw.Mingcheng.Contains(mingcheng));
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
                            where = where.And(base_jiliangdw => base_jiliangdw.Bianhao == bianhao);
                        else
                            where = where.Or(base_jiliangdw => base_jiliangdw.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_jiliangdw => base_jiliangdw.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_jiliangdw => base_jiliangdw.Bianhao.Contains(bianhao));
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
                            where = where.And(base_jiliangdw => base_jiliangdw.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_jiliangdw => base_jiliangdw.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_jiliangdw => base_jiliangdw.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_jiliangdw => base_jiliangdw.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_jiliangdw => base_jiliangdw.IsDelete == false);

            var tempData = ob_base_jiliangdwservice.LoadSortEntities(where.Compile(), false, base_jiliangdw => base_jiliangdw.ID).ToPagedList<base_jiliangdw>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_jiliangdw = tempData;
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
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                base_jiliangdw ob_base_jiliangdw = new base_jiliangdw();
                ob_base_jiliangdw.Bianhao = bianhao.Trim();
                ob_base_jiliangdw.Mingcheng = mingcheng.Trim();
                ob_base_jiliangdw.Miaoshu = miaoshu.Trim();
                ob_base_jiliangdw.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_jiliangdw.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_jiliangdw = ob_base_jiliangdwservice.AddEntity(ob_base_jiliangdw);
                ViewBag.base_jiliangdw = ob_base_jiliangdw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }
        public JsonResult GetJldw()
        {
            int _userid = (int)Session["user_id"];
            string _username = (string)Session["user_name"];

            var tempdata = ob_base_jiliangdwservice.LoadSortEntities(p => p.IsDelete == false, true, p => p.Bianhao);
            IList<Danwei> _danweis = new List<Danwei>();
            Danwei _danwei;
            foreach (base_jiliangdw _jldw in tempdata)
            {
                _danwei = new Danwei();
                _danwei.ID = _jldw.ID;
                _danwei.Bianhao = _jldw.Bianhao;
                _danwei.Mingcheng = _jldw.Mingcheng;
                _danwei.Miaoshu = _jldw.Miaoshu;
                _danweis.Add(_danwei);
            }
            return Json(_danweis);
        }

        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            base_jiliangdw tempData = ob_base_jiliangdwservice.GetEntityById(base_jiliangdw => base_jiliangdw.ID == id && base_jiliangdw.IsDelete == false);
            ViewBag.base_jiliangdw = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_jiliangdwViewModel base_jiliangdwviewmodel = new base_jiliangdwViewModel();
                base_jiliangdwviewmodel.ID = tempData.ID;
                base_jiliangdwviewmodel.Bianhao = tempData.Bianhao;
                base_jiliangdwviewmodel.Mingcheng = tempData.Mingcheng;
                base_jiliangdwviewmodel.Miaoshu = tempData.Miaoshu;
                base_jiliangdwviewmodel.MakeDate = tempData.MakeDate;
                base_jiliangdwviewmodel.MakeMan = tempData.MakeMan;
                return View(base_jiliangdwviewmodel);
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
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_jiliangdw p = ob_base_jiliangdwservice.GetEntityById(base_jiliangdw => base_jiliangdw.ID == uid);
                p.Bianhao = bianhao.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.Miaoshu = miaoshu.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_jiliangdwservice.UpdateEntity(p);
                ViewBag.saveok = ViewAddTag.ModifyOk;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.saveok = ViewAddTag.ModifyNo;
            }
            return RedirectToAction("Index", new { id = uid });
        }
        public ActionResult Delete()
        {
            string sdel = Request["del"] ?? "";
            int id;
            base_jiliangdw ob_base_jiliangdw;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_jiliangdw = ob_base_jiliangdwservice.GetEntityById(base_jiliangdw => base_jiliangdw.ID == id && base_jiliangdw.IsDelete == false);
                    ob_base_jiliangdw.IsDelete = true;
                    ob_base_jiliangdwservice.UpdateEntity(ob_base_jiliangdw);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

