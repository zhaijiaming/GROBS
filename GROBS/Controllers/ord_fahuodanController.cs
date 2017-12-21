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
    public class ord_fahuodanController : Controller
    {
        private Iord_fahuodanService ob_ord_fahuodanservice = ServiceFactory.ord_fahuodanservice;
        private object ob_ord_fanhuodanservice;

        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_fahuodan_index";
            Expression<Func<ord_fahuodan, bool>> where = PredicateExtensionses.True<ord_fahuodan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "chukudanbh":
                            string chukudanbh = scld[1];
                            string chukudanbhequal = scld[2];
                            string chukudanbhand = scld[3];
                            if (!string.IsNullOrEmpty(chukudanbh))
                            {
                                if (chukudanbhequal.Equals("="))
                                {
                                    if (chukudanbhand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                                }
                                if (chukudanbhequal.Equals("like"))
                                {
                                    if (chukudanbhand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                                }
                            }
                            break;

                        case "ddbh":
                            string ddbh = scld[1];
                            string ddbhequal = scld[2];
                            string ddbhand = scld[3];
                            if (!string.IsNullOrEmpty(ddbh))
                            {
                                if (ddbhequal.Equals("="))
                                {
                                    if (ddbhand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.DDBH == ddbh);
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.DDBH == ddbh);
                                }
                                if (ddbhequal.Equals("like"))
                                {
                                    if (ddbhand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.DDBH.Contains(ddbh));
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.DDBH.Contains(ddbh));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_fahuodan => ord_fahuodan.IsDelete == false);

            var tempData = ob_ord_fahuodanservice.LoadSortEntities(where.Compile(), false, ord_fahuodan => ord_fahuodan.ID).ToPagedList<ord_fahuodan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_fahuodan = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_fahuodan_index";
            string page = "1";
            //chukudanbh
            string chukudanbh = Request["chukudanbh"] ?? "";
            string chukudanbhequal = Request["chukudanbhequal"] ?? "";
            string chukudanbhand = Request["chukudanbhand"] ?? "";
            //ddbh
            string ddbh = Request["ddbh"] ?? "";
            string ddbhequal = Request["ddbhequal"] ?? "";
            string ddbhand = Request["ddbhand"] ?? "";

            Expression<Func<ord_fahuodan, bool>> where = PredicateExtensionses.True<ord_fahuodan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(chukudanbh))
                {
                    if (chukudanbhequal.Equals("="))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                    }
                    if (chukudanbhequal.Equals("like"))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(chukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", chukudanbh, chukudanbhequal, chukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", "", chukudanbhequal, chukudanbhand);

                //ddbh
                if (!string.IsNullOrEmpty(ddbh))
                {
                    if (ddbhequal.Equals("="))
                    {
                        if (ddbhand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.DDBH == ddbh);
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.DDBH == ddbh);
                    }
                    if (ddbhequal.Equals("like"))
                    {
                        if (ddbhand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.DDBH.Contains(ddbh));
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.DDBH.Contains(ddbh));
                    }
                }
                if (!string.IsNullOrEmpty(chukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", chukudanbh, chukudanbhequal, chukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", "", chukudanbhequal, chukudanbhand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(chukudanbh))
                {
                    if (chukudanbhequal.Equals("="))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                    }
                    if (chukudanbhequal.Equals("like"))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(chukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", chukudanbh, chukudanbhequal, chukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", "", chukudanbhequal, chukudanbhand);

                //ddbh
                if (!string.IsNullOrEmpty(ddbh))
                {
                    if (ddbhequal.Equals("="))
                    {
                        if (ddbhand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.DDBH == ddbh);
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.DDBH == ddbh);
                    }
                    if (ddbhequal.Equals("like"))
                    {
                        if (ddbhand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.DDBH.Contains(ddbh));
                        else
                            where = where.And(ord_fahuodan => ord_fahuodan.DDBH.Contains(ddbh));
                    }
                }
                if (!string.IsNullOrEmpty(ddbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddbh", ddbh, ddbhequal, ddbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddbh", "", ddbhequal, ddbhand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_fahuodan => ord_fahuodan.IsDelete == false);

            var tempData = ob_ord_fahuodanservice.LoadSortEntities(where.Compile(), false, ord_fahuodan => ord_fahuodan.ID).ToPagedList<ord_fahuodan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_fahuodan = tempData;
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
            string chukudanbh = Request["chukudanbh"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string ddid = Request["ddid"] ?? "";
            string ddbh = Request["ddbh"] ?? "";
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string chukurq = Request["chukurq"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string ckcode = Request["ckcode"] ?? "";
            string yunsongfs = Request["yunsongfs"] ?? "";
            string kddanhao = Request["kddanhao"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                ord_fahuodan ob_ord_fahuodan = new ord_fahuodan();
                ob_ord_fahuodan.ChukudanBH = chukudanbh.Trim();
                ob_ord_fahuodan.KehuDH = kehudh.Trim();
                ob_ord_fahuodan.DDID = ddid == "" ? 0 : int.Parse(ddid);
                ob_ord_fahuodan.DDBH = ddbh.Trim();
                ob_ord_fahuodan.Yunsongdizhi = yunsongdizhi.Trim();
                ob_ord_fahuodan.ChukuRQ = chukurq == "" ? DateTime.Now : DateTime.Parse(chukurq);
                ob_ord_fahuodan.Lianxiren = lianxiren.Trim();
                ob_ord_fahuodan.LianxiDH = lianxidh.Trim();
                ob_ord_fahuodan.Beizhu = beizhu.Trim();
                ob_ord_fahuodan.CKCode = ckcode.Trim();
                ob_ord_fahuodan.YunsongFS = yunsongfs.Trim();
                ob_ord_fahuodan.Kddanhao = kddanhao.Trim();
                ob_ord_fahuodan.Col1 = col1.Trim();
                ob_ord_fahuodan.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_fahuodan.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_fahuodan = ob_ord_fahuodanservice.AddEntity(ob_ord_fahuodan);
                ViewBag.ord_fahuodan = ob_ord_fahuodan;
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
            ord_fahuodan tempData = ob_ord_fahuodanservice.GetEntityById(ord_fahuodan => ord_fahuodan.ID == id && ord_fahuodan.IsDelete == false);
            ViewBag.ord_fahuodan = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_fahuodanViewModel ord_fahuodanviewmodel = new ord_fahuodanViewModel();
                ord_fahuodanviewmodel.ID = tempData.ID;
                ord_fahuodanviewmodel.ChukudanBH = tempData.ChukudanBH;
                ord_fahuodanviewmodel.KehuDH = tempData.KehuDH;
                ord_fahuodanviewmodel.DDID = tempData.DDID;
                ord_fahuodanviewmodel.DDBH = tempData.DDBH;
                ord_fahuodanviewmodel.Yunsongdizhi = tempData.Yunsongdizhi;
                ord_fahuodanviewmodel.ChukuRQ = tempData.ChukuRQ;
                ord_fahuodanviewmodel.Lianxiren = tempData.Lianxiren;
                ord_fahuodanviewmodel.LianxiDH = tempData.LianxiDH;
                ord_fahuodanviewmodel.Beizhu = tempData.Beizhu;
                ord_fahuodanviewmodel.CKCode = tempData.CKCode;
                ord_fahuodanviewmodel.YunsongFS = tempData.YunsongFS;
                ord_fahuodanviewmodel.Kddanhao = tempData.Kddanhao;
                ord_fahuodanviewmodel.Col1 = tempData.Col1;
                ord_fahuodanviewmodel.MakeDate = tempData.MakeDate;
                ord_fahuodanviewmodel.MakeMan = tempData.MakeMan;
                return View(ord_fahuodanviewmodel);
            }
        }

        //新增加的"Fahuodanlist"方法    
        public ActionResult FahuodanlistPage(string page, string sortOrder)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            int custid = (int)Session["customer_id"];
            string pagetag = "ord_fahuodan_fahuodanlist";
            Expression<Func<ord_inventoryout_v, bool>> where = PredicateExtensionses.True<ord_inventoryout_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "chukudanbh"://发货单号
                            string chukudanbh = scld[1];
                            string chukudanbhequal = scld[2];
                            string chukudanbhand = scld[3];
                            if (!string.IsNullOrEmpty(chukudanbh))
                            {
                                if (chukudanbhequal.Equals("="))
                                {
                                    if (chukudanbhand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                                }
                                if (chukudanbhequal.Equals("like"))
                                {
                                    if (chukudanbhand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                                }
                            }
                            break;

                        case "DDBH"://订单编号
                            string DDBH = scld[1];
                            string DDBHequal = scld[2];
                            string DDBHand = scld[3];
                            if (!string.IsNullOrEmpty(DDBH))
                            {
                                if (DDBHequal.Equals("="))
                                {
                                    if (DDBHand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.DDBH == DDBH);
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.DDBH == DDBH);
                                }
                                if (DDBHequal.Equals("like"))
                                {
                                    if (DDBHand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.DDBH.Contains(DDBH));
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.DDBH.Contains(DDBH));
                                }
                            }
                            break;

                        case "ChukuRQ":
                            string ChukuRQ = scld[1];
                            string ChukuRQequal = scld[2];
                            string ChukuRQand = scld[3];
                            if (!string.IsNullOrEmpty(ChukuRQ))
                            {
                                if (ChukuRQequal.Equals("="))
                                {
                                    if (ChukuRQand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.ChukuRQ == DateTime.Parse(ChukuRQ));
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.ChukuRQ == DateTime.Parse(ChukuRQ));
                                }
                                if (ChukuRQequal.Equals(">"))
                                {
                                    if (ChukuRQand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                                }
                                if (ChukuRQequal.Equals(">"))
                                {
                                    if (ChukuRQand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                                }
                            }
                            break;

                        case "YunsongFS":
                            string YunsongFS = scld[1];
                            string YunsongFSequal = scld[2];
                            string YunsongFSand = scld[3];
                            if (!string.IsNullOrEmpty(YunsongFS))
                            {
                                if (YunsongFSequal.Equals("="))
                                {
                                    if (YunsongFSand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.YunsongFS == YunsongFS);
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.YunsongFS == YunsongFS);
                                }
                            }
                            break;

                        case "Kddanhao":
                            string Kddanhao = scld[1];
                            string Kddanhaoequal = scld[2];
                            string Kddanhaoand = scld[3];
                            if (!string.IsNullOrEmpty(Kddanhao))
                            {
                                if (Kddanhaoequal.Equals("="))
                                {
                                    if (Kddanhaoand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.Kddanhao == Kddanhao);
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.Kddanhao == Kddanhao);
                                }
                                if (Kddanhaoequal.Equals("like"))
                                {
                                    if (Kddanhaoand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.Kddanhao.Contains(Kddanhao));
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.Kddanhao.Contains(Kddanhao));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            //where = where.And(ord_fahuodan => ord_fahuodan.IsDelete == false);

            //var tempData = ob_ord_fahuodanservice.LoadOutList(custid,where.Compile(), false, ord_fahuodan => ord_fahuodan.ID).ToPagedList<ord_sendlist_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            var tempData = ob_ord_fahuodanservice.LoadOutList(custid, where.Compile()).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));

            #region 排序

            ViewBag.ChukudanBHParm = string.IsNullOrEmpty(sortOrder) ? "ChukudanBH" : "ChukudanBH_desc";
            ViewBag.DDBHParm = sortOrder == "DDBH" ? "DDBH_desc" : "DDBH";
            ViewBag.ChukuRQParm = sortOrder == "ChukuRQ" ? "ChukuRQ_desc" : "ChukuRQ";
            ViewBag.YunsongFSParm = sortOrder == "YunsongFS" ? "YunsongFS_desc" : "YunsongFS";
            ViewBag.KddanhaoParm = sortOrder == "Kddanhao" ? "Kddanhao_desc" : "Kddanhao";
            switch (sortOrder)
            {
                case "Kddanhao_desc":
                    tempData = tempData.OrderByDescending(p => p.Kddanhao).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Kddanhao":
                    tempData = tempData.OrderBy(p => p.Kddanhao).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "YunsongFS_desc":
                    tempData = tempData.OrderByDescending(p => p.YunsongFS).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "YunsongFS":
                    tempData = tempData.OrderBy(p => p.YunsongFS).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "ChukuRQ_desc":
                    tempData = tempData.OrderByDescending(p => p.ChukuRQ).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "ChukuRQ":
                    tempData = tempData.OrderBy(p => p.ChukuRQ).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "DDBH_desc":
                    tempData = tempData.OrderByDescending(p => p.DDBH).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "DDBH":
                    tempData = tempData.OrderBy(p => p.DDBH).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "ChukudanBH":
                    tempData = tempData.OrderBy(p => p.ChukudanBH).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                default:
                    tempData = tempData.OrderByDescending(p => p.ChukudanBH).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
            }

            #endregion

            ViewBag.ord_fahuodan = tempData;
            return View(tempData);
        }


        public ActionResult fahuodanlist(string sortOrder)
        {
            string page = "1";
            int userid = (int)Session["user_id"];
            int custid = (int)Session["customer_id"];
            string pagetag = "ord_fahuodan_fahuodanlist";

            string chukudanbh = Request["chukudanbh"] ?? "";
            string chukudanbhequal = Request["chukudanbhequal"] ?? "";
            string chukudanbhand = Request["chukudanbhand"] ?? "and";

            string DDBH = Request["DDBH"] ?? "";
            string DDBHequal = Request["DDBHequal"] ?? "";
            string DDBHand = Request["DDBHand"] ?? "and";

            string ChukuRQ = Request["ChukuRQ"] ?? "";
            string ChukuRQequal = Request["ChukuRQequal"] ?? "";
            string ChukuRQand = Request["ChukuRQand"] ?? "and";

            string YunsongFS = Request["YunsongFS"] ?? "";
            string YunsongFSequal = Request["YunsongFSequal"] ?? "";
            string YunsongFSand = Request["YunsongFSand"] ?? "and";

            string Kddanhao = Request["Kddanhao"] ?? "";
            string Kddanhaoequal = Request["Kddanhaoequal"] ?? "";
            string Kddanhaoand = Request["Kddanhaoand"] ?? "and";

            Expression<Func<ord_inventoryout_v, bool>> where = PredicateExtensionses.True<ord_inventoryout_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //发货单号
                if (!string.IsNullOrEmpty(chukudanbh))
                {
                    if (chukudanbhequal.Equals("="))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(ord_ordermain_v => ord_ordermain_v.ChukudanBH == chukudanbh);
                        else
                            where = where.Or(ord_ordermain_v => ord_ordermain_v.ChukudanBH == chukudanbh);
                    }
                    if (chukudanbhequal.Equals("like"))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(ord_ordermain_v => ord_ordermain_v.ChukudanBH.Contains(chukudanbh));
                        else
                            where = where.Or(ord_ordermain_v => ord_ordermain_v.ChukudanBH.Contains(chukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(chukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", chukudanbh, chukudanbhequal, chukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", "", chukudanbhequal, chukudanbhand);

                //订单编号
                if (!string.IsNullOrEmpty(DDBH))
                {
                    if (DDBHequal.Equals("="))
                    {
                        if (DDBHand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.DDBH == DDBH);
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.DDBH == DDBH);
                    }
                    if (DDBHequal.Equals("like"))
                    {
                        if (DDBHand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.DDBH.Contains(DDBH));
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.DDBH.Contains(DDBH));
                    }
                }
                if (!string.IsNullOrEmpty(DDBH))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "DDBH", DDBH, DDBHequal, DDBHand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "DDBH", "", DDBHequal, DDBHand);

                if (!string.IsNullOrEmpty(ChukuRQ))
                {
                    if (ChukuRQequal.Equals("="))
                    {
                        if (ChukuRQand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.ChukuRQ == DateTime.Parse(ChukuRQ));
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.ChukuRQ == DateTime.Parse(ChukuRQ));
                    }
                    if (ChukuRQequal.Equals(">"))
                    {
                        if (ChukuRQand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                    }
                    if (ChukuRQequal.Equals(">"))
                    {
                        if (ChukuRQand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                    }
                }
                if (!string.IsNullOrEmpty(ChukuRQ))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ChukuRQ", ChukuRQ, ChukuRQequal, ChukuRQand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ChukuRQ", "", ChukuRQequal, ChukuRQand);

                if (!string.IsNullOrEmpty(YunsongFS))
                {
                    if (YunsongFSequal.Equals("="))
                    {
                        if (YunsongFSand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.YunsongFS == YunsongFS);
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.YunsongFS == YunsongFS);
                    }
                }
                if (!string.IsNullOrEmpty(YunsongFS))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "YunsongFS", YunsongFS, YunsongFSequal, YunsongFSand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "YunsongFS", "", YunsongFSequal, YunsongFSand);

                if (!string.IsNullOrEmpty(Kddanhao))
                {
                    if (Kddanhaoequal.Equals("="))
                    {
                        if (Kddanhaoand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.Kddanhao == Kddanhao);
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.Kddanhao == Kddanhao);
                    }
                    if (Kddanhaoequal.Equals("like"))
                    {
                        if (Kddanhaoand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.Kddanhao.Contains(Kddanhao));
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.Kddanhao.Contains(Kddanhao));
                    }
                }
                if (!string.IsNullOrEmpty(Kddanhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Kddanhao", Kddanhao, Kddanhaoequal, Kddanhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Kddanhao", "", Kddanhaoequal, Kddanhaoand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                if (!string.IsNullOrEmpty(sortOrder))
                {
                    where = GetOrderListSearchCondition(where, sc);
                }
                else
                {
                    #region 查询条件


                    sc.ConditionInfo = "";
                    //发货单号
                    if (!string.IsNullOrEmpty(chukudanbh))
                    {
                        if (chukudanbhequal.Equals("="))
                        {
                            if (chukudanbhand.Equals("and"))
                                where = where.And(ord_ordermain_v => ord_ordermain_v.ChukudanBH == chukudanbh);
                            else
                                where = where.Or(ord_ordermain_v => ord_ordermain_v.ChukudanBH == chukudanbh);
                        }
                        if (chukudanbhequal.Equals("like"))
                        {
                            if (chukudanbhand.Equals("and"))
                                where = where.And(ord_ordermain_v => ord_ordermain_v.ChukudanBH.Contains(chukudanbh));
                            else
                                where = where.Or(ord_ordermain_v => ord_ordermain_v.ChukudanBH.Contains(chukudanbh));
                        }
                    }
                    if (!string.IsNullOrEmpty(chukudanbh))
                        sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", chukudanbh, chukudanbhequal, chukudanbhand);
                    else
                        sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", "", chukudanbhequal, chukudanbhand);

                    //订单编号
                    if (!string.IsNullOrEmpty(DDBH))
                    {
                        if (DDBHequal.Equals("="))
                        {
                            if (DDBHand.Equals("and"))
                                where = where.And(ord_fahuodan => ord_fahuodan.DDBH == DDBH);
                            else
                                where = where.Or(ord_fahuodan => ord_fahuodan.DDBH == DDBH);
                        }
                        if (DDBHequal.Equals("like"))
                        {
                            if (DDBHand.Equals("and"))
                                where = where.And(ord_fahuodan => ord_fahuodan.DDBH.Contains(DDBH));
                            else
                                where = where.Or(ord_fahuodan => ord_fahuodan.DDBH.Contains(DDBH));
                        }
                    }
                    if (!string.IsNullOrEmpty(DDBH))
                        sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "DDBH", DDBH, DDBHequal, DDBHand);
                    else
                        sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "DDBH", "", DDBHequal, DDBHand);

                    if (!string.IsNullOrEmpty(ChukuRQ))
                    {
                        if (ChukuRQequal.Equals("="))
                        {
                            if (ChukuRQand.Equals("and"))
                                where = where.And(ord_fahuodan => ord_fahuodan.ChukuRQ == DateTime.Parse(ChukuRQ));
                            else
                                where = where.Or(ord_fahuodan => ord_fahuodan.ChukuRQ == DateTime.Parse(ChukuRQ));
                        }
                        if (ChukuRQequal.Equals(">"))
                        {
                            if (ChukuRQand.Equals("and"))
                                where = where.And(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                            else
                                where = where.Or(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                        }
                        if (ChukuRQequal.Equals(">"))
                        {
                            if (ChukuRQand.Equals("and"))
                                where = where.And(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                            else
                                where = where.Or(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                        }
                    }
                    if (!string.IsNullOrEmpty(ChukuRQ))
                        sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ChukuRQ", ChukuRQ, ChukuRQequal, ChukuRQand);
                    else
                        sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ChukuRQ", "", ChukuRQequal, ChukuRQand);

                    if (!string.IsNullOrEmpty(YunsongFS))
                    {
                        if (YunsongFSequal.Equals("="))
                        {
                            if (YunsongFSand.Equals("and"))
                                where = where.And(ord_fahuodan => ord_fahuodan.YunsongFS == YunsongFS);
                            else
                                where = where.Or(ord_fahuodan => ord_fahuodan.YunsongFS == YunsongFS);
                        }
                    }
                    if (!string.IsNullOrEmpty(YunsongFS))
                        sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "YunsongFS", YunsongFS, YunsongFSequal, YunsongFSand);
                    else
                        sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "YunsongFS", "", YunsongFSequal, YunsongFSand);

                    if (!string.IsNullOrEmpty(Kddanhao))
                    {
                        if (Kddanhaoequal.Equals("="))
                        {
                            if (Kddanhaoand.Equals("and"))
                                where = where.And(ord_fahuodan => ord_fahuodan.Kddanhao == Kddanhao);
                            else
                                where = where.Or(ord_fahuodan => ord_fahuodan.Kddanhao == Kddanhao);
                        }
                        if (Kddanhaoequal.Equals("like"))
                        {
                            if (Kddanhaoand.Equals("and"))
                                where = where.And(ord_fahuodan => ord_fahuodan.Kddanhao.Contains(Kddanhao));
                            else
                                where = where.Or(ord_fahuodan => ord_fahuodan.Kddanhao.Contains(Kddanhao));
                        }
                    }
                    if (!string.IsNullOrEmpty(Kddanhao))
                        sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Kddanhao", Kddanhao, Kddanhaoequal, Kddanhaoand);
                    else
                        sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Kddanhao", "", Kddanhaoequal, Kddanhaoand);

                    searchconditionService.GetInstance().UpdateEntity(sc);

                    #endregion
                }

            }
            ViewBag.SearchCondition = sc.ConditionInfo;

            //where = where.And(ord_fahuodan => ord_fahuodan.IsDelete == false);

            //var tempData = ob_ord_fahuodanservice.LoadOutList(custid,where.Compile(), false, ord_fahuodan => ord_fahuodan.ID).ToPagedList<ord_sendlist_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            var tempData = ob_ord_fahuodanservice.LoadOutList(custid, where.Compile()).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));

            #region 排序

            ViewBag.ChukudanBHParm = string.IsNullOrEmpty(sortOrder) ? "ChukudanBH" : "ChukudanBH_desc";
            ViewBag.DDBHParm = sortOrder == "DDBH" ? "DDBH_desc" : "DDBH";
            ViewBag.ChukuRQParm = sortOrder == "ChukuRQ" ? "ChukuRQ_desc" : "ChukuRQ";
            ViewBag.YunsongFSParm = sortOrder == "YunsongFS" ? "YunsongFS_desc" : "YunsongFS";
            ViewBag.KddanhaoParm = sortOrder == "Kddanhao" ? "Kddanhao_desc" : "Kddanhao";
            switch (sortOrder)
            {
                case "Kddanhao_desc":
                    tempData = tempData.OrderByDescending(p => p.Kddanhao).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Kddanhao":
                    tempData = tempData.OrderBy(p => p.Kddanhao).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "YunsongFS_desc":
                    tempData = tempData.OrderByDescending(p => p.YunsongFS).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "YunsongFS":
                    tempData = tempData.OrderBy(p => p.YunsongFS).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "ChukuRQ_desc":
                    tempData = tempData.OrderByDescending(p => p.ChukuRQ).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "ChukuRQ":
                    tempData = tempData.OrderBy(p => p.ChukuRQ).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "DDBH_desc":
                    tempData = tempData.OrderByDescending(p => p.DDBH).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "DDBH":
                    tempData = tempData.OrderBy(p => p.DDBH).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "ChukudanBH":
                    tempData = tempData.OrderBy(p => p.ChukudanBH).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                default:
                    tempData = tempData.OrderByDescending(p => p.ChukudanBH).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
            }

            #endregion

            ViewBag.ord_fahuodan = tempData;
            return View(tempData);
        }

        private static Expression<Func<ord_inventoryout_v, bool>> GetOrderListSearchCondition(Expression<Func<ord_inventoryout_v, bool>> where, searchcondition sc)
        {
            string[] sclist = sc.ConditionInfo.Split(';');
            foreach (string scl in sclist)
            {
                string[] scld = scl.Split(',');
                switch (scld[0])
                {
                    case "chukudanbh"://发货单号
                        string chukudanbh = scld[1];
                        string chukudanbhequal = scld[2];
                        string chukudanbhand = scld[3];
                        if (!string.IsNullOrEmpty(chukudanbh))
                        {
                            if (chukudanbhequal.Equals("="))
                            {
                                if (chukudanbhand.Equals("and"))
                                    where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                                else
                                    where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                            }
                            if (chukudanbhequal.Equals("like"))
                            {
                                if (chukudanbhand.Equals("and"))
                                    where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                                else
                                    where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                            }
                        }
                        break;

                    case "DDBH"://订单编号
                        string DDBH = scld[1];
                        string DDBHequal = scld[2];
                        string DDBHand = scld[3];
                        if (!string.IsNullOrEmpty(DDBH))
                        {
                            if (DDBHequal.Equals("="))
                            {
                                if (DDBHand.Equals("and"))
                                    where = where.And(ord_fahuodan => ord_fahuodan.DDBH == DDBH);
                                else
                                    where = where.Or(ord_fahuodan => ord_fahuodan.DDBH == DDBH);
                            }
                            if (DDBHequal.Equals("like"))
                            {
                                if (DDBHand.Equals("and"))
                                    where = where.And(ord_fahuodan => ord_fahuodan.DDBH.Contains(DDBH));
                                else
                                    where = where.Or(ord_fahuodan => ord_fahuodan.DDBH.Contains(DDBH));
                            }
                        }
                        break;

                    case "ChukuRQ":
                        string ChukuRQ = scld[1];
                        string ChukuRQequal = scld[2];
                        string ChukuRQand = scld[3];
                        if (!string.IsNullOrEmpty(ChukuRQ))
                        {
                            if (ChukuRQequal.Equals("="))
                            {
                                if (ChukuRQand.Equals("and"))
                                    where = where.And(ord_fahuodan => ord_fahuodan.ChukuRQ == DateTime.Parse(ChukuRQ));
                                else
                                    where = where.Or(ord_fahuodan => ord_fahuodan.ChukuRQ == DateTime.Parse(ChukuRQ));
                            }
                            if (ChukuRQequal.Equals(">"))
                            {
                                if (ChukuRQand.Equals("and"))
                                    where = where.And(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                                else
                                    where = where.Or(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                            }
                            if (ChukuRQequal.Equals(">"))
                            {
                                if (ChukuRQand.Equals("and"))
                                    where = where.And(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                                else
                                    where = where.Or(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                            }
                        }
                        break;

                    case "YunsongFS":
                        string YunsongFS = scld[1];
                        string YunsongFSequal = scld[2];
                        string YunsongFSand = scld[3];
                        if (!string.IsNullOrEmpty(YunsongFS))
                        {
                            if (YunsongFSequal.Equals("="))
                            {
                                if (YunsongFSand.Equals("and"))
                                    where = where.And(ord_fahuodan => ord_fahuodan.YunsongFS == YunsongFS);
                                else
                                    where = where.Or(ord_fahuodan => ord_fahuodan.YunsongFS == YunsongFS);
                            }
                        }
                        break;

                    case "Kddanhao":
                        string Kddanhao = scld[1];
                        string Kddanhaoequal = scld[2];
                        string Kddanhaoand = scld[3];
                        if (!string.IsNullOrEmpty(Kddanhao))
                        {
                            if (Kddanhaoequal.Equals("="))
                            {
                                if (Kddanhaoand.Equals("and"))
                                    where = where.And(ord_fahuodan => ord_fahuodan.Kddanhao == Kddanhao);
                                else
                                    where = where.Or(ord_fahuodan => ord_fahuodan.Kddanhao == Kddanhao);
                            }
                            if (Kddanhaoequal.Equals("like"))
                            {
                                if (Kddanhaoand.Equals("and"))
                                    where = where.And(ord_fahuodan => ord_fahuodan.Kddanhao.Contains(Kddanhao));
                                else
                                    where = where.Or(ord_fahuodan => ord_fahuodan.Kddanhao.Contains(Kddanhao));
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            return where;
        }

        public ActionResult Exportfahuodanlist()
        {
            int userid = (int)Session["user_id"];
            int custid = (int)Session["customer_id"];
            string pagetag = "ord_fahuodan_fahuodanlist";
            Expression<Func<ord_inventoryout_v, bool>> where = PredicateExtensionses.True<ord_inventoryout_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "chukudanbh"://发货单号
                            string chukudanbh = scld[1];
                            string chukudanbhequal = scld[2];
                            string chukudanbhand = scld[3];
                            if (!string.IsNullOrEmpty(chukudanbh))
                            {
                                if (chukudanbhequal.Equals("="))
                                {
                                    if (chukudanbhand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                                }
                                if (chukudanbhequal.Equals("like"))
                                {
                                    if (chukudanbhand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                                }
                            }
                            break;

                        case "DDBH"://订单编号
                            string DDBH = scld[1];
                            string DDBHequal = scld[2];
                            string DDBHand = scld[3];
                            if (!string.IsNullOrEmpty(DDBH))
                            {
                                if (DDBHequal.Equals("="))
                                {
                                    if (DDBHand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.DDBH == DDBH);
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.DDBH == DDBH);
                                }
                                if (DDBHequal.Equals("like"))
                                {
                                    if (DDBHand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.DDBH.Contains(DDBH));
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.DDBH.Contains(DDBH));
                                }
                            }
                            break;

                        case "ChukuRQ":
                            string ChukuRQ = scld[1];
                            string ChukuRQequal = scld[2];
                            string ChukuRQand = scld[3];
                            if (!string.IsNullOrEmpty(ChukuRQ))
                            {
                                if (ChukuRQequal.Equals("="))
                                {
                                    if (ChukuRQand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.ChukuRQ == DateTime.Parse(ChukuRQ));
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.ChukuRQ == DateTime.Parse(ChukuRQ));
                                }
                                if (ChukuRQequal.Equals(">"))
                                {
                                    if (ChukuRQand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                                }
                                if (ChukuRQequal.Equals(">"))
                                {
                                    if (ChukuRQand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.ChukuRQ > DateTime.Parse(ChukuRQ));
                                }
                            }
                            break;

                        case "YunsongFS":
                            string YunsongFS = scld[1];
                            string YunsongFSequal = scld[2];
                            string YunsongFSand = scld[3];
                            if (!string.IsNullOrEmpty(YunsongFS))
                            {
                                if (YunsongFSequal.Equals("="))
                                {
                                    if (YunsongFSand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.YunsongFS == YunsongFS);
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.YunsongFS == YunsongFS);
                                }
                            }
                            break;

                        case "Kddanhao":
                            string Kddanhao = scld[1];
                            string Kddanhaoequal = scld[2];
                            string Kddanhaoand = scld[3];
                            if (!string.IsNullOrEmpty(Kddanhao))
                            {
                                if (Kddanhaoequal.Equals("="))
                                {
                                    if (Kddanhaoand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.Kddanhao == Kddanhao);
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.Kddanhao == Kddanhao);
                                }
                                if (Kddanhaoequal.Equals("like"))
                                {
                                    if (Kddanhaoand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.Kddanhao.Contains(Kddanhao));
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.Kddanhao.Contains(Kddanhao));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            var tempData = ob_ord_fahuodanservice.LoadOutList(custid, where.Compile()).ToList<ord_inventoryout_v>();
            DataTable dt = new DataTable();
            dt.Columns.Add("ChukudanBH", typeof(string));
            dt.Columns.Add("DDBH", typeof(string));
            dt.Columns.Add("Yunsongdizhi", typeof(string));
            dt.Columns.Add("ChukuRQ", typeof(string));
            dt.Columns.Add("Lianxiren", typeof(string));
            dt.Columns.Add("LianxiDH", typeof(string));
            dt.Columns.Add("Beizhu", typeof(string));
            dt.Columns.Add("CKCode", typeof(string));
            dt.Columns.Add("YunsongFS", typeof(string));
            dt.Columns.Add("Kddanhao", typeof(string));
            foreach (var item in tempData)
            {
                DataRow row = dt.NewRow();
                row["ChukudanBH"] = item.ChukudanBH;
                row["DDBH"] = item.DDBH;
                row["Yunsongdizhi"] = item.Yunsongdizhi;
                row["ChukuRQ"] = item.ChukuRQ == null ? "" : Convert.ToDateTime(item.ChukuRQ).ToString("yyyy-MM-dd");
                row["Lianxiren"] = item.Lianxiren;
                row["LianxiDH"] = item.LianxiDH;
                row["Beizhu"] = item.Beizhu;
                row["CKCode"] = item.CKCode;
                row["YunsongFS"] = item.YunsongFS;
                row["Kddanhao"] = item.Kddanhao;
                dt.Rows.Add(row);
            }
            DataSet ds = new DataSet();
            dt.TableName = "FaHuoDanList";
            ds.Tables.Add(dt);
            ExcelHelper.ExportExcel(ds, "FaHuoDanList");
            return new EmptyResult();
        }

        //   
        public ActionResult Fahuodandetails(int id)
        {
            //string _custid = (string)Session["customer_id"];
            var _dd = ob_ord_fahuodanservice.GetEntityById(p => p.ID == id && p.IsDelete == false);
            if (_dd == null)
                return View();
            //发货单号
            ViewBag.fhdh = _dd.ChukudanBH;
            //销售单号
            ViewBag.xsdh = _dd.KehuDH;
            //订单序号
            ViewBag.ddxh = _dd.DDID;
            //定单编号
            ViewBag.ddbh = _dd.DDBH;
            //发货日期
            ViewBag.fhrq = Convert.ToDateTime(_dd.ChukuRQ).ToString("yyyy-MM-dd");
            //备注
            ViewBag.bz = _dd.Beizhu;
            //制单日期
            ViewBag.zdrq = Convert.ToDateTime(_dd.MakeDate).ToString("yyyy-MM-dd");
            //联系人
            ViewBag.lxr = _dd.Lianxiren;
            //联系电话
            ViewBag.lxdh = _dd.LianxiDH;
            //运送地址
            ViewBag.ysdz = _dd.Yunsongdizhi;
            var _fhmx = ServiceFactory.ord_fahuomxservice.LoadSortEntities(p => p.ChukuID == _dd.ID && p.IsDelete == false, true, s => s.ShangpinDM).ToList();
            ViewBag.ord_fahuomx = _fhmx;
            return View();
        }


        //发货数量统计
        public ActionResult fahuodanlistQty(int id)
        {
            var tempData = ob_ord_fahuodanservice.LoadEntities(ord_fahuodan => ord_fahuodan.DDID == id && ord_fahuodan.IsDelete == false).ToList<ord_fahuodan>();

            var fahuomxData = new List<ord_fahuomx>();

            foreach (var ord_fahuodan in tempData)
            {
                var _fhmx = ServiceFactory.ord_fahuomxservice.LoadEntities(p => p.ChukuID == ord_fahuodan.ID && p.IsDelete == false).ToList();

                foreach (var item in _fhmx)
                {
                    fahuomxData.Add(item);
                }
            }

            ViewBag.ord_fahuomx = fahuomxData;
            return View(fahuomxData);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string chukudanbh = Request["chukudanbh"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string ddid = Request["ddid"] ?? "";
            string ddbh = Request["ddbh"] ?? "";
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string chukurq = Request["chukurq"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string ckcode = Request["ckcode"] ?? "";
            string yunsongfs = Request["yunsongfs"] ?? "";
            string kddanhao = Request["kddanhao"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                ord_fahuodan p = ob_ord_fahuodanservice.GetEntityById(ord_fahuodan => ord_fahuodan.ID == uid);
                p.ChukudanBH = chukudanbh.Trim();
                p.KehuDH = kehudh.Trim();
                p.DDID = ddid == "" ? 0 : int.Parse(ddid);
                p.DDBH = ddbh.Trim();
                p.Yunsongdizhi = yunsongdizhi.Trim();
                p.ChukuRQ = chukurq == "" ? DateTime.Now : DateTime.Parse(chukurq);
                p.Lianxiren = lianxiren.Trim();
                p.LianxiDH = lianxidh.Trim();
                p.Beizhu = beizhu.Trim();
                p.CKCode = ckcode.Trim();
                p.YunsongFS = yunsongfs.Trim();
                p.Kddanhao = kddanhao.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_fahuodanservice.UpdateEntity(p);
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
            ord_fahuodan ob_ord_fahuodan;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_fahuodan = ob_ord_fahuodanservice.GetEntityById(ord_fahuodan => ord_fahuodan.ID == id && ord_fahuodan.IsDelete == false);
                    ob_ord_fahuodan.IsDelete = true;
                    ob_ord_fahuodanservice.UpdateEntity(ob_ord_fahuodan);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

