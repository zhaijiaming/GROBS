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
using System.ComponentModel.DataAnnotations;

namespace GROBS.Controllers
{
    public class ZhuCeZhenXX
    {
        public int ID { get; set; }
        public string Bianhao { get; set; }//注册证编号
        public string Mingcheng { get; set; }//商品名称
        public string Bianzhun { get; set; }//产品标准
        public string Chandi { get; set; }//产地
        public int ShengchanqiyeID { get; set; }//生产企业序号
        public string Shengchanqiye { get; set; }//生产企业
        public List<base_zhucezhenggg> Guige { get; set; }//规格列表
    }
    public class base_shangpinzczController : Controller
    {
        private Ibase_shangpinzczService ob_base_shangpinzczservice = ServiceFactory.base_shangpinzczservice;
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_shangpinzcz_index";
            PageMenu.Set("Index", "base_shangpinzcz", "基础数据");
            Expression<Func<base_shangpinzcz, bool>> where = PredicateExtensionses.True<base_shangpinzcz>();
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
                                        where = where.And(base_shangpinzcz => base_shangpinzcz.Bianhao == bianhao);
                                    else
                                        where = where.Or(base_shangpinzcz => base_shangpinzcz.Bianhao == bianhao);
                                }
                                if (bianhaoequal.Equals("like"))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(base_shangpinzcz => base_shangpinzcz.Bianhao.Contains(bianhao));
                                    else
                                        where = where.Or(base_shangpinzcz => base_shangpinzcz.Bianhao.Contains(bianhao));
                                }
                            }
                            break;
                        case "mingcheng":
                            string mingcheng = scld[1];
                            string mingchengequal = scld[2];
                            string mingchengand = scld[3];
                            //mingcheng
                            if (!string.IsNullOrEmpty(mingcheng))
                            {
                                if (mingchengequal.Equals("="))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_shangpinzcz => base_shangpinzcz.Mingcheng == mingcheng);
                                    else
                                        where = where.Or(base_shangpinzcz => base_shangpinzcz.Mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_shangpinzcz => base_shangpinzcz.Mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(base_shangpinzcz => base_shangpinzcz.Mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_shangpinzcz => base_shangpinzcz.IsDelete == false);
            var tempData = ob_base_shangpinzczservice.LoadSortEntities(where.Compile(), false, base_shangpinzcz => base_shangpinzcz.ID).ToPagedList<base_shangpinzcz>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shangpinzcz = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_shangpinzcz_index";
            string page = "1";
            //biaohao
            string bianhao = Request["bianhao"] ?? "";
            string bianhaoequal = Request["bianhaoequal"] ?? "";
            string bianhaoand = Request["bianhaoand"] ?? "";
            //mingcheng
            string mingcheng = Request["mingcheng"] ?? "";
            string mingchengequal = Request["mingchengequal"] ?? "";
            string mingchengand = Request["mingchengand"] ?? "";
            PageMenu.Set("Index", "base_shangpinzcz", "基础数据");
            Expression<Func<base_shangpinzcz, bool>> where = PredicateExtensionses.True<base_shangpinzcz>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //biaohao
                if (!string.IsNullOrEmpty(bianhao))
                {
                    if (bianhaoequal.Equals("="))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_shangpinzcz => base_shangpinzcz.Bianhao == bianhao);
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_shangpinzcz => base_shangpinzcz.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.Bianhao.Contains(bianhao));
                    }
                }
                if (!string.IsNullOrEmpty(bianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", bianhao, bianhaoequal, bianhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", "", bianhaoequal, bianhaoand);
                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_shangpinzcz => base_shangpinzcz.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_shangpinzcz => base_shangpinzcz.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);

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
                            where = where.And(base_shangpinzcz => base_shangpinzcz.Bianhao == bianhao);
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_shangpinzcz => base_shangpinzcz.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.Bianhao.Contains(bianhao));
                    }
                }
                if (!string.IsNullOrEmpty(bianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", bianhao, bianhaoequal, bianhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", "", bianhaoequal, bianhaoand);
                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_shangpinzcz => base_shangpinzcz.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_shangpinzcz => base_shangpinzcz.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_shangpinzcz => base_shangpinzcz.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_shangpinzcz => base_shangpinzcz.IsDelete == false);

            var tempData = ob_base_shangpinzczservice.LoadSortEntities(where.Compile(), false, base_shangpinzcz => base_shangpinzcz.ID).ToPagedList<base_shangpinzcz>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shangpinzcz = tempData;
            return View(tempData);
        }

        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            return View();
        }
        public JsonResult GetZCZNow()
        {
            int _userid = (int)Session["user_id"];
            var _mc = Request["mc"] ?? "";
            var _bh = Request["bh"] ?? "";
            var _gg = Request["gg"] ?? "";

            Expression<Func<base_shangpinzcz, bool>> where = PredicateExtensionses.True<base_shangpinzcz>();

            if (string.IsNullOrEmpty(_mc) && string.IsNullOrEmpty(_bh) && string.IsNullOrEmpty(_gg))
                return Json(1);
            if (!string.IsNullOrEmpty(_mc))
                where = where.And(p => p.Mingcheng.Contains(_mc));
            if (!string.IsNullOrEmpty(_bh))
                where = where.And(p => p.Bianhao.Contains(_bh));
            if (!string.IsNullOrEmpty(_gg))
            {
                var _gglist = ServiceFactory.base_zhucezhengggservice.LoadEntities(g => g.Guige.Contains(_gg)).ToList<base_zhucezhenggg>();
                var _zidlist = from gg in _gglist
                               select (int)gg.ZCZID;
                var _zid = _zidlist.Distinct().ToList();
                where = where.And(p => _zid.Contains(p.ID));
            }
            where = where.And(p => p.ShixiaoSF == false && p.IsDelete == false);
            var _zczlist = ob_base_shangpinzczservice.LoadSortEntities(where.Compile(), true, s => s.Bianhao);
            return Json(_zczlist.ToList<base_shangpinzcz>());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string bianhao = Request["bianhao"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string guige = Request["guige"] ?? "";
            string bianzhun = Request["bianzhun"] ?? "";
            string zhucezhengyxq = Request["zhucezhengyxq"] ?? "";
            string zhucezhengtp = Request["zhucezhengtp"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string shixiaosf = Request["shixiaosf"];
            string bianhaozw = Request["bianhaozw"] ?? "";
            string chandi = Request["chandi"] ?? "";
            string shengchanqiyeid = Request["Shengchanqiyeid"] ?? "";
            string shiyongfw = Request["shiyongfw"] ?? "";
            string zhucedl = Request["zhucedl"] ?? "";
            string shouhuojg = Request["shouhuojg"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string pizhunrq = Request["pizhunrq"] ?? "";
            try
            {
                base_shangpinzcz ob_base_shangpinzcz = new base_shangpinzcz();
                ob_base_shangpinzcz.Bianhao = bianhao.Trim();
                ob_base_shangpinzcz.Mingcheng = mingcheng.Trim();
                ob_base_shangpinzcz.Guige = guige.Trim();
                ob_base_shangpinzcz.Bianzhun = bianzhun.Trim();
                ob_base_shangpinzcz.ZhucezhengYXQ = zhucezhengyxq == "" ? DateTime.Now : DateTime.Parse(zhucezhengyxq);
                ob_base_shangpinzcz.ZhucezhengTP = zhucezhengtp.Trim();
                ob_base_shangpinzcz.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_shangpinzcz.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_shangpinzcz.ShixiaoSF = shixiaosf == "true" ? true : false;
                ob_base_shangpinzcz.BianhaoZW = bianhaozw.Trim();
                ob_base_shangpinzcz.Chandi = chandi.Trim();
                ob_base_shangpinzcz.ShengchanqiyeID = shengchanqiyeid == "" ? 0 : int.Parse(shengchanqiyeid);
                ob_base_shangpinzcz.ShiyongFW = shiyongfw.Trim();
                ob_base_shangpinzcz.ZhuceDL = zhucedl.Trim();
                ob_base_shangpinzcz.ShouhuoJG = shouhuojg.Trim();
                ob_base_shangpinzcz.Beizhu = beizhu.Trim();
                ob_base_shangpinzcz.PizhunRQ = pizhunrq == "" ? DateTime.Now : DateTime.Parse(pizhunrq);

                ob_base_shangpinzcz = ob_base_shangpinzczservice.AddEntity(ob_base_shangpinzcz);
                id = ob_base_shangpinzcz.ID.ToString();
                ViewBag.base_shangpinzcz = ob_base_shangpinzcz;
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
            base_shangpinzcz tempData = ob_base_shangpinzczservice.GetEntityById(base_shangpinzcz => base_shangpinzcz.ID == id && base_shangpinzcz.IsDelete == false);
            ViewBag.base_shangpinzcz = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_shangpinzczViewModel base_shangpinzczviewmodel = new base_shangpinzczViewModel();
                base_shangpinzczviewmodel.ID = tempData.ID;
                base_shangpinzczviewmodel.Bianhao = tempData.Bianhao;
                base_shangpinzczviewmodel.Mingcheng = tempData.Mingcheng;
                base_shangpinzczviewmodel.Guige = tempData.Guige;
                base_shangpinzczviewmodel.Bianzhun = tempData.Bianzhun;
                base_shangpinzczviewmodel.ZhucezhengYXQ = tempData.ZhucezhengYXQ;
                base_shangpinzczviewmodel.ZhucezhengTP = tempData.ZhucezhengTP;
                base_shangpinzczviewmodel.MakeDate = tempData.MakeDate;
                base_shangpinzczviewmodel.MakeMan = tempData.MakeMan;
                base_shangpinzczviewmodel.ShixiaoSF = tempData.ShixiaoSF;
                base_shangpinzczviewmodel.BianhaoZW = tempData.BianhaoZW;
                base_shangpinzczviewmodel.Chandi = tempData.Chandi;
                base_shangpinzczviewmodel.ShengchanqiyeID = (int)tempData.ShengchanqiyeID;
                base_shangpinzczviewmodel.ShiyongFW = tempData.ShiyongFW;
                base_shangpinzczviewmodel.ZhuceDL = tempData.ZhuceDL;
                base_shangpinzczviewmodel.ShouhuoJG = tempData.ShouhuoJG;
                base_shangpinzczviewmodel.Beizhu = tempData.Beizhu;
                base_shangpinzczviewmodel.PizhunRQ = tempData.PizhunRQ;
                return View(base_shangpinzczviewmodel);
            }
        }

        public JsonResult GetDetail()
        {
            int _userid = (int)Session["user_id"];
            string _username = (string)Session["user_name"];
            string _zczid = Request["zczbh"] ?? "";

            //IList<ZhuCeZhenXX> _zczxxs;
            ZhuCeZhenXX _zczxxs = null;
            if (_zczid == "")
                return Json("");
            else
            {
                base_shangpinzcz _zcz = ob_base_shangpinzczservice.GetEntityById(p => p.ID == int.Parse(_zczid));
                if (_zcz != null)
                {
                    _zczxxs = new ZhuCeZhenXX();
                    _zczxxs.ID = _zcz.ID;
                    _zczxxs.Bianhao = _zcz.Bianhao;
                    _zczxxs.Bianzhun = _zcz.Bianzhun;
                    _zczxxs.Mingcheng = _zcz.Mingcheng;
                    _zczxxs.Chandi = _zcz.Chandi;
                    _zczxxs.ShengchanqiyeID = (int)_zcz.ShengchanqiyeID;
                    base_shengchanqiye _scqy = ServiceFactory.base_shengchanqiyeservice.GetEntityById(s => s.ID == _zczxxs.ShengchanqiyeID);
                    if (_scqy != null)
                        _zczxxs.Shengchanqiye = _scqy.Qiyemingcheng;
                    else
                        _zczxxs.Shengchanqiye = "";
                    _zczxxs.Guige = ServiceFactory.base_zhucezhengggservice.LoadSortEntities(p => p.ZCZID == _zczxxs.ID && p.IsDelete == false, true, s => s.Guige).ToList<base_zhucezhenggg>();
                }
            }
            return Json(_zczxxs);
        }
        public JsonResult CheckLicenseCode()
        {
            int _userid = (int)Session["user_id"];
            var _lc = Request["lc"] ?? "";
            if (string.IsNullOrEmpty(_lc))
                return Json(1);
            var _license = ob_base_shangpinzczservice.GetEntityById(p => p.Bianhao == _lc && p.IsDelete == false);
            if (_license == null)
                return Json(1);
            return Json(-1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string bianhao = Request["bianhao"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string guige = Request["guige"] ?? "";
            string bianzhun = Request["bianzhun"] ?? "";
            string zhucezhengyxq = Request["zhucezhengyxq"] ?? "";
            string zhucezhengtp = Request["zhucezhengtp"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string shixiaosf = Request["shixiaosf"];
            string bianhaozw = Request["bianhaozw"] ?? "";
            string chandi = Request["chandi"] ?? "";
            string shengchanqiyeid = Request["Shengchanqiyeid"] ?? "";
            string shiyongfw = Request["shiyongfw"] ?? "";
            string zhucedl = Request["zhucedl"] ?? "";
            string shouhuojg = Request["shouhuojg"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string pizhunrq = Request["pizhunrq"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_shangpinzcz p = ob_base_shangpinzczservice.GetEntityById(base_shangpinzcz => base_shangpinzcz.ID == uid);
                p.Bianhao = bianhao.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.Guige = guige.Trim();
                p.Bianzhun = bianzhun.Trim();
                p.ZhucezhengYXQ = zhucezhengyxq == "" ? DateTime.Now : DateTime.Parse(zhucezhengyxq);
                p.ZhucezhengTP = zhucezhengtp.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.ShixiaoSF = shixiaosf == "true" ? true : false;
                p.BianhaoZW = bianhaozw.Trim();
                p.Chandi = chandi.Trim();
                p.ShengchanqiyeID = shengchanqiyeid == "" ? 0 : int.Parse(shengchanqiyeid);
                p.ShiyongFW = shiyongfw.Trim();
                p.ZhuceDL = zhucedl.Trim();
                p.ShouhuoJG = shouhuojg.Trim();
                p.Beizhu = beizhu.Trim();
                p.PizhunRQ = pizhunrq == "" ? DateTime.Now : DateTime.Parse(pizhunrq);
                ob_base_shangpinzczservice.UpdateEntity(p);
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
            base_shangpinzcz ob_base_shangpinzcz;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_shangpinzcz = ob_base_shangpinzczservice.GetEntityById(base_shangpinzcz => base_shangpinzcz.ID == id && base_shangpinzcz.IsDelete == false);
                    ob_base_shangpinzcz.IsDelete = true;
                    ob_base_shangpinzczservice.UpdateEntity(ob_base_shangpinzcz);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult RegistrationExport()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_shangpinzcz_index";
            Expression<Func<base_shangpinzcz, bool>> where = PredicateExtensionses.True<base_shangpinzcz>();
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
                                        where = where.And(base_shangpinzcz => base_shangpinzcz.Bianhao == bianhao);
                                    else
                                        where = where.Or(base_shangpinzcz => base_shangpinzcz.Bianhao == bianhao);
                                }
                                if (bianhaoequal.Equals("like"))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(base_shangpinzcz => base_shangpinzcz.Bianhao.Contains(bianhao));
                                    else
                                        where = where.Or(base_shangpinzcz => base_shangpinzcz.Bianhao.Contains(bianhao));
                                }
                            }
                            break;
                        case "mingcheng":
                            string mingcheng = scld[1];
                            string mingchengequal = scld[2];
                            string mingchengand = scld[3];
                            //mingcheng
                            if (!string.IsNullOrEmpty(mingcheng))
                            {
                                if (mingchengequal.Equals("="))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_shangpinzcz => base_shangpinzcz.Mingcheng == mingcheng);
                                    else
                                        where = where.Or(base_shangpinzcz => base_shangpinzcz.Mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_shangpinzcz => base_shangpinzcz.Mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(base_shangpinzcz => base_shangpinzcz.Mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_shangpinzcz => base_shangpinzcz.IsDelete == false);
            var tempData = ob_base_shangpinzczservice.LoadSortEntities(where.Compile(), false, base_shangpinzcz => base_shangpinzcz.ID);
            ViewBag.registration = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "RegistrationExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("Registration_{0}.xls", DateTime.Now.ToShortDateString()));
        }
        public ActionResult Overdue(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            var period = Request["period"] ?? "";

            if (string.IsNullOrEmpty(period))
                period = "0";
            var tempData = ob_base_shangpinzczservice.LoadEntities(p => p.IsDelete == false && p.ZhucezhengYXQ <= DateTime.Now.AddDays(int.Parse(period))).ToPagedList(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shangpinzcz = tempData;
            ViewBag.period = period;
            return View(tempData);
        }
        public ActionResult OverdueExport()
        {
            int userid = (int)Session["user_id"];
            var period = Request["period"] ?? "";
            if (string.IsNullOrEmpty(period))
                period = "0";
            var tempData = ob_base_shangpinzczservice.LoadEntities(p => p.IsDelete == false && p.ZhucezhengYXQ <= DateTime.Now.AddDays(int.Parse(period)));
            ViewBag.base_shangpinzcz = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "OverdueExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("RegistOverdue_{0}.xls", DateTime.Now.ToShortDateString()));
        }
    }
}

