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
    public class auth_gongsiController : Controller
    {
        private Iauth_gongsiService ob_auth_gongsiservice = ServiceFactory.auth_gongsiservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "auth_gongsi_index";
            Expression<Func<auth_gongsi, bool>> where = PredicateExtensionses.True<auth_gongsi>();
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
                                        where = where.And(auth_gongsi => auth_gongsi.Daima == daima);
                                    else
                                        where = where.Or(auth_gongsi => auth_gongsi.Daima == daima);
                                }
                                if (daimaequal.Equals("like"))
                                {
                                    if (daimaand.Equals("and"))
                                        where = where.And(auth_gongsi => auth_gongsi.Daima.Contains(daima));
                                    else
                                        where = where.Or(auth_gongsi => auth_gongsi.Daima.Contains(daima));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(auth_gongsi => auth_gongsi.IsDelete == false);

            var tempData = ob_auth_gongsiservice.LoadSortEntities(where.Compile(), false, auth_gongsi => auth_gongsi.ID).ToPagedList<auth_gongsi>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.auth_gongsi = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "auth_gongsi_index";
            string page = "1";
            string daima = Request["daima"] ?? "";
            string daimaequal = Request["daimaequal"] ?? "";
            string daimaand = Request["daimaand"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string mingchengequal = Request["mingchengequal"] ?? "";
            string mingchengand = Request["mingchengand"] ?? "";
            Expression<Func<auth_gongsi, bool>> where = PredicateExtensionses.True<auth_gongsi>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(auth_gongsi => auth_gongsi.Daima == daima);
                        else
                            where = where.Or(auth_gongsi => auth_gongsi.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(auth_gongsi => auth_gongsi.Daima.Contains(daima));
                        else
                            where = where.Or(auth_gongsi => auth_gongsi.Daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);

                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(auth_gongsi => auth_gongsi.Mingcheng == mingcheng);
                        else
                            where = where.Or(auth_gongsi => auth_gongsi.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(auth_gongsi => auth_gongsi.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(auth_gongsi => auth_gongsi.Mingcheng.Contains(mingcheng));
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
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(auth_gongsi => auth_gongsi.Daima == daima);
                        else
                            where = where.Or(auth_gongsi => auth_gongsi.Daima == daima);
                    }
                    if (daimaequal.Equals("like"))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(auth_gongsi => auth_gongsi.Daima.Contains(daima));
                        else
                            where = where.Or(auth_gongsi => auth_gongsi.Daima.Contains(daima));
                    }
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);

                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(auth_gongsi => auth_gongsi.Mingcheng == mingcheng);
                        else
                            where = where.Or(auth_gongsi => auth_gongsi.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(auth_gongsi => auth_gongsi.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(auth_gongsi => auth_gongsi.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(auth_gongsi => auth_gongsi.IsDelete == false);

            var tempData = ob_auth_gongsiservice.LoadSortEntities(where.Compile(), false, auth_gongsi => auth_gongsi.ID).ToPagedList<auth_gongsi>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.auth_gongsi = tempData;
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
            string jiancheng = Request["jiancheng"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string biaozhi = Request["biaozhi"] ?? "";
            string hetongbh = Request["hetongbh"] ?? "";
            string yxq = Request["yxq"] ?? "";
            string leixing = Request["leixing"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                auth_gongsi ob_auth_gongsi = new auth_gongsi();
                ob_auth_gongsi.Daima = daima.Trim();
                ob_auth_gongsi.Jiancheng = jiancheng.Trim();
                ob_auth_gongsi.Mingcheng = mingcheng.Trim();
                ob_auth_gongsi.Biaozhi = biaozhi.Trim();
                ob_auth_gongsi.HetongBH = hetongbh.Trim();
                ob_auth_gongsi.YXQ = yxq == "" ? DateTime.Now : DateTime.Parse(yxq);
                ob_auth_gongsi.Leixing = leixing.Trim();
                ob_auth_gongsi.Lianxiren = lianxiren.Trim();
                ob_auth_gongsi.LianxiDH = lianxidh.Trim();
                ob_auth_gongsi.Beizhu = beizhu.Trim();
                ob_auth_gongsi.Col1 = col1.Trim();
                ob_auth_gongsi.Col2 = col2.Trim();
                ob_auth_gongsi.Col3 = col3.Trim();
                ob_auth_gongsi.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_auth_gongsi.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_auth_gongsi = ob_auth_gongsiservice.AddEntity(ob_auth_gongsi);
                ViewBag.auth_gongsi = ob_auth_gongsi;
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
            auth_gongsi tempData = ob_auth_gongsiservice.GetEntityById(auth_gongsi => auth_gongsi.ID == id && auth_gongsi.IsDelete == false);
            ViewBag.auth_gongsi = tempData;
            if (tempData == null)
                return View();
            else
            {
                auth_gongsiViewModel auth_gongsiviewmodel = new auth_gongsiViewModel();
                auth_gongsiviewmodel.ID = tempData.ID;
                auth_gongsiviewmodel.Daima = tempData.Daima;
                auth_gongsiviewmodel.Jiancheng = tempData.Jiancheng;
                auth_gongsiviewmodel.Mingcheng = tempData.Mingcheng;
                auth_gongsiviewmodel.Biaozhi = tempData.Biaozhi;
                auth_gongsiviewmodel.HetongBH = tempData.HetongBH;
                auth_gongsiviewmodel.YXQ = tempData.YXQ;
                auth_gongsiviewmodel.Leixing = tempData.Leixing;
                auth_gongsiviewmodel.Lianxiren = tempData.Lianxiren;
                auth_gongsiviewmodel.LianxiDH = tempData.LianxiDH;
                auth_gongsiviewmodel.Beizhu = tempData.Beizhu;
                auth_gongsiviewmodel.Col1 = tempData.Col1;
                auth_gongsiviewmodel.Col2 = tempData.Col2;
                auth_gongsiviewmodel.Col3 = tempData.Col3;
                auth_gongsiviewmodel.MakeDate = tempData.MakeDate;
                auth_gongsiviewmodel.MakeMan = tempData.MakeMan;
                return View(auth_gongsiviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string daima = Request["daima"] ?? "";
            string jiancheng = Request["jiancheng"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string biaozhi = Request["biaozhi"] ?? "";
            string hetongbh = Request["hetongbh"] ?? "";
            string yxq = Request["yxq"] ?? "";
            string leixing = Request["leixing"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                auth_gongsi p = ob_auth_gongsiservice.GetEntityById(auth_gongsi => auth_gongsi.ID == uid);
                p.Daima = daima.Trim();
                p.Jiancheng = jiancheng.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.Biaozhi = biaozhi.Trim();
                p.HetongBH = hetongbh.Trim();
                p.YXQ = yxq == "" ? DateTime.Now : DateTime.Parse(yxq);
                p.Leixing = leixing.Trim();
                p.Lianxiren = lianxiren.Trim();
                p.LianxiDH = lianxidh.Trim();
                p.Beizhu = beizhu.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_auth_gongsiservice.UpdateEntity(p);
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
            auth_gongsi ob_auth_gongsi;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_auth_gongsi = ob_auth_gongsiservice.GetEntityById(auth_gongsi => auth_gongsi.ID == id && auth_gongsi.IsDelete == false);
                    ob_auth_gongsi.IsDelete = true;
                    ob_auth_gongsiservice.UpdateEntity(ob_auth_gongsi);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

