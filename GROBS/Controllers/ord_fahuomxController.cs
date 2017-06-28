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
    public class ord_fahuomxController : Controller
    {
        private Iord_fahuomxService ob_ord_fahuomxservice = ServiceFactory.ord_fahuomxservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_fahuomx_index";
            Expression<Func<ord_fahuomx, bool>> where = PredicateExtensionses.True<ord_fahuomx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "chukuid":
                            string chukuid = scld[1];
                            string chukuidequal = scld[2];
                            string chukuidand = scld[3];
                            if (!string.IsNullOrEmpty(chukuid))
                            {
                                if (chukuidequal.Equals("="))
                                {
                                    if (chukuidand.Equals("and"))
                                        where = where.And(ord_fahuomx => ord_fahuomx.ChukuID == int.Parse(chukuid));
                                    else
                                        where = where.Or(ord_fahuomx => ord_fahuomx.ChukuID == int.Parse(chukuid));
                                }
                                if (chukuidequal.Equals(">"))
                                {
                                    if (chukuidand.Equals("and"))
                                        where = where.And(ord_fahuomx => ord_fahuomx.ChukuID > int.Parse(chukuid));
                                    else
                                        where = where.Or(ord_fahuomx => ord_fahuomx.ChukuID > int.Parse(chukuid));
                                }
                                if (chukuidequal.Equals("<"))
                                {
                                    if (chukuidand.Equals("and"))
                                        where = where.And(ord_fahuomx => ord_fahuomx.ChukuID < int.Parse(chukuid));
                                    else
                                        where = where.Or(ord_fahuomx => ord_fahuomx.ChukuID < int.Parse(chukuid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_fahuomx => ord_fahuomx.IsDelete == false);

            var tempData = ob_ord_fahuomxservice.LoadSortEntities(where.Compile(), false, ord_fahuomx => ord_fahuomx.ID).ToPagedList<ord_fahuomx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_fahuomx = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_fahuomx_index";
            string page = "1";
            string chukuid = Request["chukuid"] ?? "";
            string chukuidequal = Request["chukuidequal"] ?? "";
            string chukuidand = Request["chukuidand"] ?? "";
            Expression<Func<ord_fahuomx, bool>> where = PredicateExtensionses.True<ord_fahuomx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(chukuid))
                {
                    if (chukuidequal.Equals("="))
                    {
                        if (chukuidand.Equals("and"))
                            where = where.And(ord_fahuomx => ord_fahuomx.ChukuID == int.Parse(chukuid));
                        else
                            where = where.Or(ord_fahuomx => ord_fahuomx.ChukuID == int.Parse(chukuid));
                    }
                    if (chukuidequal.Equals(">"))
                    {
                        if (chukuidand.Equals("and"))
                            where = where.And(ord_fahuomx => ord_fahuomx.ChukuID > int.Parse(chukuid));
                        else
                            where = where.Or(ord_fahuomx => ord_fahuomx.ChukuID > int.Parse(chukuid));
                    }
                    if (chukuidequal.Equals("<"))
                    {
                        if (chukuidand.Equals("and"))
                            where = where.And(ord_fahuomx => ord_fahuomx.ChukuID < int.Parse(chukuid));
                        else
                            where = where.Or(ord_fahuomx => ord_fahuomx.ChukuID < int.Parse(chukuid));
                    }
                }
                if (!string.IsNullOrEmpty(chukuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukuid", chukuid, chukuidequal, chukuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukuid", "", chukuidequal, chukuidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(chukuid))
                {
                    if (chukuidequal.Equals("="))
                    {
                        if (chukuidand.Equals("and"))
                            where = where.And(ord_fahuomx => ord_fahuomx.ChukuID == int.Parse(chukuid));
                        else
                            where = where.Or(ord_fahuomx => ord_fahuomx.ChukuID == int.Parse(chukuid));
                    }
                    if (chukuidequal.Equals(">"))
                    {
                        if (chukuidand.Equals("and"))
                            where = where.And(ord_fahuomx => ord_fahuomx.ChukuID > int.Parse(chukuid));
                        else
                            where = where.Or(ord_fahuomx => ord_fahuomx.ChukuID > int.Parse(chukuid));
                    }
                    if (chukuidequal.Equals("<"))
                    {
                        if (chukuidand.Equals("and"))
                            where = where.And(ord_fahuomx => ord_fahuomx.ChukuID < int.Parse(chukuid));
                        else
                            where = where.Or(ord_fahuomx => ord_fahuomx.ChukuID < int.Parse(chukuid));
                    }
                }
                if (!string.IsNullOrEmpty(chukuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukuid", chukuid, chukuidequal, chukuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukuid", "", chukuidequal, chukuidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_fahuomx => ord_fahuomx.IsDelete == false);

            var tempData = ob_ord_fahuomxservice.LoadSortEntities(where.Compile(), false, ord_fahuomx => ord_fahuomx.ID).ToPagedList<ord_fahuomx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_fahuomx = tempData;
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
            string chukuid = Request["chukuid"] ?? "";
            string shangpindm = Request["shangpindm"] ?? "";
            string shangpinmc = Request["shangpinmc"] ?? "";
            string zhucezheng = Request["zhucezheng"] ?? "";
            string guige = Request["guige"] ?? "";
            string pihao = Request["pihao"] ?? "";
            string xuliehao = Request["xuliehao"] ?? "";
            string shengchanrq = Request["shengchanrq"] ?? "";
            string shixiaorq = Request["shixiaorq"] ?? "";
            string chukusl = Request["chukusl"] ?? "";
            string danwei = Request["danwei"] ?? "";
            string huansuanlv = Request["huansuanlv"] ?? "";
            string taobaohao = Request["taobaohao"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                ord_fahuomx ob_ord_fahuomx = new ord_fahuomx();
                ob_ord_fahuomx.ChukuID = chukuid == "" ? 0 : int.Parse(chukuid);
                ob_ord_fahuomx.ShangpinDM = shangpindm.Trim();
                ob_ord_fahuomx.ShangpinMC = shangpinmc.Trim();
                ob_ord_fahuomx.Zhucezheng = zhucezheng.Trim();
                ob_ord_fahuomx.Guige = guige.Trim();
                ob_ord_fahuomx.Pihao = pihao.Trim();
                ob_ord_fahuomx.Xuliehao = xuliehao.Trim();
                ob_ord_fahuomx.ShengchanRQ = shengchanrq == "" ? DateTime.Now : DateTime.Parse(shengchanrq);
                ob_ord_fahuomx.ShixiaoRQ = shixiaorq == "" ? DateTime.Now : DateTime.Parse(shixiaorq);
                ob_ord_fahuomx.ChukuSL = chukusl == "" ? 0 : float.Parse(chukusl);
                ob_ord_fahuomx.Danwei = danwei.Trim();
                ob_ord_fahuomx.Huansuanlv = huansuanlv == "" ? 0 : float.Parse(huansuanlv);
                ob_ord_fahuomx.Taobaohao = taobaohao.Trim();
                ob_ord_fahuomx.Beizhu = beizhu.Trim();
                ob_ord_fahuomx.Col1 = col1.Trim();
                ob_ord_fahuomx.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_fahuomx.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_fahuomx = ob_ord_fahuomxservice.AddEntity(ob_ord_fahuomx);
                ViewBag.ord_fahuomx = ob_ord_fahuomx;
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
            ord_fahuomx tempData = ob_ord_fahuomxservice.GetEntityById(ord_fahuomx => ord_fahuomx.ID == id && ord_fahuomx.IsDelete == false);
            ViewBag.ord_fahuomx = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_fahuomxViewModel ord_fahuomxviewmodel = new ord_fahuomxViewModel();
                ord_fahuomxviewmodel.ID = tempData.ID;
                ord_fahuomxviewmodel.ChukuID = tempData.ChukuID;
                ord_fahuomxviewmodel.ShangpinDM = tempData.ShangpinDM;
                ord_fahuomxviewmodel.ShangpinMC = tempData.ShangpinMC;
                ord_fahuomxviewmodel.Zhucezheng = tempData.Zhucezheng;
                ord_fahuomxviewmodel.Guige = tempData.Guige;
                ord_fahuomxviewmodel.Pihao = tempData.Pihao;
                ord_fahuomxviewmodel.Xuliehao = tempData.Xuliehao;
                ord_fahuomxviewmodel.ShengchanRQ = tempData.ShengchanRQ;
                ord_fahuomxviewmodel.ShixiaoRQ = tempData.ShixiaoRQ;
                ord_fahuomxviewmodel.ChukuSL = tempData.ChukuSL;
                ord_fahuomxviewmodel.Danwei = tempData.Danwei;
                ord_fahuomxviewmodel.Huansuanlv = tempData.Huansuanlv;
                ord_fahuomxviewmodel.Taobaohao = tempData.Taobaohao;
                ord_fahuomxviewmodel.Beizhu = tempData.Beizhu;
                ord_fahuomxviewmodel.Col1 = tempData.Col1;
                ord_fahuomxviewmodel.MakeDate = tempData.MakeDate;
                ord_fahuomxviewmodel.MakeMan = tempData.MakeMan;
                return View(ord_fahuomxviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string chukuid = Request["chukuid"] ?? "";
            string shangpindm = Request["shangpindm"] ?? "";
            string shangpinmc = Request["shangpinmc"] ?? "";
            string zhucezheng = Request["zhucezheng"] ?? "";
            string guige = Request["guige"] ?? "";
            string pihao = Request["pihao"] ?? "";
            string xuliehao = Request["xuliehao"] ?? "";
            string shengchanrq = Request["shengchanrq"] ?? "";
            string shixiaorq = Request["shixiaorq"] ?? "";
            string chukusl = Request["chukusl"] ?? "";
            string danwei = Request["danwei"] ?? "";
            string huansuanlv = Request["huansuanlv"] ?? "";
            string taobaohao = Request["taobaohao"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                ord_fahuomx p = ob_ord_fahuomxservice.GetEntityById(ord_fahuomx => ord_fahuomx.ID == uid);
                p.ChukuID = chukuid == "" ? 0 : int.Parse(chukuid);
                p.ShangpinDM = shangpindm.Trim();
                p.ShangpinMC = shangpinmc.Trim();
                p.Zhucezheng = zhucezheng.Trim();
                p.Guige = guige.Trim();
                p.Pihao = pihao.Trim();
                p.Xuliehao = xuliehao.Trim();
                p.ShengchanRQ = shengchanrq == "" ? DateTime.Now : DateTime.Parse(shengchanrq);
                p.ShixiaoRQ = shixiaorq == "" ? DateTime.Now : DateTime.Parse(shixiaorq);
                p.ChukuSL = chukusl == "" ? 0 : float.Parse(chukusl);
                p.Danwei = danwei.Trim();
                p.Huansuanlv = huansuanlv == "" ? 0 : float.Parse(huansuanlv);
                p.Taobaohao = taobaohao.Trim();
                p.Beizhu = beizhu.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_fahuomxservice.UpdateEntity(p);
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
            ord_fahuomx ob_ord_fahuomx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_fahuomx = ob_ord_fahuomxservice.GetEntityById(ord_fahuomx => ord_fahuomx.ID == id && ord_fahuomx.IsDelete == false);
                    ob_ord_fahuomx.IsDelete = true;
                    ob_ord_fahuomxservice.UpdateEntity(ob_ord_fahuomx);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

