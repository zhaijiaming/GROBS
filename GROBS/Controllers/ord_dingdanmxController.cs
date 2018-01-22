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
    public class ord_dingdanmxController : Controller
    {
        private Iord_dingdanmxService ob_ord_dingdanmxservice = ServiceFactory.ord_dingdanmxservice;
        private Ibase_taobaospService ob_base_taobaospservice = ServiceFactory.base_taobaospservice;
        private Ibase_shangpinxxService ob_base_shangpinxxservice = ServiceFactory.base_shangpinxxservice;


        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_dingdanmx_index";
            Expression<Func<ord_dingdanmx, bool>> where = PredicateExtensionses.True<ord_dingdanmx>();
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
                                        where = where.And(ord_dingdanmx => ord_dingdanmx.DDID == int.Parse(ddid));
                                    else
                                        where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID == int.Parse(ddid));
                                }
                                if (ddidequal.Equals(">"))
                                {
                                    if (ddidand.Equals("and"))
                                        where = where.And(ord_dingdanmx => ord_dingdanmx.DDID > int.Parse(ddid));
                                    else
                                        where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID > int.Parse(ddid));
                                }
                                if (ddidequal.Equals("<"))
                                {
                                    if (ddidand.Equals("and"))
                                        where = where.And(ord_dingdanmx => ord_dingdanmx.DDID < int.Parse(ddid));
                                    else
                                        where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID < int.Parse(ddid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_dingdanmx => ord_dingdanmx.IsDelete == false);

            var tempData = ob_ord_dingdanmxservice.LoadSortEntities(where.Compile(), false, ord_dingdanmx => ord_dingdanmx.ID).ToPagedList<ord_dingdanmx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_dingdanmx = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_dingdanmx_index";
            string page = "1";
            string ddid = Request["ddid"] ?? "";
            string ddidequal = Request["ddidequal"] ?? "";
            string ddidand = Request["ddidand"] ?? "";
            Expression<Func<ord_dingdanmx, bool>> where = PredicateExtensionses.True<ord_dingdanmx>();
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
                            where = where.And(ord_dingdanmx => ord_dingdanmx.DDID == int.Parse(ddid));
                        else
                            where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID == int.Parse(ddid));
                    }
                    if (ddidequal.Equals(">"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_dingdanmx => ord_dingdanmx.DDID > int.Parse(ddid));
                        else
                            where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID > int.Parse(ddid));
                    }
                    if (ddidequal.Equals("<"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_dingdanmx => ord_dingdanmx.DDID < int.Parse(ddid));
                        else
                            where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID < int.Parse(ddid));
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
                            where = where.And(ord_dingdanmx => ord_dingdanmx.DDID == int.Parse(ddid));
                        else
                            where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID == int.Parse(ddid));
                    }
                    if (ddidequal.Equals(">"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_dingdanmx => ord_dingdanmx.DDID > int.Parse(ddid));
                        else
                            where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID > int.Parse(ddid));
                    }
                    if (ddidequal.Equals("<"))
                    {
                        if (ddidand.Equals("and"))
                            where = where.And(ord_dingdanmx => ord_dingdanmx.DDID < int.Parse(ddid));
                        else
                            where = where.Or(ord_dingdanmx => ord_dingdanmx.DDID < int.Parse(ddid));
                    }
                }
                if (!string.IsNullOrEmpty(ddid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddid", ddid, ddidequal, ddidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "ddid", "", ddidequal, ddidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_dingdanmx => ord_dingdanmx.IsDelete == false);

            var tempData = ob_ord_dingdanmxservice.LoadSortEntities(where.Compile(), false, ord_dingdanmx => ord_dingdanmx.ID).ToPagedList<ord_dingdanmx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_dingdanmx = tempData;
            return View(tempData);
        }

        public ActionResult Add()
        {
            string ddid = Request["ddid"] ?? "";
            ViewBag.ddid = ddid;
            string cpx = Request["cpx"] ?? "";
            ViewBag.cpx = cpx;
            string cglx = Request["cglx"] ?? "";
            ViewBag.cglx = cglx;

            ViewBag.userid = (int)Session["user_id"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string ddid = Request["ddid"] ?? "";
            string spid = Request["spid"] ?? "";
            string spbm = Request["spbm"] ?? "";
            string spmc = Request["spmc"] ?? "";
            string guige = Request["guige"] ?? "";
            string cgsl = Request["cgsl"] ?? "";
            string fhsl = Request["fhsl"] ?? "";
            string xsbj = Request["xsbj"] ?? "";
            string xsdj = Request["xsdj"] ?? "";
            string jine = Request["jine"] ?? "";
            string zhekou = Request["zhekou"] ?? "";
            string zhekoulv = Request["zhekoulv"] ?? "";
            string hsl = Request["hsl"] ?? "";
            string hsbm = Request["hsbm"] ?? "";
            string jbdw = Request["jbdw"] ?? "";
            string xsdw = Request["xsdw"] ?? "";
            string col1 = Request["col1"] ?? "";        
            string pfsl = Request["PFSL"] ?? "";       //批复数量
            string col3 = Request["col3"] ?? "";
            string col4 = Request["col4"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int _id = int.Parse(ddid);
            try
            {
                ord_dingdanmx ob_ord_dingdanmx = new ord_dingdanmx();
                ob_ord_dingdanmx.DDID = ddid == "" ? 0 : int.Parse(ddid);
                ob_ord_dingdanmx.SPID = spid == "" ? 0 : int.Parse(spid);
                ob_ord_dingdanmx.SPBM = spbm.Trim();
                ob_ord_dingdanmx.SPMC = spmc.Trim();
                ob_ord_dingdanmx.Guige = guige.Trim();
                ob_ord_dingdanmx.CGSL = cgsl == "" ? 0 : float.Parse(cgsl);
                ob_ord_dingdanmx.FHSL = fhsl == "" ? 0 : float.Parse(fhsl);
                ob_ord_dingdanmx.XSBJ = xsbj == "" ? 0 : decimal.Parse(xsbj);
                ob_ord_dingdanmx.XSDJ = xsdj == "" ? 0 : decimal.Parse(xsdj);
                ob_ord_dingdanmx.Jine = jine == "" ? 0 : decimal.Parse(jine);
                ob_ord_dingdanmx.Zhekou = zhekou == "" ? 0 : decimal.Parse(zhekou);
                ob_ord_dingdanmx.Zhekoulv = zhekoulv == "" ? 0 : float.Parse(zhekoulv);
                ob_ord_dingdanmx.HSL = hsl == "" ? 0 : float.Parse(hsl);
                ob_ord_dingdanmx.HSBM = hsbm.Trim();
                ob_ord_dingdanmx.JBDW = jbdw.Trim();
                ob_ord_dingdanmx.XSDW = xsdw.Trim();
                ob_ord_dingdanmx.Col1 = col1.Trim();
                ob_ord_dingdanmx.PFSL = pfsl == "" ? 0 : float.Parse(pfsl);
                ob_ord_dingdanmx.Col3 = col3.Trim();
                ob_ord_dingdanmx.Col4 = col4.Trim();
                ob_ord_dingdanmx.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_dingdanmx.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_dingdanmx = ob_ord_dingdanmxservice.AddEntity(ob_ord_dingdanmx);
                ViewBag.ord_dingdanmx = ob_ord_dingdanmx;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Edit","ord_dingdan",new { id = _id});
        }

        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            string cpx = Request["cpx"] ?? "";
            ViewBag.cpx = cpx;
            string cglx = Request["cglx"] ?? "";
            ViewBag.cglx = cglx;
            string kysl = Request["kysl"] ?? "";
            ViewBag.kysl = kysl;

            ord_dingdanmx tempData = ob_ord_dingdanmxservice.GetEntityById(ord_dingdanmx => ord_dingdanmx.ID == id && ord_dingdanmx.IsDelete == false);
            ViewBag.ord_dingdanmx = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_dingdanmxViewModel ord_dingdanmxviewmodel = new ord_dingdanmxViewModel();
                ord_dingdanmxviewmodel.ID = tempData.ID;
                ord_dingdanmxviewmodel.DDID = tempData.DDID;
                ord_dingdanmxviewmodel.SPID = tempData.SPID;
                ord_dingdanmxviewmodel.SPBM = tempData.SPBM;
                ord_dingdanmxviewmodel.SPMC = tempData.SPMC;
                ord_dingdanmxviewmodel.Guige = tempData.Guige;
                ord_dingdanmxviewmodel.CGSL = tempData.CGSL;
                ord_dingdanmxviewmodel.FHSL = tempData.FHSL;
                ord_dingdanmxviewmodel.XSBJ = tempData.XSBJ;
                ord_dingdanmxviewmodel.Danjia = tempData.Danjia;
                ord_dingdanmxviewmodel.XSDJ = tempData.XSDJ;
                ord_dingdanmxviewmodel.Jine = tempData.Jine;
                ord_dingdanmxviewmodel.Zhekou = tempData.Zhekou;
                ord_dingdanmxviewmodel.Zhekoulv = tempData.Zhekoulv;
                ord_dingdanmxviewmodel.HSL = tempData.HSL;
                ord_dingdanmxviewmodel.HSBM = tempData.HSBM;
                ord_dingdanmxviewmodel.JBDW = tempData.JBDW;
                ord_dingdanmxviewmodel.XSDW = tempData.XSDW;
                ord_dingdanmxviewmodel.Col1 = tempData.Col1;
                ord_dingdanmxviewmodel.PFSL = tempData.PFSL;
                ord_dingdanmxviewmodel.Col3 = tempData.Col3;
                ord_dingdanmxviewmodel.Col4 = tempData.Col4;
                ord_dingdanmxviewmodel.MakeDate = tempData.MakeDate;
                ord_dingdanmxviewmodel.MakeMan = tempData.MakeMan;
                return View(ord_dingdanmxviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string ddid = Request["ddid"] ?? "";
            string spid = Request["spid"] ?? "";
            string spbm = Request["spbm"] ?? "";
            string spmc = Request["spmc"] ?? "";
            string guige = Request["guige"] ?? "";
            string cgsl = Request["cgsl"] ?? "";
            string fhsl = Request["fhsl"] ?? "";
            string xsbj = Request["xsbj"] ?? "";
            string xsdj = Request["xsdj"] ?? "";
            string jine = Request["jine"] ?? "";
            string zhekou = Request["zhekou"] ?? "";
            string zhekoulv = Request["zhekoulv"] ?? "";
            string zhekoulv_percent = Request["zhekoulv_percent"] ?? "";
            string hsl = Request["hsl"] ?? "";
            string hsbm = Request["hsbm"] ?? "";
            string jbdw = Request["jbdw"] ?? "";
            string xsdw = Request["xsdw"] ?? "";
            string col1 = Request["col1"] ?? "";
            string pfsl = Request["pfsl"] ?? "";
            string col3 = Request["col3"] ?? "";
            string col4 = Request["col4"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            int _id = int.Parse(ddid);
            try
            {
                ord_dingdanmx p = ob_ord_dingdanmxservice.GetEntityById(ord_dingdanmx => ord_dingdanmx.ID == uid);
                //p.DDID = ddid == "" ? 0 : int.Parse(ddid);
                //p.SPID = spid == "" ? 0 : int.Parse(spid);
                //p.SPBM = spbm.Trim();
                //p.SPMC = spmc.Trim();
                //p.Guige = guige.Trim();
                //p.CGSL = cgsl == "" ? 0 : float.Parse(cgsl);
                //p.FHSL = fhsl == "" ? 0 : float.Parse(fhsl);
                //p.XSBJ = xsbj == "" ? 0 : float.Parse(xsbj);
                p.XSDJ = xsdj == "" ? 0 : decimal.Parse(xsdj);
                ////p.Jine = jine == "" ? 0 : float.Parse(jine);
                ////p.Zhekou = zhekou == "" ? 0 : float.Parse(zhekou);
                if (decimal.Parse(xsdj) == p.Danjia)
                {
                    p.Jine = decimal.Parse(jine);
                    p.Zhekou = decimal.Parse(zhekou);
                }
                else
                {
                    p.Jine = decimal.Parse(xsdj) * decimal.Parse(cgsl);
                    p.Zhekou = (decimal.Parse(xsbj) - decimal.Parse(xsdj)) * decimal.Parse(cgsl);                  
                }
                p.Zhekoulv = zhekoulv_percent == "" ? 0 : (float.Parse(zhekoulv_percent) / 100);
                //p.HSL = hsl == "" ? 0 : float.Parse(hsl);
                //p.HSBM = hsbm.Trim();
                //p.JBDW = jbdw.Trim();
                //p.XSDW = xsdw.Trim();
                //p.Col1 = col1.Trim();
                p.PFSL = pfsl == "" ? 0 : float.Parse(pfsl);
                //p.Col3 = col3.Trim();
                //p.Col4 = col4.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_dingdanmxservice.UpdateEntity(p);
                if (cgsl != pfsl || xsbj != xsdj)
                {

                }
                ViewBag.saveok = ViewAddTag.ModifyOk;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.saveok = ViewAddTag.ModifyNo;
            }
            return RedirectToAction("Edit","ord_dingdan", new { id = _id });
        }
        public ActionResult Delete()
        {
            string sdel = Request["del"] ?? "";
            int id;
            ord_dingdanmx ob_ord_dingdanmx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_dingdanmx = ob_ord_dingdanmxservice.GetEntityById(ord_dingdanmx => ord_dingdanmx.ID == id && ord_dingdanmx.IsDelete == false);
                    ob_ord_dingdanmx.IsDelete = true;
                    ob_ord_dingdanmxservice.UpdateEntity(ob_ord_dingdanmx);
                }
            }
            return RedirectToAction("Index");
        }

        public JsonResult DeleteFromDingdan()
        {
            string sdel = Request["del"] ?? "";
            int id;
            ord_dingdanmx ob_ord_dingdanmx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_dingdanmx = ob_ord_dingdanmxservice.GetEntityById(ord_dingdanmx => ord_dingdanmx.ID == id && ord_dingdanmx.IsDelete == false);
                    ob_ord_dingdanmx.IsDelete = true;
                    ob_ord_dingdanmxservice.UpdateEntity(ob_ord_dingdanmx);
                }
            }
            return Json(1);
        }
        public JsonResult getDingdanMingXiWithDDID()
        {
            string _ddid = Request["ddid"] ?? "";
            string _kehudm = Request["kehudm"] ?? "";
            string _cglx = Request["cglx"] ?? "";
            WebReference.MStock xcl = new WebReference.MStock();
            if (!string.IsNullOrEmpty(_ddid))
            {
                var tempdata = ServiceFactory.ord_dingdanmxservice.LoadSortEntities(p => p.DDID == int.Parse(_ddid) && p.IsDelete == false, true, p => p.DDID).ToList<ord_dingdanmx>();
                if(tempdata != null)
                {
                    foreach (ord_dingdanmx ddmx in tempdata)
                    {
                        //不属于套包
                        if (int.Parse(_cglx) != 2)
                        {
                            float u8 = (float)xcl.GetCurrentStock(_kehudm, ddmx.SPBM);
                            float num = 0;
                            float num_tb = 0;
                            try
                            {
                                var temp = ServiceFactory.ord_dingdanmxservice.LoadByItemCode(ddmx.SPBM).ToList<ord_lockquantity_v>(); ;
                                var temp1 = ServiceFactory.ord_dingdanmxservice.LoadByItemCode_TB(ddmx.SPBM).ToList<ord_lockquantitytb_v>(); ;
                                if (temp == null || temp[0].SPBM == null)
                                {
                                    num = 0;
                                    //ddmx.KYSL = u8;
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
                                    //ddmx.KYSL = u8 - float.Parse(temp[0].SPBM);
                                }
                                ddmx.KYSL = u8 - num - num_tb;
                            }
                            catch
                            {
                                //ddmx.KYSL = u8;
                                ddmx.KYSL = 0;
                            }
                        }
                        //属于套包
                        else if(int.Parse(_cglx) == 2)
                        {
                            try
                            {
                                var tempData = ServiceFactory.base_taobaospservice.LoadPackageDetailByID(ddmx.SPID).ToList<base_taobaosp_v>();
                                string tp1 = "";
                                string tp2 = "";
                                foreach (var td in tempData)
                                {
                                    float u8 = (float)xcl.GetCurrentStock(_kehudm, td.Daima);
                                    float num = 0;
                                    float num_tb = 0;
                                    var temp = ServiceFactory.ord_dingdanmxservice.LoadByItemCode(td.Daima).ToList<ord_lockquantity_v>(); ;
                                    var temp1 = ServiceFactory.ord_dingdanmxservice.LoadByItemCode_TB(td.Daima).ToList<ord_lockquantitytb_v>(); ;
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
                                    tp2 = tp2 + td.Shuliang.ToString() + ",";
                                }
                                ddmx.KYSL = getavli_tb(tp1, tp2);
                            }
                            catch(Exception ex)
                            {
                                ddmx.KYSL = 0;
                            }
                        }
                    }
                    return Json(tempdata);
                }
                else
                {
                    return Json(-1);
                }
            }
            else
            {
                return Json(-1);
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
        public JsonResult GetCommodityPrice()
        {
            var _cpx = Request["cpx"] ?? "";
            var _sp = Request["sp"] ?? "";

            if (string.IsNullOrEmpty(_cpx) || string.IsNullOrEmpty(_sp))
                return Json(-1);

            var _spjg = ServiceFactory.base_shangpinxxservice.GetEntityById(p => p.Daima == _sp && p.Chanpinxian==int.Parse(_cpx) && p.IsDelete == false);
            if (_spjg == null)
                return Json(-2);
            return Json(_spjg);
        }
        public JsonResult SafeFromImportTB()
        {
            //1.获取订单ID以及文本域
            var _ddid = Request["ddid"] ?? "";
            var _cpx = Request["cpx"] ?? "";
            var _imt = Request["imt"] ?? "";
            //2.用‘套包代码’获取套包信息，循环文本域，将'套包信息'和'一些字段'保存到ord_dingdanmx对象中
            if (string.IsNullOrEmpty(_cpx) || string.IsNullOrEmpty(_imt))
            {
                return Json(-1);
            }
            else
            {
                try
                {
                    string[] _item = _imt.Split();
                    for (int i = 0; i < _item.Count(); i = i + 2)
                    {
                        string _dm = _item[i];//代码
                        string _sl = _item[i + 1];//数量
                        var _taobao = ServiceFactory.base_taobaoservice.GetEntityById(p => p.Daima == _dm && p.CPXID == int.Parse(_cpx) && p.IsDelete == false);
                        if (_taobao == null)
                            continue;
                        ord_dingdanmx ob_ord_dingdanmx = new ord_dingdanmx();
                        ob_ord_dingdanmx.DDID = _ddid == "" ? 0 : int.Parse(_ddid);
                        ob_ord_dingdanmx.SPID = _taobao.ID;
                        ob_ord_dingdanmx.SPBM = _taobao.Daima.Trim();
                        ob_ord_dingdanmx.SPMC = _taobao.Mingcheng.Trim();
                        ob_ord_dingdanmx.Guige = _taobao.Miaoshu.Trim();
                        ob_ord_dingdanmx.CGSL = _sl == "" ? 0 : int.Parse(_sl);
                        ob_ord_dingdanmx.FHSL = 0;
                        ob_ord_dingdanmx.XSBJ = _taobao.JiaXS;
                        ob_ord_dingdanmx.XSDJ = _taobao.JiaXS;
                        ob_ord_dingdanmx.Jine = _taobao.JiaXS * decimal.Parse(_sl);
                        ob_ord_dingdanmx.Zhekou = 0;
                        ob_ord_dingdanmx.Zhekoulv = 0;
                        ob_ord_dingdanmx.HSL = 0;
                        ob_ord_dingdanmx.HSBM = "";
                        ob_ord_dingdanmx.JBDW = "";
                        ob_ord_dingdanmx.XSDW = "";
                        ob_ord_dingdanmx.Col1 = "";
                        ob_ord_dingdanmx.PFSL = 0;
                        ob_ord_dingdanmx.Col3 = "";
                        ob_ord_dingdanmx.Col4 = "";
                        ob_ord_dingdanmx.MakeDate = DateTime.Now;
                        ob_ord_dingdanmx.MakeMan = (int)Session["user_id"];
                        ob_ord_dingdanmx = ob_ord_dingdanmxservice.AddEntity(ob_ord_dingdanmx);
                    }
                    return Json(1);
                }
                catch
                {
                    return Json(-3);
                }
            }
        }
        public JsonResult SafeFromImportSP()
        {
            //1.获取订单ID以及文本域
            var _ddid = Request["ddid"] ?? "";
            var _cpx = Request["cpx"] ?? "";
            var _imt = Request["imt"] ?? "";
            //2.用‘商品代码’获取商品信息，循环文本域，将'商品信息'和'一些字段'保存到ord_dingdanmx对象中
            if (string.IsNullOrEmpty(_cpx) || string.IsNullOrEmpty(_imt))
            {
                return Json(-1);
            }
            else
            {
                try
                {
                    string[] _item = _imt.Split();
                    for (int i = 0; i < _item.Count(); i = i + 2)
                    {
                        string _dm = _item[i];//代码
                        string _sl = _item[i + 1];//数量
                        var _spxx = ServiceFactory.base_shangpinxxservice.LoadShangpinPriceAll(p => p.Daima == _dm && p.chanpinxian == int.Parse(_cpx)).ToList();
                        if (_spxx == null)
                            continue;
                        ord_dingdanmx ob_ord_dingdanmx = new ord_dingdanmx();
                        ob_ord_dingdanmx.DDID = _ddid == "" ? 0 : int.Parse(_ddid);
                        ob_ord_dingdanmx.SPID = _spxx.First().ID;
                        ob_ord_dingdanmx.SPBM = _spxx.First().Daima.Trim();
                        ob_ord_dingdanmx.SPMC = _spxx.First().Mingcheng.Trim();
                        ob_ord_dingdanmx.Guige = _spxx.First().Guige.Trim();
                        ob_ord_dingdanmx.CGSL = _sl == "" ? 0 : int.Parse(_sl);
                        ob_ord_dingdanmx.FHSL = 0;
                        ob_ord_dingdanmx.XSBJ = _spxx.First().JiaXS;
                        ob_ord_dingdanmx.XSDJ = _spxx.First().JiaXS;
                        ob_ord_dingdanmx.Jine = ob_ord_dingdanmx.XSBJ * (decimal)ob_ord_dingdanmx.CGSL;
                        ob_ord_dingdanmx.Zhekou = 0;
                        ob_ord_dingdanmx.Zhekoulv = 0;
                        ob_ord_dingdanmx.HSL = _spxx.First().Huansuanlv;
                        ob_ord_dingdanmx.HSBM = _spxx.First().Col1;
                        ob_ord_dingdanmx.JBDW = _spxx.First().Danwei;
                        ob_ord_dingdanmx.XSDW = _spxx.First().BaozhuangDW;
                        ob_ord_dingdanmx.Col1 = "";
                        ob_ord_dingdanmx.PFSL = 0;
                        ob_ord_dingdanmx.Col3 = "";
                        ob_ord_dingdanmx.Col4 = "";
                        ob_ord_dingdanmx.MakeDate = DateTime.Now;
                        ob_ord_dingdanmx.MakeMan = (int)Session["user_id"];
                        ob_ord_dingdanmx = ob_ord_dingdanmxservice.AddEntity(ob_ord_dingdanmx);
                    }
                    return Json(1);
                }
                catch
                {
                    return Json(-3);
                }
            }
        }
    }
}

