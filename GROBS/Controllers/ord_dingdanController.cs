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
    public class ord_dingdanController : Controller
    {
        private Iord_dingdanService ob_ord_dingdanservice = ServiceFactory.ord_dingdanservice;
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
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_dingdan => ord_dingdan.IsDelete == false);

            var tempData = ob_ord_dingdanservice.LoadSortEntities(where.Compile(), false, ord_dingdan => ord_dingdan.ID).ToPagedList<ord_dingdan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
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
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_dingdan => ord_dingdan.IsDelete == false);

            var tempData = ob_ord_dingdanservice.LoadSortEntities(where.Compile(), false, ord_dingdan => ord_dingdan.ID).ToPagedList<ord_dingdan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
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
            try
            {
                ord_dingdan ob_ord_dingdan = new ord_dingdan();
                ob_ord_dingdan.Bianhao = bianhao.Trim();
                ob_ord_dingdan.KHID = khid == "" ? 0 : int.Parse(khid);
                ob_ord_dingdan.CPXID = cpxid == "" ? 0 : int.Parse(cpxid);
                ob_ord_dingdan.CGLX = cglx == "" ? 0 : int.Parse(cglx);
                ob_ord_dingdan.KehuDH = kehudh.Trim();
                ob_ord_dingdan.XiadanRQ = xiadanrq == "" ? DateTime.Now : DateTime.Parse(xiadanrq);
                ob_ord_dingdan.Lianxiren = lianxiren.Trim();
                ob_ord_dingdan.LianxiDH = lianxidh.Trim();
                ob_ord_dingdan.SonghuoDZ = songhuodz.Trim();
                ob_ord_dingdan.OPID = opid == "" ? 0 : int.Parse(opid);
                ob_ord_dingdan.ACCID = accid == "" ? 0 : int.Parse(accid);
                ob_ord_dingdan.ShenheTG = shenhetg == "" ? false : Boolean.Parse(shenhetg);
                ob_ord_dingdan.ZongshuCG = zongshucg == "" ? 0 : float.Parse(zongshucg);
                ob_ord_dingdan.Zongjine = zongjine == "" ? 0 : float.Parse(zongjine);
                ob_ord_dingdan.JieshuSF = jieshusf == "" ? false : Boolean.Parse(jieshusf);
                ob_ord_dingdan.Zhuangtai = zhuangtai == "" ? 0 : int.Parse(zhuangtai);
                ob_ord_dingdan.Beizhu = beizhu.Trim();
                ob_ord_dingdan.Col1 = col1.Trim();
                ob_ord_dingdan.Col2 = col2.Trim();
                ob_ord_dingdan.Col3 = col3.Trim();
                ob_ord_dingdan.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_dingdan.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_dingdan.KehuDM = kehudm.Trim();
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
                return View(ord_dingdanviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
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
            int uid = int.Parse(id);
            try
            {
                ord_dingdan p = ob_ord_dingdanservice.GetEntityById(ord_dingdan => ord_dingdan.ID == uid);
                p.Bianhao = bianhao.Trim();
                p.KHID = khid == "" ? 0 : int.Parse(khid);
                p.CPXID = cpxid == "" ? 0 : int.Parse(cpxid);
                p.CGLX = cglx == "" ? 0 : int.Parse(cglx);
                p.KehuDH = kehudh.Trim();
                p.XiadanRQ = xiadanrq == "" ? DateTime.Now : DateTime.Parse(xiadanrq);
                p.Lianxiren = lianxiren.Trim();
                p.LianxiDH = lianxidh.Trim();
                p.SonghuoDZ = songhuodz.Trim();
                p.OPID = opid == "" ? 0 : int.Parse(opid);
                p.ACCID = accid == "" ? 0 : int.Parse(accid);
                p.ShenheTG = shenhetg == "" ? false : Boolean.Parse(shenhetg);
                p.ZongshuCG = zongshucg == "" ? 0 : float.Parse(zongshucg);
                p.Zongjine = zongjine == "" ? 0 : float.Parse(zongjine);
                p.JieshuSF = jieshusf == "" ? false : Boolean.Parse(jieshusf);
                p.Zhuangtai = zhuangtai == "" ? 0 : int.Parse(zhuangtai);
                p.Beizhu = beizhu.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.KehuDM = kehudm.Trim();
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
        public ActionResult CustomerOrderList(string page)
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
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            var tempData = ob_ord_dingdanservice.LoadCustomerOverOrders(custid, where.Compile()).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_dingdan = tempData;
            return View(tempData);
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult CustomerOrderList()
        {
            int userid = (int)Session["user_id"];
            int custid = (int)Session["customer_id"];
            string pagetag = "ord_dingdan_customerorderlist";
            string page = "1";
            string bianhao = Request["bianhao"] ?? "";
            string bianhaoequal = Request["bianhaoequal"] ?? "";
            string bianhaoand = Request["bianhaoand"] ?? "";
            Expression<Func<ord_ordermain_v, bool>> where = PredicateExtensionses.True<ord_ordermain_v>();
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
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;

            var tempData = ob_ord_dingdanservice.LoadCustomerOverOrders(custid, where.Compile()).ToPagedList<ord_ordermain_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_dingdan = tempData;
            return View(tempData);
        }
        [OutputCache(Duration = 30)]
        public ActionResult CustomerCurrentOrder()
        {
            int _userid = (int)Session["user_id"];
            var _acct = (string)Session["account"];
            int _custid = (int)Session["customer_id"];
            var _shdw = ServiceFactory.base_shouhuodanweiservice.GetEntityById(p => p.ID == _custid);
            if (_shdw != null)
                ViewBag.customername = _shdw.Mingcheng;
            else
                ViewBag.customername = "0";

            //var tempData = ob_ord_dingdanservice.LoadSortEntities(p => p.KHID == _custid && p.Zhuangtai < 12 && p.IsDelete == false, true, s => s.Bianhao);
            var tempData = ob_ord_dingdanservice.LoadCustomerActiveOrders(_custid).OrderByDescending(p=>p.Bianhao);
            ViewBag.ord_dingdan = tempData;
            return View();
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
            _dd.ZhekouJE = float.Parse(_zkje);
            _dd.Zhuangtai = 10;
            _dd.Zongjine = float.Parse(_zje);
            _dd.ZongshuCG = float.Parse(_zsl);
            _dd.ShenheTG = false;
            _dd.HH = _splist.Count();
            _dd.MakeMan = _userid;
            _dd = ob_ord_dingdanservice.AddEntity(_dd);
            if (_dd == null)
                return Json(-2);

            float _zkl = 1 - (float)(_dd.ZhekouJE / _dd.Zongjine);
            _zkl = (float)Math.Round(_zkl, 4);
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
                    _spl.spjg = float.Parse(_sp[2]);
                    _spl.spje = float.Parse(_sp[3]);
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
                    _mx.XSBJ = spg.jg;
                    _mx.XSDJ = spg.jg * _zkl;
                    _mx.XSDW = _spxx.BaozhuangDW;
                    _mx.HSL = (float)_spxx.Huansuanlv;
                    _mx.HSBM = _spxx.Col1;
                    _mx.Jine = spg.tje;
                    _mx.Zhekou = spg.tje * (1 - _zkl);
                    _mx.Zhekoulv = _zkl;
                    _mx.MakeMan = _userid;
                    _mx = ServiceFactory.ord_dingdanmxservice.AddEntity(_mx);
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
        public ActionResult CustomerOrderInfo(int id)
        {
            int _custid = (int)Session["customer_id"];
            var _dd = ob_ord_dingdanservice.GetEntityById(p => p.ID == id && p.IsDelete == false);
            if (_dd == null)
                return View();
            var _cpx = ServiceFactory.base_chanpinxianservice.GetEntityById(p => p.ID == _dd.CPXID);
            if (_cpx == null)
                ViewBag.cpx = "";
            else
                ViewBag.cpx = _cpx.Mingcheng;
            ViewBag.cglx = _dd.CGLX;
            ViewBag.sl = _dd.ZongshuCG;
            ViewBag.je = _dd.Zongjine;
            ViewBag.khdh = _dd.KehuDH;
            ViewBag.bz = _dd.Beizhu;
            ViewBag.zk = _dd.ZhekouJE;
            ViewBag.lxr = _dd.Lianxiren;
            ViewBag.lxdh = _dd.LianxiDH;
            ViewBag.shdz = _dd.SonghuoDZ;
            var _ddmx = ServiceFactory.ord_dingdanmxservice.LoadSortEntities(p => p.DDID == _dd.ID && p.IsDelete == false, true, s => s.SPBM).ToList();
            ViewBag.ord_dingdanmx = _ddmx;
            return View();
        }
    }
    public class SPList
    {
        public int spid { get; set; }
        public float spsl { get; set; }
        public float spjg { get; set; }
        public float spje { get; set; }
    }
}

