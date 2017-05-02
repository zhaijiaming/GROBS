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
    public class base_shangpinxxController : Controller
    {
        private Ibase_shangpinxxService ob_base_shangpinxxservice = ServiceFactory.base_shangpinxxservice;
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_shangpinxx_index";
            PageMenu.Set("Index", "base_shangpinxx", "基础数据");
            Expression<Func<base_shangpin_v, bool>> where = PredicateExtensionses.True<base_shangpin_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "huozhuid":
                            string huozhuid = scld[1];
                            string huozhuidequal = scld[2];
                            string huozhuidand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuid))
                            {
                                if (huozhuidequal.Equals("="))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(base_shangpin_v => base_shangpin_v.huozhuid == int.Parse(huozhuid));
                                    else
                                        where = where.Or(base_shangpin_v => base_shangpin_v.huozhuid == int.Parse(huozhuid));
                                }
                            }
                            break;
                        case "daima":
                            string daima = scld[1];
                            string daimaequal = scld[2];
                            string daimaand = scld[3];
                            if (!string.IsNullOrEmpty(daima))
                            {
                                if (daimaequal.Equals("="))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(base_shangpin_v => base_shangpin_v.daima == daima);
                                    else
                                        where = where.Or(base_shangpin_v => base_shangpin_v.daima == daima);
                                }
                                if (daimaequal.Equals("like"))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(base_shangpin_v => base_shangpin_v.daima.Contains(daima));
                                    else
                                        where = where.Or(base_shangpin_v => base_shangpin_v.daima.Contains(daima));
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
                                        where = where.And(base_shangpin_v => base_shangpin_v.mingcheng == mingcheng);
                                    else
                                        where = where.Or(base_shangpin_v => base_shangpin_v.mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_shangpin_v => base_shangpin_v.mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(base_shangpin_v => base_shangpin_v.mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        case "zhucezhengbh":
                            string zhucezhengbh = scld[1];
                            string zhucezhengbhequal = scld[2];
                            string zhucezhengbhand = scld[3];
                            if (!string.IsNullOrEmpty(zhucezhengbh))
                            {
                                if (zhucezhengbhequal.Equals("="))
                                {
                                    if (zhucezhengbhand.Equals("and"))
                                        where = where.And(p => p.ZhucezhengBH == zhucezhengbh);
                                    else
                                        where = where.Or(p => p.ZhucezhengBH == zhucezhengbh);
                                }
                                if (zhucezhengbhequal.Equals("like"))
                                {
                                    if (zhucezhengbhand.Equals("and"))
                                        where = where.And(p => p.ZhucezhengBH.Contains(zhucezhengbh));
                                    else
                                        where = where.Or(p => p.ZhucezhengBH.Contains(zhucezhengbh));
                                }
                            }
                            break;
                        case "Guige":
                            string Guige = scld[1];
                            string Guigeequal = scld[2];
                            string Guigeand = scld[3];
                            if (!string.IsNullOrEmpty(Guige))
                            {
                                if (Guigeequal.Equals("="))
                                {
                                    if (Guigeand.Equals("and"))
                                        where = where.And(p => p.Guige == Guige);
                                    else
                                        where = where.Or(p => p.Guige == Guige);
                                }
                                if (Guigeequal.Equals("like"))
                                {
                                    if (Guigeand.Equals("and"))
                                        where = where.And(p => p.Guige.Contains(Guige));
                                    else
                                        where = where.Or(p => p.Guige.Contains(Guige));
                                }
                            }
                            break;
                        case "gongyingid":
                            string gongyingid = scld[1];
                            string gongyingidequal = scld[2];
                            string gongyingidand = scld[3];
                            if (!string.IsNullOrEmpty(gongyingid))
                            {
                                if (gongyingidequal.Equals("="))
                                {
                                    if (gongyingidand.Equals("and"))
                                        where = where.And(p => p.GongyingID == int.Parse(gongyingid));
                                    else
                                        where = where.Or(p => p.GongyingID == int.Parse(gongyingid));
                                }
                            }
                            break;
                        case "shouying":
                            string shouying = scld[1];
                            string shouyingequal = scld[2];
                            string shouyingand = scld[3];
                            if (shouying == "0")
                            {
                                shouying = "";
                            }
                            if (!string.IsNullOrEmpty(shouying))
                            {
                                if (shouyingequal.Equals("="))
                                {
                                    if (shouyingand.Equals("and"))
                                        where = where.And(p => p.Shouying == int.Parse(shouying));
                                    else
                                        where = where.Or(p => p.Shouying == int.Parse(shouying));
                                }
                            }
                            break;
                        case "shenchasf":
                            string shenchasf = scld[1];
                            string shenchasfequal = scld[2];
                            string shenchasfand = scld[3];
                            if (shenchasf == "1")
                            {
                                shenchasf = "true";
                            }
                            else if (shenchasf == "2")
                            {
                                shenchasf = "false";
                            }
                            else if (shenchasf == "-1")
                            {
                                shenchasf = "";
                            }
                            if (!string.IsNullOrEmpty(shenchasf))
                            {
                                if (shenchasfequal.Equals("="))
                                {
                                    if (shenchasfand.Equals("and"))
                                        where = where.And(p => p.ShenchaSF == bool.Parse(shenchasf));
                                    else
                                        where = where.Or(p => p.ShenchaSF == bool.Parse(shenchasf));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_shangpin_v => base_shangpin_v.IsDelete == false);

            var tempData = ob_base_shangpinxxservice.LoadShangpinAll(where.Compile()).ToPagedList<base_shangpin_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shangpinxx = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_shangpinxx_index";
            string page = "1";
            //huozhuid
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            //daima
            string daima = Request["daima"] ?? "";
            string daimaequal = Request["daimaequal"] ?? "";
            string daimaand = Request["daimaand"] ?? "";
            //mingcheng
            string mingcheng = Request["mingcheng"] ?? "";
            string mingchengequal = Request["mingchengequal"] ?? "";
            string mingchengand = Request["mingchengand"] ?? "";
            //Guige
            string Guige = Request["Guige"] ?? "";
            string Guigeequal = Request["Guigeequal"] ?? "";
            string Guigeand = Request["Guigeand"] ?? "";
            //zhucezhengbh
            string zhucezhengbh = Request["zhucezhengbh"] ?? "";
            string zhucezhengbhequal = Request["zhucezhengbhequal"] ?? "";
            string zhucezhengbhand = Request["zhucezhengbhand"] ?? "";
            //gongyingid
            string gongyingid = Request["gongyingid"] ?? "";
            string gongyingidequal = Request["gongyingidequal"] ?? "";
            string gongyingidand = Request["gongyingidand"] ?? "";
            //shouying
            string shouying = Request["shouying"] ?? "";
            string shouyingequal = Request["shouyingequal"] ?? "";
            string shouyingand = Request["shouyingand"] ?? "";
            if (shouying == "0")
            {
                shouying = "";
            }
            //shenchasf
            string shenchasf = Request["shenchasf"] ?? "";
            string shenchasfequal = Request["shenchasfequal"] ?? "";
            string shenchasfand = Request["shenchasfand"] ?? "";
            if (shenchasf == "1")
            {
                shenchasf = "true";
            }
            else if (shenchasf == "2")
            {
                shenchasf = "false";
            }
            else if (shenchasf == "-1")
            {
                shenchasf = "";
            }
            PageMenu.Set("Index", "base_shangpinxx", "基础数据");
            Expression<Func<base_shangpin_v, bool>> where = PredicateExtensionses.True<base_shangpin_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(base_shangpin_v => base_shangpin_v.huozhuid == int.Parse(huozhuid));
                        else
                            where = where.Or(base_shangpin_v => base_shangpin_v.huozhuid == int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //daima
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_shangpin_v => base_shangpin_v.daima == daima);
                        else
                            where = where.Or(base_shangpin_v => base_shangpin_v.daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_shangpin_v => base_shangpin_v.daima.Contains(daima));
                        else
                            where = where.Or(base_shangpin_v => base_shangpin_v.daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);
                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_shangpin_v => base_shangpin_v.mingcheng == mingcheng);
                        else
                            where = where.Or(base_shangpin_v => base_shangpin_v.mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_shangpin_v => base_shangpin_v.mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_shangpin_v => base_shangpin_v.mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);
                //Guige
                if (!string.IsNullOrEmpty(Guige))
                {
                    if (Guigeequal.Equals("="))
                    {
                        if (Guigeand.Equals("and"))
                            where = where.And(p => p.Guige == Guige);
                        else
                            where = where.Or(p => p.Guige == Guige);
                    }
                    if (Guigeequal.Equals("like"))
                    {
                        if (Guigeand.Equals("and"))
                            where = where.And(p => p.Guige.Contains(Guige));
                        else
                            where = where.Or(p => p.Guige.Contains(Guige));
                    }
                }
                if (!string.IsNullOrEmpty(Guige))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Guige", Guige, Guigeequal, Guigeand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Guige", "", Guigeequal, Guigeand);
                //zhucezhengbh
                if (!string.IsNullOrEmpty(zhucezhengbh))
                {
                    if (zhucezhengbhequal.Equals("="))
                    {
                        if (zhucezhengbhand.Equals("and"))
                            where = where.And(p => p.ZhucezhengBH == zhucezhengbh);
                        else
                            where = where.Or(p => p.ZhucezhengBH == zhucezhengbh);
                    }
                    if (zhucezhengbhequal.Equals("like"))
                    {
                        if (zhucezhengbhand.Equals("and"))
                            where = where.And(p => p.ZhucezhengBH.Contains(zhucezhengbh));
                        else
                            where = where.Or(p => p.ZhucezhengBH.Contains(zhucezhengbh));
                    }
                }
                if (!string.IsNullOrEmpty(zhucezhengbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "zhucezhengbh", zhucezhengbh, zhucezhengbhequal, zhucezhengbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "zhucezhengbh", "", zhucezhengbhequal, zhucezhengbhand);
                //gongyingid
                if (!string.IsNullOrEmpty(gongyingid))
                {
                    if (gongyingidequal.Equals("="))
                    {
                        if (gongyingidand.Equals("and"))
                            where = where.And(p => p.GongyingID == int.Parse(gongyingid));
                        else
                            where = where.Or(p => p.GongyingID == int.Parse(gongyingid));
                    }
                }
                if (!string.IsNullOrEmpty(gongyingid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingid", gongyingid, gongyingidequal, gongyingidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingid", "", gongyingidequal, gongyingidand);
                //shouying
                if (!string.IsNullOrEmpty(shouying))
                {
                    if (shouyingequal.Equals("="))
                    {
                        if (shouyingand.Equals("and"))
                            where = where.And(p => p.Shouying == int.Parse(shouying));
                        else
                            where = where.Or(p => p.Shouying == int.Parse(shouying));
                    }
                }
                if (!string.IsNullOrEmpty(shouying))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouying", shouying, shouyingequal, shouyingand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouying", "", shouyingequal, shouyingand);
                //shenchasf
                if (!string.IsNullOrEmpty(shenchasf))
                {
                    if (shenchasfequal.Equals("="))
                    {
                        if (shenchasfand.Equals("and"))
                            where = where.And(p => p.ShenchaSF == bool.Parse(shenchasf));
                        else
                            where = where.Or(p => p.ShenchaSF == bool.Parse(shenchasf));
                    }
                }
                if (!string.IsNullOrEmpty(shenchasf))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shenchasf", shenchasf, shenchasfequal, shenchasfand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shenchasf", "", shenchasfequal, shenchasfand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(base_shangpin_v => base_shangpin_v.huozhuid == int.Parse(huozhuid));
                        else
                            where = where.Or(base_shangpin_v => base_shangpin_v.huozhuid == int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //daima
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_shangpin_v => base_shangpin_v.daima == daima);
                        else
                            where = where.Or(base_shangpin_v => base_shangpin_v.daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_shangpin_v => base_shangpin_v.daima.Contains(daima));
                        else
                            where = where.Or(base_shangpin_v => base_shangpin_v.daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);
                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_shangpin_v => base_shangpin_v.mingcheng == mingcheng);
                        else
                            where = where.Or(base_shangpin_v => base_shangpin_v.mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_shangpin_v => base_shangpin_v.mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_shangpin_v => base_shangpin_v.mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);
                //Guige
                if (!string.IsNullOrEmpty(Guige))
                {
                    if (Guigeequal.Equals("="))
                    {
                        if (Guigeand.Equals("and"))
                            where = where.And(p => p.Guige == Guige);
                        else
                            where = where.Or(p => p.Guige == Guige);
                    }
                    if (Guigeequal.Equals("like"))
                    {
                        if (Guigeand.Equals("and"))
                            where = where.And(p => p.Guige.Contains(Guige));
                        else
                            where = where.Or(p => p.Guige.Contains(Guige));
                    }
                }
                if (!string.IsNullOrEmpty(Guige))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Guige", Guige, Guigeequal, Guigeand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Guige", "", Guigeequal, Guigeand);
                //zhucezhengbh
                if (!string.IsNullOrEmpty(zhucezhengbh))
                {
                    if (zhucezhengbhequal.Equals("="))
                    {
                        if (zhucezhengbhand.Equals("and"))
                            where = where.And(p => p.ZhucezhengBH == zhucezhengbh);
                        else
                            where = where.Or(p => p.ZhucezhengBH == zhucezhengbh);
                    }
                    if (zhucezhengbhequal.Equals("like"))
                    {
                        if (zhucezhengbhand.Equals("and"))
                            where = where.And(p => p.ZhucezhengBH.Contains(zhucezhengbh));
                        else
                            where = where.Or(p => p.ZhucezhengBH.Contains(zhucezhengbh));
                    }
                }
                if (!string.IsNullOrEmpty(zhucezhengbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "zhucezhengbh", zhucezhengbh, zhucezhengbhequal, zhucezhengbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "zhucezhengbh", "", zhucezhengbhequal, zhucezhengbhand);
                //gongyingid
                if (!string.IsNullOrEmpty(gongyingid))
                {
                    if (gongyingidequal.Equals("="))
                    {
                        if (gongyingidand.Equals("and"))
                            where = where.And(p => p.GongyingID == int.Parse(gongyingid));
                        else
                            where = where.Or(p => p.GongyingID == int.Parse(gongyingid));
                    }
                }
                if (!string.IsNullOrEmpty(gongyingid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingid", gongyingid, gongyingidequal, gongyingidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingid", "", gongyingidequal, gongyingidand);
                //shouying
                if (!string.IsNullOrEmpty(shouying))
                {
                    if (shouyingequal.Equals("="))
                    {
                        if (shouyingand.Equals("and"))
                            where = where.And(p => p.Shouying == int.Parse(shouying));
                        else
                            where = where.Or(p => p.Shouying == int.Parse(shouying));
                    }
                }
                if (!string.IsNullOrEmpty(shouying))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouying", shouying, shouyingequal, shouyingand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouying", "", shouyingequal, shouyingand);
                //shenchasf
                if (!string.IsNullOrEmpty(shenchasf))
                {
                    if (shenchasfequal.Equals("="))
                    {
                        if (shenchasfand.Equals("and"))
                            where = where.And(p => p.ShenchaSF == bool.Parse(shenchasf));
                        else
                            where = where.Or(p => p.ShenchaSF == bool.Parse(shenchasf));
                    }
                }
                if (!string.IsNullOrEmpty(shenchasf))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shenchasf", shenchasf, shenchasfequal, shenchasfand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shenchasf", "", shenchasfequal, shenchasfand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            //var _scinfo = sc.ConditionInfo;
            //var _schead = _scinfo.Substring(0, _scinfo.IndexOf("shenchasf"));
            //var _sctail = _scinfo.Substring(_scinfo.IndexOf("shenchasf"));
            //string _boolhead = "";
            //string _booltail = "";
            //if (_sctail.IndexOf("true") > 0)
            //{
            //    _boolhead = _sctail.Substring(0, _sctail.IndexOf("true"));
            //    _booltail = "1" + _sctail.Substring(_sctail.IndexOf("true") + 4);
            //}
            //if (_sctail.IndexOf("false") > 0)
            //{
            //    _boolhead = _sctail.Substring(0, _sctail.IndexOf("false"));
            //    _booltail = "2" + _sctail.Substring(_sctail.IndexOf("false") + 5);
            //}
            //if (_sctail.IndexOf("")+1 > 0)
            //{
            //    _boolhead = _sctail.Substring(0, (_sctail.IndexOf("shenchasf") +10));
            //    _booltail = "-1" + _sctail.Substring(_sctail.IndexOf("shenchasf")+10);
            //}
            //var _newscinfo = _schead + _boolhead + _booltail;
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_shangpin_v => base_shangpin_v.IsDelete == false);

            var tempData = ob_base_shangpinxxservice.LoadShangpinAll(where.Compile()).ToPagedList<base_shangpin_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shangpinxx = tempData;
            return View(tempData);
        }
        /// <summary>
        /// 货主的商品列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCustomerCargos()
        {
            string _sCust = Request["cust"] ?? "";
            if (_sCust.Length == 0)
                return Json("");
            else
            {
                //var _splist = ServiceFactory.base_shangpinxxservice.LoadSortEntities(p => p.HuozhuID == int.Parse(_sCust) && p.IsDelete == false, true, s => s.Mingcheng).ToList<base_shangpinxx>();
                var _splist = ServiceFactory.base_shangpinxxservice.LoadShangpinCust(int.Parse(_sCust)).ToList<base_shangpin_v>();
                if (_splist == null)
                    return Json("");
                else
                    return Json(_splist);
            }
        }
        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            return View();
        }
        public JsonResult BriefImport()
        {
            int _userid = (int)Session["user_id"];
            var _splist = Request["sp"] ?? "";
            var _hz = Request["hz"] ?? "";
            var _gys = Request["gys"] ?? "";
            if (string.IsNullOrEmpty(_gys))
                return Json(-1);
            if (string.IsNullOrEmpty(_hz))
                return Json(-1);
            if (string.IsNullOrEmpty(_splist))
                return Json(-1);
            var _sps = _splist.Replace(" ", "$$");
            foreach (var sp in _sps.Split())
            {
                if (sp.Length > 3)
                {
                    var spp = sp.Replace("$$", " ");
                    string[] _sp = spp.Split('#');
                    var _zcz = _sp[0];
                    var _spmc = _sp[1];
                    var _gg = _sp[2];
                    base_shangpinxx _shangpin = ob_base_shangpinxxservice.GetEntityById(p => p.HuozhuID == int.Parse(_hz) && p.Guige == _gg.Trim() && p.Mingcheng == _spmc.Trim() && p.IsDelete == false);
                    if (_shangpin == null)
                    {
                        _shangpin = new base_shangpinxx();
                        _shangpin.HuozhuID = int.Parse(_hz);
                        _shangpin.HuozhuSQID = 0;
                        _shangpin.GongyingID = int.Parse(_gys);
                        _shangpin.GongyingSQID = 0;
                        _shangpin.GongyingXSID = 0;
                        _shangpin.Mingcheng = _spmc;
                        _shangpin.Guige = _gg;
                        _shangpin.Daima = _gg;
                        _shangpin.Xinghao = _gg;
                        _shangpin.JingyinSF = true;
                        _shangpin.ShenchaSF = true;
                        _shangpin.Huansuanlv = 1;
                        _shangpin.Shouying = 1;
                        _shangpin.Volchang = 0;
                        _shangpin.Volgao = 0;
                        _shangpin.Volkuan = 0;
                        _shangpin.Chanpinxian = 0;
                        _shangpin.Muluxuhao = 0;
                        _shangpin.Guanlifenlei = 0;
                        _shangpin.MakeMan = _userid;
                        _shangpin.ZhucezhengBH = _zcz;
                        if (!string.IsNullOrEmpty(_zcz))
                        {
                            base_shangpinzcz _spzcz = ServiceFactory.base_shangpinzczservice.GetEntityById(p => p.Bianhao == _zcz && p.IsDelete == false);
                            if (_spzcz != null)
                            {
                                _shangpin.ZhucezhengID = _spzcz.ID;
                                _shangpin.QiyeID = _spzcz.ShengchanqiyeID;
                                base_shengchanqiye _qy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(p => p.ID == _spzcz.ShengchanqiyeID);
                                _shangpin.Qiyemingcheng = _qy.Qiyemingcheng;
                                _shangpin.Chandi = _spzcz.Chandi;
                            }
                            else
                            {
                                _shangpin.ZhucezhengID = 0;
                                _shangpin.QiyeID = 0;
                            }
                        }
                        else
                        {
                            _shangpin.ZhucezhengID = 0;
                            _shangpin.QiyeID = 0;
                        }
                        _shangpin = ob_base_shangpinxxservice.AddEntity(_shangpin);
                    }
                }
            }
            return Json(1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhusqid = Request["huozhusqid"] ?? "";
            string daima = Request["daima"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string zhucezhengid = Request["zhucezhengid"] ?? "";
            string zhucezhengbh = Request["zhucezhengbh"] ?? "";
            string guige = Request["guige"] ?? "";
            string xinghao = Request["xinghao"] ?? "";
            string danwei = Request["danwei"] ?? "";
            string huansuanlv = Request["huansuanlv"] ?? "";
            string volchang = Request["volchang"] ?? "";
            string volkuan = Request["volkuan"] ?? "";
            string volgao = Request["volgao"] ?? "";
            string chanpinxian = Request["chanpinxian"] ?? "";
            string muluxuhao = Request["muluxuhao"] ?? "";
            string guanlifenlei = Request["guanlifenlei"] ?? "";
            string baozhuangyaoqiu = Request["baozhuangyaoqiu"] ?? "";
            string cunchutiaojian = Request["cunchutiaojian"] ?? "";
            string qiyeid = Request["qiyeid"] ?? "";
            string qiyemingcheng = Request["qiyemingcheng"] ?? "";
            string gongyingid = Request["gongyingid"] ?? "";
            string gongyingsqid = Request["gongyingsqid"] ?? "";
            string gongyingxsid = Request["gongyingxsid"] ?? "";
            string shouying = Request["shouying"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string shenchasf = Request["shenchasf"] ?? "";
            string jingyinsf = Request["jingyinsf"] ?? "";
            string baozhuangdw = Request["baozhuangdw"] ?? "";
            string shangpintm = Request["shangpintm"] ?? "";
            string chandi = Request["chandi"] ?? "";
            string ShangpinMS = Request["ShangpinMS"] ?? "";
            string WenduSX = Request["WenduSX"] ?? "";
            string WenduXX = Request["WenduXX"] ?? "";
            if (shenchasf.IndexOf("true") > -1)
                shenchasf = "true";
            else
                shenchasf = "false";
            if (jingyinsf.IndexOf("true") > -1)
                jingyinsf = "true";
            else
                jingyinsf = "false";
            try
            {
                base_shangpinxx ob_base_shangpinxx = ob_base_shangpinxxservice.GetEntityById(p => p.HuozhuID == int.Parse(huozhuid) && p.Guige == guige.Trim() && p.Mingcheng == mingcheng.Trim() && p.IsDelete == false);
                if (ob_base_shangpinxx == null)
                {
                    ob_base_shangpinxx = new base_shangpinxx();
                    ob_base_shangpinxx.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                    ob_base_shangpinxx.HuozhuSQID = huozhusqid == "" ? 0 : int.Parse(huozhusqid);
                    ob_base_shangpinxx.Daima = daima.Trim();
                    ob_base_shangpinxx.Mingcheng = mingcheng.Trim();
                    ob_base_shangpinxx.ZhucezhengID = zhucezhengid == "" ? 0 : int.Parse(zhucezhengid);
                    ob_base_shangpinxx.ZhucezhengBH = zhucezhengbh.Trim();
                    ob_base_shangpinxx.Guige = guige.Trim();
                    ob_base_shangpinxx.Xinghao = xinghao.Trim();
                    ob_base_shangpinxx.Danwei = danwei.Trim();
                    ob_base_shangpinxx.Huansuanlv = huansuanlv == "" ? 0 : float.Parse(huansuanlv);
                    ob_base_shangpinxx.Volchang = volchang == "" ? 0 : float.Parse(volchang);
                    ob_base_shangpinxx.Volkuan = volkuan == "" ? 0 : float.Parse(volkuan);
                    ob_base_shangpinxx.Volgao = volgao == "" ? 0 : float.Parse(volgao);
                    ob_base_shangpinxx.Chanpinxian = chanpinxian == "" ? 0 : int.Parse(chanpinxian);
                    ob_base_shangpinxx.Muluxuhao = muluxuhao == "" ? 0 : int.Parse(muluxuhao);
                    ob_base_shangpinxx.Guanlifenlei = guanlifenlei == "" ? 0 : int.Parse(guanlifenlei);
                    ob_base_shangpinxx.Baozhuangyaoqiu = baozhuangyaoqiu.Trim();
                    ob_base_shangpinxx.Cunchutiaojian = cunchutiaojian.Trim();
                    ob_base_shangpinxx.QiyeID = qiyeid == "" ? 0 : int.Parse(qiyeid);
                    ob_base_shangpinxx.Qiyemingcheng = qiyemingcheng.Trim();
                    ob_base_shangpinxx.GongyingID = gongyingid == "" ? 0 : int.Parse(gongyingid);
                    ob_base_shangpinxx.GongyingSQID = gongyingsqid == "" ? 0 : int.Parse(gongyingsqid);
                    ob_base_shangpinxx.GongyingXSID = gongyingxsid == "" ? 0 : int.Parse(gongyingxsid);
                    ob_base_shangpinxx.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                    ob_base_shangpinxx.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                    ob_base_shangpinxx.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                    ob_base_shangpinxx.ShenchaSF = shenchasf == "" ? false : Boolean.Parse(shenchasf);
                    ob_base_shangpinxx.JingyinSF = jingyinsf == "" ? false : Boolean.Parse(jingyinsf);
                    ob_base_shangpinxx.BaozhuangDW = baozhuangdw.Trim();
                    ob_base_shangpinxx.ShangpinTM = shangpintm.Trim();
                    ob_base_shangpinxx.Chandi = chandi.Trim();
                    ob_base_shangpinxx.ShangpinMS = ShangpinMS.Trim();
                    ob_base_shangpinxx.WenduSX = WenduSX == "" ? 0 : float.Parse(WenduSX);
                    ob_base_shangpinxx.WenduXX = WenduXX == "" ? 0 : float.Parse(WenduXX);

                    ob_base_shangpinxx = ob_base_shangpinxxservice.AddEntity(ob_base_shangpinxx);
                }
                ViewBag.base_shangpinxx = ob_base_shangpinxx;
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
            base_shangpinxx tempData = ob_base_shangpinxxservice.GetEntityById(base_shangpinxx => base_shangpinxx.ID == id && base_shangpinxx.IsDelete == false);
            ViewBag.base_shangpinxx = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_shangpinxxViewModel base_shangpinxxviewmodel = new base_shangpinxxViewModel();
                base_shangpinxxviewmodel.ID = tempData.ID;
                base_shangpinxxviewmodel.HuozhuID = tempData.HuozhuID;
                base_shangpinxxviewmodel.HuozhuSQID = tempData.HuozhuSQID;
                base_shangpinxxviewmodel.Daima = tempData.Daima;
                base_shangpinxxviewmodel.Mingcheng = tempData.Mingcheng;
                base_shangpinxxviewmodel.ZhucezhengID = tempData.ZhucezhengID;
                base_shangpinxxviewmodel.ZhucezhengBH = tempData.ZhucezhengBH;
                base_shangpinxxviewmodel.Guige = tempData.Guige;
                base_shangpinxxviewmodel.Xinghao = tempData.Xinghao;
                base_shangpinxxviewmodel.Danwei = tempData.Danwei;
                base_shangpinxxviewmodel.Huansuanlv = tempData.Huansuanlv;
                base_shangpinxxviewmodel.Volchang = tempData.Volchang;
                base_shangpinxxviewmodel.Volkuan = tempData.Volkuan;
                base_shangpinxxviewmodel.Volgao = tempData.Volgao;
                base_shangpinxxviewmodel.Chanpinxian = tempData.Chanpinxian;
                base_shangpinxxviewmodel.Muluxuhao = tempData.Muluxuhao;
                base_shangpinxxviewmodel.Guanlifenlei = tempData.Guanlifenlei;
                base_shangpinxxviewmodel.Baozhuangyaoqiu = tempData.Baozhuangyaoqiu;
                base_shangpinxxviewmodel.Cunchutiaojian = tempData.Cunchutiaojian;
                base_shangpinxxviewmodel.QiyeID = tempData.QiyeID;
                base_shangpinxxviewmodel.Qiyemingcheng = tempData.Qiyemingcheng;
                base_shangpinxxviewmodel.GongyingID = tempData.GongyingID;
                base_shangpinxxviewmodel.GongyingSQID = tempData.GongyingSQID;
                base_shangpinxxviewmodel.GongyingXSID = tempData.GongyingXSID;
                base_shangpinxxviewmodel.Shouying = tempData.Shouying;
                base_shangpinxxviewmodel.MakeDate = tempData.MakeDate;
                base_shangpinxxviewmodel.MakeMan = tempData.MakeMan;
                base_shangpinxxviewmodel.ShenchaSF = tempData.ShenchaSF;
                base_shangpinxxviewmodel.JingyinSF = tempData.JingyinSF;
                base_shangpinxxviewmodel.BaozhuangDW = tempData.BaozhuangDW;
                base_shangpinxxviewmodel.ShangpinTM = tempData.ShangpinTM;
                base_shangpinxxviewmodel.Chandi = tempData.Chandi;
                base_shangpinxxviewmodel.ShangpinMS = tempData.ShangpinMS;
                base_shangpinxxviewmodel.WenduSX = tempData.WenduSX;
                base_shangpinxxviewmodel.WenduXX = tempData.WenduXX;
                return View(base_shangpinxxviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhusqid = Request["huozhusqid"] ?? "";
            string daima = Request["daima"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string zhucezhengid = Request["zhucezhengid"] ?? "";
            string zhucezhengbh = Request["zhucezhengbh"] ?? "";
            string guige = Request["guige"] ?? "";
            string xinghao = Request["xinghao"] ?? "";
            string danwei = Request["danwei"] ?? "";
            string huansuanlv = Request["huansuanlv"] ?? "";
            string volchang = Request["volchang"] ?? "";
            string volkuan = Request["volkuan"] ?? "";
            string volgao = Request["volgao"] ?? "";
            string chanpinxian = Request["chanpinxian"] ?? "";
            string muluxuhao = Request["muluxuhao"] ?? "";
            string guanlifenlei = Request["guanlifenlei"] ?? "";
            string baozhuangyaoqiu = Request["baozhuangyaoqiu"] ?? "";
            string cunchutiaojian = Request["cunchutiaojian"] ?? "";
            string qiyeid = Request["qiyeid"] ?? "";
            string qiyemingcheng = Request["qiyemingcheng"] ?? "";
            string gongyingid = Request["gongyingid"] ?? "";
            string gongyingsqid = Request["gongyingsqid"] ?? "";
            string gongyingxsid = Request["gongyingxsid"] ?? "";
            string shouying = Request["shouying"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string shenchasf = Request["shenchasf"] ?? "";
            string jingyinsf = Request["jingyinsf"] ?? "";
            string baozhuangdw = Request["baozhuangdw"] ?? "";
            string shangpintm = Request["shangpintm"] ?? "";
            string chandi = Request["chandi"] ?? "";
            string ShangpinMS = Request["ShangpinMS"] ?? "";
            string WenduSX = Request["WenduSX"] ?? "";
            string WenduXX = Request["WenduXX"] ?? "";
            if (shenchasf.IndexOf("true") > -1)
                shenchasf = "true";
            else
                shenchasf = "false";
            if (jingyinsf.IndexOf("true") > -1)
                jingyinsf = "true";
            else
                jingyinsf = "false";
            int uid = int.Parse(id);
            try
            {
                base_shangpinxx p = ob_base_shangpinxxservice.GetEntityById(base_shangpinxx => base_shangpinxx.ID == uid);
                p.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                p.HuozhuSQID = huozhusqid == "" ? 0 : int.Parse(huozhusqid);
                p.Daima = daima.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.ZhucezhengID = zhucezhengid == "" ? 0 : int.Parse(zhucezhengid);
                p.ZhucezhengBH = zhucezhengbh.Trim();
                p.Guige = guige.Trim();
                p.Xinghao = xinghao.Trim();
                p.Danwei = danwei.Trim();
                p.Huansuanlv = huansuanlv == "" ? 0 : float.Parse(huansuanlv);
                p.Volchang = volchang == "" ? 0 : float.Parse(volchang);
                p.Volkuan = volkuan == "" ? 0 : float.Parse(volkuan);
                p.Volgao = volgao == "" ? 0 : float.Parse(volgao);
                p.Chanpinxian = chanpinxian == "" ? 0 : int.Parse(chanpinxian);
                p.Muluxuhao = muluxuhao == "" ? 0 : int.Parse(muluxuhao);
                p.Guanlifenlei = guanlifenlei == "" ? 0 : int.Parse(guanlifenlei);
                p.Baozhuangyaoqiu = baozhuangyaoqiu.Trim();
                p.Cunchutiaojian = cunchutiaojian.Trim();
                p.QiyeID = qiyeid == "" ? 0 : int.Parse(qiyeid);
                p.Qiyemingcheng = qiyemingcheng.Trim();
                p.GongyingID = gongyingid == "" ? 0 : int.Parse(gongyingid);
                p.GongyingSQID = gongyingsqid == "" ? 0 : int.Parse(gongyingsqid);
                p.GongyingXSID = gongyingxsid == "" ? 0 : int.Parse(gongyingxsid);
                p.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.ShenchaSF = shenchasf == "" ? false : Boolean.Parse(shenchasf);
                p.JingyinSF = jingyinsf == "" ? false : Boolean.Parse(jingyinsf);
                p.BaozhuangDW = baozhuangdw.Trim();
                p.ShangpinTM = shangpintm.Trim();
                p.Chandi = chandi.Trim();
                p.ShangpinMS = ShangpinMS.Trim();
                p.WenduSX = WenduSX == "" ? 0 : float.Parse(WenduSX);
                p.WenduXX = WenduXX == "" ? 0 : float.Parse(WenduXX);

                ob_base_shangpinxxservice.UpdateEntity(p);
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
            base_shangpinxx ob_base_shangpinxx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_shangpinxx = ob_base_shangpinxxservice.GetEntityById(base_shangpinxx => base_shangpinxx.ID == id && base_shangpinxx.IsDelete == false);
                    ob_base_shangpinxx.IsDelete = true;
                    ob_base_shangpinxxservice.UpdateEntity(ob_base_shangpinxx);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult CommodityExport()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_shangpinxx_index";
            Expression<Func<base_shangpin_v, bool>> where = PredicateExtensionses.True<base_shangpin_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "huozhuid":
                            string huozhuid = scld[1];
                            string huozhuidequal = scld[2];
                            string huozhuidand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuid))
                            {
                                if (huozhuidequal.Equals("="))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(base_shangpin_v => base_shangpin_v.huozhuid == int.Parse(huozhuid));
                                    else
                                        where = where.Or(base_shangpin_v => base_shangpin_v.huozhuid == int.Parse(huozhuid));
                                }
                            }
                            break;
                        case "daima":
                            string daima = scld[1];
                            string daimaequal = scld[2];
                            string daimaand = scld[3];
                            if (!string.IsNullOrEmpty(daima))
                            {
                                if (daimaequal.Equals("="))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(base_shangpin_v => base_shangpin_v.daima == daima);
                                    else
                                        where = where.Or(base_shangpin_v => base_shangpin_v.daima == daima);
                                }
                                if (daimaequal.Equals("like"))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(base_shangpin_v => base_shangpin_v.daima.Contains(daima));
                                    else
                                        where = where.Or(base_shangpin_v => base_shangpin_v.daima.Contains(daima));
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
                                        where = where.And(base_shangpin_v => base_shangpin_v.mingcheng == mingcheng);
                                    else
                                        where = where.Or(base_shangpin_v => base_shangpin_v.mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_shangpin_v => base_shangpin_v.mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(base_shangpin_v => base_shangpin_v.mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        case "Guige":
                            string Guige = scld[1];
                            string Guigeequal = scld[2];
                            string Guigeand = scld[3];
                            if (!string.IsNullOrEmpty(Guige))
                            {
                                if (Guigeequal.Equals("="))
                                {
                                    if (Guigeand.Equals("and"))
                                        where = where.And(p => p.Guige == Guige);
                                    else
                                        where = where.Or(p => p.Guige == Guige);
                                }
                                if (Guigeequal.Equals("like"))
                                {
                                    if (Guigeand.Equals("and"))
                                        where = where.And(p => p.Guige.Contains(Guige));
                                    else
                                        where = where.Or(p => p.Guige.Contains(Guige));
                                }
                            }
                            break;
                        case "zhucezhengbh":
                            string zhucezhengbh = scld[1];
                            string zhucezhengbhequal = scld[2];
                            string zhucezhengbhand = scld[3];
                            if (!string.IsNullOrEmpty(zhucezhengbh))
                            {
                                if (zhucezhengbhequal.Equals("="))
                                {
                                    if (zhucezhengbhand.Equals("and"))
                                        where = where.And(p => p.ZhucezhengBH == zhucezhengbh);
                                    else
                                        where = where.Or(p => p.ZhucezhengBH == zhucezhengbh);
                                }
                                if (zhucezhengbhequal.Equals("like"))
                                {
                                    if (zhucezhengbhand.Equals("and"))
                                        where = where.And(p => p.ZhucezhengBH.Contains(zhucezhengbh));
                                    else
                                        where = where.Or(p => p.ZhucezhengBH.Contains(zhucezhengbh));
                                }
                            }
                            break;
                        case "gongyingid":
                            string gongyingid = scld[1];
                            string gongyingidequal = scld[2];
                            string gongyingidand = scld[3];
                            if (!string.IsNullOrEmpty(gongyingid))
                            {
                                if (gongyingidequal.Equals("="))
                                {
                                    if (gongyingidand.Equals("and"))
                                        where = where.And(p => p.GongyingID == int.Parse(gongyingid));
                                    else
                                        where = where.Or(p => p.GongyingID == int.Parse(gongyingid));
                                }
                            }
                            break;
                        case "shouying":
                            string shouying = scld[1];
                            string shouyingequal = scld[2];
                            string shouyingand = scld[3];
                            if (shouying == "0")
                            {
                                shouying = "";
                            }
                            if (!string.IsNullOrEmpty(shouying))
                            {
                                if (shouyingequal.Equals("="))
                                {
                                    if (shouyingand.Equals("and"))
                                        where = where.And(p => p.Shouying == int.Parse(shouying));
                                    else
                                        where = where.Or(p => p.Shouying == int.Parse(shouying));
                                }
                            }
                            break;
                        case "shenchasf":
                            string shenchasf = scld[1];
                            string shenchasfequal = scld[2];
                            string shenchasfand = scld[3];
                            if (shenchasf == "1")
                            {
                                shenchasf = "true";
                            }
                            else if (shenchasf == "2")
                            {
                                shenchasf = "false";
                            }
                            else if (shenchasf == "-1")
                            {
                                shenchasf = "";
                            }
                            if (!string.IsNullOrEmpty(shenchasf))
                            {
                                if (shenchasfequal.Equals("="))
                                {
                                    if (shenchasfand.Equals("and"))
                                        where = where.And(p => p.ShenchaSF == bool.Parse(shenchasf));
                                    else
                                        where = where.Or(p => p.ShenchaSF == bool.Parse(shenchasf));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_shangpin_v => base_shangpin_v.IsDelete == false);
            var tempData = ob_base_shangpinxxservice.LoadShangpinAll(where.Compile());
            ViewBag.commodity = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "CommodityExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("CommodityInformation_{0}.xls", DateTime.Now.ToShortDateString()));
        }
        public ActionResult PrintSpxx()
        {
            //var spxx_huozhuid = Request["spxx_huozhuid"] ?? "";
            //ViewBag.spxx_huozhuid = spxx_huozhuid;
            return View();
        }
        public JsonResult MuluCheck()
        {
            int flag = 0;
            string mlc_huozhuid = Request["mlc_huozhuid"] ?? "";
            string muluxuhao = Request["muluxuhao"] ?? "";
            var spxx_wtkh = ServiceFactory.base_weituokehuservice.GetEntityById(p => p.ID == int.Parse(mlc_huozhuid) && p.IsDelete == false);
            var wtkh_mulu = spxx_wtkh.JingyingfanweiDM;
            var wtkh_mulus = wtkh_mulu.Split(';');
            foreach (string i in wtkh_mulus)
            {
                if (muluxuhao == i)
                {
                    flag = 1;
                    break;
                }
                else
                {
                    flag = -1;
                }
            }
            return Json(flag);
        }
        public JsonResult getSpxx()
        {
            var tempdata = ServiceFactory.base_shangpinxxservice.LoadEntities(p => p.IsDelete == false);
            if (tempdata == null)
                return Json("");
            else
                return Json(tempdata);
        }
    }
}

