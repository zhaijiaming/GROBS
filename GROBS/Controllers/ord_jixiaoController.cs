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
            return RedirectToAction("Edit", new { id = uid });
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
    }
}

