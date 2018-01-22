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
    public class base_taobaospController : Controller
    {
        private Ibase_taobaospService ob_base_taobaospservice = ServiceFactory.base_taobaospservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_taobaosp_index";        
            Expression<Func<base_taobaosp_v, bool>> where = PredicateExtensionses.True<base_taobaosp_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "tbdm":
                            string tbdm = scld[1];
                            string tbdmequal = scld[2];
                            string tbdmand = scld[3];
                            if (!string.IsNullOrEmpty(tbdm))
                            {
                                if (tbdmequal.Equals("="))
                                {
                                    if (tbdmand.Equals("and"))
                                        where = where.And(base_chanpinxian => base_chanpinxian.TBDM == tbdm);
                                    else
                                        where = where.Or(base_chanpinxian => base_chanpinxian.TBDM == tbdm);
                                }
                                if (tbdmequal.Equals("like"))
                                {
                                    if (tbdmand.Equals("and"))
                                        where = where.And(base_chanpinxian => base_chanpinxian.Mingcheng.Contains(tbdm));
                                    else
                                        where = where.Or(base_chanpinxian => base_chanpinxian.Mingcheng.Contains(tbdm));
                                }
                            }
                            break;
                        default:
                            break;                       
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            //where = where.And(base_taobaosp => base_taobaosp.IsDelete == false);

            var tempData = ob_base_taobaospservice.LoadPackageDetail(where.Compile() ).ToPagedList<base_taobaosp_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_taobaosp = tempData;          
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_taobaosp_index";
            string page = "1";
            string tbdm = Request["tbdm"] ?? "";
            string tbdmequal = Request["tbdmequal"] ?? "";
            string tbdmand = Request["tbdmand"] ?? "";
            Expression<Func<base_taobaosp_v, bool>> where = PredicateExtensionses.True<base_taobaosp_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(tbdm))
                {
                    if (tbdmequal.Equals("="))
                    {
                        if (tbdmand.Equals("and"))
                            where = where.And(base_taobaosp => base_taobaosp.TBDM == tbdm);
                        else
                            where = where.Or(base_taobaosp => base_taobaosp.TBDM == tbdm);
                    }
                    if (tbdmequal.Equals("like"))
                    {
                        if (tbdmand.Equals("and"))
                            where = where.And(base_taobaosp => base_taobaosp.TBDM.Contains(tbdm));
                        else
                            where = where.Or(base_taobaosp => base_taobaosp.TBDM.Contains(tbdm));
                    }              
                }
                if (!string.IsNullOrEmpty(tbdm))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "tbdm", tbdm, tbdmequal, tbdmand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "tbdm", "", tbdmequal, tbdmand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(tbdm))
                {
                    if (tbdmequal.Equals("="))
                    {
                        if (tbdmand.Equals("and"))
                            where = where.And(base_taobaosp => base_taobaosp.TBDM == tbdm);
                        else
                            where = where.Or(base_taobaosp => base_taobaosp.TBDM == tbdm);
                    }
                    if (tbdmequal.Equals("like"))
                    {
                        if (tbdmand.Equals("and"))
                            where = where.And(base_taobaosp => base_taobaosp.TBDM.Contains(tbdm));
                        else
                            where = where.Or(base_taobaosp => base_taobaosp.TBDM.Contains(tbdm));
                    }
                   
                }
                if (!string.IsNullOrEmpty(tbdm))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "tbdm", tbdm, tbdmequal, tbdmand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "tbdm", "", tbdmequal, tbdmand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            //where = where.And(base_taobaosp => base_taobaosp.IsDelete == false);

            var tempData = ob_base_taobaospservice.LoadPackageDetail(where.Compile()).ToPagedList<base_taobaosp_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_taobaosp = tempData;
            return View(tempData);
        }     

        public ActionResult Add()
        {
            string tbId = Request["tbId"] ?? "";
            ViewBag.tbId = int.Parse(tbId);
            string cpxid = Request["cpxid"] ?? "";
            ViewBag.cpxid = int.Parse(cpxid);

            ViewBag.userid = (int)Session["user_id"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string tbid = Request["tbid"] ?? "";
            string spid = Request["spid"] ?? "";
            string shuliang = Request["shuliang"] ?? "";
            string hsl = Request["hsl"] ?? "";
            string jbdw = Request["jbdw"] ?? "";
            string xsdw = Request["xsdw"] ?? "";
            string jiaxs = Request["jiaxs"] ?? "";
            string jiacg = Request["jiacg"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int _id = int.Parse(tbid);


            try
            {
                base_taobaosp ob_base_taobaosp = new base_taobaosp();
                ob_base_taobaosp.TBID = tbid == "" ? 0 : int.Parse(tbid);
                ob_base_taobaosp.SPID = spid == "" ? 0 : int.Parse(spid);
                ob_base_taobaosp.Shuliang = shuliang == "" ? 0 : float.Parse(shuliang);
                ob_base_taobaosp.HSL = hsl == "" ? 0 : float.Parse(hsl);
                ob_base_taobaosp.JBDW = jbdw.Trim();
                ob_base_taobaosp.XSDW = xsdw.Trim();
                ob_base_taobaosp.JiaXS = jiaxs == "" ? 0 : decimal.Parse(jiaxs);
                ob_base_taobaosp.JiaCG = jiacg == "" ? 0 : decimal.Parse(jiacg);
                ob_base_taobaosp.Col1 = col1.Trim();
                ob_base_taobaosp.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_taobaosp.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_taobaosp = ob_base_taobaospservice.AddEntity(ob_base_taobaosp);
                ViewBag.base_taobaosp = ob_base_taobaosp;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Edit", "base_taobao", new { id = _id });
        }

        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            string cpxid = Request["cpxid"] ?? "";
            ViewBag.cpxid = int.Parse(cpxid);

            base_taobaosp tempData = ob_base_taobaospservice.GetEntityById(base_taobaosp => base_taobaosp.ID == id && base_taobaosp.IsDelete == false);
            ViewBag.base_taobaosp = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_taobaospViewModel base_taobaospviewmodel = new base_taobaospViewModel();
                base_taobaospviewmodel.ID = tempData.ID;
                base_taobaospviewmodel.TBID = tempData.TBID;
                base_taobaospviewmodel.SPID = tempData.SPID;
                base_taobaospviewmodel.Shuliang = tempData.Shuliang;
                base_taobaospviewmodel.HSL = tempData.HSL;
                base_taobaospviewmodel.JBDW = tempData.JBDW;
                base_taobaospviewmodel.XSDW = tempData.XSDW;
                base_taobaospviewmodel.JiaXS = tempData.JiaXS;
                base_taobaospviewmodel.JiaCG = tempData.JiaCG;
                base_taobaospviewmodel.Col1 = tempData.Col1;
                base_taobaospviewmodel.MakeDate = tempData.MakeDate;
                base_taobaospviewmodel.MakeMan = tempData.MakeMan;
                return View(base_taobaospviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string tbid = Request["tbid"] ?? "";
            string spid = Request["spid"] ?? "";
            string shuliang = Request["shuliang"] ?? "";
            string hsl = Request["hsl"] ?? "";
            string jbdw = Request["jbdw"] ?? "";
            string xsdw = Request["xsdw"] ?? "";
            string jiaxs = Request["jiaxs"] ?? "";
            string jiacg = Request["jiacg"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            int _tbid = int.Parse(tbid);
            try
            {
                base_taobaosp p = ob_base_taobaospservice.GetEntityById(base_taobaosp => base_taobaosp.ID == uid);
                p.TBID = tbid == "" ? 0 : int.Parse(tbid);
                p.SPID = spid == "" ? 0 : int.Parse(spid);
                p.Shuliang = shuliang == "" ? 0 : float.Parse(shuliang);
                p.HSL = hsl == "" ? 0 : float.Parse(hsl);
                p.JBDW = jbdw.Trim();
                p.XSDW = xsdw.Trim();
                p.JiaXS = jiaxs == "" ? 0 : decimal.Parse(jiaxs);
                p.JiaCG = jiacg == "" ? 0 : decimal.Parse(jiacg);
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_taobaospservice.UpdateEntity(p);
                ViewBag.saveok = ViewAddTag.ModifyOk;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.saveok = ViewAddTag.ModifyNo;
            }
            return RedirectToAction("Edit","base_taobao", new { id = _tbid });
        }
        public ActionResult Delete()
        {
            string sdel = Request["del"] ?? "";
            int id;
            base_taobaosp ob_base_taobaosp;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_taobaosp = ob_base_taobaospservice.GetEntityById(base_taobaosp => base_taobaosp.ID == id && base_taobaosp.IsDelete == false);
                    ob_base_taobaosp.IsDelete = true;
                    ob_base_taobaospservice.UpdateEntity(ob_base_taobaosp);
                }
            }
            return RedirectToAction("Index");
        }

        public JsonResult GetDetail()
        {
            string tbId = Request["tbId"] ?? "";
            if (string.IsNullOrEmpty(tbId))
            {
                return Json("");
            }
            else
            {
                var tempData = ServiceFactory.base_taobaospservice.LoadPackageDetailByID(int.Parse(tbId)).ToList<base_taobaosp_v>();
                if (tempData == null)
                    return Json("");
                else
                    return Json(tempData);
            }
        }
        public int DeleteInBase_taobao()
        {
            string sdel = Request["del"] ?? "";
            int id;
            base_taobaosp ob_base_taobaosp;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_taobaosp = ob_base_taobaospservice.GetEntityById(base_taobaosp => base_taobaosp.ID == id && base_taobaosp.IsDelete == false);
                    ob_base_taobaosp.IsDelete = true;
                    ob_base_taobaospservice.UpdateEntity(ob_base_taobaosp);
                }
            }
            return 1;
        }
    }
}

