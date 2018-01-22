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
using System.Web;

namespace GROBS.Controllers
{
    public class ord_dingdanController : Controller
    {
        private Iord_dingdanService ob_ord_dingdanservice = ServiceFactory.ord_dingdanservice;
        private Iord_dingdanmxService ob_ord_dingdanmxservice = ServiceFactory.ord_dingdanmxservice;
        private Iord_fahuodanService ob_ord_fahuodanservice = ServiceFactory.ord_fahuodanservice;
        private Iord_fahuomxService ob_ord_fahuomxservice = ServiceFactory.ord_fahuomxservice;
        private Iord_qianhuoService ob_ord_qianhuoservice = ServiceFactory.ord_qianhuoservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_dingdan_index";
            Expression<Func<ord_dingdan, bool>> where = PredicateExtensionses.True<ord_dingdan>();
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
                                        where = where.And(ord_dingdan => ord_dingdan.CPXID == int.Parse(Mingcheng));
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.CPXID == int.Parse(Mingcheng));
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

            where = where.And(ord_dingdan => ord_dingdan.IsDelete == false && ord_dingdan.Zhuangtai > 0);

            var tempData = ob_ord_dingdanservice.LoadSortEntitiesNoTracking(where.Compile(), false, ord_dingdan => ord_dingdan.ID).ToPagedList<ord_dingdan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_dingdan = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_dingdan_index";
            string page = "1";
            string bianhao = Request["bianhao"] ?? "";
            string bianhaoequal = Request["bianhaoequal"] ?? "";
            string bianhaoand = Request["bianhaoand"] ?? "";
            string Zhuangtai = Request["Zhuangtai"] ?? "";
            string Zhuangtaiequal = Request["Zhuangtaiequal"] ?? "";
            string Zhuangtaiand = Request["Zhuangtaiand"] ?? "";
            string Mingcheng = Request["Mingcheng"] ?? "";
            string Mingchengequal = Request["Mingchengequal"] ?? "";
            string Mingchengand = Request["Mingchengand"] ?? "";
            string CGLX = Request["CGLX"] ?? "";
            string CGLXequal = Request["CGLXequal"] ?? "";
            string CGLXand = Request["CGLXand"] ?? "";
            string KehuDH = Request["KehuDH"] ?? "";
            string KehuDHequal = Request["KehuDHequal"] ?? "";
            string KehuDHand = Request["KehuDHand"] ?? "";
            string XiadanRQ = Request["XiadanRQ"] ?? "";
            string XiadanRQequal = Request["XiadanRQequal"] ?? "";
            string XiadanRQand = Request["XiadanRQand"] ?? "";
            Expression<Func<ord_dingdan, bool>> where = PredicateExtensionses.True<ord_dingdan>();
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

                if (!string.IsNullOrEmpty(Mingcheng))
                {
                    if (Mingchengequal.Equals("="))
                    {
                        if (Mingchengand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.CPXID == int.Parse(Mingcheng));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.CPXID == int.Parse(Mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(Mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Mingcheng", Mingcheng, Mingchengequal, Mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Mingcheng", "", Mingchengequal, Mingchengand);


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
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "XiadanRQ", "", XiadanRQequal, XiadanRQand);

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

                if (!string.IsNullOrEmpty(Mingcheng))
                {
                    if (Mingchengequal.Equals("="))
                    {
                        if (Mingchengand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.CPXID == int.Parse(Mingcheng));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.CPXID == int.Parse(Mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(Mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Mingcheng", Mingcheng, Mingchengequal, Mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "Mingcheng", "", Mingchengequal, Mingchengand);


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
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "XiadanRQ", "", XiadanRQequal, XiadanRQand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_dingdan => ord_dingdan.IsDelete == false && ord_dingdan.Zhuangtai > 0);


            var tempData = ob_ord_dingdanservice.LoadSortEntitiesNoTracking(where.Compile(), false, ord_dingdan => ord_dingdan.ID).ToPagedList<ord_dingdan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_dingdan = tempData;
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
            string bianhao = Request["bianhao"] ?? "";
            string khid = Request["khid"] ?? "";
            string cpxid = Request["cpxid"] ?? "";
            string cglx = Request["cglx"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string xiadanrq = Request["xiadanrq"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string songhuodz = Request["songhuodz"] ?? "";
            string opid = Request["opid"] ?? "";
            string accid = Request["accid"] ?? "";
            string shenhetg = Request["shenhetg"] ?? "";
            string zongshucg = Request["zongshucg"] ?? "";
            string zongjine = Request["zongjine"] ?? "";
            string jieshusf = Request["jieshusf"] ?? "";
            string zhuangtai = Request["zhuangtai"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string kehudm = Request["kehudm"] ?? "";
            string cuoxiaozk = Request["cuoxiaozk"] ?? "";
            string shenhesj = Request["shenhesj"] ?? "";
            string zhekouje = Request["zhekouje"] ?? "";
            try
            {
                ord_dingdan ob_ord_dingdan = new ord_dingdan();
                ob_ord_dingdan.Bianhao = bianhao.Trim();
                ob_ord_dingdan.KHID = khid == "" ? 0 : int.Parse(khid);
                ob_ord_dingdan.CPXID = cpxid == "" ? 0 : int.Parse(cpxid);
                ob_ord_dingdan.CGLX = cglx == "" ? 0 : int.Parse(cglx);
                ob_ord_dingdan.KehuDH = kehudh.Trim();
                //ob_ord_dingdan.XiadanRQ = xiadanrq == "" ? DateTime.Now : DateTime.Parse(xiadanrq);
                ob_ord_dingdan.XiadanRQ = DateTime.Now;
                ob_ord_dingdan.Lianxiren = lianxiren.Trim();
                ob_ord_dingdan.LianxiDH = lianxidh.Trim();
                ob_ord_dingdan.SonghuoDZ = songhuodz.Trim();
                ob_ord_dingdan.OPID = opid == "" ? 0 : int.Parse(opid);
                ob_ord_dingdan.ACCID = accid == "" ? 0 : int.Parse(accid);
                ob_ord_dingdan.ShenheTG = shenhetg == "" ? false : Boolean.Parse(shenhetg);
                ob_ord_dingdan.ZongshuCG = zongshucg == "" ? 0 : float.Parse(zongshucg);
                ob_ord_dingdan.Zongjine = zongjine == "" ? 0 : decimal.Parse(zongjine);
                ob_ord_dingdan.JieshuSF = jieshusf == "" ? false : Boolean.Parse(jieshusf);
                ob_ord_dingdan.Zhuangtai = zhuangtai == "" ? 0 : int.Parse(zhuangtai);
                ob_ord_dingdan.Beizhu = beizhu.Trim();
                ob_ord_dingdan.Col1 = col1.Trim();
                ob_ord_dingdan.Col2 = col2.Trim();
                ob_ord_dingdan.Col3 = col3.Trim();
                //ob_ord_dingdan.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_dingdan.MakeDate = DateTime.Now;
                ob_ord_dingdan.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_dingdan.KehuDM = kehudm.Trim();
                ob_ord_dingdan.CuoxiaoZK = cuoxiaozk == "" ? 0 : decimal.Parse(cuoxiaozk);
                ob_ord_dingdan.ShenheSJ = shenhesj == "" ? DateTime.Now : DateTime.Parse(shenhesj);
                ob_ord_dingdan.ZhekouJE = zhekouje == "" ? 0 : decimal.Parse(zhekouje);
                ob_ord_dingdan = ob_ord_dingdanservice.AddEntity(ob_ord_dingdan);
                id = ob_ord_dingdan.ID.ToString();
                ViewBag.ord_dingdan = ob_ord_dingdan;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Edit", new { id = int.Parse(id) });
        }

        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            ord_dingdan tempData = ob_ord_dingdanservice.GetEntityById(ord_dingdan => ord_dingdan.ID == id && ord_dingdan.IsDelete == false);
            ViewBag.ord_dingdan = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_dingdanViewModel ord_dingdanviewmodel = new ord_dingdanViewModel();
                ord_dingdanviewmodel.ID = tempData.ID;
                ord_dingdanviewmodel.Bianhao = tempData.Bianhao;
                ord_dingdanviewmodel.KHID = tempData.KHID;
                ord_dingdanviewmodel.CPXID = tempData.CPXID;
                ord_dingdanviewmodel.CGLX = tempData.CGLX;
                ord_dingdanviewmodel.KehuDH = tempData.KehuDH;
                ord_dingdanviewmodel.XiadanRQ = tempData.XiadanRQ;
                ord_dingdanviewmodel.Lianxiren = tempData.Lianxiren;
                ord_dingdanviewmodel.LianxiDH = tempData.LianxiDH;
                ord_dingdanviewmodel.SonghuoDZ = tempData.SonghuoDZ;
                ord_dingdanviewmodel.OPID = tempData.OPID;
                ord_dingdanviewmodel.ACCID = tempData.ACCID;
                ord_dingdanviewmodel.ShenheTG = tempData.ShenheTG;
                ord_dingdanviewmodel.ZongshuCG = tempData.ZongshuCG;
                ord_dingdanviewmodel.Zongjine = tempData.Zongjine;
                ord_dingdanviewmodel.JieshuSF = tempData.JieshuSF;
                ord_dingdanviewmodel.Zhuangtai = tempData.Zhuangtai;
                ord_dingdanviewmodel.Beizhu = tempData.Beizhu;
                ord_dingdanviewmodel.Col1 = tempData.Col1;
                ord_dingdanviewmodel.Col2 = tempData.Col2;
                ord_dingdanviewmodel.Col3 = tempData.Col3;
                ord_dingdanviewmodel.MakeDate = tempData.MakeDate;
                ord_dingdanviewmodel.MakeMan = tempData.MakeMan;
                ord_dingdanviewmodel.KehuDM = tempData.KehuDM;
                ord_dingdanviewmodel.CuoxiaoZK = tempData.CuoxiaoZK;
                ord_dingdanviewmodel.ShenheSJ = tempData.ShenheSJ;
                ord_dingdanviewmodel.ZhekouJE = tempData.ZhekouJE;
                ord_dingdanviewmodel.FKPZ = tempData.FKPZ;
                return View(ord_dingdanviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            int _userid = (int)Session["user_id"];
            string id = Request["id"] ?? "";
            string bianhao = Request["bianhao"] ?? "";
            string khid = Request["khid"] ?? "";
            string cpxid = Request["cpxid"] ?? "";
            string cglx = Request["cglx"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string xiadanrq = Request["xiadanrq"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string songhuodz = Request["songhuodz"] ?? "";
            string opid = Request["opid"] ?? "";
            string accid = Request["accid"] ?? "";
            string shenhetg = Request["shenhetg"] ?? "";
            string zongshucg = Request["zongshucg"] ?? "";
            string zongjine = Request["zongjine"] ?? "";
            string jieshusf = Request["jieshusf"] ?? "";
            string zhuangtai = Request["zhuangtai"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string kehudm = Request["kehudm"] ?? "";
            string cuoxiaozk = Request["cuoxiaozk"] ?? "";
            string shenhesj = Request["shenhesj"] ?? "";
            string zhekouje = Request["zhekouje"] ?? "";
            int uid = int.Parse(id);
            try
            {
                ord_dingdan p = ob_ord_dingdanservice.GetEntityById(ord_dingdan => ord_dingdan.ID == uid);
                //更新返利
                var zkje = decimal.Parse(zhekouje);
                if (!(zkje == 0 && p.ZhekouJE == 0))
                {
                    var _flxf = ServiceFactory.ord_fanlixfservice.GetEntityById(ord_fanlixf => ord_fanlixf.DDID == uid && ord_fanlixf.IsDelete == false);
                    if (_flxf != null)
                    {
                        decimal? dif = _flxf.XFJE - zkje;
                        var _fl = ServiceFactory.ord_fanliservice.GetEntityById(ord_fanlixf => ord_fanlixf.KHID == p.KHID && ord_fanlixf.IsDelete == false);
                        if (_fl != null)
                        {
                            _fl.Zonge = _fl.Zonge + dif;
                            _fl.Keyong = _fl.Keyong + dif;
                            ServiceFactory.ord_fanliservice.UpdateEntity(_fl);
                        }
                        else
                        {
                        }
                        _flxf.KHID = p.KHID;
                        _flxf.XFJE = zkje;
                        _flxf.MakeMan = _userid;
                        ServiceFactory.ord_fanlixfservice.UpdateEntity(_flxf);
                    }
                    else
                    {
                        ord_fanlixf _xf = new ord_fanlixf();
                        _xf.DDID = uid;
                        _xf.KHID = p.KHID;
                        _xf.XFJE = zkje;
                        _xf.MakeMan = _userid;
                        _xf = ServiceFactory.ord_fanlixfservice.AddEntity(_xf);
                    }
                }
                p.Bianhao = bianhao.Trim();
                p.KHID = khid == "" ? 0 : int.Parse(khid);
                p.CPXID = cpxid == "" ? 0 : int.Parse(cpxid);
                p.CGLX = cglx == "" ? 0 : int.Parse(cglx);
                p.KehuDH = kehudh.Trim();
                //p.XiadanRQ = xiadanrq == "" ? DateTime.Now : DateTime.Parse(xiadanrq);
                p.Lianxiren = lianxiren.Trim();
                p.LianxiDH = lianxidh.Trim();
                p.SonghuoDZ = songhuodz.Trim();
                p.OPID = _userid;
                p.ACCID = accid == "" ? 0 : int.Parse(accid);
                p.ShenheTG = shenhetg == "" ? false : Boolean.Parse(shenhetg);
                p.ZongshuCG = zongshucg == "" ? 0 : float.Parse(zongshucg);
                p.Zongjine = zongjine == "" ? 0 : decimal.Parse(zongjine);
                p.JieshuSF = jieshusf == "" ? false : Boolean.Parse(jieshusf);
                p.Zhuangtai = zhuangtai == "" ? 0 : int.Parse(zhuangtai);
                p.Beizhu = beizhu.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                //p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.KehuDM = kehudm.Trim();
                p.CuoxiaoZK = cuoxiaozk == "" ? 0 : decimal.Parse(cuoxiaozk);
                //p.ShenheSJ = shenhesj == "" ? DateTime.Now : DateTime.Parse(shenhesj);
                if(shenhesj != "")
                    p.ShenheSJ = DateTime.Parse(shenhesj);
                p.ZhekouJE = zhekouje == "" ? 0 : decimal.Parse(zhekouje);
                ob_ord_dingdanservice.UpdateEntity(p);

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
            ord_dingdan ob_ord_dingdan;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_dingdan = ob_ord_dingdanservice.GetEntityById(ord_dingdan => ord_dingdan.ID == id && ord_dingdan.IsDelete == false);
                    ob_ord_dingdan.IsDelete = true;
                    ob_ord_dingdanservice.UpdateEntity(ob_ord_dingdan);
                }
            }
            return RedirectToAction("Index");
        }

        [OutputCache(Duration = 30)]
        public ActionResult CustomerOrderList(string page, string sortOrder)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            int custid = (int)Session["customer_id"];
            string pagetag = "ord_dingdan_customerorderlist";
            Expression<Func<ord_ordermain_v, bool>> where = PredicateExtensionses.True<ord_ordermain_v>();
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
                            string mingchengand = scld[3];
                            if (!string.IsNullOrEmpty(Zhuangtai))
                            {
                                if (Zhuangtaiequal.Equals("="))
                                {
                                    if (mingchengand.Equals("and"))
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
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }
            where = where.And(ord_dingdan => ord_dingdan.Zhuangtai != 0);

            var tempData = ob_ord_dingdanservice.LoadCustomerOverOrders(custid, where.Compile()).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));


            #region 排序
            ViewBag.BianhaoParm = string.IsNullOrEmpty(sortOrder) ? "Bianhao" : sortOrder.Equals("Bianhao_desc") ? "Bianhao" : "Bianhao_desc";
            ViewBag.ZhuangtaiParm = sortOrder == "Zhuangtai" ? "Zhuangtai_desc" : "Zhuangtai";
            ViewBag.MingchengParm = sortOrder == "Mingcheng" ? "Mingcheng_desc" : "Mingcheng";
            ViewBag.CGLXParm = sortOrder == "CGLX" ? "CGLX_desc" : "CGLX";
            ViewBag.KehuDHParm = sortOrder == "KehuDH" ? "KehuDH_desc" : "KehuDH";
            ViewBag.XiadanRQParm = sortOrder == "XiadanRQ" ? "XiadanRQ_desc" : "XiadanRQ";

            switch (sortOrder)
            {
                case "Mingcheng_desc":
                    tempData = tempData.OrderByDescending(p => p.Mingcheng).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Mingcheng":
                    tempData = tempData.OrderBy(p => p.Mingcheng).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "CGLX_desc":
                    tempData = tempData.OrderByDescending(p => p.CGLX).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "CGLX":
                    tempData = tempData.OrderBy(p => p.CGLX).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "KehuDH_desc":
                    tempData = tempData.OrderByDescending(p => p.KehuDH).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "KehuDH":
                    tempData = tempData.OrderBy(p => p.KehuDH).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "XiadanRQ_desc":
                    tempData = tempData.OrderByDescending(p => p.XiadanRQ).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "XiadanRQ":
                    tempData = tempData.OrderBy(p => p.XiadanRQ).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Zhuangtai_desc":
                    tempData = tempData.OrderByDescending(p => p.Zhuangtai).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Zhuangtai":
                    tempData = tempData.OrderBy(p => p.Zhuangtai).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Bianhao":
                    tempData = tempData.OrderBy(p => p.Bianhao).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                default:
                    tempData = tempData.OrderByDescending(p => p.Bianhao).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
            }

            #endregion
            ViewBag.ord_dingdan = tempData;
            return View(tempData);
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult CustomerOrderList(string sortOrder)
        {
            int userid = (int)Session["user_id"];
            int custid = (int)Session["customer_id"];
            string pagetag = "ord_dingdan_customerorderlist";
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

            Expression<Func<ord_ordermain_v, bool>> where = PredicateExtensionses.True<ord_ordermain_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                #region MyRegion

                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //编号
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
                    if (Zhuangtaiequal.Equals("like"))
                    {
                        if (Zhuangtaiand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.Zhuangtai.ToString().Contains(Zhuangtai));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.Zhuangtai.ToString().Contains(Zhuangtai));
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
                    if (Mingchengequal.Equals("like"))
                    {
                        if (Mingchengand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.Mingcheng.Contains(Mingcheng));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.Mingcheng.Contains(Mingcheng));
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
                    if (CGLXequal.Equals("like"))
                    {
                        if (CGLXand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.CGLX.ToString().Contains(CGLX));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.CGLX.ToString().Contains(CGLX));
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
                }
                if (!string.IsNullOrEmpty(XiadanRQ))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "XiadanRQ", XiadanRQ, XiadanRQequal, XiadanRQand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "XiadanRQ", "", XiadanRQequal, XiadanRQand);

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
                        if (Zhuangtaiequal.Equals("like"))
                        {
                            if (Zhuangtaiand.Equals("and"))
                                where = where.And(ord_dingdan => ord_dingdan.Zhuangtai.ToString().Contains(Zhuangtai));
                            else
                                where = where.Or(ord_dingdan => ord_dingdan.Zhuangtai.ToString().Contains(Zhuangtai));
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
                        if (Mingchengequal.Equals("like"))
                        {
                            if (Mingchengand.Equals("and"))
                                where = where.And(ord_dingdan => ord_dingdan.Mingcheng.Contains(Mingcheng));
                            else
                                where = where.Or(ord_dingdan => ord_dingdan.Mingcheng.Contains(Mingcheng));
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
                        if (CGLXequal.Equals("like"))
                        {
                            if (CGLXand.Equals("and"))
                                where = where.And(ord_dingdan => ord_dingdan.CGLX.ToString().Contains(CGLX));
                            else
                                where = where.Or(ord_dingdan => ord_dingdan.CGLX.ToString().Contains(CGLX));
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
                    }
                    if (!string.IsNullOrEmpty(XiadanRQ))
                        sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "XiadanRQ", XiadanRQ, XiadanRQequal, XiadanRQand);
                    else
                        sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "XiadanRQ", "", XiadanRQequal, XiadanRQand);


                    searchconditionService.GetInstance().UpdateEntity(sc);

                    #endregion
                }
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_dingdan => ord_dingdan.Zhuangtai != 0);

            var tempData = ob_ord_dingdanservice.LoadCustomerOverOrders(custid, where.Compile()).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));

            #region 排序
            ViewBag.BianhaoParm = string.IsNullOrEmpty(sortOrder) ? "Bianhao" : sortOrder.Equals("Bianhao_desc") ? "Bianhao" : "Bianhao_desc";
            ViewBag.ZhuangtaiParm = sortOrder == "Zhuangtai" ? "Zhuangtai_desc" : "Zhuangtai";
            ViewBag.MingchengParm = sortOrder == "Mingcheng" ? "Mingcheng_desc" : "Mingcheng";
            ViewBag.CGLXParm = sortOrder == "CGLX" ? "CGLX_desc" : "CGLX";
            ViewBag.KehuDHParm = sortOrder == "KehuDH" ? "KehuDH_desc" : "KehuDH";
            ViewBag.XiadanRQParm = sortOrder == "XiadanRQ" ? "XiadanRQ_desc" : "XiadanRQ";

            switch (sortOrder)
            {
                case "Mingcheng_desc":
                    tempData = tempData.OrderByDescending(p => p.Mingcheng).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Mingcheng":
                    tempData = tempData.OrderBy(p => p.Mingcheng).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "CGLX_desc":
                    tempData = tempData.OrderByDescending(p => p.CGLX).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "CGLX":
                    tempData = tempData.OrderBy(p => p.CGLX).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "KehuDH_desc":
                    tempData = tempData.OrderByDescending(p => p.KehuDH).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "KehuDH":
                    tempData = tempData.OrderBy(p => p.KehuDH).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "XiadanRQ_desc":
                    tempData = tempData.OrderByDescending(p => p.XiadanRQ).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "XiadanRQ":
                    tempData = tempData.OrderBy(p => p.XiadanRQ).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Zhuangtai_desc":
                    tempData = tempData.OrderByDescending(p => p.Zhuangtai).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Zhuangtai":
                    tempData = tempData.OrderBy(p => p.Zhuangtai).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                case "Bianhao":
                    tempData = tempData.OrderBy(p => p.Bianhao).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
                default:
                    tempData = tempData.OrderByDescending(p => p.Bianhao).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
                    break;
            }

            #endregion

            ViewBag.ord_dingdan = tempData;
            return View(tempData);

        }

        private static Expression<Func<ord_ordermain_v, bool>> GetOrderListSearchCondition(Expression<Func<ord_ordermain_v, bool>> where, searchcondition sc)
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
                        string mingchengand = scld[3];
                        if (!string.IsNullOrEmpty(Zhuangtai))
                        {
                            if (Zhuangtaiequal.Equals("="))
                            {
                                if (mingchengand.Equals("and"))
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
                        }
                        break;
                    default:
                        break;
                }
            }
            where = where.And(ord_dingdan => ord_dingdan.Zhuangtai != 0);
            return where;
        }


        [OutputCache(Duration = 30)]
        public ActionResult CustomerCurrentOrder(string sortOrder)
        {
            int _userid = (int)Session["user_id"];
            var _acct = (string)Session["account"];
            int _custid = (int)Session["customer_id"];

            string pagetag = "ord_dingdan_CustomerCurrentOrder";
            //编号
            string bianhao = Request["bianhao"] ?? "";
            string bianhaoequal = Request["bianhaoequal"] ?? "and";
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
            string KehuDHequal = Request["KehuDHequal"] ?? "and";
            string KehuDHand = Request["KehuDHand"] ?? "and";
            //下单日期
            string XiadanRQ = Request["XiadanRQ"] ?? "";
            string XiadanRQequal = Request["XiadanRQequal"] ?? "";
            string XiadanRQand = Request["XiadanRQand"] ?? "and";

            Expression<Func<ord_ordermain_v, bool>> where = PredicateExtensionses.True<ord_ordermain_v>();
            //界面不显示欠货生成的订单
            where = where.And(p => p.QHFLAG != "1");
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == _userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                #region MyRegion


                sc = new searchcondition();
                sc.UserID = _userid;
                sc.PageBrief = pagetag;
                //编号
                if (!string.IsNullOrEmpty(bianhao))
                {
                    if (bianhaoequal.Equals("="))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(ord_ordermain_v => ord_ordermain_v.Bianhao == bianhao);
                        else
                            where = where.Or(ord_ordermain_v => ord_ordermain_v.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(ord_ordermain_v => ord_ordermain_v.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(ord_ordermain_v => ord_ordermain_v.Bianhao.Contains(bianhao));
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
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "XiadanRQ", "", XiadanRQequal, XiadanRQand);

                searchconditionService.GetInstance().AddEntity(sc);

                #endregion
            }
            else
            {

                if (!string.IsNullOrEmpty(sortOrder))
                {
                    where = GetSearchCondition(where, sc);
                }
                else
                {
                    #region 界面获取


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
                    else
                        sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "XiadanRQ", "", XiadanRQequal, XiadanRQand);

                    searchconditionService.GetInstance().UpdateEntity(sc);

                    #endregion
                }
            }
            ViewBag.SearchCondition = sc.ConditionInfo;

            var _shdw = ServiceFactory.base_shouhuodanweiservice.GetEntityById(p => p.ID == _custid);
            if (_shdw != null)
                ViewBag.customername = _shdw.Mingcheng;

            else
                ViewBag.customername = "0";
            var _gzr = ServiceFactory.ord_guanzhangriservice.GetEntityById(p => p.Guanzhangri == DateTime.Parse(DateTime.Now.ToShortDateString()) && p.IsDelete == false);
            if (_gzr == null)
            {
                ViewBag.closeday = "0";
                ViewBag.closereason = "";
            }
            else
            {
                ViewBag.closeday = "1";
                ViewBag.closereason = _gzr.Memo;
            }

            //var tempData = ob_ord_dingdanservice.LoadCustomerActiveOrders(_custid).OrderByDescending(p => p.Bianhao);
            var tempData = ob_ord_dingdanservice.LoadCustomerActiveOrders(_custid, where.Compile()).ToList<ord_ordermain_v>();

            ViewBag.BianhaoParm = string.IsNullOrEmpty(sortOrder) ? "Bianhao" : sortOrder.Equals("Bianhao_desc") ? "Bianhao" : "Bianhao_desc";
            ViewBag.ZhuangtaiParm = sortOrder == "Zhuangtai" ? "Zhuangtai_desc" : "Zhuangtai";
            ViewBag.MingchengParm = sortOrder == "Mingcheng" ? "Mingcheng_desc" : "Mingcheng";
            ViewBag.CGLXParm = sortOrder == "CGLX" ? "CGLX_desc" : "CGLX";
            ViewBag.KehuDHParm = sortOrder == "KehuDH" ? "KehuDH_desc" : "KehuDH";
            ViewBag.XiadanRQParm = sortOrder == "XiadanRQ" ? "XiadanRQ_desc" : "XiadanRQ";
            switch (sortOrder)
            {
                case "Bianhao":
                    tempData = tempData.OrderBy(p => p.Bianhao).ToList<ord_ordermain_v>();
                    break;
                case "Mingcheng_desc":
                    tempData = tempData.OrderByDescending(p => p.Mingcheng).ToList<ord_ordermain_v>();
                    break;
                case "Mingcheng":
                    tempData = tempData.OrderBy(p => p.Mingcheng).ToList<ord_ordermain_v>();
                    break;
                case "CGLX_desc":
                    tempData = tempData.OrderByDescending(p => p.CGLX).ToList<ord_ordermain_v>();
                    break;
                case "CGLX":
                    tempData = tempData.OrderBy(p => p.CGLX).ToList<ord_ordermain_v>();
                    break;
                case "KehuDH_desc":
                    tempData = tempData.OrderByDescending(p => p.KehuDH).ToList<ord_ordermain_v>();
                    break;
                case "KehuDH":
                    tempData = tempData.OrderBy(p => p.KehuDH).ToList<ord_ordermain_v>();
                    break;
                case "XiadanRQ_desc":
                    tempData = tempData.OrderByDescending(p => p.XiadanRQ).ToList<ord_ordermain_v>();
                    break;
                case "XiadanRQ":
                    tempData = tempData.OrderBy(p => p.XiadanRQ).ToList<ord_ordermain_v>();
                    break;
                case "Zhuangtai_desc":
                    tempData = tempData.OrderByDescending(p => p.Zhuangtai).ToList<ord_ordermain_v>();
                    break;
                case "Zhuangtai":
                    tempData = tempData.OrderBy(p => p.Zhuangtai).ToList<ord_ordermain_v>();
                    break;
                default:
                    tempData = tempData.OrderByDescending(p => p.Bianhao).ToList<ord_ordermain_v>();
                    break;
            }

            ViewBag.ord_dingdan = tempData;
            List<string> fh = new List<string>();
            List<string> qh = new List<string>();
            foreach (var ob_ord_dingdan in tempData)
            {
                float con = 0;
                //欠货数量
                try
                {
                    var temp = ServiceFactory.ord_dingdanservice.LoadCustomerActiveOweByID(ob_ord_dingdan.ID).ToList<ord_ordermain_vsss>();
                    if (temp == null || temp[0].QHSL == 0)
                    {
                        qh.Add("0");
                    }
                    else
                    {
                        qh.Add(temp[0].QHSL.ToString());
                    }
                }
                catch
                {
                    qh.Add("0");
                }
                //发货数量
                if (ob_ord_dingdan.Zhuangtai < 30)
                {
                    fh.Add("");
                    continue;
                }
                ord_fahuodan ff = ob_ord_fahuodanservice.GetEntityById(ord_fahuodan => ord_fahuodan.DDID == ob_ord_dingdan.ID && ord_fahuodan.IsDelete == false);
                if (ff == null)
                {
                    fh.Add("0");
                    continue;
                }
                else
                {
                    var ffmx = ServiceFactory.ord_fahuomxservice.LoadEntities(ord_fahuomx => ord_fahuomx.ChukuID == ff.ID && ord_fahuomx.IsDelete == false).ToList<ord_fahuomx>();
                    if (ffmx.Count == 0)
                    {
                        fh.Add("0");
                        continue;
                    }
                    else
                    {
                        foreach (var ord_fahuomx in ffmx)
                        {
                            con += ord_fahuomx.ChukuSL.Value;
                        }
                    }
                }
                fh.Add(con.ToString());

            }
            ViewBag.fhsl = fh;
            ViewBag.qhsl = qh;
            return View();
        }

        private static Expression<Func<ord_ordermain_v, bool>> GetSearchCondition(Expression<Func<ord_ordermain_v, bool>> where, searchcondition sc)
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
                                    where = where.And(ord_ordermain_v => ord_ordermain_v.Bianhao == bianhao);
                                else
                                    where = where.Or(ord_ordermain_v => ord_ordermain_v.Bianhao == bianhao);
                            }
                            if (bianhaoequal.Equals("like"))
                            {
                                if (bianhaoand.Equals("and"))
                                    where = where.And(ord_ordermain_v => ord_ordermain_v.Bianhao.Contains(bianhao));
                                else
                                    where = where.Or(ord_ordermain_v => ord_ordermain_v.Bianhao.Contains(bianhao));
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
            where = where.And(ord_dingdan => ord_dingdan.Zhuangtai != 0);
            return where;
        }

        public ActionResult CustomerAdd()
        {
            int userid = (int)Session["user_id"];
            int custid = (int)Session["customer_id"];

            var _cust = ServiceFactory.base_shouhuodanweiservice.GetEntityById(p => p.ID == custid && p.IsDelete == false);
            if (_cust == null)
            {
                ViewBag.lxr = "";
                ViewBag.lxdh = "";
                ViewBag.shdz = "";
                ViewBag.custcode = "";
            }
            else
            {
                ViewBag.lxr = _cust.Lianxiren;
                ViewBag.lxdh = _cust.LianxiDH;
                ViewBag.shdz = _cust.SonghuoDZ;
                ViewBag.custcode = _cust.KehuDM;
            }
            var _cpxsq = ServiceFactory.base_chanpinxiansqservice.LoadSortEntities(p => p.JXSID == custid && p.IsDelete == false, true, s => s.CPXDM).ToList();
            List<base_chanpinxiansqViewModel> cpxsq = new List<base_chanpinxiansqViewModel>();
            foreach (var sq in _cpxsq)
            {
                base_chanpinxiansqViewModel csq = new base_chanpinxiansqViewModel();
                csq.ID = sq.ID;
                var _cpx = ServiceFactory.base_chanpinxianservice.GetEntityById(p => p.ID == sq.CPXID);
                csq.CPXDM = _cpx.Mingcheng;
                csq.CPXID = sq.CPXID;
                csq.IsDelete = sq.IsDelete;
                csq.JXSDM = sq.JXSDM;
                csq.JXSID = sq.JXSID;
                csq.MakeDate = sq.MakeDate;
                csq.MakeMan = sq.MakeMan;
                csq.TingyongSF = sq.TingyongSF;
                cpxsq.Add(csq);
            }
            ViewBag.cpxsq = cpxsq;
            ViewBag.customer = custid;
            ViewBag.user = userid;
            ViewBag.khdh = custid + DateTime.Now.ToString("yyMMddhhmmss");
            return View();
        }
        public JsonResult AddOrderNow()
        {
            int _userid = (int)Session["user_id"];
            var _cust = Request["cust"] ?? "";
            var _cdm = Request["cdm"] ?? "";
            var _cpx = Request["cpx"] ?? "";
            var _lx = Request["lx"] ?? "";
            var _zsl = Request["zsl"] ?? "0";
            var _zje = Request["zje"] ?? "0";
            var _bz = Request["bz"] ?? "";
            var _zk = Request["zk"] ?? "";
            var _zkje = Request["zkje"] ?? "0";
            var _sps = Request["sps"] ?? "";
            var _lxr = Request["lxr"] ?? "";
            var _lxdh = Request["lxdh"] ?? "";
            var _shdz = Request["shdz"] ?? "";
            var _khdh = Request["khdh"] ?? "";
            var _col2 = Request["col2"] ?? "";
            var _col3 = Request["col3"] ?? "";

            if (string.IsNullOrEmpty(_cpx) || string.IsNullOrEmpty(_cdm) || string.IsNullOrEmpty(_cust) || string.IsNullOrEmpty(_lx)
                || string.IsNullOrEmpty(_sps) || string.IsNullOrEmpty(_lxr) || string.IsNullOrEmpty(_lxdh)
                || string.IsNullOrEmpty(_shdz))
                return Json(-1);
            string[] _splist = _sps.Split(';');

            //add order
            ord_dingdan _dd = new ord_dingdan();
            _dd.Beizhu = _bz;
            _dd.CGLX = int.Parse(_lx);
            _dd.CPXID = int.Parse(_cpx);
            _dd.JieshuSF = false;
            _dd.KehuDH = _khdh;
            _dd.KHID = int.Parse(_cust);
            _dd.KehuDM = _cdm;
            _dd.LianxiDH = _lxdh;
            _dd.Lianxiren = _lxr;
            _dd.SonghuoDZ = _shdz;
            _dd.XiadanRQ = DateTime.Now;
            _dd.ZhekouJE = decimal.Parse(_zkje);
            _dd.Zhuangtai = 10;
            _dd.Zongjine = decimal.Parse(_zje);
            _dd.ZongshuCG = float.Parse(_zsl);
            _dd.ShenheTG = false;
            _dd.HH = _splist.Count();
            _dd.MakeMan = _userid;
            _dd.Col2 = _col2;
            _dd.Col3 = _col3;
            _dd = ob_ord_dingdanservice.AddEntity(_dd);
            if (_dd == null)
                return Json(-2);

            decimal _zkl = 1 - (decimal)(_dd.ZhekouJE /(_dd.Zongjine + _dd.ZhekouJE));
            _zkl = (decimal)Math.Round(_zkl, 7);
            List<SPList> _sptemp = new List<SPList>();
            //add commodity
            foreach (var sp in _splist)
            {
                if (sp.Length > 5)
                {
                    string[] _sp = sp.Split(',');
                    SPList _spl = new SPList();
                    _spl.spid = int.Parse(_sp[0]);
                    _spl.spsl = float.Parse(_sp[1]);
                    _spl.spjg = decimal.Parse(_sp[2]);
                    _spl.spje = decimal.Parse(_sp[3]);
                    _sptemp.Add(_spl);
                }
            }
            var _spgroup = from p in _sptemp
                           group p by p.spid into g
                           select new
                           {
                               g.Key,
                               jg = g.Average(p => p.spjg),
                               tsl = g.Sum(p => p.spsl),
                               tje = g.Sum(p => p.spje)
                           };
            foreach (var spg in _spgroup)
            {
                if (_dd.CGLX == 1)
                {
                    var _spxx = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.ID == spg.Key && p.IsDelete == false);
                    if (_spxx != null)
                    {
                        ord_dingdanmx _mx = new ord_dingdanmx();
                        _mx.DDID = _dd.ID;
                        _mx.SPID = _spxx.ID;
                        _mx.SPBM = _spxx.Daima;
                        _mx.Guige = _spxx.Guige;
                        _mx.SPMC = _spxx.Mingcheng;
                        _mx.JBDW = _spxx.Danwei;
                        _mx.CGSL = spg.tsl;
                        _mx.FHSL = 0;
                        _mx.XSBJ = spg.jg;
                        _mx.Danjia = Math.Round(spg.jg * _zkl, 2);
                        //_mx.XSDJ = spg.jg * _zkl;
                        _mx.XSDW = _spxx.BaozhuangDW;
                        _mx.HSL = (float)_spxx.Huansuanlv;
                        _mx.HSBM = _spxx.Col1;
                        _mx.Jine = spg.tje;
                        //_mx.Zhekou = spg.tje * (1 - _zkl);
                        _mx.Zhekou = (decimal)spg.tsl * (spg.jg - spg.jg * (1 - (_dd.ZhekouJE / (_dd.Zongjine + _dd.ZhekouJE))));
                        _mx.Zhekoulv = (float)_zkl;
                        _mx.MakeMan = _userid;
                        _mx = ServiceFactory.ord_dingdanmxservice.AddEntity(_mx);
                    }
                }
                else if (_dd.CGLX == 2)
                {
                    var _tbxx = ServiceFactory.base_taobaoservice.GetEntityById(p => p.ID == spg.Key && p.IsDelete == false);
                    if (_tbxx != null)
                    {
                        ord_dingdanmx _mx = new ord_dingdanmx();
                        _mx.DDID = _dd.ID;
                        _mx.SPID = _tbxx.ID;
                        _mx.SPBM = _tbxx.Daima;
                        _mx.Guige = _tbxx.Miaoshu;
                        _mx.SPMC = _tbxx.Mingcheng;
                        _mx.JBDW = _tbxx.XSDW;
                        _mx.CGSL = spg.tsl;
                        _mx.FHSL = 0;
                        _mx.XSBJ = spg.jg;
                        _mx.Danjia = Math.Round(spg.jg * _zkl, 2);
                        //_mx.XSDJ = spg.jg * _zkl;
                        _mx.XSDW = _tbxx.XSDW;
                        _mx.HSL = 1;
                        _mx.HSBM = _tbxx.Col1;
                        _mx.Jine = spg.tje;
                        //_mx.Zhekou = spg.tje * (1 - _zkl);
                        _mx.Zhekou = (decimal)spg.tsl * (spg.jg - spg.jg * (1 - (_dd.ZhekouJE / (_dd.Zongjine + _dd.ZhekouJE))));
                        _mx.Zhekoulv = (float)_zkl;
                        _mx.MakeMan = _userid;
                        _mx = ServiceFactory.ord_dingdanmxservice.AddEntity(_mx);
                    }
                }
                else
                {

                }
            }
            //add minus
            if (_zkl < 1)
            {
                ord_fanlixf _xf = new ord_fanlixf();
                _xf.DDID = _dd.ID;
                _xf.KHID = _dd.KHID;
                _xf.XFJE = _dd.ZhekouJE;
                _xf.MakeMan = _userid;
                _xf = ServiceFactory.ord_fanlixfservice.AddEntity(_xf);
                //if (_xf != null)
                //{
                //    var _fl = ServiceFactory.ord_fanliservice.GetEntityById(p => p.KHID == _xf.KHID && p.IsDelete == false);
                //    if (_fl != null)
                //    {
                //        _fl.Zonge = _fl.Zonge - _xf.XFJE;
                //        _fl.Keyong = _fl.Keyong - _xf.XFJE;
                //        ServiceFactory.ord_fanliservice.UpdateEntity(_fl);
                //    }
                //}

            }
            return Json(1);
        }
        public JsonResult addordernow_edit()
        {
            int _userid = (int)Session["user_id"];
            var _ddid = Request["ddid"] ?? "";
            var _cust = Request["cust"] ?? "";
            var _cdm = Request["cdm"] ?? "";
            var _cpx = Request["cpx"] ?? "";
            var _lx = Request["lx"] ?? "";
            var _zsl = Request["zsl"] ?? "0";
            var _zje = Request["zje"] ?? "0";
            var _bz = Request["bz"] ?? "";
            var _zk = Request["zk"] ?? "";
            var _zkje = Request["zkje"] ?? "0";
            var _sps = Request["sps"] ?? "";
            var _lxr = Request["lxr"] ?? "";
            var _lxdh = Request["lxdh"] ?? "";
            var _shdz = Request["shdz"] ?? "";
            var _khdh = Request["khdh"] ?? "";

            if (string.IsNullOrEmpty(_cpx) || string.IsNullOrEmpty(_cdm) || string.IsNullOrEmpty(_cust) || string.IsNullOrEmpty(_lx)
                || string.IsNullOrEmpty(_sps) || string.IsNullOrEmpty(_lxr) || string.IsNullOrEmpty(_lxdh)
                || string.IsNullOrEmpty(_shdz))
                return Json(-1);
            string[] _splist = _sps.Split(';');

            //add order
            ord_dingdan _dd = ob_ord_dingdanservice.GetEntityById(p => p.ID == int.Parse(_ddid) && p.IsDelete == false);
            //ord_dingdan _dd = new ord_dingdan();
            _dd.Beizhu = _bz;
            _dd.CGLX = int.Parse(_lx);
            _dd.CPXID = int.Parse(_cpx);
            _dd.JieshuSF = false;
            _dd.KehuDH = _khdh;
            _dd.KHID = int.Parse(_cust);
            _dd.KehuDM = _cdm;
            _dd.LianxiDH = _lxdh;
            _dd.Lianxiren = _lxr;
            _dd.SonghuoDZ = _shdz;
            _dd.XiadanRQ = DateTime.Now;
            _dd.ZhekouJE = decimal.Parse(_zkje);
            _dd.Zhuangtai = 10;
            _dd.Zongjine = decimal.Parse(_zje);
            _dd.ZongshuCG = float.Parse(_zsl);
            _dd.ShenheTG = false;
            _dd.HH = _splist.Count();
            _dd.MakeMan = _userid;
            bool suc = ob_ord_dingdanservice.UpdateEntity(_dd);
            if (suc == false)
                return Json(-2);

            decimal _zkl = 1 - (decimal)(_dd.ZhekouJE /(_dd.Zongjine + _dd.ZhekouJE));
            _zkl = (decimal)Math.Round(_zkl, 7);
            List<SPList> _sptemp = new List<SPList>();
            //add commodity
            foreach (var sp in _splist)
            {
                if (sp.Length > 5)
                {
                    string[] _sp = sp.Split(',');
                    SPList _spl = new SPList();
                    _spl.spid = int.Parse(_sp[0]);
                    _spl.spsl = float.Parse(_sp[1]);
                    _spl.spjg = decimal.Parse(_sp[2]);
                    _spl.spje = decimal.Parse(_sp[3]);
                    _sptemp.Add(_spl);
                }
            }
            var _spgroup = from p in _sptemp
                           group p by p.spid into g
                           select new
                           {
                               g.Key,
                               jg = g.Average(p => p.spjg),
                               tsl = g.Sum(p => p.spsl),
                               tje = g.Sum(p => p.spje)
                           };
            //删除之前的订单表体信息
            var delmx = ServiceFactory.ord_dingdanmxservice.LoadEntities(p => p.DDID == _dd.ID && p.IsDelete == false).ToList<ord_dingdanmx>(); ;
            foreach (ord_dingdanmx _delmx in delmx)
            {
                _delmx.IsDelete = true;
                ServiceFactory.ord_dingdanmxservice.UpdateEntity(_delmx);
            }

            foreach (var spg in _spgroup)
            {
                if (_dd.CGLX == 1)
                {
                    var _spxx = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.ID == spg.Key && p.IsDelete == false);
                    if (_spxx != null)
                    {
                        ord_dingdanmx _mx = new ord_dingdanmx();
                        _mx.DDID = _dd.ID;
                        _mx.SPID = _spxx.ID;
                        _mx.SPBM = _spxx.Daima;
                        _mx.Guige = _spxx.Guige;
                        _mx.SPMC = _spxx.Mingcheng;
                        _mx.JBDW = _spxx.Danwei;
                        _mx.CGSL = spg.tsl;
                        _mx.FHSL = 0;
                        _mx.XSBJ = spg.jg;
                        _mx.Danjia = Math.Round(spg.jg * _zkl, 2);
                        //_mx.XSDJ = spg.jg * _zkl;
                        _mx.XSDW = _spxx.BaozhuangDW;
                        _mx.HSL = (float)_spxx.Huansuanlv;
                        _mx.HSBM = _spxx.Col1;
                        _mx.Jine = spg.tje;
                        _mx.Zhekou = (decimal)spg.tsl * (spg.jg - spg.jg * (1 - (_dd.ZhekouJE / (_dd.Zongjine + _dd.ZhekouJE))));
                        _mx.Zhekoulv = (float)_zkl;
                        _mx.MakeMan = _userid;
                        _mx = ServiceFactory.ord_dingdanmxservice.AddEntity(_mx);
                    }
                }
                else if (_dd.CGLX == 2)
                {
                    var _tbxx = ServiceFactory.base_taobaoservice.GetEntityById(p => p.ID == spg.Key && p.IsDelete == false);
                    if (_tbxx != null)
                    {
                        ord_dingdanmx _mx = new ord_dingdanmx();
                        _mx.DDID = _dd.ID;
                        _mx.SPID = _tbxx.ID;
                        _mx.SPBM = _tbxx.Daima;
                        _mx.Guige = _tbxx.Miaoshu;
                        _mx.SPMC = _tbxx.Mingcheng;
                        _mx.JBDW = _tbxx.XSDW;
                        _mx.CGSL = spg.tsl;
                        _mx.FHSL = 0;
                        _mx.XSBJ = spg.jg;
                        //_mx.XSDJ = spg.jg * _zkl;
                        _mx.Danjia = Math.Round(spg.jg * _zkl, 2);
                        _mx.XSDW = _tbxx.XSDW;
                        _mx.HSL = 1;
                        _mx.HSBM = _tbxx.Col1;
                        _mx.Jine = spg.tje;
                        //_mx.Zhekou = spg.tje * (1 - _zkl);
                        _mx.Zhekou = (decimal)spg.tsl * (spg.jg - spg.jg * (1 - (_dd.ZhekouJE / (_dd.Zongjine + _dd.ZhekouJE))));
                        _mx.Zhekoulv = (float)_zkl;
                        _mx.MakeMan = _userid;
                        _mx = ServiceFactory.ord_dingdanmxservice.AddEntity(_mx);
                    }
                }
                else
                {

                }
            }
            //edit minus
            var _flxf = ServiceFactory.ord_fanlixfservice.GetEntityById(p => p.DDID == _dd.ID && p.IsDelete == false);
            if (_flxf != null)
            {
                decimal? dif = _flxf.XFJE - _dd.ZhekouJE;
                var _fl = ServiceFactory.ord_fanliservice.GetEntityById(p => p.KHID == _dd.KHID && p.IsDelete == false);
                if (_fl != null)
                {
                    _fl.Zonge = _fl.Zonge + dif;
                    _fl.Keyong = _fl.Keyong + dif;
                    ServiceFactory.ord_fanliservice.UpdateEntity(_fl);
                }
                else
                {
                }
                _flxf.KHID = _dd.KHID;
                _flxf.XFJE = _dd.ZhekouJE;
                _flxf.MakeMan = _userid;
                ServiceFactory.ord_fanlixfservice.UpdateEntity(_flxf);
            }
            else
            {
                if (_dd.ZhekouJE != 0)
                {
                    ord_fanlixf _xf = new ord_fanlixf();
                    _xf.DDID = _dd.ID;
                    _xf.KHID = _dd.KHID;
                    _xf.XFJE = _dd.ZhekouJE;
                    _xf.MakeMan = _userid;
                    _xf = ServiceFactory.ord_fanlixfservice.AddEntity(_xf);
                    //if (_xf != null)
                    //{
                    //    var _fl = ServiceFactory.ord_fanliservice.GetEntityById(p => p.KHID == _xf.KHID && p.IsDelete == false);
                    //    if (_fl != null)
                    //    {
                    //        _fl.Zonge = _fl.Zonge - _xf.XFJE;
                    //        _fl.Keyong = _fl.Keyong - _xf.XFJE;
                    //        ServiceFactory.ord_fanliservice.UpdateEntity(_fl);
                    //    }
                    //}
                }
            }
            return Json(1);
        }
        public ActionResult CustomerOrderInfo(int id)
        {
            decimal zong_all = 0;
            decimal zhe_all = 0;
            //decimal zong_part = 0;
            //decimal zhe_part = 0;
            int _custid = (int)Session["customer_id"];
            var _dd = ob_ord_dingdanservice.GetEntityById(p => p.ID == id && p.KHID == _custid && p.IsDelete == false);
            if (_dd == null)
                return View();
            var _cpx = ServiceFactory.base_chanpinxianservice.GetEntityById(p => p.ID == _dd.CPXID);
            if (_cpx == null)
                ViewBag.cpx = "";
            else
                ViewBag.cpx = _cpx.Mingcheng;
            ViewBag.Bianhao = _dd.Bianhao;
            ViewBag.cglx = _dd.CGLX;
            ViewBag.sl = _dd.ZongshuCG;
            ViewBag.je = _dd.Zongjine;
            ViewBag.khdh = _dd.KehuDH;
            ViewBag.bz = _dd.Beizhu;
            ViewBag.zk = _dd.ZhekouJE;
            ViewBag.lxr = _dd.Lianxiren;
            ViewBag.lxdh = _dd.LianxiDH;
            ViewBag.shdz = _dd.SonghuoDZ;
            ViewBag.Zhuangtai = _dd.Zhuangtai;
            ViewBag.Col2 = _dd.Col2;
            ViewBag.Col3 = _dd.Col3;
            if (_dd.FKPZ == null)
                ViewBag.fkpz = "";
            else
                ViewBag.fkpz = "/files/zhengzhao/" + _dd.FKPZ;
            var _ddmx = ServiceFactory.ord_dingdanmxservice.LoadSortEntities(p => p.DDID == _dd.ID && p.IsDelete == false, true, s => s.SPBM).ToList();
            if (_dd.Zhuangtai < 20)
            {
                foreach (ord_dingdanmx ddmx in _ddmx)
                {
                    if (ddmx.PFSL == null)
                    {
                        break;
                    }
                    zong_all = zong_all + Convert.ToDecimal(ddmx.CGSL ?? 0) * Convert.ToDecimal(ddmx.XSDJ ?? 0);
                    zhe_all = zhe_all + (Convert.ToDecimal(1 - ddmx.Zhekoulv ?? 1)) / Convert.ToDecimal(ddmx.Zhekoulv) * Convert.ToDecimal(ddmx.CGSL ?? 0) * Convert.ToDecimal(ddmx.XSDJ ?? 0);
                    //zong_part = zong_part + Convert.ToDecimal(ddmx.PFSL ?? 0) * Convert.ToDecimal(ddmx.XSDJ ?? 0);
                    //zhe_part = zhe_part + (Convert.ToDecimal(1 - ddmx.Zhekoulv ?? 1)) / Convert.ToDecimal(ddmx.Zhekoulv) * Convert.ToDecimal(ddmx.PFSL ?? 0) * Convert.ToDecimal(ddmx.XSDJ ?? 0);

                }
                ViewBag.zhe_all = Math.Round(zhe_all, 2);
                ViewBag.zong_all = Math.Round(zong_all, 2);
                //ViewBag.zhe_part = Math.Round(zhe_part, 2);
                //ViewBag.zong_part = Math.Round(zong_part + zhe_part, 2);
            }
            else
            {
                ViewBag.zhe_all = _dd.ZhekouJE; ;
                ViewBag.zong_all = _dd.Zongjine;
            }
            ViewBag.ord_dingdanmx = _ddmx;
            return View();
        }

        public ActionResult CustomerOrderEdit(int id)
        {
            int _custid = (int)Session["customer_id"];
            var _dd = ob_ord_dingdanservice.GetEntityById(p => p.ID == id && p.KHID == _custid && p.IsDelete == false);
            if (_dd == null)
                return View();
            var _cpx = ServiceFactory.base_chanpinxianservice.GetEntityById(p => p.ID == _dd.CPXID);
            if (_cpx == null)
                ViewBag.cpx = "";
            else
            {
                ViewBag.cpx = _cpx.Mingcheng;
                ViewBag.CPXDM = _cpx.ID;
            }
            ViewBag.ddid = id;
            ViewBag.Bianhao = _dd.Bianhao;
            ViewBag.customer = _dd.KHID;
            ViewBag.custcode = _dd.KehuDM;
            ViewBag.cglx = _dd.CGLX;
            ViewBag.sl = _dd.ZongshuCG;
            ViewBag.je = _dd.Zongjine;
            ViewBag.khdh = _dd.KehuDH;
            ViewBag.bz = _dd.Beizhu;
            ViewBag.zk = _dd.ZhekouJE;
            ViewBag.lxr = _dd.Lianxiren;
            ViewBag.lxdh = _dd.LianxiDH;
            ViewBag.shdz = _dd.SonghuoDZ;
            ViewBag.Col2 = _dd.Col2;
            ViewBag.Col3 = _dd.Col3;
            if (_dd.FKPZ == null)
                ViewBag.fkpz = "";
            else
                ViewBag.fkpz = "/files/zhengzhao/" + _dd.FKPZ;
            var _ddmx = ServiceFactory.ord_dingdanmxservice.LoadSortEntities(p => p.DDID == _dd.ID && p.IsDelete == false, true, s => s.SPBM).ToList();
            ViewBag.ord_dingdanmx = _ddmx;
            return View();
        }
        public ActionResult UploadBankTicket(int id)
        {
            decimal zong_all = 0;
            decimal zhe_all = 0;
            decimal zong_part = 0;
            decimal zhe_part = 0;
            int _custid = (int)Session["customer_id"];
            var _dd = ob_ord_dingdanservice.GetEntityById(p => p.ID == id && p.KHID == _custid && p.IsDelete == false);
            if (_dd == null)
                return View();
            var _cpx = ServiceFactory.base_chanpinxianservice.GetEntityById(p => p.ID == _dd.CPXID);
            if (_cpx == null)
                ViewBag.cpx = "";
            else
                ViewBag.cpx = _cpx.Mingcheng;
            ViewBag.ddbh = _dd.Bianhao;
            ViewBag.shdz = _dd.SonghuoDZ;
            ViewBag.lxr = _dd.Lianxiren;
            ViewBag.lxdh = _dd.LianxiDH;
            ViewBag.cglx = _dd.CGLX;
            ViewBag.sl = _dd.ZongshuCG;
            ViewBag.je = _dd.Zongjine;
            ViewBag.khdh = _dd.KehuDH;
            ViewBag.bz = _dd.Beizhu;
            ViewBag.zk = _dd.ZhekouJE;
            ViewBag.ddid = _dd.ID;
            ViewBag.Col2 = _dd.Col2;
            ViewBag.Col3 = _dd.Col3;
            if (_dd.FKPZ == null)
                ViewBag.upfile = "";
            else
                ViewBag.upfile = _dd.FKPZ;
            var _ddmx = ServiceFactory.ord_dingdanmxservice.LoadSortEntities(p => p.DDID == _dd.ID && p.IsDelete == false, true, s => s.SPBM).ToList();
            foreach (ord_dingdanmx ddmx in _ddmx)
            {
                //zong_all = zong_all + Convert.ToDecimal(ddmx.CGSL ?? 0) * Convert.ToDecimal(ddmx.XSDJ ?? 0);
                //zhe_all = zhe_all + (Convert.ToDecimal(1 - ddmx.Zhekoulv ?? 1)) / Convert.ToDecimal(ddmx.Zhekoulv) * Convert.ToDecimal(ddmx.CGSL ?? 0) * Convert.ToDecimal(ddmx.XSDJ ?? 0);
                zong_part = zong_part + Convert.ToDecimal(ddmx.PFSL ?? 0) * (ddmx.XSDJ ?? 0);
                //zhe_part = zhe_part + (Convert.ToDecimal(1 - ddmx.Zhekoulv ?? 1)) / Convert.ToDecimal(ddmx.Zhekoulv) * Convert.ToDecimal(ddmx.PFSL ?? 0) * Convert.ToDecimal(ddmx.XSDJ ?? 0);
                zhe_part = zhe_part + (decimal)(ddmx.XSBJ - ddmx.XSDJ) * Convert.ToDecimal(ddmx.PFSL ?? 0);

            }
            //ViewBag.zhe_all = Math.Round(zhe_all, 2);
            //ViewBag.zong_all = Math.Round(zong_all, 2) ;
            ViewBag.zhe_all = _dd.ZhekouJE;
            ViewBag.zong_all = _dd.Zongjine;
            ViewBag.zhe_part = Math.Round(zhe_part, 2);
            ViewBag.zong_part = Math.Round(zong_part, 2);
            ViewBag.ord_dingdanmx = _ddmx;
            return View();
        }
        public JsonResult TicketUpload()
        {
            int _custid = (int)Session["customer_id"];
            var _ddid = Request["did"] ?? "";
            var _ddpath = Request["dfl"] ?? "";
            var payway = Request["payway"] ?? "";
            var zje = Request["zje"] ?? "";
            var zk = Request["zk"] ?? "";
            var sl = Request["sl"] ?? "";
            var str = Request["str"] ?? "";

            if (string.IsNullOrEmpty(_ddid) || string.IsNullOrEmpty(_ddpath))
                return Json(-1);
            var _dd = ob_ord_dingdanservice.GetEntityById(p => p.ID == int.Parse(_ddid) && p.KHID == _custid && p.IsDelete == false);
            if (_dd == null)
                return Json(-2);
            //更新折扣
            var zkje = decimal.Parse(zk);
            if (payway == "0" && _dd.ZhekouJE != 0)
            {
                var _flxf = ServiceFactory.ord_fanlixfservice.GetEntityById(ord_fanlixf => ord_fanlixf.DDID == _dd.ID && ord_fanlixf.IsDelete == false);
                if (_flxf != null)
                {
                    decimal? dif = _flxf.XFJE - zkje;
                    var _fl = ServiceFactory.ord_fanliservice.GetEntityById(ord_fanlixf => ord_fanlixf.KHID == _dd.KHID && ord_fanlixf.IsDelete == false);
                    if (_fl != null)
                    {
                        _fl.Zonge = _fl.Zonge + dif;
                        _fl.Keyong = _fl.Keyong + dif;
                        ServiceFactory.ord_fanliservice.UpdateEntity(_fl);
                    }
                    else
                    {
                    }
                    _flxf.KHID = _dd.KHID;
                    _flxf.XFJE = zkje;
                    _flxf.MakeMan = _custid;
                    ServiceFactory.ord_fanlixfservice.UpdateEntity(_flxf);
                }
                else
                {
                    ord_fanlixf _xf = new ord_fanlixf();
                    _xf.DDID = _dd.ID;
                    _xf.KHID = _dd.KHID;
                    _xf.XFJE = zkje;
                    _xf.MakeMan = _custid;
                    _xf = ServiceFactory.ord_fanlixfservice.AddEntity(_xf);
                }
            }
            _dd.FKPZ = _ddpath;
            _dd.Zhuangtai = 20;
            _dd.TicketUpTime = DateTime.Now;
            _dd.ZongshuCG = float.Parse(sl);
            _dd.Zongjine = decimal.Parse(zje);
            _dd.ZhekouJE = decimal.Parse(zk);
            if (payway == "1")
            {
                if (str != "")
                {
                    string[] _splist = str.Split(';');
                    foreach (var sp in _splist)
                    {
                        if (sp.Length > 1)
                        {
                            string[] _sp = sp.Split(',');
                            ord_qianhuo ob_ord_qianhuo = new ord_qianhuo();
                            ob_ord_qianhuo.MXID = int.Parse(_sp[0]);
                            ob_ord_qianhuo.QHSL = float.Parse(_sp[1]);
                            ob_ord_qianhuo.DJRQ = DateTime.Now;
                            ob_ord_qianhuo.MakeDate = DateTime.Now;
                            ob_ord_qianhuo.MakeMan = _custid;
                            ob_ord_qianhuo = ob_ord_qianhuoservice.AddEntity(ob_ord_qianhuo);
                        }
                    }
                }
            }
            else if (payway == "0")
            {
                var _ddmx = ServiceFactory.ord_dingdanmxservice.LoadSortEntities(p => p.DDID == _dd.ID && p.IsDelete == false, true, s => s.SPBM).ToList();
                foreach (ord_dingdanmx ddmx in _ddmx)
                {
                    ddmx.CGSL = ddmx.PFSL;
                    ddmx.Jine = Convert.ToDecimal(ddmx.PFSL) * ddmx.XSDJ;
                    ddmx.Zhekou = Convert.ToDecimal(ddmx.PFSL) * (ddmx.XSBJ - ddmx.XSDJ);
                    ob_ord_dingdanmxservice.UpdateEntity(ddmx);
                }
            }
            else
            { return Json(-1); }
            var _b = ob_ord_dingdanservice.UpdateEntity(_dd);
            if (!_b)
                return Json(-3);
            return Json(1);
        }
        public JsonResult RecieveCheck()
        {
            int _custid = (int)Session["customer_id"];
            var _ddid = Request["did"] ?? "";
            if (string.IsNullOrEmpty(_ddid))
                return Json(-1);
            var _dd = ob_ord_dingdanservice.GetEntityById(p => p.ID == int.Parse(_ddid) && p.KHID == _custid && p.IsDelete == false);
            if (_dd == null)
                return Json(-2);
            _dd.Zhuangtai = 70;
            _dd.JieshuSF = true;
            _dd.MakeDate = DateTime.Now;
            var _b = ob_ord_dingdanservice.UpdateEntity(_dd);
            if (!_b)
                return Json(-3);
            return Json(1);
        }
        public JsonResult DeleteOrder()
        {
            var _sdel = Request["sdel"] ?? "";
            if (string.IsNullOrEmpty(_sdel))
                return Json(-1);
            foreach (string sD in _sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    var id = int.Parse(sD);
                    var ob_ord_dingdan = ob_ord_dingdanservice.GetEntityById(ord_dingdan => ord_dingdan.ID == id && ord_dingdan.IsDelete == false);
                    ob_ord_dingdan.Zhuangtai = 0;
                    ob_ord_dingdan.JieshuSF = true;
                    ob_ord_dingdanservice.UpdateEntity(ob_ord_dingdan);

                    if (ob_ord_dingdan.ZhekouJE != null || ob_ord_dingdan.ZhekouJE != 0)
                    {
                        //删除返利消费
                        var _flxf = ServiceFactory.ord_fanlixfservice.GetEntityById(p => p.DDID == ob_ord_dingdan.ID && p.IsDelete == false);
                        if (_flxf != null)
                        {
                            _flxf.IsDelete = true;
                            ServiceFactory.ord_fanlixfservice.UpdateEntity(_flxf);
                        }
                        //更新返利信息
                        //var _fl = ServiceFactory.ord_fanliservice.GetEntityById(p => p.KHID == ob_ord_dingdan.KHID && p.IsDelete == false);
                        //if (_fl != null)
                        //{
                        //    _fl.Zonge = _fl.Zonge + ob_ord_dingdan.ZhekouJE;
                        //    _fl.Keyong = _fl.Keyong + ob_ord_dingdan.ZhekouJE;
                        //    ServiceFactory.ord_fanliservice.UpdateEntity(_fl);
                        //}
                    }
                }

            }
            return Json(1);
        }
        public JsonResult CancelOrder()
        {
            var _sdel = Request["sdel"] ?? "";
            int _userid = (int)Session["user_id"];
            if (string.IsNullOrEmpty(_sdel))
                return Json(-1);
            foreach (string sD in _sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    var id = int.Parse(sD);
                    var ob_ord_dingdan = ob_ord_dingdanservice.GetEntityById(ord_dingdan => ord_dingdan.ID == id && ord_dingdan.IsDelete == false);
                    ob_ord_dingdan.Zhuangtai = -1;
                    ob_ord_dingdan.OPID = _userid;
                    ob_ord_dingdan.JieshuSF = true;
                    ob_ord_dingdanservice.UpdateEntity(ob_ord_dingdan);

                    if (ob_ord_dingdan.ZhekouJE != null || ob_ord_dingdan.ZhekouJE != 0)
                    {
                        //删除返利消费
                        var _flxf = ServiceFactory.ord_fanlixfservice.GetEntityById(p => p.DDID == ob_ord_dingdan.ID && p.IsDelete == false);
                        if (_flxf != null)
                        {
                            _flxf.IsDelete = true;
                            ServiceFactory.ord_fanlixfservice.UpdateEntity(_flxf);
                        }
                        //更新返利信息
                        //var _fl = ServiceFactory.ord_fanliservice.GetEntityById(p => p.KHID == ob_ord_dingdan.KHID && p.IsDelete == false);
                        //if (_fl != null)
                        //{
                        //    _fl.Zonge = _fl.Zonge + ob_ord_dingdan.ZhekouJE;
                        //    _fl.Keyong = _fl.Keyong + ob_ord_dingdan.ZhekouJE;
                        //    ServiceFactory.ord_fanliservice.UpdateEntity(_fl);
                        //}
                    }
                }

            }
            return Json(1);
        }
        public JsonResult referorder()
        {
            var _sid = Request["_sid"] ?? "";
            if (string.IsNullOrEmpty(_sid))
                return Json(-1);
            foreach (string sD in _sid.Split(','))
            {
                if (sD.Length > 0)
                {
                    var id = int.Parse(sD);
                    var ob_ord_dingdan = ob_ord_dingdanservice.GetEntityById(ord_dingdan => ord_dingdan.ID == id && ord_dingdan.IsDelete == false);
                    ob_ord_dingdan.Zhuangtai = 12;
                    ob_ord_dingdanservice.UpdateEntity(ob_ord_dingdan);
                }
            }
            return Json(1);
        }

        public ActionResult CustomerCurrentOrderExportFile()
        {
            int _userid = (int)Session["user_id"];
            var _acct = (string)Session["account"];
            int _custid = (int)Session["customer_id"];

            string pagetag = "ord_dingdan_CustomerCurrentOrder";
            Expression<Func<ord_ordermain_v, bool>> where = PredicateExtensionses.True<ord_ordermain_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == _userid && searchcondition.PageBrief == pagetag);
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
                                        where = where.And(ord_ordermain_v => ord_ordermain_v.Bianhao == bianhao);
                                    else
                                        where = where.Or(ord_ordermain_v => ord_ordermain_v.Bianhao == bianhao);
                                }
                                if (bianhaoequal.Equals("like"))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(ord_ordermain_v => ord_ordermain_v.Bianhao.Contains(bianhao));
                                    else
                                        where = where.Or(ord_ordermain_v => ord_ordermain_v.Bianhao.Contains(bianhao));
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
            where = where.And(ord_dingdan => ord_dingdan.Zhuangtai != 0);

            var tempData = ob_ord_dingdanservice.LoadCustomerActiveOrders(_custid, where.Compile()).OrderByDescending(p => p.Bianhao);
            ViewBag.ord_dingdan = tempData;
            List<string> fh = new List<string>();
            List<string> qh = new List<string>();
            foreach (var ob_ord_dingdan in tempData)
            {
                float con = 0;
                //欠货数量
                try
                {
                    var temp = ServiceFactory.ord_dingdanservice.LoadCustomerActiveOweByID(ob_ord_dingdan.ID).ToList<ord_ordermain_vsss>();
                    if (temp == null || temp[0].QHSL == 0)
                    {
                        qh.Add("0");
                    }
                    else
                    {
                        qh.Add(temp[0].QHSL.ToString());
                    }
                }
                catch
                {
                    qh.Add("0");
                }
                //发货数量
                if (ob_ord_dingdan.Zhuangtai < 30)
                {
                    fh.Add("");
                    continue;
                }
                ord_fahuodan ff = ob_ord_fahuodanservice.GetEntityById(ord_fahuodan => ord_fahuodan.DDID == ob_ord_dingdan.ID && ord_fahuodan.IsDelete == false);
                if (ff == null)
                {
                    fh.Add("0");
                    continue;
                }
                else
                {
                    var ffmx = ServiceFactory.ord_fahuomxservice.LoadEntities(ord_fahuomx => ord_fahuomx.ChukuID == ff.ID && ord_fahuomx.IsDelete == false).ToList<ord_fahuomx>();
                    if (ffmx.Count == 0)
                    {
                        fh.Add("0");
                        continue;
                    }
                    else
                    {
                        foreach (var ord_fahuomx in ffmx)
                        {
                            con += ord_fahuomx.ChukuSL.Value;
                        }
                    }
                }
                fh.Add(con.ToString());
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("Bianhao", typeof(string));
            dt.Columns.Add("Zhuangtai", typeof(string));//订单状态
            dt.Columns.Add("Mingcheng", typeof(string));
            dt.Columns.Add("CGLX", typeof(string));//订单类型
            dt.Columns.Add("KehuDH", typeof(string));
            dt.Columns.Add("XiadanRQ", typeof(string));
            dt.Columns.Add("ZongshuCG", typeof(string));

            dt.Columns.Add("YFQty", typeof(string));
            dt.Columns.Add("DFQty", typeof(string));
            dt.Columns.Add("QHQty", typeof(string));

            dt.Columns.Add("Zongjine", typeof(string));
            dt.Columns.Add("ZhekouJE", typeof(string));
            dt.Columns.Add("ShiFuJine", typeof(string));
            dt.Columns.Add("Beizhu", typeof(string));
            var i = 0;
            foreach (var item in tempData)
            {
                DataRow row = dt.NewRow();
                row["Bianhao"] = item.Bianhao;
                row["Zhuangtai"] = MvcApplication.OrderState[(Int32)item.Zhuangtai];//订单状态
                row["Mingcheng"] = item.Mingcheng;
                row["CGLX"] = MvcApplication.OrderType[(Int32)item.CGLX];//订单类型
                row["KehuDH"] = item.KehuDH;
                row["XiadanRQ"] = item.XiadanRQ;
                row["ZongshuCG"] = item.ZongshuCG;
                row["YFQty"] = fh[i];
                row["DFQty"] = fh[i] == "" ? "" : (item.ZongshuCG - float.Parse(fh[i]) - float.Parse(qh[i])).ToString();
                row["QHQty"] = fh[i] == "" ? "" : qh[i];
                row["Zongjine"] = item.Zongjine;
                row["ZhekouJE"] = item.ZhekouJE;
                row["ShiFuJine"] = item.Zongjine - (item.ZhekouJE == null ? 0 : item.ZhekouJE);
                row["Beizhu"] = item.Beizhu;
                dt.Rows.Add(row);
                i++;
            }
            DataSet ds = new DataSet();
            dt.TableName = "PurchaseOrders";
            ds.Tables.Add(dt);
            ExcelHelper.ExportExcel(ds, "PurchaseOrders");
            return new EmptyResult();
        }


        public ActionResult CustomerOrderListExportFile()
        {
            int userid = (int)Session["user_id"];
            int custid = (int)Session["customer_id"];
            string pagetag = "ord_dingdan_customerorderlist";

            Expression<Func<ord_ordermain_v, bool>> where = PredicateExtensionses.True<ord_ordermain_v>();
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
                            string mingchengand = scld[3];
                            if (!string.IsNullOrEmpty(Zhuangtai))
                            {
                                if (Zhuangtaiequal.Equals("="))
                                {
                                    if (mingchengand.Equals("and"))
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
            where = where.And(ord_dingdan => ord_dingdan.Zhuangtai != 0);

            var tempData = ob_ord_dingdanservice.LoadCustomerOverOrders(custid, where.Compile()).ToList<ord_ordermain_v>();
            //var tempData = ob_ord_dingdanservice.LoadCustomerOverOrders(custid, where.Compile()).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));

            DataTable dt = new DataTable();
            dt.Columns.Add("Bianhao", typeof(string));
            dt.Columns.Add("Zhuangtai", typeof(string));
            dt.Columns.Add("KehuMC", typeof(string));
            dt.Columns.Add("Mingcheng", typeof(string));
            dt.Columns.Add("CGLX", typeof(string));
            dt.Columns.Add("KehuDH", typeof(string));
            dt.Columns.Add("XiadanRQ", typeof(string));
            dt.Columns.Add("Lianxiren", typeof(string));
            dt.Columns.Add("LianxiDH", typeof(string));
            dt.Columns.Add("SonghuoDZ", typeof(string));
            dt.Columns.Add("ZongshuCG", typeof(string));
            dt.Columns.Add("Zongjine", typeof(string));
            dt.Columns.Add("ZhekouJE", typeof(string));
            dt.Columns.Add("ShiJiFu", typeof(string));
            dt.Columns.Add("Beizhu", typeof(string));
            foreach (var item in tempData)
            {
                DataRow row = dt.NewRow();
                row["Bianhao"] = item.Bianhao;
                row["Zhuangtai"] = MvcApplication.OrderState[(Int32)item.Zhuangtai];
                row["KehuMC"] = item.KehuMC;
                row["Mingcheng"] = item.Mingcheng;
                row["CGLX"] = MvcApplication.OrderType[(Int32)item.CGLX];
                row["KehuDH"] = item.KehuDH;
                row["XiadanRQ"] = item.XiadanRQ == null ? "" : Convert.ToDateTime(item.XiadanRQ).ToString("yyyy-MM-dd");
                row["Lianxiren"] = item.Lianxiren;
                row["LianxiDH"] = item.LianxiDH;
                row["SonghuoDZ"] = item.SonghuoDZ;
                row["ZongshuCG"] = item.ZongshuCG;
                row["Zongjine"] = item.Zongjine;
                row["ZhekouJE"] = item.ZhekouJE == null ? 0 : item.ZhekouJE;
                row["ShiJiFu"] = item.Zongjine - (item.ZhekouJE == null ? 0 : item.ZhekouJE);
                row["Beizhu"] = item.Beizhu;
                dt.Rows.Add(row);
            }
            DataSet ds = new DataSet();
            dt.TableName = "CustomerOrders";
            ds.Tables.Add(dt);
            ExcelHelper.ExportExcel(ds, "CustomerOrders");
            return new EmptyResult();
        }

        public JsonResult check()
        {
            int _userid = (int)Session["user_id"];
            var _ddid = Request["ddid"] ?? "";
            var _opid = Request["opid"] ?? "";
            var _zongjine = Request["zongjine"] ?? "";
            var _zhekouJE = Request["zhekouje"] ?? "";

            if (string.IsNullOrEmpty(_opid))
                return Json(-1);
            try
            {
                var id = int.Parse(_ddid);
                var opid = int.Parse(_opid);
                var ob_ord_dingdan = ob_ord_dingdanservice.GetEntityById(ord_dingdan => ord_dingdan.ID == id && ord_dingdan.IsDelete == false);
                //更新返利
                var zkje = decimal.Parse(_zhekouJE);
                if (!(zkje == 0 && ob_ord_dingdan.ZhekouJE == 0))
                {
                    var _flxf = ServiceFactory.ord_fanlixfservice.GetEntityById(ord_fanlixf => ord_fanlixf.DDID == id && ord_fanlixf.IsDelete == false);
                    if (_flxf != null)
                    {
                        decimal? dif = _flxf.XFJE - zkje;
                        var _fl = ServiceFactory.ord_fanliservice.GetEntityById(ord_fanlixf => ord_fanlixf.KHID == ob_ord_dingdan.KHID && ord_fanlixf.IsDelete == false);
                        if (_fl != null)
                        {
                            _fl.Zonge = _fl.Zonge + dif;
                            _fl.Keyong = _fl.Keyong + dif;
                            ServiceFactory.ord_fanliservice.UpdateEntity(_fl);
                        }
                        else
                        {
                        }
                        _flxf.KHID = ob_ord_dingdan.KHID;
                        _flxf.XFJE = zkje;
                        _flxf.MakeMan = _userid;
                        ServiceFactory.ord_fanlixfservice.UpdateEntity(_flxf);
                    }
                    else
                    {
                        ord_fanlixf _xf = new ord_fanlixf();
                        _xf.DDID = id;
                        _xf.KHID = ob_ord_dingdan.KHID;
                        _xf.XFJE = zkje;
                        _xf.MakeMan = _userid;
                        _xf = ServiceFactory.ord_fanlixfservice.AddEntity(_xf);
                    }
                }
                ob_ord_dingdan.Zhuangtai = 16;
                ob_ord_dingdan.OPID = opid;
                ob_ord_dingdan.Zongjine = decimal.Parse(_zongjine);
                ob_ord_dingdan.ZhekouJE = decimal.Parse(_zhekouJE);
                ob_ord_dingdan.MakeDate = DateTime.Now;
                ob_ord_dingdanservice.UpdateEntity(ob_ord_dingdan);
                return Json(1);
            }
            catch (Exception)
            {
                return Json(-1);
            }
        }
        public JsonResult deliverycount()
        {
            string _KHID = Request["khid"] ?? "";
            string _CPXID = Request["cpxid"] ?? "";
            int KHID = int.Parse(_KHID);
            int CPXID = int.Parse(_CPXID);
            int count = 0;
            try
            {
                var temp = ServiceFactory.ord_dingdanservice.getdeliverycount(KHID, CPXID).ToList<ord_deliverycount_v>(); ;
                if (temp == null || temp[0].Col3 == null)
                {
                    count = 0;
                }
                else
                {
                    count = int.Parse(temp[0].Col3);
                }
                return Json(count);
            }
            catch 
            {
                return Json(-1);
            }
        }
    }
    public class SPList
    {
        public int spid { get; set; }
        public float spsl { get; set; }
        public decimal spjg { get; set; }
        public decimal spje { get; set; }
    }
}

