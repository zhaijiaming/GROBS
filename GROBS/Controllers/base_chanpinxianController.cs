using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Expressions;
using X.PagedList;
using GROBS.EFModels;
using GROBS.IBSL;
using GROBS.BSL;
using GROBS.Common;
using GROBS.Models;
using System.Collections;
using System.Collections.Generic;

namespace GROBS.Controllers
{
    public class ChanPinXian
    {
        public int ID { get; set; }
        public int Huozhuxuhao { get; set; }
        public string Mingcheng { get; set; }
        public string Miaoshu { get; set; }
        public string MakeDate { get; set; }
        public string MakeMan { get; set; }

    }
    public class base_chanpinxianController : Controller
    {
        private Ibase_chanpinxianService ob_base_chanpinxianservice = ServiceFactory.base_chanpinxianservice;
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_chanpinxian_index";
            PageMenu.Set("Index", "base_chanpinxian", "基础数据");
            Expression<Func<base_chanpinxian, bool>> where = PredicateExtensionses.True<base_chanpinxian>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "huozhuxuhao":
                            string huozhuxuhao = scld[1];
                            string huozhuxuhaoequal = scld[2];
                            string huozhuxuhaoand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuxuhao))
                            {
                                if (huozhuxuhaoequal.Equals("="))
                                {
                                    if (huozhuxuhaoand.Equals("and"))
                                        where = where.And(base_chanpinxian => base_chanpinxian.Huozhuxuhao == int.Parse(huozhuxuhao));
                                    else
                                        where = where.Or(base_chanpinxian => base_chanpinxian.Huozhuxuhao == int.Parse(huozhuxuhao));
                                }
                            }
                            break;
                        case "mingcheng":
                            string mingcheng = scld[1];
                            string mingchengequal = scld[2];
                            string mingchengand = scld[3];
                            if (!string.IsNullOrEmpty(mingcheng))
                            {
                                if (mingchengequal.Equals("="))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_chanpinxian => base_chanpinxian.Mingcheng == mingcheng);
                                    else
                                        where = where.Or(base_chanpinxian => base_chanpinxian.Mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_chanpinxian => base_chanpinxian.Mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(base_chanpinxian => base_chanpinxian.Mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_chanpinxian => base_chanpinxian.IsDelete == false);

            var tempData = ob_base_chanpinxianservice.LoadSortEntities(where.Compile(), false, base_chanpinxian => base_chanpinxian.ID).ToPagedList<base_chanpinxian>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_chanpinxian = tempData;
            return View(tempData);
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_chanpinxian_index";
            string page = "1";
            //huozhuxuhao
            string huozhuxuhao = Request["huozhuxuhao"] ?? "";
            string huozhuxuhaoequal = Request["huozhuxuhaoequal"] ?? "";
            string huozhuxuhaoand = Request["huozhuxuhaoand"] ?? "";
            //mingcheng
            string mingcheng = Request["mingcheng"] ?? "";
            string mingchengequal = Request["mingchengequal"] ?? "";
            string mingchengand = Request["mingchengand"] ?? "";
            PageMenu.Set("Index", "base_chanpinxian", "基础数据");
            Expression<Func<base_chanpinxian, bool>> where = PredicateExtensionses.True<base_chanpinxian>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //huozhuxuhao
                if (!string.IsNullOrEmpty(huozhuxuhao))
                {
                    if (huozhuxuhaoequal.Equals("="))
                    {
                        if (huozhuxuhaoand.Equals("and"))
                            where = where.And(base_chanpinxian => base_chanpinxian.Huozhuxuhao == int.Parse(huozhuxuhao));
                        else
                            where = where.Or(base_chanpinxian => base_chanpinxian.Huozhuxuhao == int.Parse(huozhuxuhao));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuxuhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuxuhao", huozhuxuhao, huozhuxuhaoequal, huozhuxuhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuxuhao", "", huozhuxuhaoequal, huozhuxuhaoand);
                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_chanpinxian => base_chanpinxian.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_chanpinxian => base_chanpinxian.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_chanpinxian => base_chanpinxian.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_chanpinxian => base_chanpinxian.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuxuhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuxuhao", huozhuxuhao, huozhuxuhaoequal, huozhuxuhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuxuhao", "", huozhuxuhaoequal, huozhuxuhaoand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(huozhuxuhao))
                {
                    if (huozhuxuhaoequal.Equals("="))
                    {
                        if (huozhuxuhaoand.Equals("and"))
                            where = where.And(base_chanpinxian => base_chanpinxian.Huozhuxuhao == int.Parse(huozhuxuhao));
                        else
                            where = where.Or(base_chanpinxian => base_chanpinxian.Huozhuxuhao == int.Parse(huozhuxuhao));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuxuhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuxuhao", huozhuxuhao, huozhuxuhaoequal, huozhuxuhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuxuhao", "", huozhuxuhaoequal, huozhuxuhaoand);
                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_chanpinxian => base_chanpinxian.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_chanpinxian => base_chanpinxian.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_chanpinxian => base_chanpinxian.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_chanpinxian => base_chanpinxian.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_chanpinxian => base_chanpinxian.IsDelete == false);

            var tempData = ob_base_chanpinxianservice.LoadSortEntities(where.Compile(), false, base_chanpinxian => base_chanpinxian.ID).ToPagedList<base_chanpinxian>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_chanpinxian = tempData;
            return View(tempData);

        }

        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            return View();
        }

        public ActionResult Save()
        {
            string id = Request["ob_base_chanpinxian_id"] ?? "";
            string gysid = Request["gysid"] ?? "";
            string huozhuxuhao = Request["ob_base_chanpinxian_huozhuxuhao"] ?? "";
            string mingcheng = Request["ob_base_chanpinxian_mingcheng"] ?? "";
            string miaoshu = Request["ob_base_chanpinxian_miaoshu"] ?? "";
            string col1 = Request["ob_base_chanpinxian_col1"] ?? "";
            string col2 = Request["ob_base_chanpinxian_col2"] ?? "";
            string col3 = Request["ob_base_chanpinxian_col3"] ?? "";
            string makedate = Request["ob_base_chanpinxian_makedate"] ?? "";
            string makeman = Request["ob_base_chanpinxian_makeman"] ?? "";
            try
            {
                base_chanpinxian ob_base_chanpinxian = new base_chanpinxian();
                ob_base_chanpinxian.Huozhuxuhao = huozhuxuhao == "" ? 0 : int.Parse(huozhuxuhao);
                ob_base_chanpinxian.GYSID = gysid == "" ? 0 : int.Parse(gysid);
                ob_base_chanpinxian.Mingcheng = mingcheng.Trim();
                ob_base_chanpinxian.Miaoshu = miaoshu.Trim();
                ob_base_chanpinxian.Col1 = col1.Trim();
                ob_base_chanpinxian.Col2 = col2.Trim();
                ob_base_chanpinxian.Col3 = col3.Trim();
                ob_base_chanpinxian.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_chanpinxian.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_chanpinxian = ob_base_chanpinxianservice.AddEntity(ob_base_chanpinxian);
                ViewBag.base_chanpinxian = ob_base_chanpinxian;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            base_chanpinxian tempData = ob_base_chanpinxianservice.GetEntityById(base_chanpinxian => base_chanpinxian.ID == id && base_chanpinxian.IsDelete == false);
            ViewBag.base_chanpinxian = tempData;
            return View();
        }
        public JsonResult GetDetail()
        {
            int _userid = (int)Session["user_id"];
            string _username = (string)Session["user_name"];
            string _cpxid = Request["cpxid"] ?? "";
            IList<ChanPinXian> _cpxs;
            if (_cpxid == "")
                return Json("");
            else
            {
                _cpxs = new List<ChanPinXian>();
                var tempdata = ob_base_chanpinxianservice.LoadSortEntities(p => p.IsDelete == false && p.Huozhuxuhao == int.Parse(_cpxid), false, p => p.Mingcheng);
                foreach (base_chanpinxian _cpx in tempdata)
                {
                    if (_cpx.ID > 0)
                    {
                        ChanPinXian _cpxxx = new ChanPinXian();
                        _cpxxx.ID = _cpx.ID;
                        _cpxxx.Mingcheng = _cpx.Mingcheng;
                        _cpxs.Add(_cpxxx);
                    }
                }
            }
            return Json(_cpxs);
        }

        public ActionResult Update()
        {
            string id = Request["ob_base_chanpinxian_id"] ?? "";
            string huozhuxuhao = Request["ob_base_chanpinxian_huozhuxuhao"] ?? "";
            string gysid = Request["gysid"] ?? "";
            string mingcheng = Request["ob_base_chanpinxian_mingcheng"] ?? "";
            string miaoshu = Request["ob_base_chanpinxian_miaoshu"] ?? "";
            string col1 = Request["ob_base_chanpinxian_col1"] ?? "";
            string col2 = Request["ob_base_chanpinxian_col2"] ?? "";
            string col3 = Request["ob_base_chanpinxian_col3"] ?? "";
            string makedate = Request["ob_base_chanpinxian_makedate"] ?? "";
            string makeman = Request["ob_base_chanpinxian_makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_chanpinxian p = ob_base_chanpinxianservice.GetEntityById(base_chanpinxian => base_chanpinxian.ID == uid);
                p.GYSID = gysid == "" ? 0 : int.Parse(gysid);
                p.Huozhuxuhao = huozhuxuhao == "" ? 0 : int.Parse(huozhuxuhao);
                p.Mingcheng = mingcheng.Trim();
                p.Miaoshu = miaoshu.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_chanpinxianservice.UpdateEntity(p);
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
            base_chanpinxian ob_base_chanpinxian;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_chanpinxian = ob_base_chanpinxianservice.GetEntityById(base_chanpinxian => base_chanpinxian.ID == id && base_chanpinxian.IsDelete == false);
                    ob_base_chanpinxian.IsDelete = true;
                    ob_base_chanpinxianservice.UpdateEntity(ob_base_chanpinxian);
                }
            }
            return RedirectToAction("Index");
        }
        public JsonResult GetCpxFromGysid()
        {
            string _cpxid = Request["cpxid"] ?? "";
            IList<ChanPinXian> _cpxs;
            if (_cpxid == "")
                return Json("");
            else
            {
                _cpxs = new List<ChanPinXian>();
                var tempdata = ob_base_chanpinxianservice.LoadSortEntities(p => p.IsDelete == false && p.GYSID == int.Parse(_cpxid), false, p => p.Mingcheng);
                foreach (base_chanpinxian _cpx in tempdata)
                {
                    if (_cpx.ID > 0)
                    {
                        ChanPinXian _cpxxx = new ChanPinXian();
                        _cpxxx.ID = _cpx.ID;
                        _cpxxx.Mingcheng = _cpx.Mingcheng;
                        _cpxs.Add(_cpxxx);
                    }
                }
            }
            return Json(_cpxs);
        }
    }
}

