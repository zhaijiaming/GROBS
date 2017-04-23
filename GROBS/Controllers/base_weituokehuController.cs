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
    public class base_weituokehuController : Controller
    {
        private Ibase_weituokehuService ob_base_weituokehuservice = ServiceFactory.base_weituokehuservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_weituokehu_index";
            PageMenu.Set("Index", "base_weituokehu", "基础数据");
            Expression<Func<base_weituokehu, bool>> where = PredicateExtensionses.True<base_weituokehu>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
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
                                        where = where.And(base_weituokehu => base_weituokehu.Daima == daima);
                                    else
                                        where = where.Or(base_weituokehu => base_weituokehu.Daima == daima);
                                }
                                if (daimaequal.Equals("like"))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(base_weituokehu => base_weituokehu.Daima.Contains(daima));
                                    else
                                        where = where.Or(base_weituokehu => base_weituokehu.Daima.Contains(daima));
                                }
                            }
                            break;
                        case "jiancheng":
                            string jiancheng = scld[1];
                            string jianchengequal = scld[2];
                            string jianchengand = scld[3];
                            if (!string.IsNullOrEmpty(jiancheng))
                            {
                                if (jianchengequal.Equals("="))
                                {
                                    if (jianchengand.Equals("and"))
                                        where = where.And(base_weituokehu => base_weituokehu.Jiancheng == jiancheng);
                                    else
                                        where = where.Or(base_weituokehu => base_weituokehu.Jiancheng == jiancheng);
                                }
                                if (jianchengequal.Equals("like"))
                                {
                                    if (jianchengand.Equals("and"))
                                        where = where.And(base_weituokehu => base_weituokehu.Jiancheng.Contains(jiancheng));
                                    else
                                        where = where.Or(base_weituokehu => base_weituokehu.Jiancheng.Contains(jiancheng));
                                }
                            }
                            break;
                        case "hetongbianhao":
                            string hetongbianhao = scld[1];
                            string hetongbianhaoequal = scld[2];
                            string hetongbianhaoand = scld[3];
                            if (!string.IsNullOrEmpty(hetongbianhao))
                            {
                                if (hetongbianhaoequal.Equals("="))
                                {
                                    if (hetongbianhaoand.Equals("and"))
                                        where = where.And(base_weituokehu => base_weituokehu.Hetongbianhao == hetongbianhao);
                                    else
                                        where = where.Or(base_weituokehu => base_weituokehu.Hetongbianhao == hetongbianhao);
                                }
                                if (hetongbianhaoequal.Equals("like"))
                                {
                                    if (hetongbianhaoand.Equals("and"))
                                        where = where.And(base_weituokehu => base_weituokehu.Hetongbianhao.Contains(hetongbianhao));
                                    else
                                        where = where.Or(base_weituokehu => base_weituokehu.Hetongbianhao.Contains(hetongbianhao));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_weituokehu => base_weituokehu.IsDelete == false);

            var tempData = ob_base_weituokehuservice.LoadSortEntities(where.Compile(), false, base_weituokehu => base_weituokehu.ID).ToPagedList<base_weituokehu>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_weituokehu = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_weituokehu_index";
            string page = "1";
            //daima
            string daima = Request["daima"] ?? "";
            string daimaequal = Request["daimaequal"] ?? "";
            string daimaand = Request["daimaand"] ?? "";

            //jiancheng
            string jiancheng = Request["jiancheng"] ?? "";
            string jianchengequal = Request["jianchengequal"] ?? "";
            string jianchengand = Request["jianchengand"] ?? "";

            //hetongbianhao
            string hetongbianhao = Request["hetongbianhao"] ?? "";
            string hetongbianhaoequal = Request["hetongbianhaoequal"] ?? "";
            string hetongbianhaoand = Request["hetongbianhaoand"] ?? "";

            PageMenu.Set("Index", "base_weituokehu", "基础数据");
            Expression<Func<base_weituokehu, bool>> where = PredicateExtensionses.True<base_weituokehu>();
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
                            where = where.And(base_weituokehu => base_weituokehu.Daima == daima);
                        else
                            where = where.Or(base_weituokehu => base_weituokehu.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_weituokehu => base_weituokehu.Daima.Contains(daima));
                        else
                            where = where.Or(base_weituokehu => base_weituokehu.Daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);

                //jiancheng
                if (!string.IsNullOrEmpty(jiancheng))
                {
                    if (jianchengequal.Equals("="))
                    {
                        if (jianchengand.Equals("and"))
                            where = where.And(base_weituokehu => base_weituokehu.Jiancheng == jiancheng);
                        else
                            where = where.Or(base_weituokehu => base_weituokehu.Jiancheng == jiancheng);
                    }
                    if (jianchengequal.Equals("like"))
                    {
                        if (jianchengand.Equals("and"))
                            where = where.And(base_weituokehu => base_weituokehu.Jiancheng.Contains(jiancheng));
                        else
                            where = where.Or(base_weituokehu => base_weituokehu.Jiancheng.Contains(jiancheng));
                    }
                }
                if (!string.IsNullOrEmpty(jiancheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jiancheng", jiancheng, jianchengequal, jianchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jiancheng", "", jianchengequal, jianchengand);

                //hetongbianhao
                if (!string.IsNullOrEmpty(hetongbianhao))
                {
                    if (hetongbianhaoequal.Equals("="))
                    {
                        if (hetongbianhaoand.Equals("and"))
                            where = where.And(base_weituokehu => base_weituokehu.Hetongbianhao == hetongbianhao);
                        else
                            where = where.Or(base_weituokehu => base_weituokehu.Hetongbianhao == hetongbianhao);
                    }
                    if (hetongbianhaoequal.Equals("like"))
                    {
                        if (hetongbianhaoand.Equals("and"))
                            where = where.And(base_weituokehu => base_weituokehu.Hetongbianhao.Contains(hetongbianhao));
                        else
                            where = where.Or(base_weituokehu => base_weituokehu.Hetongbianhao.Contains(hetongbianhao));
                    }
                }
                if (!string.IsNullOrEmpty(hetongbianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "hetongbianhao", hetongbianhao, hetongbianhaoequal, hetongbianhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "hetongbianhao", "", hetongbianhaoequal, hetongbianhaoand);

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
                            where = where.And(base_weituokehu => base_weituokehu.Daima == daima);
                        else
                            where = where.Or(base_weituokehu => base_weituokehu.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_weituokehu => base_weituokehu.Daima.Contains(daima));
                        else
                            where = where.Or(base_weituokehu => base_weituokehu.Daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);

                //jiancheng
                if (!string.IsNullOrEmpty(jiancheng))
                {
                    if (jianchengequal.Equals("="))
                    {
                        if (jianchengand.Equals("and"))
                            where = where.And(base_weituokehu => base_weituokehu.Jiancheng == jiancheng);
                        else
                            where = where.Or(base_weituokehu => base_weituokehu.Jiancheng == jiancheng);
                    }
                    if (jianchengequal.Equals("like"))
                    {
                        if (jianchengand.Equals("and"))
                            where = where.And(base_weituokehu => base_weituokehu.Jiancheng.Contains(jiancheng));
                        else
                            where = where.Or(base_weituokehu => base_weituokehu.Jiancheng.Contains(jiancheng));
                    }
                }
                if (!string.IsNullOrEmpty(jiancheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jiancheng", jiancheng, jianchengequal, jianchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jiancheng", "", jianchengequal, jianchengand);

                //hetongbianhao
                if (!string.IsNullOrEmpty(hetongbianhao))
                {
                    if (hetongbianhaoequal.Equals("="))
                    {
                        if (hetongbianhaoand.Equals("and"))
                            where = where.And(base_weituokehu => base_weituokehu.Hetongbianhao == hetongbianhao);
                        else
                            where = where.Or(base_weituokehu => base_weituokehu.Hetongbianhao == hetongbianhao);
                    }
                    if (hetongbianhaoequal.Equals("like"))
                    {
                        if (hetongbianhaoand.Equals("and"))
                            where = where.And(base_weituokehu => base_weituokehu.Hetongbianhao.Contains(hetongbianhao));
                        else
                            where = where.Or(base_weituokehu => base_weituokehu.Hetongbianhao.Contains(hetongbianhao));
                    }
                }
                if (!string.IsNullOrEmpty(hetongbianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "hetongbianhao", hetongbianhao, hetongbianhaoequal, hetongbianhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "hetongbianhao", "", hetongbianhaoequal, hetongbianhaoand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_weituokehu => base_weituokehu.IsDelete == false);

            var tempData = ob_base_weituokehuservice.LoadSortEntities(where.Compile(), false, base_weituokehu => base_weituokehu.ID).ToPagedList<base_weituokehu>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_weituokehu = tempData;
            return View(tempData);
        }
        public JsonResult GetCustomer()
        {
            var _custid = Request["cust"] ?? "";
            if (string.IsNullOrEmpty(_custid))
                return Json(-1);
            var _custallow = ob_base_weituokehuservice.GetEntityById(p => p.ID == int.Parse(_custid));
            if (_custallow == null)
                return Json(-1);
            return Json(_custallow);
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
            string jiancheng = Request["jiancheng"] ?? "";
            string kehumingcheng = Request["kehumingcheng"] ?? "";
            string hetongbianhao = Request["hetongbianhao"] ?? "";
            string yingyezhizhaobh = Request["yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["yingyezhizhaotp"] ?? "";
            string zuzhijigoubh = Request["zuzhijigoubh"] ?? "";
            string zuzhijigouyxq = Request["zuzhijigouyxq"] ?? "";
            string zuzhijigoutp = Request["zuzhijigoutp"] ?? "";
            string shuiwudengjibh = Request["shuiwudengjibh"] ?? "";
            string shuiwudengjiyxq = Request["shuiwudengjiyxq"] ?? "";
            string shuiwudengjitp = Request["shuiwudengjitp"] ?? "";
            string jingyingxukebh = Request["jingyingxukebh"] ?? "";
            string jingyingxukeyxq = Request["jingyingxukeyxq"] ?? "";
            string jingyingxuketp = Request["jingyingxuketp"] ?? "";
            string jingyingfanwei = Request["jingyingfanwei"] ?? "";
            string jingyingfanweidm = Request["jingyingfanweidm"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidianhua = Request["lianxidianhua"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string shouying = Request["shouying"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string col4 = Request["col4"] ?? "";
            string col5 = Request["col5"] ?? "";
            string col6 = Request["col6"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string shenchasf = Request["shenchasf"] ?? "";
            string hezuosf = Request["hezuosf"] ?? "";
            string beianbh = Request["beianbh"] ?? "";
            string beianyxq = Request["beianyxq"] ?? "";
            string beianpzrq = Request["beianpzrq"] ?? "";
            string beianfzjg = Request["beianfzjg"] ?? "";
            string beiantp = Request["beiantp"] ?? "";
            string xukepzrq = Request["xukepzrq"] ?? "";
            string xukefzjg = Request["xukefzjg"] ?? "";
            string zhucedz = Request["zhucedz"] ?? "";
            string jingyindz = Request["jingyindz"] ?? "";
            string kufangdz = Request["kufangdz"] ?? "";
            string weituonr = Request["weituonr"] ?? "";
            string weituoksrq = Request["weituoksrq"] ?? "";
            string weituojsrq = Request["weituojsrq"] ?? "";
            string weituoqx = Request["weituoqx"] ?? "";
            string tiezwbq = Request["tiezwbq"] ?? "";
            string hetongtp = Request["hetongtp"] ?? "";
            string faren = Request["faren"] ?? "";
            string fuzeren = Request["fuzeren"] ?? "";
            string heyisf = Request["heyisf"] ?? "";
            if (shenchasf.IndexOf("true") > -1)
                shenchasf = "true";
            if (hezuosf.IndexOf("true") > -1)
                hezuosf = "true";
            if (heyisf.IndexOf("true") > -1)
                heyisf = "true";
            if (tiezwbq.IndexOf("true") > -1)
                tiezwbq = "true";
            else
                tiezwbq = "false";
            try
            {
                base_weituokehu ob_base_weituokehu = new base_weituokehu();
                ob_base_weituokehu.Daima = daima.Trim();
                ob_base_weituokehu.Jiancheng = jiancheng.Trim();
                ob_base_weituokehu.Kehumingcheng = kehumingcheng.Trim();
                ob_base_weituokehu.Hetongbianhao = hetongbianhao.Trim();
                ob_base_weituokehu.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                ob_base_weituokehu.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                ob_base_weituokehu.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                ob_base_weituokehu.ZuzhijigouBH = zuzhijigoubh.Trim();
                ob_base_weituokehu.ZuzhijigouYXQ = zuzhijigouyxq == "" ? DateTime.Now : DateTime.Parse(zuzhijigouyxq);
                ob_base_weituokehu.ZuzhijigouTP = zuzhijigoutp.Trim();
                ob_base_weituokehu.ShuiwudengjiBH = shuiwudengjibh.Trim();
                ob_base_weituokehu.ShuiwudengjiYXQ = shuiwudengjiyxq == "" ? DateTime.Now : DateTime.Parse(shuiwudengjiyxq);
                ob_base_weituokehu.ShuiwudengjiTP = shuiwudengjitp.Trim();
                ob_base_weituokehu.JingyingxukeBH = jingyingxukebh.Trim();
                ob_base_weituokehu.JingyingxukeYXQ = jingyingxukeyxq == "" ? DateTime.Now : DateTime.Parse(jingyingxukeyxq);
                ob_base_weituokehu.JingyingxukeTP = jingyingxuketp.Trim();
                ob_base_weituokehu.Jingyingfanwei = jingyingfanwei.Trim();
                ob_base_weituokehu.JingyingfanweiDM = jingyingfanweidm.Trim();
                ob_base_weituokehu.Lianxiren = lianxiren.Trim();
                ob_base_weituokehu.Lianxidianhua = lianxidianhua.Trim();
                ob_base_weituokehu.Beizhu = beizhu.Trim();
                ob_base_weituokehu.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                ob_base_weituokehu.Col1 = col1.Trim();
                ob_base_weituokehu.Col2 = col2.Trim();
                ob_base_weituokehu.Col3 = col3.Trim();
                ob_base_weituokehu.Col4 = col4.Trim();
                ob_base_weituokehu.Col5 = col5.Trim();
                ob_base_weituokehu.Col6 = col6.Trim();
                ob_base_weituokehu.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_weituokehu.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_weituokehu.ShenchaSF = shenchasf == "" ? false : Boolean.Parse(shenchasf);
                ob_base_weituokehu.HezuoSF = hezuosf == "" ? false : Boolean.Parse(hezuosf);
                ob_base_weituokehu.BeianBH = beianbh.Trim();
                ob_base_weituokehu.BeianYXQ = beianyxq == "" ? DateTime.Now : DateTime.Parse(beianyxq);
                ob_base_weituokehu.BeianPZRQ = beianpzrq == "" ? DateTime.Now : DateTime.Parse(beianpzrq);
                ob_base_weituokehu.BeianFZJG = beianfzjg.Trim();
                ob_base_weituokehu.BeianTP = beiantp.Trim();
                ob_base_weituokehu.XukePZRQ = xukepzrq == "" ? DateTime.Now : DateTime.Parse(xukepzrq);
                ob_base_weituokehu.XukeFZJG = xukefzjg.Trim();
                ob_base_weituokehu.ZhuceDZ = zhucedz.Trim();
                ob_base_weituokehu.JingyinDZ = jingyindz.Trim();
                ob_base_weituokehu.KufangDZ = kufangdz.Trim();
                ob_base_weituokehu.WeituoNR = weituonr.Trim();
                ob_base_weituokehu.WeituoKSRQ = weituoksrq == "" ? DateTime.Now : DateTime.Parse(weituoksrq);
                ob_base_weituokehu.WeituoJSRQ = weituojsrq == "" ? DateTime.Now : DateTime.Parse(weituojsrq);
                ob_base_weituokehu.WeituoQX = weituoqx == "" ? 0 : int.Parse(weituoqx);
                ob_base_weituokehu.TieZWBQ = tiezwbq == "" ? false : Boolean.Parse(tiezwbq);
                ob_base_weituokehu.HetongTP = hetongtp.Trim();
                ob_base_weituokehu.Faren = faren.Trim();
                ob_base_weituokehu.Fuzeren = fuzeren.Trim();
                ob_base_weituokehu.HeyiSF = heyisf == "" ? false : Boolean.Parse(heyisf);
                ob_base_weituokehu = ob_base_weituokehuservice.AddEntity(ob_base_weituokehu);
                ViewBag.base_weituokehu = ob_base_weituokehu;
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
            base_weituokehu tempData = ob_base_weituokehuservice.GetEntityById(base_weituokehu => base_weituokehu.ID == id && base_weituokehu.IsDelete == false);
            ViewBag.base_weituokehu = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_weituokehuViewModel base_weituokehuviewmodel = new base_weituokehuViewModel();
                base_weituokehuviewmodel.ID = tempData.ID;
                base_weituokehuviewmodel.Daima = tempData.Daima;
                base_weituokehuviewmodel.Jiancheng = tempData.Jiancheng;
                base_weituokehuviewmodel.Kehumingcheng = tempData.Kehumingcheng;
                base_weituokehuviewmodel.Hetongbianhao = tempData.Hetongbianhao;
                base_weituokehuviewmodel.YingyezhizhaoBH = tempData.YingyezhizhaoBH;
                base_weituokehuviewmodel.YingyezhizhaoYXQ = tempData.YingyezhizhaoYXQ;
                base_weituokehuviewmodel.YingyezhizhaoTP = tempData.YingyezhizhaoTP;
                base_weituokehuviewmodel.ZuzhijigouBH = tempData.ZuzhijigouBH;
                base_weituokehuviewmodel.ZuzhijigouYXQ = tempData.ZuzhijigouYXQ;
                base_weituokehuviewmodel.ZuzhijigouTP = tempData.ZuzhijigouTP;
                base_weituokehuviewmodel.ShuiwudengjiBH = tempData.ShuiwudengjiBH;
                base_weituokehuviewmodel.ShuiwudengjiYXQ = tempData.ShuiwudengjiYXQ;
                base_weituokehuviewmodel.ShuiwudengjiTP = tempData.ShuiwudengjiTP;
                base_weituokehuviewmodel.JingyingxukeBH = tempData.JingyingxukeBH;
                base_weituokehuviewmodel.JingyingxukeYXQ = tempData.JingyingxukeYXQ;
                base_weituokehuviewmodel.JingyingxukeTP = tempData.JingyingxukeTP;
                base_weituokehuviewmodel.Jingyingfanwei = tempData.Jingyingfanwei;
                base_weituokehuviewmodel.JingyingfanweiDM = tempData.JingyingfanweiDM;
                base_weituokehuviewmodel.Lianxiren = tempData.Lianxiren;
                base_weituokehuviewmodel.Lianxidianhua = tempData.Lianxidianhua;
                base_weituokehuviewmodel.Beizhu = tempData.Beizhu;
                base_weituokehuviewmodel.Shouying = tempData.Shouying;
                base_weituokehuviewmodel.Col1 = tempData.Col1;
                base_weituokehuviewmodel.Col2 = tempData.Col2;
                base_weituokehuviewmodel.Col3 = tempData.Col3;
                base_weituokehuviewmodel.Col4 = tempData.Col4;
                base_weituokehuviewmodel.Col5 = tempData.Col5;
                base_weituokehuviewmodel.Col6 = tempData.Col6;
                base_weituokehuviewmodel.MakeDate = tempData.MakeDate;
                base_weituokehuviewmodel.MakeMan = tempData.MakeMan;
                base_weituokehuviewmodel.ShenchaSF = tempData.ShenchaSF;
                base_weituokehuviewmodel.HezuoSF = tempData.HezuoSF;
                base_weituokehuviewmodel.BeianBH = tempData.BeianBH;
                base_weituokehuviewmodel.BeianYXQ = tempData.BeianYXQ;
                base_weituokehuviewmodel.BeianPZRQ = tempData.BeianPZRQ;
                base_weituokehuviewmodel.BeianFZJG = tempData.BeianFZJG;
                base_weituokehuviewmodel.BeianTP = tempData.BeianTP;
                base_weituokehuviewmodel.XukePZRQ = tempData.XukePZRQ;
                base_weituokehuviewmodel.XukeFZJG = tempData.XukeFZJG;
                base_weituokehuviewmodel.ZhuceDZ = tempData.ZhuceDZ;
                base_weituokehuviewmodel.JingyinDZ = tempData.JingyinDZ;
                base_weituokehuviewmodel.KufangDZ = tempData.KufangDZ;
                base_weituokehuviewmodel.WeituoNR = tempData.WeituoNR;
                base_weituokehuviewmodel.WeituoKSRQ = tempData.WeituoKSRQ;
                base_weituokehuviewmodel.WeituoJSRQ = tempData.WeituoJSRQ;
                base_weituokehuviewmodel.WeituoQX = tempData.WeituoQX;
                base_weituokehuviewmodel.TieZWBQ = tempData.TieZWBQ;
                base_weituokehuviewmodel.HetongTP = tempData.HetongTP;
                base_weituokehuviewmodel.Faren = tempData.Faren;
                base_weituokehuviewmodel.Fuzeren = tempData.Fuzeren;
                base_weituokehuviewmodel.HeyiSF = tempData.HeyiSF;
                return View(base_weituokehuviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string daima = Request["daima"] ?? "";
            string jiancheng = Request["jiancheng"] ?? "";
            string kehumingcheng = Request["kehumingcheng"] ?? "";
            string hetongbianhao = Request["hetongbianhao"] ?? "";
            string yingyezhizhaobh = Request["yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["yingyezhizhaotp"] ?? "";
            string zuzhijigoubh = Request["zuzhijigoubh"] ?? "";
            string zuzhijigouyxq = Request["zuzhijigouyxq"] ?? "";
            string zuzhijigoutp = Request["zuzhijigoutp"] ?? "";
            string shuiwudengjibh = Request["shuiwudengjibh"] ?? "";
            string shuiwudengjiyxq = Request["shuiwudengjiyxq"] ?? "";
            string shuiwudengjitp = Request["shuiwudengjitp"] ?? "";
            string jingyingxukebh = Request["jingyingxukebh"] ?? "";
            string jingyingxukeyxq = Request["jingyingxukeyxq"] ?? "";
            string jingyingxuketp = Request["jingyingxuketp"] ?? "";
            string jingyingfanwei = Request["jingyingfanwei"] ?? "";
            string jingyingfanweidm = Request["jingyingfanweidm"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidianhua = Request["lianxidianhua"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string shouying = Request["shouying"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string col4 = Request["col4"] ?? "";
            string col5 = Request["col5"] ?? "";
            string col6 = Request["col6"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string shenchasf = Request["shenchasf"] ?? "";
            string hezuosf = Request["hezuosf"] ?? "";
            string beianbh = Request["beianbh"] ?? "";
            string beianyxq = Request["beianyxq"] ?? "";
            string beianpzrq = Request["beianpzrq"] ?? "";
            string beianfzjg = Request["beianfzjg"] ?? "";
            string beiantp = Request["beiantp"] ?? "";
            string xukepzrq = Request["xukepzrq"] ?? "";
            string xukefzjg = Request["xukefzjg"] ?? "";
            string zhucedz = Request["zhucedz"] ?? "";
            string jingyindz = Request["jingyindz"] ?? "";
            string kufangdz = Request["kufangdz"] ?? "";
            string weituonr = Request["weituonr"] ?? "";
            string weituoksrq = Request["weituoksrq"] ?? "";
            string weituojsrq = Request["weituojsrq"] ?? "";
            string weituoqx = Request["weituoqx"] ?? "";
            string tiezwbq = Request["tiezwbq"] ?? "";
            string hetongtp = Request["hetongtp"] ?? "";
            string faren = Request["faren"] ?? "";
            string fuzeren = Request["fuzeren"] ?? "";
            string heyisf = Request["heyisf"] ?? "";
            if (tiezwbq.IndexOf("true") > -1)
                tiezwbq = "true";
            else
                tiezwbq = "false";
            if (shenchasf.IndexOf("true") > -1)
                shenchasf = "true";
            if (hezuosf.IndexOf("true") > -1)
                hezuosf = "true";
            if (heyisf.IndexOf("true") > -1)
                heyisf = "true";
            int uid = int.Parse(id);
            try
            {
                base_weituokehu p = ob_base_weituokehuservice.GetEntityById(base_weituokehu => base_weituokehu.ID == uid);
                p.Daima = daima.Trim();
                p.Jiancheng = jiancheng.Trim();
                p.Kehumingcheng = kehumingcheng.Trim();
                p.Hetongbianhao = hetongbianhao.Trim();
                p.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                p.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                p.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                p.ZuzhijigouBH = zuzhijigoubh.Trim();
                p.ZuzhijigouYXQ = zuzhijigouyxq == "" ? DateTime.Now : DateTime.Parse(zuzhijigouyxq);
                p.ZuzhijigouTP = zuzhijigoutp.Trim();
                p.ShuiwudengjiBH = shuiwudengjibh.Trim();
                p.ShuiwudengjiYXQ = shuiwudengjiyxq == "" ? DateTime.Now : DateTime.Parse(shuiwudengjiyxq);
                p.ShuiwudengjiTP = shuiwudengjitp.Trim();
                p.JingyingxukeBH = jingyingxukebh.Trim();
                p.JingyingxukeYXQ = jingyingxukeyxq == "" ? DateTime.Now : DateTime.Parse(jingyingxukeyxq);
                p.JingyingxukeTP = jingyingxuketp.Trim();
                p.Jingyingfanwei = jingyingfanwei.Trim();
                p.JingyingfanweiDM = jingyingfanweidm.Trim();
                p.Lianxiren = lianxiren.Trim();
                p.Lianxidianhua = lianxidianhua.Trim();
                p.Beizhu = beizhu.Trim();
                p.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.Col4 = col4.Trim();
                p.Col5 = col5.Trim();
                p.Col6 = col6.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.ShenchaSF = shenchasf == "" ? false : Boolean.Parse(shenchasf);
                p.HezuoSF = hezuosf == "" ? false : Boolean.Parse(hezuosf);
                p.BeianBH = beianbh.Trim();
                p.BeianYXQ = beianyxq == "" ? DateTime.Now : DateTime.Parse(beianyxq);
                p.BeianPZRQ = beianpzrq == "" ? DateTime.Now : DateTime.Parse(beianpzrq);
                p.BeianFZJG = beianfzjg.Trim();
                p.BeianTP = beiantp.Trim();
                p.XukePZRQ = xukepzrq == "" ? DateTime.Now : DateTime.Parse(xukepzrq);
                p.XukeFZJG = xukefzjg.Trim();
                p.ZhuceDZ = zhucedz.Trim();
                p.JingyinDZ = jingyindz.Trim();
                p.KufangDZ = kufangdz.Trim();
                p.WeituoNR = weituonr.Trim();
                p.WeituoKSRQ = weituoksrq == "" ? DateTime.Now : DateTime.Parse(weituoksrq);
                p.WeituoJSRQ = weituojsrq == "" ? DateTime.Now : DateTime.Parse(weituojsrq);
                p.WeituoQX = weituoqx == "" ? 0 : int.Parse(weituoqx);
                p.TieZWBQ = tiezwbq == "" ? false : Boolean.Parse(tiezwbq);
                p.HetongTP = hetongtp.Trim();
                p.Faren = faren.Trim();
                p.Fuzeren = fuzeren.Trim();
                p.HeyiSF = heyisf == "" ? false : Boolean.Parse(heyisf);
                ob_base_weituokehuservice.UpdateEntity(p);
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
            base_weituokehu ob_base_weituokehu;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_weituokehu = ob_base_weituokehuservice.GetEntityById(base_weituokehu => base_weituokehu.ID == id && base_weituokehu.IsDelete == false);
                    ob_base_weituokehu.IsDelete = true;
                    ob_base_weituokehuservice.UpdateEntity(ob_base_weituokehu);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult YYZZOverdue(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            var period = Request["period"] ?? "";

            if (string.IsNullOrEmpty(period))
                period = "0";
            var tempData = ob_base_weituokehuservice.LoadEntities(p => p.IsDelete == false && p.YingyezhizhaoYXQ <= DateTime.Now.AddDays(int.Parse(period))).ToPagedList(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_weituokehu = tempData;
            ViewBag.period = period;
            return View("Overdue");
        }
        public ActionResult JYXKOverdue(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            var period = Request["period"] ?? "";

            if (string.IsNullOrEmpty(period))
                period = "0";
            var tempData = ob_base_weituokehuservice.LoadEntities(p => p.IsDelete == false && p.JingyingxukeYXQ <= DateTime.Now.AddDays(int.Parse(period))).ToPagedList(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_weituokehu = tempData;
            ViewBag.period = period;
            return View("Overdue");
        }
    }
}

