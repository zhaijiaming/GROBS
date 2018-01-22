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
    public class base_taobaoController : Controller
    {
        private Ibase_taobaoService ob_base_taobaoservice = ServiceFactory.base_taobaoservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_taobao_index";
            Expression<Func<base_taobao, bool>> where = PredicateExtensionses.True<base_taobao>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "daima":
                            string daima = scld[1];
                            string daimaequal = scld[2];
                            string gysidand = scld[3];
                            if (!string.IsNullOrEmpty(daima))
                            {
                                if (daimaequal.Equals("="))
                                {
                                    if (gysidand.Equals("and"))
                                        where = where.And(base_taobao => base_taobao.Daima == daima);
                                    else
                                        where = where.Or(base_taobao => base_taobao.Daima == daima);
                                }                   
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_taobao => base_taobao.IsDelete == false);

            var tempData = ob_base_taobaoservice.LoadSortEntities(where.Compile(), false, base_taobao => base_taobao.ID).ToPagedList<base_taobao>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_taobao = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_taobao_index";
            string page = "1";
            //daima
            string daima = Request["daima"] ?? "";
            string daimaequal = Request["daimaequal"] ?? "";
            string daimaand = Request["daimaand"] ?? "";
            Expression<Func<base_taobao, bool>> where = PredicateExtensionses.True<base_taobao>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_taobao => base_taobao.Daima == daima);
                        else
                            where = where.Or(base_taobao => base_taobao.Daima == daima);
                    }       
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(daima))
                {
                    if (daimaequal.Equals("="))
                    {
                        if (daimaand.Equals("and"))
                            where = where.And(base_taobao => base_taobao.Daima == daima);
                        else
                            where = where.Or(base_taobao => base_taobao.Daima == daima);
                    }
                   
                }
                if (!string.IsNullOrEmpty(daima))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", daima, daimaequal, daimaand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "daima", "", daimaequal, daimaand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_taobao => base_taobao.IsDelete == false);

            var tempData = ob_base_taobaoservice.LoadSortEntities(where.Compile(), false, base_taobao => base_taobao.ID).ToPagedList<base_taobao>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_taobao = tempData;
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
            string gysid = Request["gysid"] ?? "";
            string cpxid = Request["cpxid"] ?? "";
            string daima = Request["daima"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string miaoshu = Request["miaoshu"] ?? "";
            string jiaxs = Request["jiaxs"] ?? "";
            string jiacg = Request["jiacg"] ?? "";
            string xsdw = Request["xsdw"] ?? "";
            string tingyongsf = Request["tingyongsf"] ?? "";
            string kongzhisf = Request["kongzhisf"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                base_taobao ob_base_taobao = new base_taobao();
                ob_base_taobao.GYSID = gysid == "" ? 0 : int.Parse(gysid);
                ob_base_taobao.CPXID = cpxid == "" ? 0 : int.Parse(cpxid);
                ob_base_taobao.Daima = daima.Trim();
                ob_base_taobao.Mingcheng = mingcheng.Trim();
                ob_base_taobao.Miaoshu = miaoshu.Trim();
                ob_base_taobao.JiaXS = jiaxs == "" ? 0 : decimal.Parse(jiaxs);
                ob_base_taobao.JiaCG = jiacg == "" ? 0 : decimal.Parse(jiacg);
                ob_base_taobao.XSDW = xsdw.Trim();
                ob_base_taobao.TingyongSF = tingyongsf == "" ? false : Boolean.Parse(tingyongsf);
                ob_base_taobao.KongzhiSF = kongzhisf == "" ? false : Boolean.Parse(kongzhisf);
                ob_base_taobao.Col1 = col1.Trim();
                ob_base_taobao.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_taobao.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_taobao = ob_base_taobaoservice.AddEntity(ob_base_taobao);
                id = ob_base_taobao.ID.ToString();
                ViewBag.base_taobao = ob_base_taobao;
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
            base_taobao tempData = ob_base_taobaoservice.GetEntityById(base_taobao => base_taobao.ID == id && base_taobao.IsDelete == false);
            ViewBag.base_taobao = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_taobaoViewModel base_taobaoviewmodel = new base_taobaoViewModel();
                base_taobaoviewmodel.ID = tempData.ID;
                base_taobaoviewmodel.GYSID = tempData.GYSID;
                base_taobaoviewmodel.CPXID = tempData.CPXID;
                base_taobaoviewmodel.Daima = tempData.Daima;
                base_taobaoviewmodel.Mingcheng = tempData.Mingcheng;
                base_taobaoviewmodel.Miaoshu = tempData.Miaoshu;
                base_taobaoviewmodel.JiaXS = tempData.JiaXS;
                base_taobaoviewmodel.JiaCG = tempData.JiaCG;
                base_taobaoviewmodel.XSDW = tempData.XSDW;
                base_taobaoviewmodel.TingyongSF = tempData.TingyongSF;
                base_taobaoviewmodel.KongzhiSF = tempData.KongzhiSF;
                base_taobaoviewmodel.Col1 = tempData.Col1;
                base_taobaoviewmodel.MakeDate = tempData.MakeDate;
                base_taobaoviewmodel.MakeMan = tempData.MakeMan;
                return View(base_taobaoviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string gysid = Request["gysid"] ?? "";
            string cpxid = Request["cpxid"] ?? "";
            string daima = Request["daima"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string miaoshu = Request["miaoshu"] ?? "";
            string jiaxs = Request["jiaxs"] ?? "";
            string jiacg = Request["jiacg"] ?? "";
            string xsdw = Request["xsdw"] ?? "";
            string tingyongsf = Request["tingyongsf"] ?? "";
            string kongzhisf = Request["kongzhisf"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_taobao p = ob_base_taobaoservice.GetEntityById(base_taobao => base_taobao.ID == uid);
                p.GYSID = gysid == "" ? 0 : int.Parse(gysid);
                p.CPXID = cpxid == "" ? 0 : int.Parse(cpxid);
                p.Daima = daima.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.Miaoshu = miaoshu.Trim();
                p.JiaXS = jiaxs == "" ? 0 : decimal.Parse(jiaxs);
                p.JiaCG = jiacg == "" ? 0 : decimal.Parse(jiacg);
                p.XSDW = xsdw.Trim();
                p.TingyongSF = tingyongsf == "" ? false : Boolean.Parse(tingyongsf);
                p.KongzhiSF = kongzhisf == "" ? false : Boolean.Parse(kongzhisf);
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_taobaoservice.UpdateEntity(p);
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
            base_taobao ob_base_taobao;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_taobao = ob_base_taobaoservice.GetEntityById(base_taobao => base_taobao.ID == id && base_taobao.IsDelete == false);
                    ob_base_taobao.IsDelete = true;
                    ob_base_taobaoservice.UpdateEntity(ob_base_taobao);
                }
            }
            return RedirectToAction("Index");
        }
        public JsonResult getTaobaoWithQuery()
        {
            var tbdm = Request["req_tbdm"] ?? "";
            var tbmc = Request["req_tbmc"] ?? "";
            var gys = Request["req_gys"] ?? "";
            var cpxid = Request["cpxid"] ?? "";

            Expression<Func<base_taobao, bool>> where = PredicateExtensionses.True<base_taobao>();
            if (!string.IsNullOrEmpty(tbdm))
                where = where.And(p => p.Daima.Contains(tbdm));
            if (!string.IsNullOrEmpty(tbmc))
                where = where.And(p => p.Mingcheng.Contains(tbmc));
            if (!string.IsNullOrEmpty(gys))
                where = where.And(p => p.GYSID == int.Parse(gys));
            where = where.And(p => p.CPXID == int.Parse(cpxid) && p.IsDelete == false);

            var tempData = ServiceFactory.base_taobaoservice.LoadSortEntities(where.Compile(), true, p => p.Mingcheng).ToList<base_taobao>();
            if (tempData == null)
                return Json(-1);
            return Json(tempData);
        }
        public JsonResult GetPackagebyCust()
        {
            var _cpx = Request["cpx"] ?? "";
            var _dm = Request["dm"] ?? "";
            var _sl = Request["sl"] ?? "";
            var _sm = Request["sm"] ?? "";
            if (string.IsNullOrEmpty(_cpx) || string.IsNullOrEmpty(_dm) || string.IsNullOrEmpty(_sl))
                return Json(-1);
            try
            {
                int _cpxid = int.Parse(_cpx);
                float _shul = float.Parse(_sl);
                var _tb = ob_base_taobaoservice.GetEntityById(p => p.Daima == _dm && p.CPXID == int.Parse(_cpx) && p.IsDelete == false && p.TingyongSF == false);
                if (_tb == null)
                    return Json(-3);
                else
                {
                    if (_tb.KongzhiSF)
                        return Json(-4);
                    else
                        return Json(_tb);
                }
            }
            catch
            {
                return Json(-2);
            }
        }
        public JsonResult GetProducts()
        {
            var _cpx = Request["cpx"] ?? "";
            var _imp = Request["imt"] ?? "";

            if (string.IsNullOrEmpty(_cpx) || string.IsNullOrEmpty(_imp))
                return Json(-1);
            try
            {
                string[] _item = _imp.Split();
                List<ord_productsViewModel> _products = new List<ord_productsViewModel>();
                for (int i = 0; i < _item.Count(); i = i + 2)
                {
                    string _dm = _item[i];
                    if (i + 1 >= _item.Count())
                        break;
                    string _sh = _item[i + 1];
                    if (string.IsNullOrEmpty(_dm) || string.IsNullOrEmpty(_sh))
                        continue;
                    float _sl = float.Parse(_sh);
                    var _sp = ob_base_taobaoservice.GetEntityById(p=>p.Daima==_dm && p.CPXID==int.Parse(_cpx) && p.IsDelete==false);
                    if (_sp==null)
                        continue;
                    ord_productsViewModel _pro = new ord_productsViewModel();
                    _pro.ID = _sp.ID;
                    _pro.Daima = _sp.Daima;
                    _pro.Guige = _sp.Miaoshu;
                    _pro.JiaXS = _sp.JiaXS;
                    _pro.Mingcheng = _sp.Mingcheng;
                    _pro.Shuliang = _sl;
                    _pro.Jine = _pro.JiaXS * (decimal)_pro.Shuliang;
                    _products.Add(_pro);
                }
                return Json(_products);
            }
            catch
            {
                return Json(-3);
            }

        }
    }
}

