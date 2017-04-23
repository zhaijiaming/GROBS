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
    public class base_shengchanqiyeController : Controller
    {
        private Ibase_shengchanqiyeService ob_base_shengchanqiyeservice = ServiceFactory.base_shengchanqiyeservice;
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page, string sortOrder)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_shengchanqiye_index";
            PageMenu.Set("Index", "base_shengchanqiye", "基础数据");
            Expression<Func<base_shengchanqiye, bool>> where = PredicateExtensionses.True<base_shengchanqiye>();
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
                                        where = where.And(base_shengchanqiye => base_shengchanqiye.Daima == daima);
                                    else
                                        where = where.Or(base_shengchanqiye => base_shengchanqiye.Daima == daima);
                                }
                                if (daimaequal.Equals("like"))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(base_shengchanqiye => base_shengchanqiye.Daima.Contains(daima));
                                    else
                                        where = where.Or(base_shengchanqiye => base_shengchanqiye.Daima.Contains(daima));
                                }
                            }
                            break;
                        case "qiyemingcheng":
                            string qiyemingcheng = scld[1];
                            string qiyemingchengequal = scld[2];
                            string qiyemingchengand = scld[3];
                            if (!string.IsNullOrEmpty(qiyemingcheng))
                            {
                                if (qiyemingchengequal.Equals("="))
                                {
                                    if (qiyemingchengand.Equals("and"))
                                        where = where.And(base_shengchanqiye => base_shengchanqiye.Qiyemingcheng == qiyemingcheng);
                                    else
                                        where = where.Or(base_shengchanqiye => base_shengchanqiye.Qiyemingcheng == qiyemingcheng);
                                }
                                if (qiyemingchengequal.Equals("like"))
                                {
                                    if (qiyemingchengand.Equals("and"))
                                        where = where.And(base_shengchanqiye => base_shengchanqiye.Qiyemingcheng.Contains(qiyemingcheng));
                                    else
                                        where = where.Or(base_shengchanqiye => base_shengchanqiye.Qiyemingcheng.Contains(qiyemingcheng));
                                }
                            }
                            break;
                        //case "shouying":
                        //    string shouying = scld[1];
                        //    string shouyingequal = scld[2];
                        //    string shouyingand = scld[3];
                        //    if (!string.IsNullOrEmpty(shouying))
                        //    {
                        //        if (shouyingequal.Equals("="))
                        //        {
                        //            if (shouyingand.Equals("and"))
                        //                where = where.And(base_shengchanqiye => base_shengchanqiye.Shouying == int.Parse(shouying));
                        //            else
                        //                where = where.Or(base_shengchanqiye => base_shengchanqiye.Shouying == int.Parse(shouying));
                        //        }
                        //    }
                        //    break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_shengchanqiye => base_shengchanqiye.IsDelete == false);

            var tempData = ob_base_shengchanqiyeservice.LoadSortEntities(where.Compile(), false, base_shengchanqiye => base_shengchanqiye.ID).ToPagedList<base_shengchanqiye>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shengchanqiye = tempData;
            //return View(tempData);

            //ViewBag.DaimaSortParm = String.IsNullOrEmpty(sortOrder) ? "daima_desc" : "";

            //var students = from s in base_shengchanqiye
            //               select s;
            //switch (sortOrder)
            //{
            //    case "daima_desc":
            //        students = students.OrderByDescending(s => s.LastName);
            //        break;
            //    default:
            //        students = students.OrderBy(s => s.LastName);
            //        break;
            //}

            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_shengchanqiye_index";
            string page = "1";
            //daima
            string daima = Request["daima"] ?? "";
            string daimaequal = Request["daimaequal"] ?? "";
            string daimaand = Request["daimaand"] ?? "";
            //qiyemingcheng
            string qiyemingcheng = Request["qiyemingcheng"] ?? "";
            string qiyemingchengequal = Request["qiyemingchengequal"] ?? "";
            string qiyemingchengand = Request["qiyemingchengand"] ?? "";
            //shouying
            //string shouying = Request["shouying"] ?? "";
            //string shouyingequal = Request["shouyingequal"] ?? "";
            //string shouyingand = Request["shouyingand"] ?? "";
            PageMenu.Set("Index", "base_shengchanqiye", "基础数据");
            Expression<Func<base_shengchanqiye, bool>> where = PredicateExtensionses.True<base_shengchanqiye>();
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
                            where = where.And(base_shengchanqiye => base_shengchanqiye.Daima == daima);
                        else
                            where = where.Or(base_shengchanqiye => base_shengchanqiye.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_shengchanqiye => base_shengchanqiye.Daima.Contains(daima));
                        else
                            where = where.Or(base_shengchanqiye => base_shengchanqiye.Daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);
                //qiyemingcheng
                if (!string.IsNullOrEmpty(qiyemingcheng))
                {
                    if (qiyemingchengequal.Equals("="))
                    {
                        if (qiyemingchengand.Equals("and"))
                            where = where.And(base_shengchanqiye => base_shengchanqiye.Qiyemingcheng == qiyemingcheng);
                        else
                            where = where.Or(base_shengchanqiye => base_shengchanqiye.Qiyemingcheng == qiyemingcheng);
                    }
                    if (qiyemingchengequal.Equals("like"))
                    {
                        if (qiyemingchengand.Equals("and"))
                            where = where.And(base_shengchanqiye => base_shengchanqiye.Qiyemingcheng.Contains(qiyemingcheng));
                        else
                            where = where.Or(base_shengchanqiye => base_shengchanqiye.Qiyemingcheng.Contains(qiyemingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(qiyemingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "qiyemingcheng", qiyemingcheng, qiyemingchengequal, qiyemingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "qiyemingcheng", "", qiyemingchengequal, qiyemingchengand);
                //shouying
                //if (!string.IsNullOrEmpty(shouying))
                //{
                //    if (shouyingequal.Equals("="))
                //    {
                //        if (shouyingand.Equals("and"))
                //            where = where.And(base_shengchanqiye => base_shengchanqiye.Shouying == int.Parse(shouying));
                //        else
                //            where = where.Or(base_shengchanqiye => base_shengchanqiye.Shouying == int.Parse(shouying));
                //    }
                //}
                //if (!string.IsNullOrEmpty(shouying))
                //    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouying", shouying, shouyingequal, shouyingand);
                //else
                //    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouying", "", shouyingequal, shouyingand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_shengchanqiye => base_shengchanqiye.Daima == daima);
                        else
                            where = where.Or(base_shengchanqiye => base_shengchanqiye.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_shengchanqiye => base_shengchanqiye.Daima.Contains(daima));
                        else
                            where = where.Or(base_shengchanqiye => base_shengchanqiye.Daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);

                //name
                if (!string.IsNullOrEmpty(qiyemingcheng))
                {
                    if (qiyemingchengequal.Equals("="))
                    {
                        if (qiyemingchengand.Equals("and"))
                            where = where.And(base_shengchanqiye => base_shengchanqiye.Qiyemingcheng == qiyemingcheng);
                        else
                            where = where.Or(base_shengchanqiye => base_shengchanqiye.Qiyemingcheng == qiyemingcheng);
                    }
                    if (qiyemingchengequal.Equals("like"))
                    {
                        if (qiyemingchengand.Equals("and"))
                            where = where.And(base_shengchanqiye => base_shengchanqiye.Qiyemingcheng.Contains(qiyemingcheng));
                        else
                            where = where.Or(base_shengchanqiye => base_shengchanqiye.Qiyemingcheng.Contains(qiyemingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(qiyemingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "qiyemingcheng", qiyemingcheng, qiyemingchengequal, qiyemingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "qiyemingcheng", "", qiyemingchengequal, qiyemingchengand);
                //shouying
                //if (!string.IsNullOrEmpty(shouying))
                //{
                //    if (shouyingequal.Equals("="))
                //    {
                //        if (shouyingand.Equals("and"))
                //            where = where.And(base_shengchanqiye => base_shengchanqiye.Shouying == int.Parse(shouying));
                //        else
                //            where = where.Or(base_shengchanqiye => base_shengchanqiye.Shouying == int.Parse(shouying));
                //    }
                //}
                //if (!string.IsNullOrEmpty(shouying))
                //    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouying", shouying, shouyingequal, shouyingand);
                //else
                //    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouying", "", shouyingequal, shouyingand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_shengchanqiye => base_shengchanqiye.IsDelete == false);

            var tempData = ob_base_shengchanqiyeservice.LoadSortEntities(where.Compile(), false, base_shengchanqiye => base_shengchanqiye.ID).ToPagedList<base_shengchanqiye>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shengchanqiye = tempData;
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
            string daima = Request["daima"] ?? "";
            string qiyemingcheng = Request["qiyemingcheng"] ?? "";
            string yingyezhizhaobh = Request["yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["yingyezhizhaotp"] ?? "";
            string shengchanxukebh = Request["shengchanxukebh"] ?? "";
            string shengchanxukeyxq = Request["shengchanxukeyxq"] ?? "";
            string shengchanxuketp = Request["shengchanxuketp"] ?? "";
            string shouying = Request["shouying"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            //新增
            string qiyedizhi = Request["qiyedizhi"] ?? "";
            string shengchandizhi = Request["shengchandizhi"] ?? "";
            string hezuosf = Request["hezuosf"] ?? "";
            string shenchasf = Request["shenchasf"] ?? "";
            string beianbh = Request["beianbh"] ?? "";
            string beianyxq = Request["beianyxq"] ?? "";
            string beianpzrq = Request["beianpzrq"] ?? "";
            string beianfzjg = Request["beianfzjg"] ?? "";
            string beiantp = Request["beiantp"] ?? "";
            string xukepzrq = Request["xukepzrq"] ?? "";
            string xukefzjg = Request["xukefzjg"] ?? "";
            string xukefanwei = Request["xukefanwei"] ?? "";
            string xukefanweidm = Request["xukefanweidm"] ?? "";
            if (shenchasf.IndexOf("true") > -1)
                shenchasf = "true";
            if (hezuosf.IndexOf("true") > -1)
                hezuosf = "true";
            int flag = 0;

            try
            {
                base_shengchanqiye ob_base_shengchanqiye = new base_shengchanqiye();
                ob_base_shengchanqiye.Daima = daima.Trim();
                ob_base_shengchanqiye.Qiyemingcheng = qiyemingcheng.Trim();
                ob_base_shengchanqiye.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                ob_base_shengchanqiye.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                ob_base_shengchanqiye.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                ob_base_shengchanqiye.ShengchanxukeBH = shengchanxukebh.Trim();
                ob_base_shengchanqiye.ShengchanxukeYXQ = shengchanxukeyxq == "" ? DateTime.Now : DateTime.Parse(shengchanxukeyxq);
                ob_base_shengchanqiye.ShengchanxukeTP = shengchanxuketp.Trim();
                ob_base_shengchanqiye.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                ob_base_shengchanqiye.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_shengchanqiye.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                //新增
                ob_base_shengchanqiye.Qiyedizhi = qiyedizhi.Trim();
                ob_base_shengchanqiye.Shengchandizhi = shengchandizhi.Trim();
                ob_base_shengchanqiye.HezuoSF = hezuosf == "" ? false : Boolean.Parse(hezuosf);
                ob_base_shengchanqiye.ShenchaSF = shenchasf == "" ? false : Boolean.Parse(shenchasf);
                ob_base_shengchanqiye.BeianBH = beianbh.Trim();
                ob_base_shengchanqiye.BeianYXQ = beianyxq == "" ? DateTime.Now : DateTime.Parse(beianyxq);
                ob_base_shengchanqiye.BeianPZRQ = beianpzrq == "" ? DateTime.Now : DateTime.Parse(beianpzrq);
                ob_base_shengchanqiye.BeianFZJG = beianfzjg.Trim();
                ob_base_shengchanqiye.BeianTP = beiantp.Trim();
                ob_base_shengchanqiye.XukePZRQ = xukepzrq == "" ? DateTime.Now : DateTime.Parse(xukepzrq);
                ob_base_shengchanqiye.XukeFZJG = xukefzjg.Trim();
                ob_base_shengchanqiye.Xukefanwei = xukefanwei.Trim();
                ob_base_shengchanqiye.XukefanweiDM = xukefanweidm.Trim();

                ob_base_shengchanqiye = ob_base_shengchanqiyeservice.AddEntity(ob_base_shengchanqiye);
                ViewBag.base_shengchanqiye = ob_base_shengchanqiye;
                if (ViewBag.base_shengchanqiye == null)
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

        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            base_shengchanqiye tempData = ob_base_shengchanqiyeservice.GetEntityById(base_shengchanqiye => base_shengchanqiye.ID == id && base_shengchanqiye.IsDelete == false);
            ViewBag.base_shengchanqiye = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_shengchanqiyeViewModel base_shengchanqiyeviewmodel = new base_shengchanqiyeViewModel();
                base_shengchanqiyeviewmodel.ID = tempData.ID;
                base_shengchanqiyeviewmodel.Daima = tempData.Daima;
                base_shengchanqiyeviewmodel.Qiyemingcheng = tempData.Qiyemingcheng;
                base_shengchanqiyeviewmodel.YingyezhizhaoBH = tempData.YingyezhizhaoBH;
                base_shengchanqiyeviewmodel.YingyezhizhaoYXQ = tempData.YingyezhizhaoYXQ;
                base_shengchanqiyeviewmodel.YingyezhizhaoTP = tempData.YingyezhizhaoTP;
                base_shengchanqiyeviewmodel.ShengchanxukeBH = tempData.ShengchanxukeBH;
                base_shengchanqiyeviewmodel.ShengchanxukeYXQ = tempData.ShengchanxukeYXQ;
                base_shengchanqiyeviewmodel.ShengchanxukeTP = tempData.ShengchanxukeTP;
                base_shengchanqiyeviewmodel.Shouying = tempData.Shouying;
                base_shengchanqiyeviewmodel.MakeDate = tempData.MakeDate;
                base_shengchanqiyeviewmodel.MakeMan = tempData.MakeMan;
                //新增
                base_shengchanqiyeviewmodel.Qiyedizhi = tempData.Qiyedizhi;
                base_shengchanqiyeviewmodel.Shengchandizhi = tempData.Shengchandizhi;
                base_shengchanqiyeviewmodel.HezuoSF = tempData.HezuoSF;
                base_shengchanqiyeviewmodel.ShenchaSF = tempData.ShenchaSF;
                base_shengchanqiyeviewmodel.BeianBH = tempData.BeianBH;
                base_shengchanqiyeviewmodel.BeianYXQ = tempData.BeianYXQ;
                base_shengchanqiyeviewmodel.BeianPZRQ = tempData.BeianPZRQ;
                base_shengchanqiyeviewmodel.BeianFZJG = tempData.BeianFZJG;
                base_shengchanqiyeviewmodel.BeianTP = tempData.BeianTP;
                base_shengchanqiyeviewmodel.XukePZRQ = tempData.XukePZRQ;
                base_shengchanqiyeviewmodel.XukeFZJG = tempData.XukeFZJG;
                base_shengchanqiyeviewmodel.Xukefanwei = tempData.Xukefanwei;
                base_shengchanqiyeviewmodel.XukefanweiDM = tempData.XukefanweiDM;
                return View(base_shengchanqiyeviewmodel);

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string daima = Request["daima"] ?? "";
            string qiyemingcheng = Request["qiyemingcheng"] ?? "";
            string yingyezhizhaobh = Request["yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["yingyezhizhaotp"] ?? "";
            string shengchanxukebh = Request["shengchanxukebh"] ?? "";
            string shengchanxukeyxq = Request["shengchanxukeyxq"] ?? "";
            string shengchanxuketp = Request["shengchanxuketp"] ?? "";
            string shouying = Request["shouying"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            //string makeman = Session["user_id"].ToString();
            //新增
            string qiyedizhi = Request["qiyedizhi"] ?? "";
            string shengchandizhi = Request["shengchandizhi"] ?? "";
            string hezuosf = Request["hezuosf"] ?? "";
            string shenchasf = Request["shenchasf"] ?? "";
            string beianbh = Request["beianbh"] ?? "";
            string beianyxq = Request["beianyxq"] ?? "";
            string beianpzrq = Request["beianpzrq"] ?? "";
            string beianfzjg = Request["beianfzjg"] ?? "";
            string beiantp = Request["beiantp"] ?? "";
            string xukepzrq = Request["xukepzrq"] ?? "";
            string xukefzjg = Request["xukefzjg"] ?? "";
            string xukefanwei = Request["xukefanwei"] ?? "";
            string xukefanweidm = Request["xukefanweidm"] ?? "";
            if (shenchasf.IndexOf("true") > -1)
                shenchasf = "true";
            if (hezuosf.IndexOf("true") > -1)
                hezuosf = "true";
            int uid = int.Parse(id);
            int flag = 0;

            try
            {
                base_shengchanqiye p = ob_base_shengchanqiyeservice.GetEntityById(base_shengchanqiye => base_shengchanqiye.ID == uid);
                p.Daima = daima.Trim();
                p.Qiyemingcheng = qiyemingcheng.Trim();
                p.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                p.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                p.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                p.ShengchanxukeBH = shengchanxukebh.Trim();
                p.ShengchanxukeYXQ = shengchanxukeyxq == "" ? DateTime.Now : DateTime.Parse(shengchanxukeyxq);
                p.ShengchanxukeTP = shengchanxuketp.Trim();
                p.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                //新增
                p.Qiyedizhi = qiyedizhi.Trim();
                p.Shengchandizhi = shengchandizhi.Trim();
                p.HezuoSF = hezuosf == "" ? false : Boolean.Parse(hezuosf);
                p.ShenchaSF = shenchasf == "" ? false : Boolean.Parse(shenchasf);
                p.BeianBH = beianbh.Trim();
                p.BeianYXQ = beianyxq == "" ? DateTime.Now : DateTime.Parse(beianyxq);
                p.BeianPZRQ = beianpzrq == "" ? DateTime.Now : DateTime.Parse(beianpzrq);
                p.BeianFZJG = beianfzjg.Trim();
                p.BeianTP = beiantp.Trim();
                p.XukePZRQ = xukepzrq == "" ? DateTime.Now : DateTime.Parse(xukepzrq);
                p.XukeFZJG = xukefzjg.Trim();
                p.Xukefanwei = xukefanwei.Trim();
                p.XukefanweiDM = xukefanweidm.Trim();

                ob_base_shengchanqiyeservice.UpdateEntity(p);
                ViewBag.saveok = ViewAddTag.ModifyOk;
                if (!ob_base_shengchanqiyeservice.UpdateEntity(p))
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
            //return RedirectToAction("Edit", new { id = uid });
            return Json(new { _flag = flag });
        }
        public ActionResult Delete()
        {
            string sdel = Request["del"] ?? "";
            int id;
            base_shengchanqiye ob_base_shengchanqiye;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_shengchanqiye = ob_base_shengchanqiyeservice.GetEntityById(base_shengchanqiye => base_shengchanqiye.ID == id && base_shengchanqiye.IsDelete == false);
                    ob_base_shengchanqiye.IsDelete = true;
                    ob_base_shengchanqiyeservice.UpdateEntity(ob_base_shengchanqiye);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

