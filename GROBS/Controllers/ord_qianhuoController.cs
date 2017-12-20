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
    public class ord_qianhuoController : Controller
    {
        private Iord_qianhuoService ob_ord_qianhuoservice = ServiceFactory.ord_qianhuoservice;
        private Iord_dingdanService ob_ord_dingdanservice = ServiceFactory.ord_dingdanservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_qianhuo_index";
            Expression<Func<ord_qianhuo, bool>> where = PredicateExtensionses.True<ord_qianhuo>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "mxid":
                            string mxid = scld[1];
                            string mxidequal = scld[2];
                            string mxidand = scld[3];
                            if (!string.IsNullOrEmpty(mxid))
                            {
                                if (mxidequal.Equals("="))
                                {
                                    if (mxidand.Equals("and"))
                                        where = where.And(ord_qianhuo => ord_qianhuo.MXID == int.Parse(mxid));
                                    else
                                        where = where.Or(ord_qianhuo => ord_qianhuo.MXID == int.Parse(mxid));
                                }
                                if (mxidequal.Equals(">"))
                                {
                                    if (mxidand.Equals("and"))
                                        where = where.And(ord_qianhuo => ord_qianhuo.MXID > int.Parse(mxid));
                                    else
                                        where = where.Or(ord_qianhuo => ord_qianhuo.MXID > int.Parse(mxid));
                                }
                                if (mxidequal.Equals("<"))
                                {
                                    if (mxidand.Equals("and"))
                                        where = where.And(ord_qianhuo => ord_qianhuo.MXID < int.Parse(mxid));
                                    else
                                        where = where.Or(ord_qianhuo => ord_qianhuo.MXID < int.Parse(mxid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_qianhuo => ord_qianhuo.IsDelete == false);

            var tempData = ob_ord_qianhuoservice.LoadSortEntities(where.Compile(), false, ord_qianhuo => ord_qianhuo.ID).ToPagedList<ord_qianhuo>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_qianhuo = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_qianhuo_index";
            string page = "1";
            string mxid = Request["mxid"] ?? "";
            string mxidequal = Request["mxidequal"] ?? "";
            string mxidand = Request["mxidand"] ?? "";
            Expression<Func<ord_qianhuo, bool>> where = PredicateExtensionses.True<ord_qianhuo>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(mxid))
                {
                    if (mxidequal.Equals("="))
                    {
                        if (mxidand.Equals("and"))
                            where = where.And(ord_qianhuo => ord_qianhuo.MXID == int.Parse(mxid));
                        else
                            where = where.Or(ord_qianhuo => ord_qianhuo.MXID == int.Parse(mxid));
                    }
                    if (mxidequal.Equals(">"))
                    {
                        if (mxidand.Equals("and"))
                            where = where.And(ord_qianhuo => ord_qianhuo.MXID > int.Parse(mxid));
                        else
                            where = where.Or(ord_qianhuo => ord_qianhuo.MXID > int.Parse(mxid));
                    }
                    if (mxidequal.Equals("<"))
                    {
                        if (mxidand.Equals("and"))
                            where = where.And(ord_qianhuo => ord_qianhuo.MXID < int.Parse(mxid));
                        else
                            where = where.Or(ord_qianhuo => ord_qianhuo.MXID < int.Parse(mxid));
                    }
                }
                if (!string.IsNullOrEmpty(mxid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mxid", mxid, mxidequal, mxidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mxid", "", mxidequal, mxidand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(mxid))
                {
                    if (mxidequal.Equals("="))
                    {
                        if (mxidand.Equals("and"))
                            where = where.And(ord_qianhuo => ord_qianhuo.MXID == int.Parse(mxid));
                        else
                            where = where.Or(ord_qianhuo => ord_qianhuo.MXID == int.Parse(mxid));
                    }
                    if (mxidequal.Equals(">"))
                    {
                        if (mxidand.Equals("and"))
                            where = where.And(ord_qianhuo => ord_qianhuo.MXID > int.Parse(mxid));
                        else
                            where = where.Or(ord_qianhuo => ord_qianhuo.MXID > int.Parse(mxid));
                    }
                    if (mxidequal.Equals("<"))
                    {
                        if (mxidand.Equals("and"))
                            where = where.And(ord_qianhuo => ord_qianhuo.MXID < int.Parse(mxid));
                        else
                            where = where.Or(ord_qianhuo => ord_qianhuo.MXID < int.Parse(mxid));
                    }
                }
                if (!string.IsNullOrEmpty(mxid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mxid", mxid, mxidequal, mxidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mxid", "", mxidequal, mxidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_qianhuo => ord_qianhuo.IsDelete == false);

            var tempData = ob_ord_qianhuoservice.LoadSortEntities(where.Compile(), false, ord_qianhuo => ord_qianhuo.ID).ToPagedList<ord_qianhuo>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_qianhuo = tempData;
            return View(tempData);
        }
        [OutputCache(Duration = 30)]
        public ActionResult CustomerOweList(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            int custid = (int)Session["customer_id"];
            string pagetag = "ord_dingdan_customerowelist";
            Expression<Func<ord_ordermain_vsss, bool>> where = PredicateExtensionses.True<ord_ordermain_vsss>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "bianhao":
                            string bianhao = scld[1];
                            string bianhaoequal = scld[2];
                            string bianhaoand = scld[3];
                            if (!string.IsNullOrEmpty(bianhao))
                            {
                                if (bianhaoequal.Equals("="))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.Bianhao == bianhao);
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.Bianhao == bianhao);
                                }
                                if (bianhaoequal.Equals("like"))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.Bianhao.Contains(bianhao));
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.Bianhao.Contains(bianhao));
                                }
                            }
                            break;
                        case "Zhuangtai":
                            string Zhuangtai = scld[1];
                            string Zhuangtaiequal = scld[2];
                            string Zhuangtaiand = scld[3];
                            if (!string.IsNullOrEmpty(Zhuangtai))
                            {
                                if (Zhuangtaiequal.Equals("="))
                                {
                                    if (Zhuangtaiand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.Zhuangtai == int.Parse(Zhuangtai));
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.Zhuangtai == int.Parse(Zhuangtai));
                                }
                            }
                            break;
                        case "Mingcheng":
                            string Mingcheng = scld[1];
                            string Mingchengequal = scld[2];
                            string Mingchengand = scld[3];
                            if (!string.IsNullOrEmpty(Mingcheng))
                            {
                                if (Mingchengequal.Equals("="))
                                {
                                    if (Mingchengand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.Mingcheng == Mingcheng);
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.Mingcheng == Mingcheng);
                                }
                            }
                            break;
                        case "CGLX":
                            string CGLX = scld[1];
                            string CGLXequal = scld[2];
                            string CGLXand = scld[3];
                            if (!string.IsNullOrEmpty(CGLX))
                            {
                                if (CGLXequal.Equals("="))
                                {
                                    if (CGLXand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.CGLX == int.Parse(CGLX));
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.CGLX == int.Parse(CGLX));
                                }
                            }
                            break;
                        case "KehuDH":
                            string KehuDH = scld[1];
                            string KehuDHequal = scld[2];
                            string KehuDHand = scld[3];
                            if (!string.IsNullOrEmpty(KehuDH))
                            {
                                if (KehuDHequal.Equals("="))
                                {
                                    if (KehuDHand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.KehuDH == KehuDH);
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.KehuDH == KehuDH);
                                }
                                if (KehuDHequal.Equals("like"))
                                {
                                    if (KehuDHand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.KehuDH.Contains(KehuDH));
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.KehuDH.Contains(KehuDH));
                                }
                            }
                            break;
                        case "XiadanRQ":
                            string XiadanRQ = scld[1];
                            string XiadanRQequal = scld[2];
                            string XiadanRQand = scld[3];
                            if (!string.IsNullOrEmpty(XiadanRQ))
                            {
                                if (XiadanRQequal.Equals("="))
                                {
                                    if (XiadanRQand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.XiadanRQ == DateTime.Parse(XiadanRQ));
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.XiadanRQ >= DateTime.Parse(XiadanRQ));
                                }
                                else if (XiadanRQequal.Equals(">"))
                                {
                                    if (XiadanRQand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.XiadanRQ > DateTime.Parse(XiadanRQ));
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.XiadanRQ > DateTime.Parse(XiadanRQ));
                                }
                                else
                                {
                                    if (XiadanRQand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.XiadanRQ < DateTime.Parse(XiadanRQ));
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.XiadanRQ < DateTime.Parse(XiadanRQ));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }
            where = where.And(ord_dingdan => ord_dingdan.Zhuangtai != 0);

            var tempData = ob_ord_dingdanservice.LoadCustomerActiveOwe(custid, where.Compile()).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_dingdan = tempData;
            return View(tempData);
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult CustomerOweList()
        {
            int userid = (int)Session["user_id"];
            int custid = (int)Session["customer_id"];
            string pagetag = "ord_dingdan_customerowelist";
            string page = "1";
            //编号
            string bianhao = Request["bianhao"] ?? "";
            string bianhaoequal = Request["bianhaoequal"] ?? "";
            string bianhaoand = Request["bianhaoand"] ?? "";
            //订单状态
            string Zhuangtai = Request["Zhuangtai"] ?? "";
            string Zhuangtaiequal = Request["ZhuangtaiEqual"] ?? "";
            string Zhuangtaiand = Request["Zhuangtaiand"] ?? "";
            //产品线
            string Mingcheng = Request["Mingcheng"] ?? "";
            string Mingchengequal = Request["Mingchengequal"] ?? "";
            string Mingchengand = Request["Mingchengand"] ?? "";
            //订单类型
            string CGLX = Request["CGLX"] ?? "";
            string CGLXequal = Request["CGLXequal"] ?? "";
            string CGLXand = Request["CGLXand"] ?? "";
            //客户单号
            string KehuDH = Request["KehuDH"] ?? "";
            string KehuDHequal = Request["KehuDHequal"] ?? "";
            string KehuDHand = Request["KehuDHand"] ?? "";
            //下单日期
            string XiadanRQ = Request["XiadanRQ"] ?? "";
            string XiadanRQequal = Request["XiadanRQequal"] ?? "";
            string XiadanRQand = Request["XiadanRQand"] ?? "";
            Expression<Func<ord_ordermain_vsss, bool>> where = PredicateExtensionses.True<ord_ordermain_vsss>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(bianhao))
                {
                    if (bianhaoequal.Equals("="))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(ord_ordermain_vsss => ord_ordermain_vsss.Bianhao == bianhao);
                        else
                            where = where.Or(ord_ordermain_vsss => ord_ordermain_vsss.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(ord_ordermain_vsss => ord_ordermain_vsss.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(ord_ordermain_vsss => ord_ordermain_vsss.Bianhao.Contains(bianhao));
                    }
                }
                if (!string.IsNullOrEmpty(bianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", bianhao, bianhaoequal, bianhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", "", bianhaoequal, bianhaoand);

                //订单状态
                if (!string.IsNullOrEmpty(Zhuangtai))
                {
                    if (Zhuangtaiequal.Equals("="))
                    {
                        if (Zhuangtaiand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.Zhuangtai == int.Parse(Zhuangtai));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.Zhuangtai == int.Parse(Zhuangtai));
                    }
                }
                if (!string.IsNullOrEmpty(Zhuangtai))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Zhuangtai", Zhuangtai, Zhuangtaiequal, Zhuangtaiand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Zhuangtai", "", Zhuangtaiequal, Zhuangtaiand);

                //产品线
                if (!string.IsNullOrEmpty(Mingcheng))
                {
                    if (Mingchengequal.Equals("="))
                    {
                        if (Mingchengand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.Mingcheng == Mingcheng);
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.Mingcheng == Mingcheng);
                    }
                }
                if (!string.IsNullOrEmpty(Mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Mingcheng", Mingcheng, Mingchengequal, Mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Mingcheng", "", Mingchengequal, Mingchengand);

                //订单类型
                if (!string.IsNullOrEmpty(CGLX))
                {
                    if (CGLXequal.Equals("="))
                    {
                        if (CGLXand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.CGLX == int.Parse(CGLX));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.CGLX == int.Parse(CGLX));
                    }
                }
                if (!string.IsNullOrEmpty(CGLX))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "CGLX", CGLX, CGLXequal, CGLXand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "CGLX", "", CGLXequal, CGLXand);

                //客户单号
                if (!string.IsNullOrEmpty(KehuDH))
                {
                    if (KehuDHequal.Equals("="))
                    {
                        if (KehuDHand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.KehuDH == KehuDH);
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.KehuDH == KehuDH);
                    }
                    if (KehuDHequal.Equals("like"))
                    {
                        if (KehuDHand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.KehuDH.Contains(KehuDH));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.KehuDH.Contains(KehuDH));
                    }
                }
                if (!string.IsNullOrEmpty(KehuDH))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "KehuDH", KehuDH, KehuDHequal, KehuDHand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "KehuDH", "", KehuDHequal, KehuDHand);

                //下单日期
                if (!string.IsNullOrEmpty(XiadanRQ))
                {
                    if (XiadanRQequal.Equals("="))
                    {
                        if (XiadanRQand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.XiadanRQ == DateTime.Parse(XiadanRQ));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.XiadanRQ == DateTime.Parse(XiadanRQ));
                    }
                    else if (XiadanRQequal.Equals(">"))
                    {
                        if (XiadanRQand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.XiadanRQ > DateTime.Parse(XiadanRQ));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.XiadanRQ > DateTime.Parse(XiadanRQ));
                    }
                    else
                    {
                        if (XiadanRQand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.XiadanRQ < DateTime.Parse(XiadanRQ));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.XiadanRQ < DateTime.Parse(XiadanRQ));
                    }

                }
                if (!string.IsNullOrEmpty(XiadanRQ))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "XiadanRQ", XiadanRQ, XiadanRQequal, XiadanRQand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(bianhao))
                {
                    if (bianhaoequal.Equals("="))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.Bianhao == bianhao);
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.Bianhao.Contains(bianhao));
                    }
                }
                if (!string.IsNullOrEmpty(bianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", bianhao, bianhaoequal, bianhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", "", bianhaoequal, bianhaoand);

                //订单状态
                if (!string.IsNullOrEmpty(Zhuangtai))
                {
                    if (Zhuangtaiequal.Equals("="))
                    {
                        if (Zhuangtaiand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.Zhuangtai == int.Parse(Zhuangtai));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.Zhuangtai == int.Parse(Zhuangtai));
                    }
                }
                if (!string.IsNullOrEmpty(Zhuangtai))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Zhuangtai", Zhuangtai, Zhuangtaiequal, Zhuangtaiand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Zhuangtai", "", Zhuangtaiequal, Zhuangtaiand);

                //产品线
                if (!string.IsNullOrEmpty(Mingcheng))
                {
                    if (Mingchengequal.Equals("="))
                    {
                        if (Mingchengand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.Mingcheng == Mingcheng);
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.Mingcheng == Mingcheng);
                    }
                }
                if (!string.IsNullOrEmpty(Mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Mingcheng", Mingcheng, Mingchengequal, Mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Mingcheng", "", Mingchengequal, Mingchengand);

                //订单类型
                if (!string.IsNullOrEmpty(CGLX))
                {
                    if (CGLXequal.Equals("="))
                    {
                        if (CGLXand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.CGLX == int.Parse(CGLX));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.CGLX == int.Parse(CGLX));
                    }
                }
                if (!string.IsNullOrEmpty(CGLX))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "CGLX", CGLX, CGLXequal, CGLXand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "CGLX", "", CGLXequal, CGLXand);

                //客户单号
                if (!string.IsNullOrEmpty(KehuDH))
                {
                    if (KehuDHequal.Equals("="))
                    {
                        if (KehuDHand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.KehuDH == KehuDH);
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.KehuDH == KehuDH);
                    }
                    if (KehuDHequal.Equals("like"))
                    {
                        if (KehuDHand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.KehuDH.Contains(KehuDH));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.KehuDH.Contains(KehuDH));
                    }
                }
                if (!string.IsNullOrEmpty(KehuDH))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "KehuDH", KehuDH, KehuDHequal, KehuDHand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "KehuDH", "", KehuDHequal, KehuDHand);

                //下单日期
                if (!string.IsNullOrEmpty(XiadanRQ))
                {
                    if (XiadanRQequal.Equals("="))
                    {
                        if (XiadanRQand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.XiadanRQ == DateTime.Parse(XiadanRQ));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.XiadanRQ >= DateTime.Parse(XiadanRQ));
                    }
                    else if (XiadanRQequal.Equals(">"))
                    {
                        if (XiadanRQand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.XiadanRQ > DateTime.Parse(XiadanRQ));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.XiadanRQ > DateTime.Parse(XiadanRQ));
                    }
                    else
                    {
                        if (XiadanRQand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.XiadanRQ < DateTime.Parse(XiadanRQ));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.XiadanRQ < DateTime.Parse(XiadanRQ));
                    }
                }
                if (!string.IsNullOrEmpty(XiadanRQ))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "XiadanRQ", XiadanRQ, XiadanRQequal, XiadanRQand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_dingdan => ord_dingdan.Zhuangtai != 0);

            var tempData = ob_ord_dingdanservice.LoadCustomerActiveOwe(custid, where.Compile()).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_dingdan = tempData;
            return View(tempData);
        }

        public ActionResult ExportCustomerOweList()
        {
            int userid = (int)Session["user_id"];
            int custid = (int)Session["customer_id"];
            string pagetag = "ord_dingdan_customerowelist";
            Expression<Func<ord_ordermain_vsss, bool>> where = PredicateExtensionses.True<ord_ordermain_vsss>();
            //searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid);
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "bianhao":
                            string bianhao = scld[1];
                            string bianhaoequal = scld[2];
                            string bianhaoand = scld[3];
                            if (!string.IsNullOrEmpty(bianhao))
                            {
                                if (bianhaoequal.Equals("="))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.Bianhao == bianhao);
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.Bianhao == bianhao);
                                }
                                if (bianhaoequal.Equals("like"))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.Bianhao.Contains(bianhao));
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.Bianhao.Contains(bianhao));
                                }
                            }
                            break;
                        case "Zhuangtai":
                            string Zhuangtai = scld[1];
                            string Zhuangtaiequal = scld[2];
                            string Zhuangtaiand = scld[3];
                            if (!string.IsNullOrEmpty(Zhuangtai))
                            {
                                if (Zhuangtaiequal.Equals("="))
                                {
                                    if (Zhuangtaiand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.Zhuangtai == int.Parse(Zhuangtai));
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.Zhuangtai == int.Parse(Zhuangtai));
                                }
                            }
                            break;
                        case "Mingcheng":
                            string Mingcheng = scld[1];
                            string Mingchengequal = scld[2];
                            string Mingchengand = scld[3];
                            if (!string.IsNullOrEmpty(Mingcheng))
                            {
                                if (Mingchengequal.Equals("="))
                                {
                                    if (Mingchengand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.Mingcheng == Mingcheng);
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.Mingcheng == Mingcheng);
                                }
                            }
                            break;
                        case "CGLX":
                            string CGLX = scld[1];
                            string CGLXequal = scld[2];
                            string CGLXand = scld[3];
                            if (!string.IsNullOrEmpty(CGLX))
                            {
                                if (CGLXequal.Equals("="))
                                {
                                    if (CGLXand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.CGLX == int.Parse(CGLX));
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.CGLX == int.Parse(CGLX));
                                }
                            }
                            break;
                        case "KehuDH":
                            string KehuDH = scld[1];
                            string KehuDHequal = scld[2];
                            string KehuDHand = scld[3];
                            if (!string.IsNullOrEmpty(KehuDH))
                            {
                                if (KehuDHequal.Equals("="))
                                {
                                    if (KehuDHand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.KehuDH == KehuDH);
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.KehuDH == KehuDH);
                                }
                                if (KehuDHequal.Equals("like"))
                                {
                                    if (KehuDHand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.KehuDH.Contains(KehuDH));
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.KehuDH.Contains(KehuDH));
                                }
                            }
                            break;
                        case "XiadanRQ":
                            string XiadanRQ = scld[1];
                            string XiadanRQequal = scld[2];
                            string XiadanRQand = scld[3];
                            if (!string.IsNullOrEmpty(XiadanRQ))
                            {
                                if (XiadanRQequal.Equals("="))
                                {
                                    if (XiadanRQand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.XiadanRQ == DateTime.Parse(XiadanRQ));
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.XiadanRQ >= DateTime.Parse(XiadanRQ));
                                }
                                else if (XiadanRQequal.Equals(">"))
                                {
                                    if (XiadanRQand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.XiadanRQ > DateTime.Parse(XiadanRQ));
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.XiadanRQ > DateTime.Parse(XiadanRQ));
                                }
                                else
                                {
                                    if (XiadanRQand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.XiadanRQ < DateTime.Parse(XiadanRQ));
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.XiadanRQ < DateTime.Parse(XiadanRQ));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_dingdan => ord_dingdan.Zhuangtai != 0);

            var tempData = ob_ord_dingdanservice.LoadCustomerActiveOwe(custid, where.Compile()).ToList<ord_ordermain_vsss>();
            DataTable dt = new DataTable();
            dt.Columns.Add("Bianhao", typeof(string));
            dt.Columns.Add("Zhuangtai", typeof(string));
            dt.Columns.Add("KehuMC", typeof(string));
            dt.Columns.Add("Mingcheng", typeof(string));
            dt.Columns.Add("CGLX", typeof(string));
            dt.Columns.Add("KehuDH", typeof(string));
            dt.Columns.Add("XiadanRQ", typeof(string));
            dt.Columns.Add("SPBM", typeof(string));
            dt.Columns.Add("CGSL", typeof(string));
            dt.Columns.Add("QHSL", typeof(string));
            dt.Columns.Add("XSDJ", typeof(string));
            dt.Columns.Add("Zhekou", typeof(string));
            dt.Columns.Add("Jine", typeof(string));
            dt.Columns.Add("Beizhu", typeof(string));
            foreach (var item in tempData)
            {
                DataRow row = dt.NewRow();
                row["Bianhao"] = item.Bianhao;
                row["Zhuangtai"] = MvcApplication.OrderState[(Int32)item.Zhuangtai];//订单状态
                row["KehuMC"] = item.KehuMC;
                row["Mingcheng"] = item.Mingcheng;
                row["CGLX"] = MvcApplication.OrderType[(Int32)item.CGLX];//订单类型
                row["KehuDH"] = item.KehuDH;
                row["XiadanRQ"] = item.XiadanRQ == null ? "" : Convert.ToDateTime(item.XiadanRQ).ToString("yyyy-MM-dd");
                row["SPBM"] = item.SPBM;
                row["CGSL"] = item.CGSL;
                row["QHSL"] = item.QHSL;
                row["XSDJ"] = item.XSDJ;
                row["Zhekou"] = item.Zhekou;
                row["Jine"] = item.Jine;
                row["Beizhu"] = item.Beizhu;
                dt.Rows.Add(row);
            }
            DataSet ds = new DataSet();
            dt.TableName = "CustomerOweList";
            ds.Tables.Add(dt);
            ExcelHelper.ExportExcel(ds, "CustomerOweList");
            return new EmptyResult();
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
            string mxid = Request["mxid"] ?? "";
            string qhsl = Request["qhsl"] ?? "";
            string djrq = Request["djrq"] ?? "";
            string caigousf = Request["caigousf"] ?? "";
            string caigoubh = Request["caigoubh"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                ord_qianhuo ob_ord_qianhuo = new ord_qianhuo();
                ob_ord_qianhuo.MXID = mxid == "" ? 0 : int.Parse(mxid);
                ob_ord_qianhuo.QHSL = qhsl == "" ? 0 : float.Parse(qhsl);
                ob_ord_qianhuo.DJRQ = djrq == "" ? DateTime.Now : DateTime.Parse(djrq);
                ob_ord_qianhuo.CaigouSF = caigousf == "" ? false : Boolean.Parse(caigousf);
                ob_ord_qianhuo.CaigouBH = caigoubh.Trim();
                ob_ord_qianhuo.Col1 = col1.Trim();
                ob_ord_qianhuo.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_qianhuo.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_qianhuo = ob_ord_qianhuoservice.AddEntity(ob_ord_qianhuo);
                ViewBag.ord_qianhuo = ob_ord_qianhuo;
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
            ord_qianhuo tempData = ob_ord_qianhuoservice.GetEntityById(ord_qianhuo => ord_qianhuo.ID == id && ord_qianhuo.IsDelete == false);
            ViewBag.ord_qianhuo = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_qianhuoViewModel ord_qianhuoviewmodel = new ord_qianhuoViewModel();
                ord_qianhuoviewmodel.ID = tempData.ID;
                ord_qianhuoviewmodel.MXID = tempData.MXID;
                ord_qianhuoviewmodel.QHSL = tempData.QHSL;
                ord_qianhuoviewmodel.DJRQ = tempData.DJRQ;
                ord_qianhuoviewmodel.CaigouSF = tempData.CaigouSF;
                ord_qianhuoviewmodel.CaigouBH = tempData.CaigouBH;
                ord_qianhuoviewmodel.Col1 = tempData.Col1;
                ord_qianhuoviewmodel.MakeDate = tempData.MakeDate;
                ord_qianhuoviewmodel.MakeMan = tempData.MakeMan;
                return View(ord_qianhuoviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string mxid = Request["mxid"] ?? "";
            string qhsl = Request["qhsl"] ?? "";
            string djrq = Request["djrq"] ?? "";
            string caigousf = Request["caigousf"] ?? "";
            string caigoubh = Request["caigoubh"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                ord_qianhuo p = ob_ord_qianhuoservice.GetEntityById(ord_qianhuo => ord_qianhuo.ID == uid);
                p.MXID = mxid == "" ? 0 : int.Parse(mxid);
                p.QHSL = qhsl == "" ? 0 : float.Parse(qhsl);
                p.DJRQ = djrq == "" ? DateTime.Now : DateTime.Parse(djrq);
                p.CaigouSF = caigousf == "" ? false : Boolean.Parse(caigousf);
                p.CaigouBH = caigoubh.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_qianhuoservice.UpdateEntity(p);
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
            ord_qianhuo ob_ord_qianhuo;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_qianhuo = ob_ord_qianhuoservice.GetEntityById(ord_qianhuo => ord_qianhuo.ID == id && ord_qianhuo.IsDelete == false);
                    ob_ord_qianhuo.IsDelete = true;
                    ob_ord_qianhuoservice.UpdateEntity(ob_ord_qianhuo);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

