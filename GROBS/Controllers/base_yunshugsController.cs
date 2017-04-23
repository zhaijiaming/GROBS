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
    public class base_yunshugsController : Controller
    {
        private Ibase_yunshugsService ob_base_yunshugsservice = ServiceFactory.base_yunshugsservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_yunshugs_index";
            PageMenu.Set("Index", "base_yunshugs", "基础数据");
            Expression<Func<base_yunshugs, bool>> where = PredicateExtensionses.True<base_yunshugs>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "jiancheng":
                            string jiancheng = scld[1];
                            string jianchengequal = scld[2];
                            string jianchengand = scld[3];
                            if (!string.IsNullOrEmpty(jiancheng))
                            {
                                if (jianchengequal.Equals("="))
                                {
                                    if (jianchengand.Equals("and"))
                                        where = where.And(base_yunshugs => base_yunshugs.Jiancheng == jiancheng);
                                    else
                                        where = where.Or(base_yunshugs => base_yunshugs.Jiancheng == jiancheng);
                                }
                                if (jianchengequal.Equals("like"))
                                {
                                    if (jianchengand.Equals("and"))
                                        where = where.And(base_yunshugs => base_yunshugs.Jiancheng.Contains(jiancheng));
                                    else
                                        where = where.Or(base_yunshugs => base_yunshugs.Jiancheng.Contains(jiancheng));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_yunshugs => base_yunshugs.IsDelete == false);

            var tempData = ob_base_yunshugsservice.LoadSortEntities(where.Compile(), false, base_yunshugs => base_yunshugs.ID).ToPagedList<base_yunshugs>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_yunshugs = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_yunshugs_index";
            string page = "1";
            string jiancheng = Request["jiancheng"] ?? "";
            string jianchengequal = Request["jianchengequal"] ?? "";
            string jianchengand = Request["jianchengand"] ?? "";
            PageMenu.Set("Index", "base_yunshugs", "基础数据");
            Expression<Func<base_yunshugs, bool>> where = PredicateExtensionses.True<base_yunshugs>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(jiancheng))
                {
                    if (jianchengequal.Equals("="))
                    {
                        if (jianchengand.Equals("and"))
                            where = where.And(base_yunshugs => base_yunshugs.Jiancheng == jiancheng);
                        else
                            where = where.Or(base_yunshugs => base_yunshugs.Jiancheng == jiancheng);
                    }
                    if (jianchengequal.Equals("like"))
                    {
                        if (jianchengand.Equals("and"))
                            where = where.And(base_yunshugs => base_yunshugs.Jiancheng.Contains(jiancheng));
                        else
                            where = where.Or(base_yunshugs => base_yunshugs.Jiancheng.Contains(jiancheng));
                    }
                }
                if (!string.IsNullOrEmpty(jiancheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jiancheng", jiancheng, jianchengequal, jianchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jiancheng", "", jianchengequal, jianchengand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(jiancheng))
                {
                    if (jianchengequal.Equals("="))
                    {
                        if (jianchengand.Equals("and"))
                            where = where.And(base_yunshugs => base_yunshugs.Jiancheng == jiancheng);
                        else
                            where = where.Or(base_yunshugs => base_yunshugs.Jiancheng == jiancheng);
                    }
                    if (jianchengequal.Equals("like"))
                    {
                        if (jianchengand.Equals("and"))
                            where = where.And(base_yunshugs => base_yunshugs.Jiancheng.Contains(jiancheng));
                        else
                            where = where.Or(base_yunshugs => base_yunshugs.Jiancheng.Contains(jiancheng));
                    }
                }
                if (!string.IsNullOrEmpty(jiancheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jiancheng", jiancheng, jianchengequal, jianchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jiancheng", "", jianchengequal, jianchengand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_yunshugs => base_yunshugs.IsDelete == false);

            var tempData = ob_base_yunshugsservice.LoadSortEntities(where.Compile(), false, base_yunshugs => base_yunshugs.ID).ToPagedList<base_yunshugs>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_yunshugs = tempData;
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
            string jiancheng = Request["jiancheng"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string miaoshu = Request["miaoshu"] ?? "";
            string leixing = Request["leixing"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string fuzeren = Request["fuzeren"] ?? "";
            string fuzedh = Request["fuzedh"] ?? "";
            string jiezhangfs = Request["jiezhangfs"] ?? "";
            string yingyezz = Request["yingyezz"] ?? "";
            string zhizhaotp = Request["zhizhaotp"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                base_yunshugs ob_base_yunshugs = new base_yunshugs();
                ob_base_yunshugs.Jiancheng = jiancheng.Trim();
                ob_base_yunshugs.Mingcheng = mingcheng.Trim();
                ob_base_yunshugs.Miaoshu = miaoshu.Trim();
                ob_base_yunshugs.Leixing = leixing == "" ? 0 : int.Parse(leixing);
                ob_base_yunshugs.Lianxiren = lianxiren.Trim();
                ob_base_yunshugs.LianxiDH = lianxidh.Trim();
                ob_base_yunshugs.Fuzeren = fuzeren.Trim();
                ob_base_yunshugs.FuzeDH = fuzedh.Trim();
                ob_base_yunshugs.JiezhangFS = jiezhangfs.Trim();
                ob_base_yunshugs.YingyeZZ = yingyezz.Trim();
                ob_base_yunshugs.ZhizhaoTP = zhizhaotp.Trim();
                ob_base_yunshugs.Col1 = col1.Trim();
                ob_base_yunshugs.Col2 = col2.Trim();
                ob_base_yunshugs.Col3 = col3.Trim();
                ob_base_yunshugs.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_yunshugs.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_yunshugs = ob_base_yunshugsservice.AddEntity(ob_base_yunshugs);
                ViewBag.base_yunshugs = ob_base_yunshugs;
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
            base_yunshugs tempData = ob_base_yunshugsservice.GetEntityById(base_yunshugs => base_yunshugs.ID == id && base_yunshugs.IsDelete == false);
            ViewBag.base_yunshugs = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_yunshugsViewModel base_yunshugsviewmodel = new base_yunshugsViewModel();
                base_yunshugsviewmodel.ID = tempData.ID;
                base_yunshugsviewmodel.Jiancheng = tempData.Jiancheng;
                base_yunshugsviewmodel.Mingcheng = tempData.Mingcheng;
                base_yunshugsviewmodel.Miaoshu = tempData.Miaoshu;
                base_yunshugsviewmodel.Leixing = tempData.Leixing;
                base_yunshugsviewmodel.Lianxiren = tempData.Lianxiren;
                base_yunshugsviewmodel.LianxiDH = tempData.LianxiDH;
                base_yunshugsviewmodel.Fuzeren = tempData.Fuzeren;
                base_yunshugsviewmodel.FuzeDH = tempData.FuzeDH;
                base_yunshugsviewmodel.JiezhangFS = tempData.JiezhangFS;
                base_yunshugsviewmodel.YingyeZZ = tempData.YingyeZZ;
                base_yunshugsviewmodel.ZhizhaoTP = tempData.ZhizhaoTP;
                base_yunshugsviewmodel.Col1 = tempData.Col1;
                base_yunshugsviewmodel.Col2 = tempData.Col2;
                base_yunshugsviewmodel.Col3 = tempData.Col3;
                base_yunshugsviewmodel.MakeDate = tempData.MakeDate;
                base_yunshugsviewmodel.MakeMan = tempData.MakeMan;
                return View(base_yunshugsviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string jiancheng = Request["jiancheng"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string miaoshu = Request["miaoshu"] ?? "";
            string leixing = Request["leixing"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string fuzeren = Request["fuzeren"] ?? "";
            string fuzedh = Request["fuzedh"] ?? "";
            string jiezhangfs = Request["jiezhangfs"] ?? "";
            string yingyezz = Request["yingyezz"] ?? "";
            string zhizhaotp = Request["zhizhaotp"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_yunshugs p = ob_base_yunshugsservice.GetEntityById(base_yunshugs => base_yunshugs.ID == uid);
                p.Jiancheng = jiancheng.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.Miaoshu = miaoshu.Trim();
                p.Leixing = leixing == "" ? 0 : int.Parse(leixing);
                p.Lianxiren = lianxiren.Trim();
                p.LianxiDH = lianxidh.Trim();
                p.Fuzeren = fuzeren.Trim();
                p.FuzeDH = fuzedh.Trim();
                p.JiezhangFS = jiezhangfs.Trim();
                p.YingyeZZ = yingyezz.Trim();
                p.ZhizhaoTP = zhizhaotp.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_yunshugsservice.UpdateEntity(p);
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
            base_yunshugs ob_base_yunshugs;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_yunshugs = ob_base_yunshugsservice.GetEntityById(base_yunshugs => base_yunshugs.ID == id && base_yunshugs.IsDelete == false);
                    ob_base_yunshugs.IsDelete = true;
                    ob_base_yunshugsservice.UpdateEntity(ob_base_yunshugs);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

