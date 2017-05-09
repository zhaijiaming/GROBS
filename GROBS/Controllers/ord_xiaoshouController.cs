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
    public class ord_xiaoshouController : Controller
    {
        private Iord_xiaoshouService ob_ord_xiaoshouservice = ServiceFactory.ord_xiaoshouservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_xiaoshou_index";
            Expression<Func<ord_xiaoshou, bool>> where = PredicateExtensionses.True<ord_xiaoshou>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "ddid":
                            string ddid = scld[1];
                            string ddidequal = scld[2];
                            string ddidand = scld[3];
                            if (!string.IsNullOrEmpty(ddid))
                            {
                                if (ddidequal.Equals("="))
                                {
                                    if (ddidand.Equals("and"))
                                        where = where.And(ord_xiaoshou => ord_xiaoshou.DDID == int.Parse(ddid));
                                    else
                                        where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID == int.Parse(ddid));
                                }
                                if (ddidequal.Equals(">"))
                                {
                                    if (ddidand.Equals("and"))
                                        where = where.And(ord_xiaoshou => ord_xiaoshou.DDID > int.Parse(ddid));
                                    else
                                        where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID > int.Parse(ddid));
                                }
                                if (ddidequal.Equals("<"))
                                {
                                    if (ddidand.Equals("and"))
                                        where = where.And(ord_xiaoshou => ord_xiaoshou.DDID < int.Parse(ddid));
                                    else
                                        where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID < int.Parse(ddid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_xiaoshou => ord_xiaoshou.IsDelete == false);

            var tempData = ob_ord_xiaoshouservice.LoadSortEntities(where.Compile(), false, ord_xiaoshou => ord_xiaoshou.ID).ToPagedList<ord_xiaoshou>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_xiaoshou = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_xiaoshou_index";
            string page = "1";
            string ddid = Request["ddid"] ?? "";
            string ddidequal = Request["ddidequal"] ?? "";
            string ddidand = Request["ddidand"] ?? "";
            Expression<Func<ord_xiaoshou, bool>> where = PredicateExtensionses.True<ord_xiaoshou>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(ddid))
                {
                    if (ddidequal.Equals("="))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_xiaoshou => ord_xiaoshou.DDID == int.Parse(ddid));
                        else
                            where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID == int.Parse(ddid));
                    }
                    if (ddidequal.Equals(">"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_xiaoshou => ord_xiaoshou.DDID > int.Parse(ddid));
                        else
                            where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID > int.Parse(ddid));
                    }
                    if (ddidequal.Equals("<"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_xiaoshou => ord_xiaoshou.DDID < int.Parse(ddid));
                        else
                            where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID < int.Parse(ddid));
                    }
                }
                if (!string.IsNullOrEmpty(ddid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddid", ddid, ddidequal, ddidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddid", "", ddidequal, ddidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(ddid))
                {
                    if (ddidequal.Equals("="))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_xiaoshou => ord_xiaoshou.DDID == int.Parse(ddid));
                        else
                            where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID == int.Parse(ddid));
                    }
                    if (ddidequal.Equals(">"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_xiaoshou => ord_xiaoshou.DDID > int.Parse(ddid));
                        else
                            where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID > int.Parse(ddid));
                    }
                    if (ddidequal.Equals("<"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_xiaoshou => ord_xiaoshou.DDID < int.Parse(ddid));
                        else
                            where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID < int.Parse(ddid));
                    }
                }
                if (!string.IsNullOrEmpty(ddid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddid", ddid, ddidequal, ddidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddid", "", ddidequal, ddidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_xiaoshou => ord_xiaoshou.IsDelete == false);

            var tempData = ob_ord_xiaoshouservice.LoadSortEntities(where.Compile(), false, ord_xiaoshou => ord_xiaoshou.ID).ToPagedList<ord_xiaoshou>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_xiaoshou = tempData;
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
            string ddid = Request["ddid"] ?? "";
            string ddbh = Request["ddbh"] ?? "";
            string xsdh = Request["xsdh"] ?? "";
            string kddh = Request["kddh"] ?? "";
            string fahuorq = Request["fahuorq"] ?? "";
            string kefu = Request["kefu"] ?? "";
            string kefudh = Request["kefudh"] ?? "";
            string zhengdansf = Request["zhengdansf"] ?? "";
            string guanbisf = Request["guanbisf"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string kuaidigs = Request["kuaidigs"] ?? "";
            string fayunfs = Request["fayunfs"] ?? "";
            try
            {
                ord_xiaoshou ob_ord_xiaoshou = new ord_xiaoshou();
                ob_ord_xiaoshou.DDID = ddid == "" ? 0 : int.Parse(ddid);
                ob_ord_xiaoshou.DDBH = ddbh.Trim();
                ob_ord_xiaoshou.XSDH = xsdh.Trim();
                ob_ord_xiaoshou.KDDH = kddh.Trim();
                ob_ord_xiaoshou.FahuoRQ = fahuorq == "" ? DateTime.Now : DateTime.Parse(fahuorq);
                ob_ord_xiaoshou.Kefu = kefu.Trim();
                ob_ord_xiaoshou.KefuDH = kefudh.Trim();
                ob_ord_xiaoshou.ZhengdanSF = zhengdansf == "" ? false : Boolean.Parse(zhengdansf);
                ob_ord_xiaoshou.GuanbiSF = guanbisf == "" ? false : Boolean.Parse(guanbisf);
                ob_ord_xiaoshou.Col1 = col1.Trim();
                ob_ord_xiaoshou.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_xiaoshou.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_xiaoshou.KuaidiGS = kuaidigs.Trim();
                ob_ord_xiaoshou.FayunFS = fayunfs.Trim();
                ob_ord_xiaoshou = ob_ord_xiaoshouservice.AddEntity(ob_ord_xiaoshou);
                ViewBag.ord_xiaoshou = ob_ord_xiaoshou;
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
            ord_xiaoshou tempData = ob_ord_xiaoshouservice.GetEntityById(ord_xiaoshou => ord_xiaoshou.ID == id && ord_xiaoshou.IsDelete == false);
            ViewBag.ord_xiaoshou = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_xiaoshouViewModel ord_xiaoshouviewmodel = new ord_xiaoshouViewModel();
                ord_xiaoshouviewmodel.ID = tempData.ID;
                ord_xiaoshouviewmodel.DDID = tempData.DDID;
                ord_xiaoshouviewmodel.DDBH = tempData.DDBH;
                ord_xiaoshouviewmodel.XSDH = tempData.XSDH;
                ord_xiaoshouviewmodel.KDDH = tempData.KDDH;
                ord_xiaoshouviewmodel.FahuoRQ = tempData.FahuoRQ;
                ord_xiaoshouviewmodel.Kefu = tempData.Kefu;
                ord_xiaoshouviewmodel.KefuDH = tempData.KefuDH;
                ord_xiaoshouviewmodel.ZhengdanSF = tempData.ZhengdanSF;
                ord_xiaoshouviewmodel.GuanbiSF = tempData.GuanbiSF;
                ord_xiaoshouviewmodel.Col1 = tempData.Col1;
                ord_xiaoshouviewmodel.MakeDate = tempData.MakeDate;
                ord_xiaoshouviewmodel.MakeMan = tempData.MakeMan;
                ord_xiaoshouviewmodel.KuaidiGS = tempData.KuaidiGS;
                ord_xiaoshouviewmodel.FayunFS = tempData.FayunFS;
                return View(ord_xiaoshouviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string ddid = Request["ddid"] ?? "";
            string ddbh = Request["ddbh"] ?? "";
            string xsdh = Request["xsdh"] ?? "";
            string kddh = Request["kddh"] ?? "";
            string fahuorq = Request["fahuorq"] ?? "";
            string kefu = Request["kefu"] ?? "";
            string kefudh = Request["kefudh"] ?? "";
            string zhengdansf = Request["zhengdansf"] ?? "";
            string guanbisf = Request["guanbisf"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string kuaidigs = Request["kuaidigs"] ?? "";
            string fayunfs = Request["fayunfs"] ?? "";
            int uid = int.Parse(id);
            try
            {
                ord_xiaoshou p = ob_ord_xiaoshouservice.GetEntityById(ord_xiaoshou => ord_xiaoshou.ID == uid);
                p.DDID = ddid == "" ? 0 : int.Parse(ddid);
                p.DDBH = ddbh.Trim();
                p.XSDH = xsdh.Trim();
                p.KDDH = kddh.Trim();
                p.FahuoRQ = fahuorq == "" ? DateTime.Now : DateTime.Parse(fahuorq);
                p.Kefu = kefu.Trim();
                p.KefuDH = kefudh.Trim();
                p.ZhengdanSF = zhengdansf == "" ? false : Boolean.Parse(zhengdansf);
                p.GuanbiSF = guanbisf == "" ? false : Boolean.Parse(guanbisf);
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.KuaidiGS = kuaidigs.Trim();
                p.FayunFS = fayunfs.Trim();
                ob_ord_xiaoshouservice.UpdateEntity(p);
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
            ord_xiaoshou ob_ord_xiaoshou;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_xiaoshou = ob_ord_xiaoshouservice.GetEntityById(ord_xiaoshou => ord_xiaoshou.ID == id && ord_xiaoshou.IsDelete == false);
                    ob_ord_xiaoshou.IsDelete = true;
                    ob_ord_xiaoshouservice.UpdateEntity(ob_ord_xiaoshou);
                }
            }
            return RedirectToAction("Index");
        }
        [OutputCache(Duration =30)]
        public ActionResult GetSendList(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            int custid = (int)Session["customer_id"];
            string pagetag = "ord_xiaoshou_sendlist";
            Expression<Func<ord_sendlist_v, bool>> where = PredicateExtensionses.True<ord_sendlist_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "ddid":
                            string ddid = scld[1];
                            string ddidequal = scld[2];
                            string ddidand = scld[3];
                            if (!string.IsNullOrEmpty(ddid))
                            {
                                if (ddidequal.Equals("="))
                                {
                                    if (ddidand.Equals("and"))
                                        where = where.And(ord_xiaoshou => ord_xiaoshou.DDID == int.Parse(ddid));
                                    else
                                        where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID == int.Parse(ddid));
                                }
                                if (ddidequal.Equals(">"))
                                {
                                    if (ddidand.Equals("and"))
                                        where = where.And(ord_xiaoshou => ord_xiaoshou.DDID > int.Parse(ddid));
                                    else
                                        where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID > int.Parse(ddid));
                                }
                                if (ddidequal.Equals("<"))
                                {
                                    if (ddidand.Equals("and"))
                                        where = where.And(ord_xiaoshou => ord_xiaoshou.DDID < int.Parse(ddid));
                                    else
                                        where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID < int.Parse(ddid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            var tempData = ob_ord_xiaoshouservice.LoadSendList(custid,where.Compile()).ToPagedList<ord_sendlist_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_xiaoshou = tempData;
            return View(tempData);
        }
        [HttpPost]
        [OutputCache(Duration =30)]
        public ActionResult GetSendList()
        {
            int userid = (int)Session["user_id"];
            int custid = (int)Session["customer_id"];
            string pagetag = "ord_xiaoshou_sendlist";
            string page = "1";
            string ddid = Request["ddid"] ?? "";
            string ddidequal = Request["ddidequal"] ?? "";
            string ddidand = Request["ddidand"] ?? "";
            Expression<Func<ord_sendlist_v, bool>> where = PredicateExtensionses.True<ord_sendlist_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(ddid))
                {
                    if (ddidequal.Equals("="))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_xiaoshou => ord_xiaoshou.DDID == int.Parse(ddid));
                        else
                            where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID == int.Parse(ddid));
                    }
                    if (ddidequal.Equals(">"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_xiaoshou => ord_xiaoshou.DDID > int.Parse(ddid));
                        else
                            where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID > int.Parse(ddid));
                    }
                    if (ddidequal.Equals("<"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_xiaoshou => ord_xiaoshou.DDID < int.Parse(ddid));
                        else
                            where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID < int.Parse(ddid));
                    }
                }
                if (!string.IsNullOrEmpty(ddid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddid", ddid, ddidequal, ddidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddid", "", ddidequal, ddidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(ddid))
                {
                    if (ddidequal.Equals("="))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_xiaoshou => ord_xiaoshou.DDID == int.Parse(ddid));
                        else
                            where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID == int.Parse(ddid));
                    }
                    if (ddidequal.Equals(">"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_xiaoshou => ord_xiaoshou.DDID > int.Parse(ddid));
                        else
                            where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID > int.Parse(ddid));
                    }
                    if (ddidequal.Equals("<"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_xiaoshou => ord_xiaoshou.DDID < int.Parse(ddid));
                        else
                            where = where.Or(ord_xiaoshou => ord_xiaoshou.DDID < int.Parse(ddid));
                    }
                }
                if (!string.IsNullOrEmpty(ddid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddid", ddid, ddidequal, ddidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddid", "", ddidequal, ddidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;

            var tempData = ob_ord_xiaoshouservice.LoadSendList(custid,where.Compile()).ToPagedList<ord_sendlist_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_xiaoshou = tempData;
            return View(tempData);
        }
    }
}

