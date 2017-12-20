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
        public ActionResult Fahuodanlist(string page)
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
                        case "ChukudanBH"://发货单号
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

                        //case "DDBH"://订单编号
                        //    string DDBH = scld[1];
                        //    string DDBHequal = scld[2];
                        //    string DDBHand = scld[3];
                        //    if (!string.IsNullOrEmpty(DDBH))
                        //    {
                        //        if (chukudanbhequal.Equals("="))
                        //        {
                        //            if (chukudanbhand.Equals("and"))
                        //                where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                        //            else
                        //                where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                        //        }
                        //        if (chukudanbhequal.Equals("like"))
                        //        {
                        //            if (chukudanbhand.Equals("and"))
                        //                where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                        //            else
                        //                where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                        //        }
                        //    }
                        //    break

                        //        case "chukudanbh":
                        //    string chukudanbh = scld[1];
                        //    string chukudanbhequal = scld[2];
                        //    string chukudanbhand = scld[3];
                        //    if (!string.IsNullOrEmpty(chukudanbh))
                        //    {
                        //        if (chukudanbhequal.Equals("="))
                        //        {
                        //            if (chukudanbhand.Equals("and"))
                        //                where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                        //            else
                        //                where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                        //        }
                        //        if (chukudanbhequal.Equals("like"))
                        //        {
                        //            if (chukudanbhand.Equals("and"))
                        //                where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                        //            else
                        //                where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                        //        }
                        //    }
                        //    break

                        //        case "chukudanbh":
                        //    string chukudanbh = scld[1];
                        //    string chukudanbhequal = scld[2];
                        //    string chukudanbhand = scld[3];
                        //    if (!string.IsNullOrEmpty(chukudanbh))
                        //    {
                        //        if (chukudanbhequal.Equals("="))
                        //        {
                        //            if (chukudanbhand.Equals("and"))
                        //                where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                        //            else
                        //                where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                        //        }
                        //        if (chukudanbhequal.Equals("like"))
                        //        {
                        //            if (chukudanbhand.Equals("and"))
                        //                where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                        //            else
                        //                where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                        //        }
                        //    }
                        //    break

                        //        case "chukudanbh":
                        //    string chukudanbh = scld[1];
                        //    string chukudanbhequal = scld[2];
                        //    string chukudanbhand = scld[3];
                        //    if (!string.IsNullOrEmpty(chukudanbh))
                        //    {
                        //        if (chukudanbhequal.Equals("="))
                        //        {
                        //            if (chukudanbhand.Equals("and"))
                        //                where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                        //            else
                        //                where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                        //        }
                        //        if (chukudanbhequal.Equals("like"))
                        //        {
                        //            if (chukudanbhand.Equals("and"))
                        //                where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                        //            else
                        //                where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                        //        }
                        //    }
                        //    break
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            //where = where.And(ord_fahuodan => ord_fahuodan.IsDelete == false);

            //var tempData = ob_ord_fahuodanservice.LoadOutList(custid,where.Compile(), false, ord_fahuodan => ord_fahuodan.ID).ToPagedList<ord_sendlist_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            var tempData = ob_ord_fahuodanservice.LoadOutList(custid, where.Compile()).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_fahuodan = tempData;
            return View(tempData);
        }


        public ActionResult Exportfahuodanlist()
        {
            //if (string.IsNullOrEmpty(page))
            //    page = "1";
            int userid = (int)Session["user_id"];
            int custid = (int)Session["customer_id"];
            //string pagetag = "ord_fahuodan_index";
            Expression<Func<ord_inventoryout_v, bool>> where = PredicateExtensionses.True<ord_inventoryout_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid);
            //searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
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
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            var tempData = ob_ord_fahuodanservice.LoadOutList(custid, where.Compile()).ToList<ord_inventoryout_v>();
            //var tempData = ob_ord_fahuodanservice.LoadOutList(custid, where.Compile()).ToPagedList<ord_inventoryout_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
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

