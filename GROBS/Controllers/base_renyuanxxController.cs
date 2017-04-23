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
    public class RenYuan
    {
        public int ID { get; set; }
        public string Mingcheng { get; set; }
        public int? Xingbie { get; set; }
        public string Bumen { get; set; }
        public string Zhiwei { get; set; }
        public string Zhize { get; set; }
        public string Beizhu { get; set; }
    }
    public class base_renyuanxxController : Controller
    {
        private Ibase_renyuanxxService ob_base_renyuanxxservice = ServiceFactory.base_renyuanxxservice;
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_renyuanxx_index";
            PageMenu.Set("Index", "base_renyuanxx", "基础数据");
            Expression<Func<base_renyuanxx, bool>> where = PredicateExtensionses.True<base_renyuanxx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "mingcheng":
                            string mingcheng = scld[1];
                            string mingchengequal = scld[2];
                            string mingchengand = scld[3];
                            if (!string.IsNullOrEmpty(mingcheng))
                            {
                                if (mingchengequal.Equals("="))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_renyuanxx => base_renyuanxx.Mingcheng == mingcheng);
                                    else
                                        where = where.Or(base_renyuanxx => base_renyuanxx.Mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_renyuanxx => base_renyuanxx.Mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(base_renyuanxx => base_renyuanxx.Mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        case "bumen":
                            string bumen = scld[1];
                            string bumenequal = scld[2];
                            string bumenand = scld[3];
                            if (!string.IsNullOrEmpty(bumen))
                            {
                                if (bumenequal.Equals("="))
                                {
                                    if (bumenand.Equals("and"))
                                        where = where.And(base_renyuanxx => base_renyuanxx.Bumen == bumen);
                                    else
                                        where = where.Or(base_renyuanxx => base_renyuanxx.Bumen == bumen);
                                }
                                if (bumenequal.Equals("like"))
                                {
                                    if (bumenand.Equals("and"))
                                        where = where.And(base_renyuanxx => base_renyuanxx.Bumen.Contains(bumen));
                                    else
                                        where = where.Or(base_renyuanxx => base_renyuanxx.Bumen.Contains(bumen));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_renyuanxx => base_renyuanxx.IsDelete == false);

            var tempData = ob_base_renyuanxxservice.LoadSortEntities(where.Compile(), false, base_renyuanxx => base_renyuanxx.ID).ToPagedList<base_renyuanxx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_renyuanxx = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_renyuanxx_index";
            string page = "1";
            //mingcheng
            string mingcheng = Request["mingcheng"] ?? "";
            string mingchengequal = Request["mingchengequal"] ?? "";
            string mingchengand = Request["mingchengand"] ?? "";
            //bumen
            string bumen = Request["bumen"] ?? "";
            string bumenequal = Request["bumenequal"] ?? "";
            string bumenand = Request["bumenand"] ?? "";
            PageMenu.Set("Index", "base_renyuanxx", "基础数据");
            Expression<Func<base_renyuanxx, bool>> where = PredicateExtensionses.True<base_renyuanxx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_renyuanxx => base_renyuanxx.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_renyuanxx => base_renyuanxx.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_renyuanxx => base_renyuanxx.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_renyuanxx => base_renyuanxx.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);
                //bumen
                if (!string.IsNullOrEmpty(bumen))
                {
                    if (bumenequal.Equals("="))
                    {
                        if (bumenand.Equals("and"))
                            where = where.And(base_renyuanxx => base_renyuanxx.Bumen == bumen);
                        else
                            where = where.Or(base_renyuanxx => base_renyuanxx.Bumen == bumen);
                    }
                    if (bumenequal.Equals("like"))
                    {
                        if (bumenand.Equals("and"))
                            where = where.And(base_renyuanxx => base_renyuanxx.Bumen.Contains(bumen));
                        else
                            where = where.Or(base_renyuanxx => base_renyuanxx.Bumen.Contains(bumen));
                    }
                }
                if (!string.IsNullOrEmpty(bumen))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bumen", bumen, bumenequal, bumenand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bumen", "", bumenequal, bumenand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_renyuanxx => base_renyuanxx.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_renyuanxx => base_renyuanxx.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_renyuanxx => base_renyuanxx.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_renyuanxx => base_renyuanxx.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);
                //bumen
                if (!string.IsNullOrEmpty(bumen))
                {
                    if (bumenequal.Equals("="))
                    {
                        if (bumenand.Equals("and"))
                            where = where.And(base_renyuanxx => base_renyuanxx.Bumen == bumen);
                        else
                            where = where.Or(base_renyuanxx => base_renyuanxx.Bumen == bumen);
                    }
                    if (bumenequal.Equals("like"))
                    {
                        if (bumenand.Equals("and"))
                            where = where.And(base_renyuanxx => base_renyuanxx.Bumen.Contains(bumen));
                        else
                            where = where.Or(base_renyuanxx => base_renyuanxx.Bumen.Contains(bumen));
                    }
                }
                if (!string.IsNullOrEmpty(bumen))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bumen", bumen, bumenequal, bumenand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bumen", "", bumenequal, bumenand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_renyuanxx => base_renyuanxx.IsDelete == false);

            var tempData = ob_base_renyuanxxservice.LoadSortEntities(where.Compile(), false, base_renyuanxx => base_renyuanxx.ID).ToPagedList<base_renyuanxx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_renyuanxx = tempData;
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
            string userid = Request["userid"] ?? "";
            string bianhao = Request["bianhao"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string xingbie = Request["xingbie"] ?? "";
            string dianhua = Request["dianhua"] ?? "";
            string shengri = Request["shengri"] ?? "";
            string bumen = Request["bumen"] ?? "";
            string zhiwei = Request["zhiwei"] ?? "";
            string zhize = Request["zhize"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                base_renyuanxx ob_base_renyuanxx = new base_renyuanxx();
                ob_base_renyuanxx.Bianhao = bianhao.Trim();
                ob_base_renyuanxx.UserID = userid == "" ? 0 : int.Parse(userid);
                ob_base_renyuanxx.Mingcheng = mingcheng.Trim();
                ob_base_renyuanxx.Xingbie = xingbie == "" ? 0 : int.Parse(xingbie);
                ob_base_renyuanxx.Dianhua = dianhua.Trim();
                ob_base_renyuanxx.Shengri = shengri == "" ? DateTime.Now : DateTime.Parse(shengri);
                ob_base_renyuanxx.Bumen = bumen.Trim();
                ob_base_renyuanxx.Zhiwei = zhiwei.Trim();
                ob_base_renyuanxx.Zhize = zhize.Trim();
                ob_base_renyuanxx.Beizhu = beizhu.Trim();
                ob_base_renyuanxx.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_renyuanxx.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_renyuanxx = ob_base_renyuanxxservice.AddEntity(ob_base_renyuanxx);
                ViewBag.base_renyuanxx = ob_base_renyuanxx;
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
            base_renyuanxx tempData = ob_base_renyuanxxservice.GetEntityById(base_renyuanxx => base_renyuanxx.ID == id && base_renyuanxx.IsDelete == false);
            ViewBag.base_renyuanxx = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_renyuanxxViewModel base_renyuanxxviewmodel = new base_renyuanxxViewModel();
                base_renyuanxxviewmodel.ID = tempData.ID;
                base_renyuanxxviewmodel.UserID = tempData.UserID;
                base_renyuanxxviewmodel.Bianhao = tempData.Bianhao;
                base_renyuanxxviewmodel.Mingcheng = tempData.Mingcheng;
                base_renyuanxxviewmodel.Xingbie = tempData.Xingbie;
                base_renyuanxxviewmodel.Dianhua = tempData.Dianhua;
                base_renyuanxxviewmodel.Shengri = tempData.Shengri;
                base_renyuanxxviewmodel.Bumen = tempData.Bumen;
                base_renyuanxxviewmodel.Zhiwei = tempData.Zhiwei;
                base_renyuanxxviewmodel.Zhize = tempData.Zhize;
                base_renyuanxxviewmodel.Beizhu = tempData.Beizhu;
                base_renyuanxxviewmodel.MakeDate = tempData.MakeDate;
                base_renyuanxxviewmodel.MakeMan = tempData.MakeMan;
                return View(base_renyuanxxviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string userid = Request["userid"] ?? "";
            string bianhao = Request["bianhao"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string xingbie = Request["xingbie"] ?? "";
            string dianhua = Request["dianhua"] ?? "";
            string shengri = Request["shengri"] ?? "";
            string bumen = Request["bumen"] ?? "";
            string zhiwei = Request["zhiwei"] ?? "";
            string zhize = Request["zhize"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_renyuanxx p = ob_base_renyuanxxservice.GetEntityById(base_renyuanxx => base_renyuanxx.ID == uid);
                p.Bianhao = bianhao.Trim();
                p.UserID = userid == "" ? 0 : int.Parse(userid);
                p.Mingcheng = mingcheng.Trim();
                p.Xingbie = xingbie == "" ? 0 : int.Parse(xingbie);
                p.Dianhua = dianhua.Trim();
                p.Shengri = shengri == "" ? DateTime.Now : DateTime.Parse(shengri);
                p.Bumen = bumen.Trim();
                p.Zhiwei = zhiwei.Trim();
                p.Zhize = zhize.Trim();
                p.Beizhu = beizhu.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_renyuanxxservice.UpdateEntity(p);
                ViewBag.saveok = ViewAddTag.ModifyOk;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.saveok = ViewAddTag.ModifyNo;
            }
            return RedirectToAction("Index", new { id = uid });
        }
        public ActionResult Delete()
        {
            string sdel = Request["del"] ?? "";
            int id;
            base_renyuanxx ob_base_renyuanxx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_renyuanxx = ob_base_renyuanxxservice.GetEntityById(base_renyuanxx => base_renyuanxx.ID == id && base_renyuanxx.IsDelete == false);
                    ob_base_renyuanxx.IsDelete = true;
                    ob_base_renyuanxxservice.UpdateEntity(ob_base_renyuanxx);
                }
            }
            return RedirectToAction("Index");
        }
        public JsonResult GetRenyuan()
        {
            var _rytemp = ob_base_renyuanxxservice.LoadSortEntities(p => p.IsDelete == false, true, s => s.Bumen);
            List<RenYuan> _rylist = new List<RenYuan>();
            RenYuan _ry;
            foreach (var ry in _rytemp)
            {
                _ry = new RenYuan();
                _ry.ID = ry.ID;
                _ry.Mingcheng = ry.Mingcheng;
                _ry.Xingbie = ry.Xingbie;
                _ry.Bumen = ry.Bumen;
                _ry.Zhiwei = ry.Zhiwei;
                _ry.Zhize = ry.Zhize;
                _ry.Beizhu = ry.Beizhu;
                _rylist.Add(_ry);
            }
            return Json(_rylist);
        }
    }
}

