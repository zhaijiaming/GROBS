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
    public class base_shouhuomingxiController : Controller
    {
        private Ibase_shouhuomingxiService ob_base_shouhuomingxiservice = ServiceFactory.base_shouhuomingxiservice;
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_shouhuomingxi_index";
            PageMenu.Set("Index", "base_shouhuomingxi", "基础数据");
            Expression<Func<base_shouhuomingxi, bool>> where = PredicateExtensionses.True<base_shouhuomingxi>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "shouhuofangid":
                            string shouhuofangid = scld[1];
                            string shouhuofangidequal = scld[2];
                            string shouhuofangidand = scld[3];
                            if (!string.IsNullOrEmpty(shouhuofangid))
                            {
                                if (shouhuofangidequal.Equals("="))
                                {
                                    if (shouhuofangidand.Equals("and"))
                                        where = where.And(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID == int.Parse(shouhuofangid));
                                    else
                                        where = where.Or(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID == int.Parse(shouhuofangid));
                                }
                                if (shouhuofangidequal.Equals(">"))
                                {
                                    if (shouhuofangidand.Equals("and"))
                                        where = where.And(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID > int.Parse(shouhuofangid));
                                    else
                                        where = where.Or(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID > int.Parse(shouhuofangid));
                                }
                                if (shouhuofangidequal.Equals("<"))
                                {
                                    if (shouhuofangidand.Equals("and"))
                                        where = where.And(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID < int.Parse(shouhuofangid));
                                    else
                                        where = where.Or(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID < int.Parse(shouhuofangid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_shouhuomingxi => base_shouhuomingxi.IsDelete == false);

            var tempData = ob_base_shouhuomingxiservice.LoadSortEntities(where.Compile(), false, base_shouhuomingxi => base_shouhuomingxi.ID).ToPagedList<base_shouhuomingxi>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shouhuomingxi = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_shouhuomingxi_index";
            string page = "1";
            string shouhuofangid = Request["shouhuofangid"] ?? "";
            string shouhuofangidequal = Request["shouhuofangidequal"] ?? "";
            string shouhuofangidand = Request["shouhuofangidand"] ?? "";
            PageMenu.Set("Index", "base_shouhuomingxi", "基础数据");
            Expression<Func<base_shouhuomingxi, bool>> where = PredicateExtensionses.True<base_shouhuomingxi>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(shouhuofangid))
                {
                    if (shouhuofangidequal.Equals("="))
                    {
                        if (shouhuofangidand.Equals("and"))
                            where = where.And(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID == int.Parse(shouhuofangid));
                        else
                            where = where.Or(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID == int.Parse(shouhuofangid));
                    }
                    if (shouhuofangidequal.Equals(">"))
                    {
                        if (shouhuofangidand.Equals("and"))
                            where = where.And(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID > int.Parse(shouhuofangid));
                        else
                            where = where.Or(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID > int.Parse(shouhuofangid));
                    }
                    if (shouhuofangidequal.Equals("<"))
                    {
                        if (shouhuofangidand.Equals("and"))
                            where = where.And(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID < int.Parse(shouhuofangid));
                        else
                            where = where.Or(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID < int.Parse(shouhuofangid));
                    }
                }
                if (!string.IsNullOrEmpty(shouhuofangid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouhuofangid", shouhuofangid, shouhuofangidequal, shouhuofangidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouhuofangid", "", shouhuofangidequal, shouhuofangidand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(shouhuofangid))
                {
                    if (shouhuofangidequal.Equals("="))
                    {
                        if (shouhuofangidand.Equals("and"))
                            where = where.And(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID == int.Parse(shouhuofangid));
                        else
                            where = where.Or(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID == int.Parse(shouhuofangid));
                    }
                    if (shouhuofangidequal.Equals(">"))
                    {
                        if (shouhuofangidand.Equals("and"))
                            where = where.And(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID > int.Parse(shouhuofangid));
                        else
                            where = where.Or(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID > int.Parse(shouhuofangid));
                    }
                    if (shouhuofangidequal.Equals("<"))
                    {
                        if (shouhuofangidand.Equals("and"))
                            where = where.And(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID < int.Parse(shouhuofangid));
                        else
                            where = where.Or(base_shouhuomingxi => base_shouhuomingxi.ShouhuofangID < int.Parse(shouhuofangid));
                    }
                }
                if (!string.IsNullOrEmpty(shouhuofangid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouhuofangid", shouhuofangid, shouhuofangidequal, shouhuofangidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouhuofangid", "", shouhuofangidequal, shouhuofangidand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_shouhuomingxi => base_shouhuomingxi.IsDelete == false);

            var tempData = ob_base_shouhuomingxiservice.LoadSortEntities(where.Compile(), false, base_shouhuomingxi => base_shouhuomingxi.ID).ToPagedList<base_shouhuomingxi>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shouhuomingxi = tempData;
            return View(tempData);
        }
        public ActionResult GetDetail()
        {
            int shouhuodanwei = int.Parse(Request["shouhuodanwei"]);
            var tempData = ob_base_shouhuomingxiservice.LoadSortEntities(p => p.ShouhuofangID == shouhuodanwei && p.IsDelete == false, false, base_shouhuomingxi => base_shouhuomingxi.ID);
            ViewBag.base_shouhuomingxi = tempData;
            return View();
        }

        public JsonResult GetMingxiDetail()
        {
            int _userid = (int)Session["user_id"];
            string _username = (string)Session["user_name"];
            string shdw_id = Request["shdw_id"] ?? "";


            if (shdw_id == "")
                return Json("");
            else
            {
                var tempdata = ob_base_shouhuomingxiservice.LoadSortEntities(p => p.IsDelete == false && p.ShouhuofangID == int.Parse(shdw_id), false, s => s.Mingcheng);
                List<base_shouhuomingxi> _mxlist = new List<base_shouhuomingxi>();
                base_shouhuomingxi _mx;
                foreach (var shmx in tempdata)
                {
                    _mx = new base_shouhuomingxi();
                    _mx = shmx;
                    _mxlist.Add(_mx);

                }
                return Json(_mxlist);
            }
        }
        public ActionResult Add()
        {
            string id = Request["getId"] ?? "";
            var _id = long.Parse(id);
            ViewBag.id = _id;

            string huozhuid = Request["huozhuid"] ?? "";
            var _huozhuid = long.Parse(huozhuid);
            ViewBag.huozhuid = _huozhuid;

            ViewBag.userid = (int)Session["user_id"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string shouhuofangid = Request["shouhuofangid"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string dizhi = Request["dizhi"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidianhua = Request["lianxidianhua"] ?? "";
            string xiaoshouid = Request["xiaoshouid"] ?? "";
            string xiaoshouren = Request["xiaoshouren"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int _id = int.Parse(shouhuofangid);
            try
            {
                base_shouhuomingxi ob_base_shouhuomingxi = new base_shouhuomingxi();
                ob_base_shouhuomingxi.ShouhuofangID = shouhuofangid == "" ? 0 : int.Parse(shouhuofangid);
                ob_base_shouhuomingxi.Mingcheng = mingcheng.Trim();
                ob_base_shouhuomingxi.Dizhi = dizhi.Trim();
                ob_base_shouhuomingxi.Lianxiren = lianxiren.Trim();
                ob_base_shouhuomingxi.Lianxidianhua = lianxidianhua.Trim();
                ob_base_shouhuomingxi.XiaoshouID = xiaoshouid == "" ? 0 : int.Parse(xiaoshouid);
                ob_base_shouhuomingxi.Xiaoshouren = xiaoshouren.Trim();
                ob_base_shouhuomingxi.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_shouhuomingxi.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_shouhuomingxi = ob_base_shouhuomingxiservice.AddEntity(ob_base_shouhuomingxi);
                ViewBag.base_shouhuomingxi = ob_base_shouhuomingxi;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Edit", "base_shouhuodanwei", new { id = _id });
        }

        public ActionResult ShdwIndex(int? id)
        {
            if (id == null)
            {
                var tempData = ob_base_shouhuomingxiservice.LoadSortEntities(p => p.IsDelete == false, false, s => s.Dizhi);
                ViewBag.base_shouhuomingxi = tempData;
                return View();
            }
            else
            {
                var tempData = ob_base_shouhuomingxiservice.LoadSortEntities(p => p.ShouhuofangID == id && p.IsDelete == false, false, s => s.Dizhi);
                ViewBag.base_shouhuomingxi = tempData;
                return View();
            }

        }

        [OutputCache(Duration = 10)]
        public ActionResult Edit(int id)
        {
            string biaoshi = Request["biaoshi"] ?? "";
            if (!string.IsNullOrEmpty(biaoshi))
            {
                int _biaoshi = int.Parse(biaoshi);
                ViewBag.biaoshi = _biaoshi;
            }

            base_shouhuomingxi tempData = ob_base_shouhuomingxiservice.GetEntityById(base_shouhuomingxi => base_shouhuomingxi.ID == id && base_shouhuomingxi.IsDelete == false);
            ViewBag.base_shouhuomingxi = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_shouhuomingxiViewModel base_shouhuomingxiviewmodel = new base_shouhuomingxiViewModel();
                base_shouhuomingxiviewmodel.ID = tempData.ID;
                base_shouhuomingxiviewmodel.ShouhuofangID = tempData.ShouhuofangID;
                base_shouhuomingxiviewmodel.Mingcheng = tempData.Mingcheng;
                base_shouhuomingxiviewmodel.Dizhi = tempData.Dizhi;
                base_shouhuomingxiviewmodel.Lianxiren = tempData.Lianxiren;
                base_shouhuomingxiviewmodel.Lianxidianhua = tempData.Lianxidianhua;
                base_shouhuomingxiviewmodel.XiaoshouID = tempData.XiaoshouID;
                base_shouhuomingxiviewmodel.Xiaoshouren = tempData.Xiaoshouren;
                base_shouhuomingxiviewmodel.MakeDate = tempData.MakeDate;
                base_shouhuomingxiviewmodel.MakeMan = tempData.MakeMan;
                return View(base_shouhuomingxiviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string shouhuofangid = Request["shouhuofangid"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string dizhi = Request["dizhi"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidianhua = Request["lianxidianhua"] ?? "";
            string xiaoshouid = Request["xiaoshouid"] ?? "";
            string xiaoshouren = Request["xiaoshouren"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            int swdwid = int.Parse(shouhuofangid);
            try
            {
                base_shouhuomingxi p = ob_base_shouhuomingxiservice.GetEntityById(base_shouhuomingxi => base_shouhuomingxi.ID == uid);
                p.ShouhuofangID = shouhuofangid == "" ? 0 : int.Parse(shouhuofangid);
                p.Mingcheng = mingcheng.Trim();
                p.Dizhi = dizhi.Trim();
                p.Lianxiren = lianxiren.Trim();
                p.Lianxidianhua = lianxidianhua.Trim();
                p.XiaoshouID = xiaoshouid == "" ? 0 : int.Parse(xiaoshouid);
                p.Xiaoshouren = xiaoshouren.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_shouhuomingxiservice.UpdateEntity(p);
                ViewBag.saveok = ViewAddTag.ModifyOk;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.saveok = ViewAddTag.ModifyNo;
            }
            return RedirectToAction("Edit", "base_shouhuodanwei", new { id = swdwid });
        }
        public int DeleteNow()
        {
            string sdel = Request["del"] ?? "";
            int id;
            base_shouhuomingxi ob_base_shouhuomingxi;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_shouhuomingxi = ob_base_shouhuomingxiservice.GetEntityById(base_shouhuomingxi => base_shouhuomingxi.ID == id && base_shouhuomingxi.IsDelete == false);
                    ob_base_shouhuomingxi.IsDelete = true;
                    ob_base_shouhuomingxiservice.UpdateEntity(ob_base_shouhuomingxi);
                }
            }
            return 1;
        }

        public ActionResult Delete()
        {
            string sdel = Request["del"] ?? "";
            string _swdwid = Request["shdwid"] ?? "";
            int swdwid = int.Parse(_swdwid);
            int id;
            base_shouhuomingxi ob_base_shouhuomingxi;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_shouhuomingxi = ob_base_shouhuomingxiservice.GetEntityById(base_shouhuomingxi => base_shouhuomingxi.ID == id && base_shouhuomingxi.IsDelete == false);
                    ob_base_shouhuomingxi.IsDelete = true;
                    ob_base_shouhuomingxiservice.UpdateEntity(ob_base_shouhuomingxi);
                }
            }
            return RedirectToAction("Edit", "base_shouhuodanwei", new { id = swdwid });
        }
    }
}

