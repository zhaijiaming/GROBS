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
        private Iord_dingdanmxService ob_ord_dingdanmxservice = ServiceFactory.ord_dingdanmxservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
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
            where = where.And(ord_dingdan => ord_dingdan.Zhuangtai != 0 && ord_dingdan.QHSL != 0);

            var tempData = ob_ord_dingdanservice.LoadOweAll(where.Compile()).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            foreach (var td in tempData)
            {
                WebReference.MStock xcl = new WebReference.MStock();
                //不属于套包
                if (td.CGLX != 2)
                {
                    float u8 = (float)xcl.GetCurrentStock(td.KehuDM, td.SPBM);
                    float num = 0;
                    float num_tb = 0;
                    try
                    {
                        var temp = ServiceFactory.ord_dingdanmxservice.LoadByItemCode(td.SPBM).ToList<ord_lockquantity_v>(); ;
                        var temp1 = ServiceFactory.ord_dingdanmxservice.LoadByItemCode_TB(td.SPBM).ToList<ord_lockquantitytb_v>(); ;
                        if (temp == null || temp[0].SPBM == null)
                        {
                            num = 0;
                        }
                        else
                        {
                            num = float.Parse(temp[0].SPBM);
                        }
                        if (temp1 == null || temp1[0].SPBM == null)
                        {
                            num_tb = 0;
                        }
                        else
                        {
                            num_tb = float.Parse(temp1[0].SPBM);
                        }
                        td.KYSL = u8 - num - num_tb;
                    }
                    catch
                    {
                        td.KYSL = 0;
                    }
                }
                //属于套包
                else if (td.CGLX == 2)
                {
                    try
                    {
                        var tb = ServiceFactory.base_taobaospservice.LoadPackageDetailByID(td.SPID).ToList<base_taobaosp_v>();
                        string tp1 = "";
                        string tp2 = "";
                        foreach (var tbs in tb)
                        {
                            float u8 = (float)xcl.GetCurrentStock(td.KehuDM, tbs.Daima);
                            float num = 0;
                            float num_tb = 0;
                            var temp = ServiceFactory.ord_dingdanmxservice.LoadByItemCode(tbs.Daima).ToList<ord_lockquantity_v>(); ;
                            var temp1 = ServiceFactory.ord_dingdanmxservice.LoadByItemCode_TB(tbs.Daima).ToList<ord_lockquantitytb_v>(); ;
                            if (temp == null || temp[0].SPBM == null)
                            {
                                num = 0;
                            }
                            else
                            {
                                num = float.Parse(temp[0].SPBM);
                            }
                            if (temp1 == null || temp1[0].SPBM == null)
                            {
                                num_tb = 0;
                            }
                            else
                            {
                                num_tb = float.Parse(temp1[0].SPBM);
                            }
                            tp1 = tp1 + (u8 - num - num_tb).ToString() + ",";
                            tp2 = tp2 + tbs.Shuliang.ToString() + ",";
                        }
                        td.KYSL = getavli_tb(tp1, tp2);
                    }
                    catch (Exception ex)
                    {
                        td.KYSL = 0;
                    }
                }
            }
            ViewBag.ord_qianhuo = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_dingdan_customerowelist";
            string page = "1";
            //编号
            string bianhao = Request["bianhao"] ?? "";
            string bianhaoequal = Request["bianhaoequal"] ?? "";
            string bianhaoand = Request["bianhaoand"] ?? "and";
            //订单状态
            string Zhuangtai = Request["Zhuangtai"] ?? "";
            string Zhuangtaiequal = Request["ZhuangtaiEqual"] ?? "";
            string Zhuangtaiand = Request["Zhuangtaiand"] ?? "and";
            //产品线
            string Mingcheng = Request["Mingcheng"] ?? "";
            string Mingchengequal = Request["Mingchengequal"] ?? "";
            string Mingchengand = Request["Mingchengand"] ?? "and";
            //订单类型
            string CGLX = Request["CGLX"] ?? "";
            string CGLXequal = Request["CGLXequal"] ?? "";
            string CGLXand = Request["CGLXand"] ?? "and";
            //客户单号
            string KehuDH = Request["KehuDH"] ?? "";
            string KehuDHequal = Request["KehuDHequal"] ?? "";
            string KehuDHand = Request["KehuDHand"] ?? "and";
            //下单日期
            string XiadanRQ = Request["XiadanRQ"] ?? "";
            string XiadanRQequal = Request["XiadanRQequal"] ?? "";
            string XiadanRQand = Request["XiadanRQand"] ?? "and";
            Expression<Func<ord_ordermain_vsss, bool>> where = PredicateExtensionses.True<ord_ordermain_vsss>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {

                #region MyRegion

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

                #endregion

            }
            else
            {
                    #region MyRegion

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

                    #endregion
                
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_dingdan => ord_dingdan.Zhuangtai != 0 && ord_dingdan.QHSL != 0);

            var tempData = ob_ord_dingdanservice.LoadOweAll(where.Compile()).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            foreach (var td in tempData)
            {
                WebReference.MStock xcl = new WebReference.MStock();
                //不属于套包
                if (td.CGLX != 2)
                {
                    float u8 = (float)xcl.GetCurrentStock(td.KehuDM, td.SPBM);
                    float num = 0;
                    float num_tb = 0;
                    try
                    {
                        var temp = ServiceFactory.ord_dingdanmxservice.LoadByItemCode(td.SPBM).ToList<ord_lockquantity_v>(); ;
                        var temp1 = ServiceFactory.ord_dingdanmxservice.LoadByItemCode_TB(td.SPBM).ToList<ord_lockquantitytb_v>(); ;
                        if (temp == null || temp[0].SPBM == null)
                        {
                            num = 0;
                        }
                        else
                        {
                            num = float.Parse(temp[0].SPBM);
                        }
                        if (temp1 == null || temp1[0].SPBM == null)
                        {
                            num_tb = 0;
                        }
                        else
                        {
                            num_tb = float.Parse(temp1[0].SPBM);
                        }
                        td.KYSL = u8 - num - num_tb;
                    }
                    catch
                    {
                        td.KYSL = 0;
                    }
                }
                //属于套包
                else if (td.CGLX == 2)
                {
                    try
                    {
                        var tb = ServiceFactory.base_taobaospservice.LoadPackageDetailByID(td.SPID).ToList<base_taobaosp_v>();
                        string tp1 = "";
                        string tp2 = "";
                        foreach (var tbs in tb)
                        {
                            float u8 = (float)xcl.GetCurrentStock(td.KehuDM, tbs.Daima);
                            float num = 0;
                            float num_tb = 0;
                            var temp = ServiceFactory.ord_dingdanmxservice.LoadByItemCode(tbs.Daima).ToList<ord_lockquantity_v>(); ;
                            var temp1 = ServiceFactory.ord_dingdanmxservice.LoadByItemCode_TB(tbs.Daima).ToList<ord_lockquantitytb_v>(); ;
                            if (temp == null || temp[0].SPBM == null)
                            {
                                num = 0;
                            }
                            else
                            {
                                num = float.Parse(temp[0].SPBM);
                            }
                            if (temp1 == null || temp1[0].SPBM == null)
                            {
                                num_tb = 0;
                            }
                            else
                            {
                                num_tb = float.Parse(temp1[0].SPBM);
                            }
                            tp1 = tp1 + (u8 - num - num_tb).ToString() + ",";
                            tp2 = tp2 + tbs.Shuliang.ToString() + ",";
                        }
                        td.KYSL = getavli_tb(tp1, tp2);
                    }
                    catch (Exception ex)
                    {
                        td.KYSL = 0;
                    }
                }
            }
            ViewBag.ord_qianhuo = tempData;
            return View(tempData);
        }
        [OutputCache(Duration = 30)]
        public ActionResult CustomerOweList(string page, string sortOrder)
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


            #region 排序

            ViewBag.BianhaoParm = string.IsNullOrEmpty(sortOrder) ? "Bianhao" : sortOrder.Equals("Bianhao_desc") ? "Bianhao" :"Bianhao_desc";
            ViewBag.ZhuangtaiParm = sortOrder == "Zhuangtai" ? "Zhuangtai_desc" : "Zhuangtai";
            ViewBag.MingchengParm = sortOrder == "Mingcheng" ? "Mingcheng_desc" : "Mingcheng";
            ViewBag.CGLXParm = sortOrder == "CGLX" ? "CGLX_desc" : "CGLX";
            ViewBag.KehuDHParm = sortOrder == "KehuDH" ? "KehuDH_desc" : "KehuDH";
            ViewBag.XiadanRQParm = sortOrder == "XiadanRQ" ? "XiadanRQ_desc" : "XiadanRQ";
            ViewBag.SPBMParm = sortOrder == "SPBM" ? "SPBM_desc" : "SPBM";
            switch (sortOrder)
            {
                case "SPBM_desc":
                    tempData = tempData.OrderByDescending(p => p.SPBM).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "SPBM":
                    tempData = tempData.OrderBy(p => p.SPBM).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "XiadanRQ_desc":
                    tempData = tempData.OrderByDescending(p => p.XiadanRQ).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "XiadanRQ":
                    tempData = tempData.OrderBy(p => p.XiadanRQ).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "KehuDH_desc":
                    tempData = tempData.OrderByDescending(p => p.KehuDH).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "KehuDH":
                    tempData = tempData.OrderBy(p => p.KehuDH).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "CGLX_desc":
                    tempData = tempData.OrderByDescending(p => p.CGLX).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "CGLX":
                    tempData = tempData.OrderBy(p => p.CGLX).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Mingcheng_desc":
                    tempData = tempData.OrderByDescending(p => p.Mingcheng).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Mingcheng":
                    tempData = tempData.OrderBy(p => p.Mingcheng).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Zhuangtai_desc":
                    tempData = tempData.OrderByDescending(p => p.Zhuangtai).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Zhuangtai":
                    tempData = tempData.OrderBy(p => p.Zhuangtai).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Bianhao":
                    tempData = tempData.OrderBy(p => p.Bianhao).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                default:
                    tempData = tempData.OrderByDescending(p => p.Bianhao).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
            }

            #endregion

            ViewBag.ord_dingdan = tempData;
            return View(tempData);
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult CustomerOweList(string sortOrder)
        {
            int userid = (int)Session["user_id"];
            int custid = (int)Session["customer_id"];
            string pagetag = "ord_dingdan_customerowelist";
            string page = "1";
            //编号
            string bianhao = Request["bianhao"] ?? "";
            string bianhaoequal = Request["bianhaoequal"] ?? "";
            string bianhaoand = Request["bianhaoand"] ?? "and";
            //订单状态
            string Zhuangtai = Request["Zhuangtai"] ?? "";
            string Zhuangtaiequal = Request["ZhuangtaiEqual"] ?? "";
            string Zhuangtaiand = Request["Zhuangtaiand"] ?? "and";
            //产品线
            string Mingcheng = Request["Mingcheng"] ?? "";
            string Mingchengequal = Request["Mingchengequal"] ?? "";
            string Mingchengand = Request["Mingchengand"] ?? "and";
            //订单类型
            string CGLX = Request["CGLX"] ?? "";
            string CGLXequal = Request["CGLXequal"] ?? "";
            string CGLXand = Request["CGLXand"] ?? "and";
            //客户单号
            string KehuDH = Request["KehuDH"] ?? "";
            string KehuDHequal = Request["KehuDHequal"] ?? "";
            string KehuDHand = Request["KehuDHand"] ?? "and";
            //下单日期
            string XiadanRQ = Request["XiadanRQ"] ?? "";
            string XiadanRQequal = Request["XiadanRQequal"] ?? "";
            string XiadanRQand = Request["XiadanRQand"] ?? "and";
            Expression<Func<ord_ordermain_vsss, bool>> where = PredicateExtensionses.True<ord_ordermain_vsss>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {

                #region MyRegion
               
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

                #endregion

            }
            else
            {

                if (!string.IsNullOrEmpty(sortOrder))
                {
                    where = GetOrderListSearchCondition(where, sc);
                }
                else
                {

                    #region MyRegion

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

                    #endregion
                }
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_dingdan => ord_dingdan.Zhuangtai != 0);

            var tempData = ob_ord_dingdanservice.LoadCustomerActiveOwe(custid, where.Compile()).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));

            #region 排序

            ViewBag.BianhaoParm = string.IsNullOrEmpty(sortOrder) ? "Bianhao" : sortOrder.Equals("Bianhao_desc") ? "Bianhao" : "Bianhao_desc";
            ViewBag.ZhuangtaiParm = sortOrder == "Zhuangtai" ? "Zhuangtai_desc" : "Zhuangtai";
            ViewBag.MingchengParm = sortOrder == "Mingcheng" ? "Mingcheng_desc" : "Mingcheng";
            ViewBag.CGLXParm = sortOrder == "CGLX" ? "CGLX_desc" : "CGLX";
            ViewBag.KehuDHParm = sortOrder == "KehuDH" ? "KehuDH_desc" : "KehuDH";
            ViewBag.XiadanRQParm = sortOrder == "XiadanRQ" ? "XiadanRQ_desc" : "XiadanRQ";
            ViewBag.SPBMParm = sortOrder == "SPBM" ? "SPBM_desc" : "SPBM";
            switch (sortOrder)
            {
                case "SPBM_desc":
                    tempData = tempData.OrderByDescending(p => p.SPBM).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "SPBM":
                    tempData = tempData.OrderBy(p => p.SPBM).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "XiadanRQ_desc":
                    tempData = tempData.OrderByDescending(p => p.XiadanRQ).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "XiadanRQ":
                    tempData = tempData.OrderBy(p => p.XiadanRQ).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "KehuDH_desc":
                    tempData = tempData.OrderByDescending(p => p.KehuDH).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "KehuDH":
                    tempData = tempData.OrderBy(p => p.KehuDH).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "CGLX_desc":
                    tempData = tempData.OrderByDescending(p => p.CGLX).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "CGLX":
                    tempData = tempData.OrderBy(p => p.CGLX).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Mingcheng_desc":
                    tempData = tempData.OrderByDescending(p => p.Mingcheng).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Mingcheng":
                    tempData = tempData.OrderBy(p => p.Mingcheng).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Zhuangtai_desc":
                    tempData = tempData.OrderByDescending(p => p.Zhuangtai).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Zhuangtai":
                    tempData = tempData.OrderBy(p => p.Zhuangtai).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Bianhao":
                    tempData = tempData.OrderBy(p => p.Bianhao).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                default:
                    tempData = tempData.OrderByDescending(p => p.Bianhao).ToPagedList<ord_ordermain_vsss>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
            }

            #endregion


            ViewBag.ord_dingdan = tempData;
            return View(tempData);
        }

        private static Expression<Func<ord_ordermain_vsss, bool>> GetOrderListSearchCondition(Expression<Func<ord_ordermain_vsss, bool>> where, searchcondition sc)
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

            return where;
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

        //欠货数量统计
        public ActionResult customerowelistQty(int id)
        {
            int userid = (int)Session["user_id"];
            int custid = (int)Session["customer_id"];

            var tempData = ob_ord_dingdanservice.LoadCustomerActiveOwe(custid, p => p.DDID == id).ToList<ord_ordermain_vsss>();

            ViewBag.ord_qianhuoData = tempData;
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

        public JsonResult Ddmake()
        {
            int _userid = (int)Session["user_id"];
            var sid = Request["sid"] ?? "";
            var kedh = Request["kedh"] ?? "";
            var sl = Request["sl"] ?? "";
            var je = Request["je"] ?? "";
            var ddbh = Request["ddbh"] ?? "";
            var bz = Request["bz"] ?? "";
            string[] mxid = sid.Split(',');
             
            if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(kedh))
                return Json(-1);
            ord_dingdan dingdan = ob_ord_dingdanservice.GetEntityById(p => p.Bianhao == ddbh && p.IsDelete ==false);
            if (dingdan == null)
            { return Json(-2); }
            //ord_dingdan dingdan_n = new ord_dingdan();
            dingdan.ZongshuCG = float.Parse(sl);
            dingdan.Zongjine = decimal.Parse(je);
            dingdan.XiadanRQ = DateTime.Now;
            dingdan.ZhekouJE = 0;
            dingdan.Beizhu = bz;
            dingdan.Zhuangtai = 30;
            dingdan.MakeDate = DateTime.Now;
            dingdan.MakeMan = _userid;
            dingdan.ShenheSJ = DateTime.Now;
            dingdan.ShenheTG = true;
            dingdan.HH = mxid.Count() - 1;
            dingdan.Col1 = "1";
            dingdan = ob_ord_dingdanservice.AddEntity(dingdan);
            try
            {
                foreach (string sD in sid.Split(','))
                {
                    if (sD.Length > 0)
                    {
                        ord_qianhuo qianhuo = ob_ord_qianhuoservice.GetEntityById(ord_qianhuo => ord_qianhuo.ID == int.Parse(sD) && ord_qianhuo.IsDelete == false);
                        if (qianhuo == null)
                        {
                            return Json(-1);
                        }
                        else
                        {
                            ord_dingdanmx dingdanmx = ob_ord_dingdanmxservice.GetEntityById(p => p.ID == qianhuo.MXID && p.IsDelete == false);
                            //ord_dingdanmx _mx = new ord_dingdanmx();
                            float? num = dingdanmx.CGSL - dingdanmx.PFSL;
                            dingdanmx.DDID = dingdan.ID;
                            dingdanmx.CGSL = num;
                            dingdanmx.PFSL = num;
                            dingdanmx.FHSL = 0;
                            dingdanmx.Danjia = dingdanmx.XSDJ;
                            dingdanmx.Jine = (decimal)num * dingdanmx.XSDJ;
                            dingdanmx.Zhekou = 0;
                            dingdanmx.Zhekoulv = 1;
                            dingdanmx.MakeDate = DateTime.Now;
                            dingdanmx.MakeMan = _userid;
                            dingdanmx = ServiceFactory.ord_dingdanmxservice.AddEntity(dingdanmx);

                            qianhuo.QHSL = 0;
                            ob_ord_qianhuoservice.UpdateEntity(qianhuo);
                        }
                    }
                }
                return Json(1);
            }
            catch (Exception ex)
            {
                dingdan.IsDelete = true;
                ob_ord_dingdanservice.UpdateEntity(dingdan);
                return Json(-3);
            }          
        }
        public int getavli_tb(string tp1, string tp2)
        {
            string[] str1 = tp1.Split(',');
            string[] str2 = tp2.Split(',');
            int nn = 100000;
            for (int i = 0; i < str1.Length - 1; i++)
            {
                int gr = int.Parse(str1[i]) / int.Parse(str2[i]);
                nn = nn > gr ? gr : nn;
            }
            return nn;
        }
    }
}

