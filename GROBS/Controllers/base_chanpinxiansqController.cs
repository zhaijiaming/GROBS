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
    public class base_chanpinxiansqController : Controller
    {
        private Ibase_chanpinxiansqService ob_base_chanpinxiansqservice = ServiceFactory.base_chanpinxiansqservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_chanpinxiansq_index";
            PageMenu.Set("Index", "base_chanpinxiansq", "»ù´¡Êý¾Ý");
            Expression<Func<base_chanpinxiansq, bool>> where = PredicateExtensionses.True<base_chanpinxiansq>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "jxsdm":
                            string jxsdm = scld[1];
                            string jxsdmequal = scld[2];
                            string jxsdmand = scld[3];
                            if (!string.IsNullOrEmpty(jxsdm))
                            {
                                if (jxsdmequal.Equals("="))
                                {
                                    if (jxsdmand.Equals("and"))
                                        where = where.And(base_chanpinxiansq => base_chanpinxiansq.JXSDM == jxsdm);
                                    else
                                        where = where.Or(base_chanpinxiansq => base_chanpinxiansq.JXSDM== jxsdm);
                                }                                                            
                            }
                            break;
                        case "cpxdm":
                            string cpxdm = scld[1];
                            string cpxdmequal = scld[2];
                            string cpxdmand = scld[3];
                            if (!string.IsNullOrEmpty(cpxdm))
                            {
                                if (cpxdmequal.Equals("="))
                                {
                                    if (cpxdmand.Equals("and"))
                                        where = where.And(base_chanpinxiansq => base_chanpinxiansq.CPXDM == cpxdm);
                                    else
                                        where = where.Or(base_chanpinxiansq => base_chanpinxiansq.CPXDM == cpxdm);
                                }
                                if (cpxdmequal.Equals("like"))
                                {
                                    if (cpxdmand.Equals("and"))
                                        where = where.And(base_chanpinxiansq => base_chanpinxiansq.CPXDM.Contains(cpxdm));
                                    else
                                        where = where.Or(base_chanpinxiansq => base_chanpinxiansq.CPXDM.Contains(cpxdm));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_chanpinxiansq => base_chanpinxiansq.IsDelete == false);

            var tempData = ob_base_chanpinxiansqservice.LoadSortEntities(where.Compile(), false, base_chanpinxiansq => base_chanpinxiansq.ID).ToPagedList<base_chanpinxiansq>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_chanpinxiansq = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_chanpinxiansq_index";
            string page = "1";
            //jxsdm
            string jxsdm = Request["jxsdm"] ?? "";
            string jxsdmequal = Request["jxsdmequal"] ?? "";
            string jxsdmand = Request["jxsdmand"] ?? "";
            //cpxdm
            string cpxdm = Request["cpxdm"] ?? "";
            string cpxdmequal = Request["cpxdmequal"] ?? "";
            string cpxdmand = Request["cpxdmand"] ?? "";        
            Expression<Func<base_chanpinxiansq, bool>> where = PredicateExtensionses.True<base_chanpinxiansq>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;

                //jxsdm
                if (!string.IsNullOrEmpty(jxsdm))
                {
                    if (jxsdmequal.Equals("="))
                    {
                        if (jxsdmand.Equals("and"))
                            where = where.And(base_chanpinxiansq => base_chanpinxiansq.JXSDM == jxsdm);
                        else
                            where = where.Or(base_chanpinxiansq => base_chanpinxiansq.JXSDM == jxsdm);
                    }                       
                }
                if (!string.IsNullOrEmpty(jxsdm))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jxsdm", jxsdm, jxsdmequal, jxsdmand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jxsdm", "", jxsdmequal, jxsdmand);

                //cpxdm
                if (!string.IsNullOrEmpty(cpxdm))
                {
                    if (cpxdmequal.Equals("="))
                    {
                        if (cpxdmand.Equals("and"))
                            where = where.And(base_chanpinxiansq => base_chanpinxiansq.CPXDM == cpxdm);
                        else
                            where = where.Or(base_chanpinxiansq => base_chanpinxiansq.CPXDM == cpxdm);
                    }
                    if (cpxdmequal.Equals("like"))
                    {
                        if (cpxdmand.Equals("and"))
                            where = where.And(base_chanpinxiansq => base_chanpinxiansq.CPXDM.Contains(cpxdm));
                        else
                            where = where.Or(base_chanpinxiansq => base_chanpinxiansq.CPXDM.Contains(cpxdm));
                    }
                }
                if (!string.IsNullOrEmpty(jxsdm))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jxsdm", jxsdm, jxsdmequal, jxsdmand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jxsdm", "", jxsdmequal, jxsdmand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(jxsdm))
                {
                    if (jxsdmequal.Equals("="))
                    {
                        if (jxsdmand.Equals("and"))
                            where = where.And(base_chanpinxiansq => base_chanpinxiansq.JXSDM == jxsdm);
                        else
                            where = where.Or(base_chanpinxiansq => base_chanpinxiansq.JXSDM== jxsdm);
                    }   
                }
                if (!string.IsNullOrEmpty(jxsdm))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jxsdm", jxsdm, jxsdmequal, jxsdmand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "jxsdm", "", jxsdmequal, jxsdmand);
                if (!string.IsNullOrEmpty(cpxdm))
                {
                    if (cpxdmequal.Equals("="))
                    {
                        if (cpxdmand.Equals("and"))
                            where = where.And(base_chanpinxian => base_chanpinxian.CPXDM == cpxdm);
                        else
                            where = where.Or(base_chanpinxian => base_chanpinxian.CPXDM == cpxdm);
                    }
                    if (cpxdmequal.Equals("like"))
                    {
                        if (cpxdmand.Equals("and"))
                            where = where.And(base_chanpinxian => base_chanpinxian.CPXDM.Contains(cpxdm));
                        else
                            where = where.Or(base_chanpinxian => base_chanpinxian.CPXDM.Contains(cpxdm));
                    }
                }
                if (!string.IsNullOrEmpty(cpxdm))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "cpxdm", cpxdm, cpxdmequal, cpxdmand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "cpxdm", "", cpxdmequal, cpxdmand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_chanpinxiansq => base_chanpinxiansq.IsDelete == false);

            var tempData = ob_base_chanpinxiansqservice.LoadSortEntities(where.Compile(), false, base_chanpinxiansq => base_chanpinxiansq.ID).ToPagedList<base_chanpinxiansq>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_chanpinxiansq = tempData;
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
            string jxsid = Request["jxsid"] ?? "";
            string cpxid = Request["cpxid"] ?? "";
            string jxsdm = Request["jxsdm"] ?? "";
            string cpxdm = Request["cpxdm"] ?? "";
            string tingyongsf = Request["tingyongsf"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                base_chanpinxiansq ob_base_chanpinxiansq = new base_chanpinxiansq();
                ob_base_chanpinxiansq.JXSID = jxsid == "" ? 0 : int.Parse(jxsid);
                ob_base_chanpinxiansq.CPXID = cpxid == "" ? 0 : int.Parse(cpxid);
                ob_base_chanpinxiansq.JXSDM = jxsdm.Trim();
                ob_base_chanpinxiansq.CPXDM = cpxdm.Trim();
                ob_base_chanpinxiansq.TingyongSF = tingyongsf == "" ? false : Boolean.Parse(tingyongsf);
                ob_base_chanpinxiansq.Col1 = col1.Trim();
                ob_base_chanpinxiansq.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_chanpinxiansq.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_chanpinxiansq = ob_base_chanpinxiansqservice.AddEntity(ob_base_chanpinxiansq);
                ViewBag.base_chanpinxiansq = ob_base_chanpinxiansq;
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
            base_chanpinxiansq tempData = ob_base_chanpinxiansqservice.GetEntityById(base_chanpinxiansq => base_chanpinxiansq.ID == id && base_chanpinxiansq.IsDelete == false);
            ViewBag.base_chanpinxiansq = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_chanpinxiansqViewModel base_chanpinxiansqviewmodel = new base_chanpinxiansqViewModel();
                base_chanpinxiansqviewmodel.ID = tempData.ID;
                base_chanpinxiansqviewmodel.JXSID = tempData.JXSID;
                base_chanpinxiansqviewmodel.CPXID = tempData.CPXID;
                base_chanpinxiansqviewmodel.JXSDM = tempData.JXSDM;
                base_chanpinxiansqviewmodel.CPXDM = tempData.CPXDM;
                base_chanpinxiansqviewmodel.TingyongSF = tempData.TingyongSF;
                base_chanpinxiansqviewmodel.Col1 = tempData.Col1;
                base_chanpinxiansqviewmodel.MakeDate = tempData.MakeDate;
                base_chanpinxiansqviewmodel.MakeMan = tempData.MakeMan;
                return View(base_chanpinxiansqviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string jxsid = Request["jxsid"] ?? "";
            string cpxid = Request["cpxid"] ?? "";
            string jxsdm = Request["jxsdm"] ?? "";
            string cpxdm = Request["cpxdm"] ?? "";
            string tingyongsf = Request["tingyongsf"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_chanpinxiansq p = ob_base_chanpinxiansqservice.GetEntityById(base_chanpinxiansq => base_chanpinxiansq.ID == uid);
                p.JXSID = jxsid == "" ? 0 : int.Parse(jxsid);
                p.CPXID = cpxid == "" ? 0 : int.Parse(cpxid);
                p.JXSDM = jxsdm.Trim();
                p.CPXDM = cpxdm.Trim();
                p.TingyongSF = tingyongsf == "" ? false : Boolean.Parse(tingyongsf);
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_chanpinxiansqservice.UpdateEntity(p);
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
            base_chanpinxiansq ob_base_chanpinxiansq;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_chanpinxiansq = ob_base_chanpinxiansqservice.GetEntityById(base_chanpinxiansq => base_chanpinxiansq.ID == id && base_chanpinxiansq.IsDelete == false);
                    ob_base_chanpinxiansq.IsDelete = true;
                    ob_base_chanpinxiansqservice.UpdateEntity(ob_base_chanpinxiansq);
                }
            }
            return RedirectToAction("Index");
        }

        public JsonResult getProductLineWithID()
        {
            var jxsid = Request["jxsid"] ?? "";

            var _cpxsq = ServiceFactory.base_chanpinxiansqservice.LoadSortEntities(p => p.JXSID == int.Parse(jxsid) && p.IsDelete == false, true, s => s.CPXDM).ToList();
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

            return Json(cpxsq);
        }
    }
}

