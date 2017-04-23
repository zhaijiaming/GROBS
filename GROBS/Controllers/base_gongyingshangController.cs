using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Expressions;
using X.PagedList;
using GROBS.EFModels;
using GROBS.IBSL;
using GROBS.BSL;
using GROBS.Common;
using GROBS.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace GROBS.Controllers
{
    public class base_gongyingshangController : Controller
    {
        private Ibase_gongyingshangService ob_base_gongyingshangservice = ServiceFactory.base_gongyingshangservice;

        //private GongyingshangContext db = new GongyingshangContext();
        [OutputCache(Duration = 30)]
        public ActionResult Index(/*string sortOrder,*/string page, string sortOrder)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            //Expression<Func<base_gongyingshang, bool>> where = PredicateExtensionses.True<base_gongyingshang>();
            int userid = (int)Session["user_id"];
            string pagetag = "base_gongyingshang_index";
            PageMenu.Set("Index", "base_gongyingshang", "基础数据");
            Expression<Func<base_gongyingshang, bool>> where = PredicateExtensionses.True<base_gongyingshang>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "daima":
                            string daima = scld[1];
                            string daimaequal = scld[2];
                            string daimaand = scld[3];
                            if (!string.IsNullOrEmpty(daima))
                            {
                                if (daimaequal.Equals("="))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(base_gongyingshang => base_gongyingshang.Daima == daima);
                                    else
                                        where = where.Or(base_gongyingshang => base_gongyingshang.Daima == daima);
                                }
                                if (daimaequal.Equals("like"))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(base_gongyingshang => base_gongyingshang.Daima.Contains(daima));
                                    else
                                        where = where.Or(base_gongyingshang => base_gongyingshang.Daima.Contains(daima));
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
                                        where = where.And(base_gongyingshang => base_gongyingshang.Mingcheng == mingcheng);
                                    else
                                        where = where.Or(base_gongyingshang => base_gongyingshang.Mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_gongyingshang => base_gongyingshang.Mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(base_gongyingshang => base_gongyingshang.Mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_gongyingshang => base_gongyingshang.IsDelete == false);

            //ViewBag.DaimaSortParm = sortOrder == "daima" ? "daima_desc" : "daima";
            //ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            //ViewBag.YyzzSortParm = sortOrder == "Yyzz" ? "Yyzz_desc" : "Yyzz";
            //ViewBag.YyzzyxqSortParm = sortOrder == "Yyzzyxq" ? "Yyzzyxq_desc" : "Yyzzyxq";
            //ViewBag.YyzztpSortParm = sortOrder == "Yyzztp" ? "Yyzztp_desc" : "Yyzztp";
            //ViewBag.JyxkSortParm = sortOrder == "Jyxk" ? "Jyxk_desc" : "Jyxk";
            //ViewBag.JyxkyxqSortParm = sortOrder == "Jyxkyxq" ? "Jyxkyxq_desc" : "Jyxkyxq";
            //ViewBag.JyxktpSortParm = sortOrder == "Jyxktp" ? "Jyxktp_desc" : "Jyxktp";
            //switch (sortOrder)
            //{
            //    case "daima":
            //        var tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), true, base_gongyingshang => base_gongyingshang.Daima).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            //        ViewBag.base_gongyingshang = tempData;
            //        return View(tempData);
            //    case "daima_desc":
            //        tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), false, base_gongyingshang => base_gongyingshang.Daima).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            //        ViewBag.base_gongyingshang = tempData;
            //        return View(tempData);
            //    case "name":
            //        tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), true, base_gongyingshang => base_gongyingshang.Mingcheng).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            //        ViewBag.base_gongyingshang = tempData;
            //        return View(tempData);
            //    case "name_desc":
            //        tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), false, base_gongyingshang => base_gongyingshang.Mingcheng).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            //        ViewBag.base_gongyingshang = tempData;
            //        return View(tempData);
            //    case "Yyzz":
            //        tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), true, base_gongyingshang => base_gongyingshang.YingyezhizhaoBH).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            //        ViewBag.base_gongyingshang = tempData;
            //        return View(tempData);
            //    case "Yyzz_desc":
            //        tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), false, base_gongyingshang => base_gongyingshang.YingyezhizhaoBH).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            //        ViewBag.base_gongyingshang = tempData;
            //        return View(tempData);
            //    case "Yyzzyxq":
            //        tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), true, base_gongyingshang => base_gongyingshang.YingyezhizhaoYXQ).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            //        ViewBag.base_gongyingshang = tempData;
            //        return View(tempData);
            //    case "Yyzzyxq_desc":
            //        tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), false, base_gongyingshang => base_gongyingshang.YingyezhizhaoYXQ).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            //        ViewBag.base_gongyingshang = tempData;
            //        return View(tempData);
            //    case "Yyzztp":
            //        tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), true, base_gongyingshang => base_gongyingshang.YingyezhizhaoTP).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            //        ViewBag.base_gongyingshang = tempData;
            //        return View(tempData);
            //    case "Yyzztp_desc":
            //        tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), false, base_gongyingshang => base_gongyingshang.YingyezhizhaoTP).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            //        ViewBag.base_gongyingshang = tempData;
            //        return View(tempData);
            //    case "Jyxk":
            //        tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), true, base_gongyingshang => base_gongyingshang.JingyingxukeBH).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            //        ViewBag.base_gongyingshang = tempData;
            //        return View(tempData);
            //    case "Jyxk_desc":
            //        tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), false, base_gongyingshang => base_gongyingshang.JingyingxukeBH).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            //        ViewBag.base_gongyingshang = tempData;
            //        return View(tempData);
            //    case "Jyxkyxq":
            //        tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), true, base_gongyingshang => base_gongyingshang.JingyingxukeYXQ).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            //        ViewBag.base_gongyingshang = tempData;
            //        return View(tempData);
            //    case "Jyxkyxq_desc":
            //        tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), false, base_gongyingshang => base_gongyingshang.JingyingxukeYXQ).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            //        ViewBag.base_gongyingshang = tempData;
            //        return View(tempData);
            //    case "Jyxktp":
            //        tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), true, base_gongyingshang => base_gongyingshang.JingyingxukeTP).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            //        ViewBag.base_gongyingshang = tempData;
            //        return View(tempData);
            //    case "Jyxktp_desc":
            //        tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), false, base_gongyingshang => base_gongyingshang.JingyingxukeTP).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            //        ViewBag.base_gongyingshang = tempData;
            //        return View(tempData);
            //    default:
            //        tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), false, base_gongyingshang => base_gongyingshang.ID).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            //        ViewBag.base_gongyingshang = tempData;
            //        return View(tempData);
            //}
            var tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), false, base_gongyingshang => base_gongyingshang.ID).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_gongyingshang = tempData;
            return View(tempData);









            //ViewBag.DaimaSortParm = String.IsNullOrEmpty(sortOrder) ? "daima_desc" : "";

            //var gongyingshangs = from s in tempData
            //                     select s;
            //switch (sortOrder)
            //{
            //    case "daima_desc":
            //        gongyingshangs = gongyingshangs.OrderByDescending(s => s.Daima);
            //        break;

            //    default:
            //        gongyingshangs = gongyingshangs.OrderBy(s => s.Daima);
            //        break;
            //}
            //return View(gongyingshangs.ToList());

        }





        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_gongyingshang_index";
            string page = "1";
            string daima = Request["daima"] ?? "";
            string daimaequal = Request["daimaequal"] ?? "";
            string daimaand = Request["daimaand"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string mingchengequal = Request["mingchengequal"] ?? "";
            string mingchengand = Request["mingchengand"] ?? "";
            PageMenu.Set("Index", "base_gongyingshang", "基础数据");
            Expression<Func<base_gongyingshang, bool>> where = PredicateExtensionses.True<base_gongyingshang>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //daima
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_gongyingshang => base_gongyingshang.Daima == daima);
                        else
                            where = where.Or(base_gongyingshang => base_gongyingshang.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_gongyingshang => base_gongyingshang.Daima.Contains(daima));
                        else
                            where = where.Or(base_gongyingshang => base_gongyingshang.Daima.Contains(daima));
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
                            where = where.And(base_gongyingshang => base_gongyingshang.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_gongyingshang => base_gongyingshang.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_gongyingshang => base_gongyingshang.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_gongyingshang => base_gongyingshang.Mingcheng.Contains(mingcheng));
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
                //daima
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_gongyingshang => base_gongyingshang.Daima == daima);
                        else
                            where = where.Or(base_gongyingshang => base_gongyingshang.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_gongyingshang => base_gongyingshang.Daima.Contains(daima));
                        else
                            where = where.Or(base_gongyingshang => base_gongyingshang.Daima.Contains(daima));
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
                            where = where.And(base_gongyingshang => base_gongyingshang.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_gongyingshang => base_gongyingshang.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_gongyingshang => base_gongyingshang.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_gongyingshang => base_gongyingshang.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_gongyingshang => base_gongyingshang.IsDelete == false);

            var tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), false, base_gongyingshang => base_gongyingshang.ID).ToPagedList<base_gongyingshang>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_gongyingshang = tempData;
            return View(tempData);
        }

        public ActionResult Add(string page)
        {

            ViewBag.userid = (int)Session["user_id"];
            return View();
        }

        public ActionResult Save()
        {
            string id = Request["ob_base_gongyingshang_id"] ?? "";
            string daima = Request["Daima"] ?? "";
            string mingcheng = Request["Mingcheng"] ?? "";
            string yingyezhizhaobh = Request["ob_base_gongyingshang_yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["ob_base_gongyingshang_yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["ob_base_gongyingshang_yingyezhizhaotp"] ?? "";
            string jingyingxukebh = Request["ob_base_gongyingshang_jingyingxukebh"] ?? "";
            string jingyingxukeyxq = Request["ob_base_gongyingshang_jingyingxukeyxq"] ?? "";
            string jingyingxuketp = Request["ob_base_gongyingshang_jingyingxuketp"] ?? "";
            string jingyingfanwei = Request["ob_base_gongyingshang_jingyingfanwei"] ?? "";
            string jingyingfanweidm = Request["ob_base_gongyingshang_jingyingfanweidm"] ?? "";
            string shouying = Request["ob_base_gongyingshang_shouying"] ?? "";
            string makedate = Request["ob_base_gongyingshang_makedate"] ?? "";
            string makeman = Request["ob_base_gongyingshang_makeman"] ?? "";
            //增加
            string shenchasf = Request["shenchasf"] ?? "";
            string hezuosf = Request["hezuosf"] ?? "";
            string beianbh = Request["ob_base_gongyingshang_beianbh"] ?? "";
            string beianyxq = Request["ob_base_gongyingshang_beianyxq"] ?? "";
            string beianpzrq = Request["ob_base_gongyingshang_beianpzrq"] ?? "";
            string beianfzjg = Request["ob_base_gongyingshang_beianfzjg"] ?? "";
            string beiantp = Request["ob_base_gongyingshang_beiantp"] ?? "";
            string xukepzrq = Request["ob_base_gongyingshang_xukepzrq"] ?? "";
            string xukefzjg = Request["ob_base_gongyingshang_xukefzjg"] ?? "";
            if (shenchasf.IndexOf("true") > -1)
                shenchasf = "true";
            if (hezuosf.IndexOf("true") > -1)
                hezuosf = "true";
            int flag = 0;
            try
            {
                base_gongyingshang ob_base_gongyingshang = new base_gongyingshang();
                ob_base_gongyingshang.Daima = daima.Trim();
                ob_base_gongyingshang.Mingcheng = mingcheng.Trim();
                ob_base_gongyingshang.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                ob_base_gongyingshang.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                ob_base_gongyingshang.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                ob_base_gongyingshang.JingyingxukeBH = jingyingxukebh.Trim();
                ob_base_gongyingshang.JingyingxukeYXQ = jingyingxukeyxq == "" ? DateTime.Now : DateTime.Parse(jingyingxukeyxq);
                ob_base_gongyingshang.JingyingxukeTP = jingyingxuketp.Trim();
                ob_base_gongyingshang.Jingyingfanwei = jingyingfanwei.Trim();
                ob_base_gongyingshang.JingyingfanweiDM = jingyingfanweidm.Trim();
                //ob_base_gongyingshang.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                ob_base_gongyingshang.Shouying = 1;
                ob_base_gongyingshang.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_gongyingshang.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                //增加
                ob_base_gongyingshang.ShenchaSF = shenchasf == "" ? false : Boolean.Parse(shenchasf);
                ob_base_gongyingshang.HezuoSF = hezuosf == "" ? false : Boolean.Parse(hezuosf);
                ob_base_gongyingshang.BeianBH = beianbh.Trim();
                ob_base_gongyingshang.BeianYXQ = beianyxq == "" ? DateTime.Now : DateTime.Parse(beianyxq);
                ob_base_gongyingshang.BeianPZRQ = beianpzrq == "" ? DateTime.Now : DateTime.Parse(beianpzrq);
                ob_base_gongyingshang.BeianFZJG = beianfzjg.Trim();
                ob_base_gongyingshang.BeianTP = beiantp.Trim();
                ob_base_gongyingshang.XukePZRQ = xukepzrq == "" ? DateTime.Now : DateTime.Parse(xukepzrq);
                ob_base_gongyingshang.XukeFZJG = xukefzjg.Trim();

                ob_base_gongyingshang = ob_base_gongyingshangservice.AddEntity(ob_base_gongyingshang);
                ViewBag.base_gongyingshang = ob_base_gongyingshang;
                if (ViewBag.base_gongyingshang == null)
                {
                    flag = -2;
                }
                else { flag = 1; }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                flag = -1;

            }
            //return RedirectToAction("Index");
            return Json(new { _flag = flag });
        }
        public ActionResult Edit(int id)
        {

            base_gongyingshang tempData = ob_base_gongyingshangservice.GetEntityById(base_gongyingshang => base_gongyingshang.ID == id && base_gongyingshang.IsDelete == false);
            ViewBag.base_gongyingshang = tempData;
            return View();
            //if (tempData == null)
            //    return View();
            //else {
            //    base_gongyingshangViewModel base_gongyingshangviewmodel = new base_gongyingshangViewModel();
            //    base_gongyingshangviewmodel.ID = tempData.ID;
            //    base_gongyingshangviewmodel.Daima = tempData.Daima;
            //    base_gongyingshangviewmodel.Mingcheng = tempData.Mingcheng;
            //    base_gongyingshangviewmodel.YingyezhizhaoBH = tempData.YingyezhizhaoBH;
            //    base_gongyingshangviewmodel.YingyezhizhaoYXQ = (DateTime)tempData.YingyezhizhaoYXQ;
            //    base_gongyingshangviewmodel.YingyezhizhaoTP = tempData.YingyezhizhaoTP;
            //    base_gongyingshangviewmodel.JingyingxukeBH = tempData.JingyingxukeBH;
            //    base_gongyingshangviewmodel.JingyingxukeYXQ = (DateTime)tempData.JingyingxukeYXQ;
            //    base_gongyingshangviewmodel.JingyingxukeTP = tempData.JingyingxukeTP;
            //    base_gongyingshangviewmodel.Jingyingfanwei = tempData.Jingyingfanwei;
            //    base_gongyingshangviewmodel.JingyingfanweiDM = tempData.JingyingfanweiDM;
            //    base_gongyingshangviewmodel.Shouying = (int)tempData.Shouying;
            //    base_gongyingshangviewmodel.MakeDate = tempData.MakeDate;
            //    base_gongyingshangviewmodel.MakeMan = tempData.MakeMan;
            //    //新增
            //    base_gongyingshangviewmodel.HezuoSF = tempData.HezuoSF;
            //    base_gongyingshangviewmodel.ShenchaSF = tempData.ShenchaSF;
            //    base_gongyingshangviewmodel.BeianBH = tempData.BeianBH;
            //    base_gongyingshangviewmodel.BeianYXQ = tempData.BeianYXQ;
            //    base_gongyingshangviewmodel.BeianPZRQ = tempData.BeianPZRQ;
            //    base_gongyingshangviewmodel.BeianFZJG = tempData.BeianFZJG;
            //    base_gongyingshangviewmodel.BeianTP = tempData.BeianTP;
            //    base_gongyingshangviewmodel.XukePZRQ = tempData.XukePZRQ; 
            //    base_gongyingshangviewmodel.XukeFZJG = tempData.XukeFZJG;
            //    return View(base_gongyingshangviewmodel);
            //}
        }

        public ActionResult Update()
        {

            string id = Request["ob_base_gongyingshang_id"] ?? "";
            string daima = Request["ob_base_gongyingshang_daima"] ?? "";
            string mingcheng = Request["ob_base_gongyingshang_mingcheng"] ?? "";
            string yingyezhizhaobh = Request["ob_base_gongyingshang_yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["ob_base_gongyingshang_yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["ob_base_gongyingshang_yingyezhizhaotp"] ?? "";
            string jingyingxukebh = Request["ob_base_gongyingshang_jingyingxukebh"] ?? "";
            string jingyingxukeyxq = Request["ob_base_gongyingshang_jingyingxukeyxq"] ?? "";
            string jingyingxuketp = Request["ob_base_gongyingshang_jingyingxuketp"] ?? "";
            string jingyingfanwei = Request["ob_base_gongyingshang_jingyingfanwei"] ?? "";
            string jingyingfanweidm = Request["ob_base_gongyingshang_jingyingfanweidm"] ?? "";
            string shouying = Request["ob_base_gongyingshang_shouying"] ?? "";
            string makedate = Request["ob_base_gongyingshang_makedate"] ?? "";
            //增加
            string shenchasf = Request["shenchasf"] ?? "";
            string hezuosf = Request["hezuosf"] ?? "";
            string beianbh = Request["ob_base_gongyingshang_beianbh"] ?? "";
            string beianyxq = Request["ob_base_gongyingshang_beianyxq"] ?? "";
            string beianpzrq = Request["ob_base_gongyingshang_beianpzrq"] ?? "";
            string beianfzjg = Request["ob_base_gongyingshang_beianfzjg"] ?? "";
            string beiantp = Request["ob_base_gongyingshang_beiantp"] ?? "";
            string xukepzrq = Request["ob_base_gongyingshang_xukepzrq"] ?? "";
            string xukefzjg = Request["ob_base_gongyingshang_xukefzjg"] ?? "";
            //string makeman = Request["ob_base_gongyingshang_makeman"] ?? "";
            string makeman = Session["user_id"].ToString();
            if (shenchasf.IndexOf("true") > -1)
                shenchasf = "true";
            if (hezuosf.IndexOf("true") > -1)
                hezuosf = "true";
            int uid = int.Parse(id);
            int flag = 0;
            try
            {
                base_gongyingshang p = ob_base_gongyingshangservice.GetEntityById(base_gongyingshang => base_gongyingshang.ID == uid);
                p.Daima = daima.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                p.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                p.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                p.JingyingxukeBH = jingyingxukebh.Trim();
                p.JingyingxukeYXQ = jingyingxukeyxq == "" ? DateTime.Now : DateTime.Parse(jingyingxukeyxq);
                p.JingyingxukeTP = jingyingxuketp.Trim();
                p.Jingyingfanwei = jingyingfanwei.Trim();
                p.JingyingfanweiDM = jingyingfanweidm.Trim();
                p.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                //增加
                p.ShenchaSF = shenchasf == "" ? false : Boolean.Parse(shenchasf);
                p.HezuoSF = hezuosf == "" ? false : Boolean.Parse(hezuosf);
                p.BeianBH = beianbh.Trim();
                p.BeianYXQ = beianyxq == "" ? DateTime.Now : DateTime.Parse(beianyxq);
                p.BeianPZRQ = beianpzrq == "" ? DateTime.Now : DateTime.Parse(beianpzrq);
                p.BeianFZJG = beianfzjg.Trim();
                p.BeianTP = beiantp.Trim();
                p.XukePZRQ = xukepzrq == "" ? DateTime.Now : DateTime.Parse(xukepzrq);
                p.XukeFZJG = xukefzjg.Trim();
                bool tt = ob_base_gongyingshangservice.UpdateEntity(p);
                ViewBag.saveok = ViewAddTag.ModifyOk;
                if (!tt)
                {
                    flag = -2;
                }
                else { flag = 1; }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.saveok = ViewAddTag.ModifyNo;
                flag = -1;
            }
            //return RedirectToAction("Edit", new { id = uid, _flag = flag });
            return Json(new { id = uid, _flag = flag });
        }
        public ActionResult Delete()
        {
            string sdel = Request["del"] ?? "";
            int id;
            base_gongyingshang ob_base_gongyingshang;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_gongyingshang = ob_base_gongyingshangservice.GetEntityById(base_gongyingshang => base_gongyingshang.ID == id && base_gongyingshang.IsDelete == false);
                    ob_base_gongyingshang.IsDelete = true;
                    ob_base_gongyingshangservice.UpdateEntity(ob_base_gongyingshang);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult SupplierExport()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_gongyingshang_index";
            Expression<Func<base_gongyingshang, bool>> where = PredicateExtensionses.True<base_gongyingshang>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "daima":
                            string daima = scld[1];
                            string daimaequal = scld[2];
                            string daimaand = scld[3];
                            if (!string.IsNullOrEmpty(daima))
                            {
                                if (daimaequal.Equals("="))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(base_gongyingshang => base_gongyingshang.Daima == daima);
                                    else
                                        where = where.Or(base_gongyingshang => base_gongyingshang.Daima == daima);
                                }
                                if (daimaequal.Equals("like"))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(base_gongyingshang => base_gongyingshang.Daima.Contains(daima));
                                    else
                                        where = where.Or(base_gongyingshang => base_gongyingshang.Daima.Contains(daima));
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
                                        where = where.And(base_gongyingshang => base_gongyingshang.Mingcheng == mingcheng);
                                    else
                                        where = where.Or(base_gongyingshang => base_gongyingshang.Mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_gongyingshang => base_gongyingshang.Mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(base_gongyingshang => base_gongyingshang.Mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_gongyingshang => base_gongyingshang.IsDelete == false);
            var tempData = ob_base_gongyingshangservice.LoadSortEntities(where.Compile(), false, base_gongyingshang => base_gongyingshang.ID);
            ViewBag.supplier = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "SupplierExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("SupplierInformation_{0}.xls", DateTime.Now.ToShortDateString()));
        }
    }
}

