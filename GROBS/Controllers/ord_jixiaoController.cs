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
using System.Data;

namespace GROBS.Controllers
{
    public class ord_jixiaoController : Controller
    {
        private Iord_jixiaoService ob_ord_jixiaoservice = ServiceFactory.ord_jixiaoservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_jixiao_index";
            Expression<Func<ord_jixiao, bool>> where = PredicateExtensionses.True<ord_jixiao>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "khid":
                            string khid = scld[1];
                            string khidequal = scld[2];
                            string khidand = scld[3];
                            if (!string.IsNullOrEmpty(khid))
                            {
                                if (khidequal.Equals("="))
                                {
                                    if (khidand.Equals("and"))
                                        where = where.And(ord_jixiao => ord_jixiao.KHID == int.Parse(khid));
                                    else
                                        where = where.Or(ord_jixiao => ord_jixiao.KHID == int.Parse(khid));
                                }
                                if (khidequal.Equals(">"))
                                {
                                    if (khidand.Equals("and"))
                                        where = where.And(ord_jixiao => ord_jixiao.KHID > int.Parse(khid));
                                    else
                                        where = where.Or(ord_jixiao => ord_jixiao.KHID > int.Parse(khid));
                                }
                                if (khidequal.Equals("<"))
                                {
                                    if (khidand.Equals("and"))
                                        where = where.And(ord_jixiao => ord_jixiao.KHID < int.Parse(khid));
                                    else
                                        where = where.Or(ord_jixiao => ord_jixiao.KHID < int.Parse(khid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_jixiao => ord_jixiao.IsDelete == false);

            var tempData = ob_ord_jixiaoservice.LoadSortEntities(where.Compile(), false, ord_jixiao => ord_jixiao.ID).ToPagedList<ord_jixiao>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_jixiao = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_jixiao_index";
            string page = "1";
            string khid = Request["khid"] ?? "";
            string khidequal = Request["khidequal"] ?? "";
            string khidand = Request["khidand"] ?? "";
            Expression<Func<ord_jixiao, bool>> where = PredicateExtensionses.True<ord_jixiao>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(khid))
                {
                    if (khidequal.Equals("="))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.KHID == int.Parse(khid));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.KHID == int.Parse(khid));
                    }
                    if (khidequal.Equals(">"))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.KHID > int.Parse(khid));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.KHID > int.Parse(khid));
                    }
                    if (khidequal.Equals("<"))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.KHID < int.Parse(khid));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.KHID < int.Parse(khid));
                    }
                }
                if (!string.IsNullOrEmpty(khid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", khid, khidequal, khidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", "", khidequal, khidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(khid))
                {
                    if (khidequal.Equals("="))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.KHID == int.Parse(khid));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.KHID == int.Parse(khid));
                    }
                    if (khidequal.Equals(">"))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.KHID > int.Parse(khid));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.KHID > int.Parse(khid));
                    }
                    if (khidequal.Equals("<"))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.KHID < int.Parse(khid));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.KHID < int.Parse(khid));
                    }
                }
                if (!string.IsNullOrEmpty(khid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", khid, khidequal, khidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", "", khidequal, khidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_jixiao => ord_jixiao.IsDelete == false);

            var tempData = ob_ord_jixiaoservice.LoadSortEntities(where.Compile(), false, ord_jixiao => ord_jixiao.ID).ToPagedList<ord_jixiao>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_jixiao = tempData;
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
            string khid = Request["khid"] ?? "";
            string niandu = Request["niandu"] ?? "";
            string yuefen = Request["yuefen"] ?? "";
            string zhibiao = Request["zhibiao"] ?? "";
            string yeji = Request["yeji"] ?? "";
            string dachenglv = Request["dachenglv"] ?? "";
            string flsqje = Request["flsqje"] ?? "";
            string flffje = Request["flffje"] ?? "";
            string fafangsf = Request["fafangsf"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                ord_jixiao ob_ord_jixiao = new ord_jixiao();
                ob_ord_jixiao.KHID = khid == "" ? 0 : int.Parse(khid);
                ob_ord_jixiao.Niandu = niandu == "" ? 0 : int.Parse(niandu);
                ob_ord_jixiao.Yuefen = yuefen == "" ? 0 : int.Parse(yuefen);
                ob_ord_jixiao.Zhibiao = zhibiao == "" ? 0 : float.Parse(zhibiao);
                ob_ord_jixiao.Yeji = yeji == "" ? 0 : float.Parse(yeji);
                ob_ord_jixiao.Dachenglv = dachenglv == "" ? 0 : float.Parse(dachenglv);
                ob_ord_jixiao.FLSQJE = flsqje == "" ? 0 : float.Parse(flsqje);
                ob_ord_jixiao.FLFFJE = flffje == "" ? 0 : float.Parse(flffje);
                ob_ord_jixiao.FafangSF = fafangsf == "" ? false : Boolean.Parse(fafangsf);
                ob_ord_jixiao.Col1 = col1.Trim();
                ob_ord_jixiao.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_jixiao.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_jixiao = ob_ord_jixiaoservice.AddEntity(ob_ord_jixiao);
                ViewBag.ord_jixiao = ob_ord_jixiao;
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
            ord_jixiao tempData = ob_ord_jixiaoservice.GetEntityById(ord_jixiao => ord_jixiao.ID == id && ord_jixiao.IsDelete == false);
            ViewBag.ord_jixiao = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_jixiaoViewModel ord_jixiaoviewmodel = new ord_jixiaoViewModel();
                ord_jixiaoviewmodel.ID = tempData.ID;
                ord_jixiaoviewmodel.KHID = tempData.KHID;
                ord_jixiaoviewmodel.Niandu = tempData.Niandu;
                ord_jixiaoviewmodel.Yuefen = tempData.Yuefen;
                ord_jixiaoviewmodel.Zhibiao = tempData.Zhibiao;
                ord_jixiaoviewmodel.Yeji = tempData.Yeji;
                ord_jixiaoviewmodel.Dachenglv = tempData.Dachenglv;
                ord_jixiaoviewmodel.FLSQJE = tempData.FLSQJE;
                ord_jixiaoviewmodel.FLFFJE = tempData.FLFFJE;
                ord_jixiaoviewmodel.FafangSF = tempData.FafangSF;
                ord_jixiaoviewmodel.Col1 = tempData.Col1;
                ord_jixiaoviewmodel.MakeDate = tempData.MakeDate;
                ord_jixiaoviewmodel.MakeMan = tempData.MakeMan;
                return View(ord_jixiaoviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string khid = Request["khid"] ?? "";
            string niandu = Request["niandu"] ?? "";
            string yuefen = Request["yuefen"] ?? "";
            string zhibiao = Request["zhibiao"] ?? "";
            string yeji = Request["yeji"] ?? "";
            string dachenglv = Request["dachenglv"] ?? "";
            string flsqje = Request["flsqje"] ?? "";
            string flffje = Request["flffje"] ?? "";
            string fafangsf = Request["fafangsf"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                ord_jixiao p = ob_ord_jixiaoservice.GetEntityById(ord_jixiao => ord_jixiao.ID == uid);
                p.KHID = khid == "" ? 0 : int.Parse(khid);
                p.Niandu = niandu == "" ? 0 : int.Parse(niandu);
                p.Yuefen = yuefen == "" ? 0 : int.Parse(yuefen);
                p.Zhibiao = zhibiao == "" ? 0 : float.Parse(zhibiao);
                p.Yeji = yeji == "" ? 0 : float.Parse(yeji);
                p.Dachenglv = dachenglv == "" ? 0 : float.Parse(dachenglv);
                p.FLSQJE = flsqje == "" ? 0 : float.Parse(flsqje);
                p.FLFFJE = flffje == "" ? 0 : float.Parse(flffje);
                p.FafangSF = fafangsf == "" ? false : Boolean.Parse(fafangsf);
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_jixiaoservice.UpdateEntity(p);
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
            ord_jixiao ob_ord_jixiao;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_jixiao = ob_ord_jixiaoservice.GetEntityById(ord_jixiao => ord_jixiao.ID == id && ord_jixiao.IsDelete == false);
                    ob_ord_jixiao.IsDelete = true;
                    ob_ord_jixiaoservice.UpdateEntity(ob_ord_jixiao);
                }
            }
            return RedirectToAction("Index");
        }
        [OutputCache(Duration = 30)]
        public ActionResult CustomerTarget(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            var _khid = (int)Session["customer_id"];
            ViewBag.khid = _khid;
            int userid = (int)Session["user_id"];
            string pagetag = "ord_jixiao_customertarget";
            Expression<Func<ord_jixiao, bool>> where = PredicateExtensionses.True<ord_jixiao>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "khid":
                            string khid = scld[1];
                            string khidequal = scld[2];
                            string khidand = string.IsNullOrEmpty(scld[3]) ? "and" : scld[3];
                            if (!string.IsNullOrEmpty(khid))
                            {
                                if (khidequal.Equals("="))
                                {
                                    if (khidand.Equals("and"))
                                        where = where.And(ord_jixiao => ord_jixiao.KHID == int.Parse(khid));
                                    else
                                        where = where.Or(ord_jixiao => ord_jixiao.KHID == int.Parse(khid));
                                }
                                if (khidequal.Equals(">"))
                                {
                                    if (khidand.Equals("and"))
                                        where = where.And(ord_jixiao => ord_jixiao.KHID > int.Parse(khid));
                                    else
                                        where = where.Or(ord_jixiao => ord_jixiao.KHID > int.Parse(khid));
                                }
                                if (khidequal.Equals("<"))
                                {
                                    if (khidand.Equals("and"))
                                        where = where.And(ord_jixiao => ord_jixiao.KHID < int.Parse(khid));
                                    else
                                        where = where.Or(ord_jixiao => ord_jixiao.KHID < int.Parse(khid));
                                }
                            }
                            break;
                        case "Niandu":
                            string Niandu = scld[1];
                            string Nianduequal = scld[2];
                            string Nianduand = string.IsNullOrEmpty(scld[3]) ? "and" : scld[3];
                            if (!string.IsNullOrEmpty(Niandu))
                            {
                                if (Nianduequal.Equals("="))
                                {
                                    if (Nianduand.Equals("and"))
                                        where = where.And(ord_jixiao => ord_jixiao.Niandu == int.Parse(Niandu));
                                    else
                                        where = where.Or(ord_jixiao => ord_jixiao.Niandu == int.Parse(Niandu));
                                }
                            }
                            break;
                        case "Yuefen":
                            string Yuefen = scld[1];
                            string Yuefenequal = scld[2];
                            string Yuefenand = string.IsNullOrEmpty(scld[3]) ? "and" : scld[3];
                            if (!string.IsNullOrEmpty(Yuefen))
                            {
                                if (Yuefenequal.Equals("="))
                                {
                                    if (Yuefenand.Equals("and"))
                                        where = where.And(ord_jixiao => ord_jixiao.Yuefen == int.Parse(Yuefen));
                                    else
                                        where = where.Or(ord_jixiao => ord_jixiao.Yuefen == int.Parse(Yuefen));
                                }
                            }
                            break;
                        case "FafangSF":
                            string FafangSF = scld[1];
                            string FafangSFequal = scld[2];
                            string FafangSFand = string.IsNullOrEmpty(scld[3]) ? "and" : scld[3];
                            if (!string.IsNullOrEmpty(FafangSF))
                            {
                                if (FafangSFequal.Equals("="))
                                {
                                    if (FafangSFand.Equals("and"))
                                        where = where.And(ord_jixiao => ord_jixiao.FafangSF == Boolean.Parse(FafangSF == "yes" ? "True" : "False"));
                                    else
                                        where = where.Or(ord_jixiao => ord_jixiao.FafangSF == Boolean.Parse(FafangSF == "yes" ? "True" : "False"));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_jixiao => ord_jixiao.IsDelete == false);
            where = where.And(p => p.KHID == _khid);

            var tempData = ob_ord_jixiaoservice.LoadSortEntities(where.Compile(), true, ord_jixiao => ord_jixiao.Niandu).ToPagedList<ord_jixiao>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_jixiao = tempData;
            return View(tempData);
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult CustomerTarget()
        {
            int userid = (int)Session["user_id"];
            var _khid = (int)Session["customer_id"];
            ViewBag.khid = _khid;
            string pagetag = "ord_jixiao_customertarget";
            string page = "1";

            string khid = Request["khid"] ?? "";
            string khidequal = Request["khidequal"] ?? "";
            string khidand = Request["khidand"] ?? "";

            string Niandu = Request["Niandu"] ?? "";
            string Nianduequal = Request["Nianduequal"] ?? "";
            string Nianduand = Request["Nianduand"] ?? "and";

            string Yuefen = Request["Yuefen"] ?? "";
            string Yuefenequal = Request["Yuefenequal"] ?? "";
            string Yuefenand = Request["Yuefenand"] ?? "and";

            string FafangSF = Request["FafangSF"] ?? "";
            string FafangSFequal = Request["FafangSFequal"] ?? "";
            string FafangSFand = Request["FafangSFand"] ?? "and";

            Expression<Func<ord_jixiao, bool>> where = PredicateExtensionses.True<ord_jixiao>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(khid))
                {
                    if (khidequal.Equals("="))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.KHID == int.Parse(khid));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.KHID == int.Parse(khid));
                    }
                    if (khidequal.Equals(">"))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.KHID > int.Parse(khid));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.KHID > int.Parse(khid));
                    }
                    if (khidequal.Equals("<"))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.KHID < int.Parse(khid));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.KHID < int.Parse(khid));
                    }
                }
                if (!string.IsNullOrEmpty(khid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", khid, khidequal, khidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", "", khidequal, khidand);

                if (!string.IsNullOrEmpty(Niandu))
                {
                    if (Nianduequal.Equals("="))
                    {
                        if (Nianduand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.Niandu == int.Parse(Niandu));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.Niandu == int.Parse(Niandu));
                    }
                }
                if (!string.IsNullOrEmpty(Niandu))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Niandu", Niandu, Nianduequal, Nianduand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Niandu", "", Nianduequal, Nianduand);


                if (!string.IsNullOrEmpty(Yuefen))
                {
                    if (Yuefenequal.Equals("="))
                    {
                        if (Yuefenand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.Yuefen == int.Parse(Yuefen));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.Yuefen == int.Parse(Yuefen));
                    }
                }
                if (!string.IsNullOrEmpty(Yuefen))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Yuefen", Yuefen, Yuefenequal, Yuefenand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Yuefen", "", Yuefenequal, Yuefenand);

                if (!string.IsNullOrEmpty(FafangSF))
                {
                    if (FafangSFequal.Equals("="))
                    {
                        if (FafangSFand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.FafangSF == Boolean.Parse(FafangSF == "yes" ? "True" : "False"));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.FafangSF == Boolean.Parse(FafangSF == "yes" ? "True" : "False"));
                    }
                }
                if (!string.IsNullOrEmpty(FafangSF))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "FafangSF", FafangSF, FafangSFequal, FafangSFand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "FafangSF", "", FafangSFequal, FafangSFand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(khid))
                {
                    if (khidequal.Equals("="))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.KHID == int.Parse(khid));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.KHID == int.Parse(khid));
                    }
                    if (khidequal.Equals(">"))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.KHID > int.Parse(khid));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.KHID > int.Parse(khid));
                    }
                    if (khidequal.Equals("<"))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.KHID < int.Parse(khid));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.KHID < int.Parse(khid));
                    }
                }
                if (!string.IsNullOrEmpty(khid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", khid, khidequal, khidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", "", khidequal, khidand);
                if (!string.IsNullOrEmpty(Niandu))
                {
                    if (Nianduequal.Equals("="))
                    {
                        if (Nianduand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.Niandu == int.Parse(Niandu));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.Niandu == int.Parse(Niandu));
                    }
                }
                if (!string.IsNullOrEmpty(Niandu))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Niandu", Niandu, Nianduequal, Nianduand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Niandu", "", Nianduequal, Nianduand);


                if (!string.IsNullOrEmpty(Yuefen))
                {
                    if (Yuefenequal.Equals("="))
                    {
                        if (Yuefenand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.Yuefen == int.Parse(Yuefen));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.Yuefen == int.Parse(Yuefen));
                    }
                }
                if (!string.IsNullOrEmpty(Yuefen))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Yuefen", Yuefen, Yuefenequal, Yuefenand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Yuefen", "", Yuefenequal, Yuefenand);

                if (!string.IsNullOrEmpty(FafangSF))
                {
                    if (FafangSFequal.Equals("="))
                    {
                        if (FafangSFand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.FafangSF == Boolean.Parse(FafangSF == "yes" ? "True" : "False"));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.FafangSF == Boolean.Parse(FafangSF == "yes" ? "True" : "False"));
                    }
                }
                if (!string.IsNullOrEmpty(FafangSF))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "FafangSF", FafangSF, FafangSFequal, FafangSFand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "FafangSF", "", FafangSFequal, FafangSFand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_jixiao => ord_jixiao.IsDelete == false);
            where = where.And(p => p.KHID == _khid);

            var tempData = ob_ord_jixiaoservice.LoadSortEntities(where.Compile(), true, ord_jixiao => ord_jixiao.Niandu).ToPagedList<ord_jixiao>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_jixiao = tempData;
            return View(tempData);
        }

        public ActionResult ExportCustomerTarget()
        {
            var _khid = (int)Session["customer_id"];

            int userid = (int)Session["user_id"];
            string pagetag = "ord_jixiao_customertarget";
            Expression<Func<ord_jixiao, bool>> where = PredicateExtensionses.True<ord_jixiao>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "khid":
                            string khid = scld[1];
                            string khidequal = scld[2];
                            string khidand = scld[3];
                            if (!string.IsNullOrEmpty(khid))
                            {
                                if (khidequal.Equals("="))
                                {
                                    if (khidand.Equals("and"))
                                        where = where.And(ord_jixiao => ord_jixiao.KHID == int.Parse(khid));
                                    else
                                        where = where.Or(ord_jixiao => ord_jixiao.KHID == int.Parse(khid));
                                }
                                if (khidequal.Equals(">"))
                                {
                                    if (khidand.Equals("and"))
                                        where = where.And(ord_jixiao => ord_jixiao.KHID > int.Parse(khid));
                                    else
                                        where = where.Or(ord_jixiao => ord_jixiao.KHID > int.Parse(khid));
                                }
                                if (khidequal.Equals("<"))
                                {
                                    if (khidand.Equals("and"))
                                        where = where.And(ord_jixiao => ord_jixiao.KHID < int.Parse(khid));
                                    else
                                        where = where.Or(ord_jixiao => ord_jixiao.KHID < int.Parse(khid));
                                }
                            }
                            break;
                        case "Niandu":
                            string Niandu = scld[1];
                            string Nianduequal = scld[2];
                            string Nianduand = scld[3];
                            if (!string.IsNullOrEmpty(Niandu))
                            {
                                if (Nianduequal.Equals("="))
                                {
                                    if (Nianduand.Equals("and"))
                                        where = where.And(ord_jixiao => ord_jixiao.Niandu == int.Parse(Niandu));
                                    else
                                        where = where.Or(ord_jixiao => ord_jixiao.Niandu == int.Parse(Niandu));
                                }
                            }
                            break;
                        case "Yuefen":
                            string Yuefen = scld[1];
                            string Yuefenequal = scld[2];
                            string Yuefenand = scld[3];
                            if (!string.IsNullOrEmpty(Yuefen))
                            {
                                if (Yuefenequal.Equals("="))
                                {
                                    if (Yuefenand.Equals("and"))
                                        where = where.And(ord_jixiao => ord_jixiao.Yuefen == int.Parse(Yuefen));
                                    else
                                        where = where.Or(ord_jixiao => ord_jixiao.Yuefen == int.Parse(Yuefen));
                                }
                            }
                            break;
                        case "FafangSF":
                            string FafangSF = scld[1];
                            string FafangSFequal = scld[2];
                            string FafangSFand = scld[3];
                            if (!string.IsNullOrEmpty(FafangSF))
                            {
                                if (FafangSFequal.Equals("="))
                                {
                                    if (FafangSFand.Equals("and"))
                                        where = where.And(ord_jixiao => ord_jixiao.FafangSF == Boolean.Parse(FafangSF == "yes" ? "True" : "False"));
                                    else
                                        where = where.Or(ord_jixiao => ord_jixiao.FafangSF == Boolean.Parse(FafangSF == "yes" ? "True" : "False"));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }
            where = where.And(ord_jixiao => ord_jixiao.IsDelete == false);
            where = where.And(p => p.KHID == _khid);

            var tempData = ob_ord_jixiaoservice.LoadSortEntities(where.Compile(), true, ord_jixiao => ord_jixiao.Niandu).ToList<ord_jixiao>();

            DataTable dt = new DataTable();
            dt.Columns.Add("Niandu", typeof(string));
            dt.Columns.Add("Yuefen", typeof(string));
            dt.Columns.Add("Zhibiao", typeof(string));
            dt.Columns.Add("Yeji", typeof(string));
            dt.Columns.Add("Dachenglv", typeof(string));
            dt.Columns.Add("FLSQJE", typeof(string));
            dt.Columns.Add("FLFFJE", typeof(string));
            dt.Columns.Add("FafangSF", typeof(string));
            foreach (var item in tempData)
            {
                DataRow row = dt.NewRow();
                row["Niandu"] = item.Niandu;
                row["Yuefen"] = item.Yuefen;
                row["Zhibiao"] = item.Zhibiao;
                row["Yeji"] = item.Yeji;
                row["Dachenglv"] = item.Dachenglv;
                row["FLSQJE"] = item.FLFFJE;
                row["FLFFJE"] = item.FLFFJE;
                row["FafangSF"] = item.FafangSF;
                dt.Rows.Add(row);
            }
            DataSet ds = new DataSet();
            dt.TableName = "CustomerTarget";
            ds.Tables.Add(dt);
            ExcelHelper.ExportExcel(ds, "CustomerTarget");
            return new EmptyResult();
        }

        public ActionResult CustomerTargetNow()
        {
            string thisYear = DateTime.Now.Year.ToString();
            var khid = (int)Session["customer_id"];
            ViewBag.khid = khid;
            ViewBag.thisYear = thisYear;
            int userid = (int)Session["user_id"];

            string pagetag = "ord_jixiao_customertargetnow";

            string Yuefen = Request["Yuefen"] ?? "";
            string Yuefenequal = Request["Yuefenequal"] ?? "";
            string Yuefenand = Request["Yuefenand"] ?? "and";

            string FafangSF = Request["FafangSF"] ?? "";
            string FafangSFequal = Request["FafangSFequal"] ?? "=";
            string FafangSFand = Request["FafangSFand"] ?? "and";

            Expression<Func<ord_jixiao, bool>> where = PredicateExtensionses.True<ord_jixiao>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(Yuefen))
                {
                    if (Yuefenequal.Equals("="))
                    {
                        if (Yuefenand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.Yuefen == int.Parse(Yuefen));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.Yuefen == int.Parse(Yuefen));
                    }
                }
                if (!string.IsNullOrEmpty(Yuefen))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Yuefen", Yuefen, Yuefenequal, Yuefenand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Yuefen", "", Yuefenequal, Yuefenand);

                if (!string.IsNullOrEmpty(FafangSF))
                {
                    if (FafangSFequal.Equals("="))
                    {
                        if (FafangSFand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.FafangSF == Boolean.Parse(FafangSF == "yes" ? "True" : "False"));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.FafangSF == Boolean.Parse(FafangSF == "yes" ? "True" : "False"));
                    }
                }
                if (!string.IsNullOrEmpty(FafangSF))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "FafangSF", FafangSF, FafangSFequal, FafangSFand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "FafangSF", "", FafangSFequal, FafangSFand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(Yuefen))
                {
                    if (Yuefenequal.Equals("="))
                    {
                        if (Yuefenand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.Yuefen == int.Parse(Yuefen));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.Yuefen == int.Parse(Yuefen));
                    }
                }
                if (!string.IsNullOrEmpty(Yuefen))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Yuefen", Yuefen, Yuefenequal, Yuefenand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Yuefen", "", Yuefenequal, Yuefenand);

                if (!string.IsNullOrEmpty(FafangSF))
                {
                    if (FafangSFequal.Equals("="))
                    {
                        if (FafangSFand.Equals("and"))
                            where = where.And(ord_jixiao => ord_jixiao.FafangSF == Boolean.Parse(FafangSF == "yes" ? "True" : "False"));
                        else
                            where = where.Or(ord_jixiao => ord_jixiao.FafangSF == Boolean.Parse(FafangSF == "yes" ? "True" : "False"));
                    }
                }
                if (!string.IsNullOrEmpty(FafangSF))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "FafangSF", FafangSF, FafangSFequal, FafangSFand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "FafangSF", "", FafangSFequal, FafangSFand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;

            where = where.And(p => p.Niandu == int.Parse(thisYear));
            where = where.And(p => p.KHID == khid);
            where = where.And(p => p.IsDelete == false);

            var tempdata = ServiceFactory.ord_jixiaoservice.LoadSortEntities(where.Compile(), true, p => p.Yuefen).ToList<ord_jixiao>();
            ViewBag.thisYearDate = tempdata;
            return View();
        }

        public ActionResult customerTargetNowExport()
        {
            string thisYear = DateTime.Now.Year.ToString();
            var khid = (int)Session["customer_id"];
            int userid = (int)Session["user_id"];
            string pagetag = "ord_jixiao_customertargetnow";

            Expression<Func<ord_jixiao, bool>> where = PredicateExtensionses.True<ord_jixiao>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "Yuefen":
                            string Yuefen = scld[1];
                            string Yuefenequal = scld[2];
                            string Yuefenand = scld[3];
                            if (!string.IsNullOrEmpty(Yuefen))
                            {
                                if (Yuefenequal.Equals("="))
                                {
                                    if (Yuefenand.Equals("and"))
                                        where = where.And(ord_jixiao => ord_jixiao.Yuefen == int.Parse(Yuefen));
                                    else
                                        where = where.Or(ord_jixiao => ord_jixiao.Yuefen == int.Parse(Yuefen));
                                }
                            }

                            break;
                        case "FafangSF":
                            string FafangSF = scld[1];
                            string FafangSFequal = scld[2];
                            string FafangSFand = scld[3];
                            if (!string.IsNullOrEmpty(FafangSF))
                            {
                                if (FafangSFequal.Equals("="))
                                {
                                    if (FafangSFand.Equals("and"))
                                        where = where.And(ord_jixiao => ord_jixiao.FafangSF == Boolean.Parse(FafangSF == "yes" ? "True" : "False"));
                                    else
                                        where = where.Or(ord_jixiao => ord_jixiao.FafangSF == Boolean.Parse(FafangSF == "yes" ? "True" : "False"));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            ViewBag.SearchCondition = sc.ConditionInfo;

            where = where.And(p => p.Niandu == int.Parse(thisYear));
            where = where.And(p => p.KHID == khid);
            where = where.And(p => p.IsDelete == false);

            var tempdata = ServiceFactory.ord_jixiaoservice.LoadSortEntities(where.Compile(), true, p => p.Yuefen).ToList<ord_jixiao>();

            DataTable dt = new DataTable();
            dt.Columns.Add("Yuefen", typeof(string));
            dt.Columns.Add("Zhibiao", typeof(string));
            dt.Columns.Add("Yeji", typeof(string));
            dt.Columns.Add("Dachenglv", typeof(string));
            dt.Columns.Add("FLSQJE", typeof(string));
            dt.Columns.Add("FLFFJE", typeof(string));
            dt.Columns.Add("FafangSF", typeof(string));
            foreach (var item in tempdata)
            {
                DataRow row = dt.NewRow();
                row["Yuefen"] = item.Yuefen;
                row["Zhibiao"] = item.Zhibiao;
                row["Yeji"] = item.Yeji;
                row["Dachenglv"] = item.Dachenglv;
                row["FLSQJE"] = item.FLSQJE;
                row["FLFFJE"] = item.FLFFJE;
                row["FafangSF"] = item.FafangSF;
                dt.Rows.Add(row);
            }
            DataSet ds = new DataSet();
            dt.TableName = "CustomerTargetNow";
            ds.Tables.Add(dt);
            ExcelHelper.ExportExcel(ds, "CustomerTargetNow");
            return new EmptyResult();
        }
    }
}

