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
    public class ZhuCeZhengGG
    {
        public int ID { get; set; }
        public int ZCZID { get; set; }
        public string Guige { get; set; }
        public string Memo { get; set; }
        public string Makedate { get; set; }
        public string Makeman { get; set; }
    }
    public class base_zhucezhengggController : Controller
    {
        private Ibase_zhucezhengggService ob_base_zhucezhengggservice = ServiceFactory.base_zhucezhengggservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_zhucezhenggg_index";
            Expression<Func<base_zhucezhenggg, bool>> where = PredicateExtensionses.True<base_zhucezhenggg>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "zczid":
                            string zczid = scld[1];
                            string zczidequal = scld[2];
                            string zczidand = scld[3];
                            if (!string.IsNullOrEmpty(zczid))
                            {
                                if (zczidequal.Equals("="))
                                {
                                    if (zczidand.Equals("and"))
                                        where = where.And(base_zhucezhenggg => base_zhucezhenggg.ZCZID == int.Parse(zczid));
                                    else
                                        where = where.Or(base_zhucezhenggg => base_zhucezhenggg.ZCZID == int.Parse(zczid));
                                }
                                if (zczidequal.Equals(">"))
                                {
                                    if (zczidand.Equals("and"))
                                        where = where.And(base_zhucezhenggg => base_zhucezhenggg.ZCZID > int.Parse(zczid));
                                    else
                                        where = where.Or(base_zhucezhenggg => base_zhucezhenggg.ZCZID > int.Parse(zczid));
                                }
                                if (zczidequal.Equals("<"))
                                {
                                    if (zczidand.Equals("and"))
                                        where = where.And(base_zhucezhenggg => base_zhucezhenggg.ZCZID < int.Parse(zczid));
                                    else
                                        where = where.Or(base_zhucezhenggg => base_zhucezhenggg.ZCZID < int.Parse(zczid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_zhucezhenggg => base_zhucezhenggg.IsDelete == false);

            var tempData = ob_base_zhucezhengggservice.LoadSortEntities(where.Compile(), false, base_zhucezhenggg => base_zhucezhenggg.ID).ToPagedList<base_zhucezhenggg>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_zhucezhenggg = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_zhucezhenggg_index";
            string page = "1";
            string zczid = Request["zczid"] ?? "";
            string zczidequal = Request["zczidequal"] ?? "";
            string zczidand = Request["zczidand"] ?? "";
            Expression<Func<base_zhucezhenggg, bool>> where = PredicateExtensionses.True<base_zhucezhenggg>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(zczid))
                {
                    if (zczidequal.Equals("="))
                    {
                        if (zczidand.Equals("and"))
                            where = where.And(base_zhucezhenggg => base_zhucezhenggg.ZCZID == int.Parse(zczid));
                        else
                            where = where.Or(base_zhucezhenggg => base_zhucezhenggg.ZCZID == int.Parse(zczid));
                    }
                    if (zczidequal.Equals(">"))
                    {
                        if (zczidand.Equals("and"))
                            where = where.And(base_zhucezhenggg => base_zhucezhenggg.ZCZID > int.Parse(zczid));
                        else
                            where = where.Or(base_zhucezhenggg => base_zhucezhenggg.ZCZID > int.Parse(zczid));
                    }
                    if (zczidequal.Equals("<"))
                    {
                        if (zczidand.Equals("and"))
                            where = where.And(base_zhucezhenggg => base_zhucezhenggg.ZCZID < int.Parse(zczid));
                        else
                            where = where.Or(base_zhucezhenggg => base_zhucezhenggg.ZCZID < int.Parse(zczid));
                    }
                }
                if (!string.IsNullOrEmpty(zczid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "zczid", zczid, zczidequal, zczidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "zczid", "", zczidequal, zczidand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(zczid))
                {
                    if (zczidequal.Equals("="))
                    {
                        if (zczidand.Equals("and"))
                            where = where.And(base_zhucezhenggg => base_zhucezhenggg.ZCZID == int.Parse(zczid));
                        else
                            where = where.Or(base_zhucezhenggg => base_zhucezhenggg.ZCZID == int.Parse(zczid));
                    }
                    if (zczidequal.Equals(">"))
                    {
                        if (zczidand.Equals("and"))
                            where = where.And(base_zhucezhenggg => base_zhucezhenggg.ZCZID > int.Parse(zczid));
                        else
                            where = where.Or(base_zhucezhenggg => base_zhucezhenggg.ZCZID > int.Parse(zczid));
                    }
                    if (zczidequal.Equals("<"))
                    {
                        if (zczidand.Equals("and"))
                            where = where.And(base_zhucezhenggg => base_zhucezhenggg.ZCZID < int.Parse(zczid));
                        else
                            where = where.Or(base_zhucezhenggg => base_zhucezhenggg.ZCZID < int.Parse(zczid));
                    }
                }
                if (!string.IsNullOrEmpty(zczid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "zczid", zczid, zczidequal, zczidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "zczid", "", zczidequal, zczidand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_zhucezhenggg => base_zhucezhenggg.IsDelete == false);

            var tempData = ob_base_zhucezhengggservice.LoadSortEntities(where.Compile(), false, base_zhucezhenggg => base_zhucezhenggg.ID).ToPagedList<base_zhucezhenggg>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_zhucezhenggg = tempData;
            return View(tempData);
        }
        public JsonResult GetDetail()
        {
            int _userid = (int)Session["user_id"];
            string _username = (string)Session["user_name"];
            string _zczid = Request["zczbh"] ?? "";
            IList<ZhuCeZhengGG> _guiges;
            if (_zczid == "")
                return Json("");
            else
            {
                _guiges = new List<ZhuCeZhengGG>();
                var tempdata = ob_base_zhucezhengggservice.LoadSortEntities(p => p.IsDelete == false && p.ZCZID == int.Parse(_zczid), false, p => p.Guige);
                //_guiges = tempdata.ToList<base_zhucezhenggg>();
                foreach (base_zhucezhenggg _zczgg in tempdata)
                {
                    if (_zczgg.ID > 0)
                    {
                        ZhuCeZhengGG _guige = new ZhuCeZhengGG();
                        _guige.ID = _zczgg.ID;
                        _guige.ZCZID = (int)_zczgg.ZCZID;
                        _guige.Guige = _zczgg.Guige;
                        _guige.Memo = _zczgg.Memo;
                        _guige.Makedate = _zczgg.MakeDate.ToShortDateString();
                        userinfo _user = ServiceFactory.userinfoservice.GetEntityById(p => p.ID == _zczgg.MakeMan);
                        _guige.Makeman = _user.FullName;
                        _guiges.Add(_guige);
                    }
                }
            }
            return Json(_guiges);
        }
        public int AddNow()
        {
            int _userid = (int)Session["user_id"];
            string _username = (string)Session["user_name"];
            string _zczid = Request["addzcz"] ?? "";
            string _guige = Request["addguige"] ?? "";
            string _memo = Request["addmemo"] ?? "";
            try
            {
                if (_zczid == "" || _guige == "")
                    return 0;
                else
                {
                    var _gg = _guige.Split();
                    foreach (var gg in _gg)
                    {
                        base_zhucezhenggg ob_base_zhucezhenggg = ob_base_zhucezhengggservice.GetEntityById(p => p.ZCZID == int.Parse(_zczid) && p.Guige == gg.Trim() && p.IsDelete == false);
                        if (ob_base_zhucezhenggg == null && gg.Length > 0)
                        {
                            ob_base_zhucezhenggg = new base_zhucezhenggg();
                            ob_base_zhucezhenggg.ZCZID = int.Parse(_zczid);
                            ob_base_zhucezhenggg.Guige = gg.Trim();
                            ob_base_zhucezhenggg.Memo = _memo.Trim();
                            ob_base_zhucezhenggg.MakeDate = DateTime.Now;
                            ob_base_zhucezhenggg.MakeMan = _userid;
                            ob_base_zhucezhenggg = ob_base_zhucezhengggservice.AddEntity(ob_base_zhucezhenggg);
                        }
                    }
                    //base_zhucezhenggg ob_base_zhucezhenggg = ob_base_zhucezhengggservice.GetEntityById(p => p.Guige == _guige.Trim() && p.IsDelete == false);
                    //if (ob_base_zhucezhenggg == null)
                    //{
                    //    ob_base_zhucezhenggg = new base_zhucezhenggg();
                    //    ob_base_zhucezhenggg.ZCZID = int.Parse(_zczid);
                    //    ob_base_zhucezhenggg.Guige = _guige.Trim();
                    //    ob_base_zhucezhenggg.Memo = _memo.Trim();
                    //    ob_base_zhucezhenggg.MakeDate = DateTime.Now;
                    //    ob_base_zhucezhenggg.MakeMan = _userid;
                    //    ob_base_zhucezhenggg = ob_base_zhucezhengggservice.AddEntity(ob_base_zhucezhenggg);
                    //    ViewBag.base_zhucezhenggg = ob_base_zhucezhenggg;
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            return 1;
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
            string zczid = Request["zczid"] ?? "";
            string guige = Request["guige"] ?? "";
            string memo = Request["memo"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                base_zhucezhenggg ob_base_zhucezhenggg = new base_zhucezhenggg();
                ob_base_zhucezhenggg.ZCZID = zczid == "" ? 0 : int.Parse(zczid);
                ob_base_zhucezhenggg.Guige = guige.Trim();
                ob_base_zhucezhenggg.Memo = memo.Trim();
                ob_base_zhucezhenggg.Col1 = col1.Trim();
                ob_base_zhucezhenggg.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_zhucezhenggg.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_zhucezhenggg = ob_base_zhucezhengggservice.AddEntity(ob_base_zhucezhenggg);
                ViewBag.base_zhucezhenggg = ob_base_zhucezhenggg;
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
            base_zhucezhenggg tempData = ob_base_zhucezhengggservice.GetEntityById(base_zhucezhenggg => base_zhucezhenggg.ID == id && base_zhucezhenggg.IsDelete == false);
            ViewBag.base_zhucezhenggg = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_zhucezhengggViewModel base_zhucezhengggviewmodel = new base_zhucezhengggViewModel();
                base_zhucezhengggviewmodel.ID = tempData.ID;
                base_zhucezhengggviewmodel.ZCZID = tempData.ZCZID;
                base_zhucezhengggviewmodel.Guige = tempData.Guige;
                base_zhucezhengggviewmodel.Memo = tempData.Memo;
                base_zhucezhengggviewmodel.Col1 = tempData.Col1;
                base_zhucezhengggviewmodel.MakeDate = tempData.MakeDate;
                base_zhucezhengggviewmodel.MakeMan = tempData.MakeMan;
                return View(base_zhucezhengggviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string zczid = Request["zczid"] ?? "";
            string guige = Request["guige"] ?? "";
            string memo = Request["memo"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_zhucezhenggg p = ob_base_zhucezhengggservice.GetEntityById(base_zhucezhenggg => base_zhucezhenggg.ID == uid);
                p.ZCZID = zczid == "" ? 0 : int.Parse(zczid);
                p.Guige = guige.Trim();
                p.Memo = memo.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_zhucezhengggservice.UpdateEntity(p);
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
            base_zhucezhenggg ob_base_zhucezhenggg;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_zhucezhenggg = ob_base_zhucezhengggservice.GetEntityById(base_zhucezhenggg => base_zhucezhenggg.ID == id && base_zhucezhenggg.IsDelete == false);
                    ob_base_zhucezhenggg.IsDelete = true;
                    ob_base_zhucezhengggservice.UpdateEntity(ob_base_zhucezhenggg);
                }
            }
            return RedirectToAction("Index");
        }
        public int DeleteNow()
        {
            string sdel = Request["del"] ?? "";
            int id;
            base_zhucezhenggg ob_base_zhucezhenggg;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_zhucezhenggg = ob_base_zhucezhengggservice.GetEntityById(base_zhucezhenggg => base_zhucezhenggg.ID == id && base_zhucezhenggg.IsDelete == false);
                    ob_base_zhucezhenggg.IsDelete = true;
                    ob_base_zhucezhengggservice.UpdateEntity(ob_base_zhucezhenggg);
                }
            }
            return 1;
        }
    }
}

