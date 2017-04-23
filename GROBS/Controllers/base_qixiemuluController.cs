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
    public class QiXieMuLu
    {
        public int ID { get; set; }
        public string Bianhao { get; set; }
        public string Mingcheng { get; set; }
        public string Miaoshu { get; set; }
        public int? GuanliFL { get; set; }
    }
    public class base_qixiemuluController : Controller
    {
        private Ibase_qixiemuluService ob_base_qixiemuluservice = ServiceFactory.base_qixiemuluservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_qixiemulu_index";
            PageMenu.Set("Index", "base_qixiemulu", "基础数据");
            Expression<Func<base_qixiemulu, bool>> where = PredicateExtensionses.True<base_qixiemulu>();
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
                                        where = where.And(base_qixiemulu => base_qixiemulu.Bianhao == bianhao);
                                    else
                                        where = where.Or(base_qixiemulu => base_qixiemulu.Bianhao == bianhao);
                                }
                                if (bianhaoequal.Equals("like"))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(base_qixiemulu => base_qixiemulu.Bianhao.Contains(bianhao));
                                    else
                                        where = where.Or(base_qixiemulu => base_qixiemulu.Bianhao.Contains(bianhao));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_qixiemulu => base_qixiemulu.IsDelete == false);

            var tempData = ob_base_qixiemuluservice.LoadSortEntities(where.Compile(), true, base_qixiemulu => base_qixiemulu.Bianhao).ToPagedList<base_qixiemulu>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_qixiemulu = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_qixiemulu_index";
            string page = "1";
            //bianhao
            string bianhao = Request["bianhao"] ?? "";
            string bianhaoequal = Request["bianhaoequal"] ?? "";
            string bianhaoand = Request["bianhaoand"] ?? "";
            //mingcheng
            string mingcheng = Request["mingcheng"] ?? "";
            string mingchengequal = Request["mingchengequal"] ?? "";
            string mingchengand = Request["mingchengand"] ?? "";
            //guanlifl
            string guanlifl = Request["guanlifl"] ?? "";
            string guanliflequal = Request["guanliflequal"] ?? "";
            string guanlifland = Request["guanlifland"] ?? "";
            PageMenu.Set("Index", "base_qixiemulu", "基础数据");
            Expression<Func<base_qixiemulu, bool>> where = PredicateExtensionses.True<base_qixiemulu>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //bianhao
                if (!string.IsNullOrEmpty(bianhao))
                {
                    if (bianhaoequal.Equals("="))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_qixiemulu => base_qixiemulu.Bianhao == bianhao);
                        else
                            where = where.Or(base_qixiemulu => base_qixiemulu.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_qixiemulu => base_qixiemulu.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_qixiemulu => base_qixiemulu.Bianhao.Contains(bianhao));
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
                            where = where.And(base_qixiemulu => base_qixiemulu.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_qixiemulu => base_qixiemulu.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_qixiemulu => base_qixiemulu.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_qixiemulu => base_qixiemulu.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);
                //guanlifl
                if (!string.IsNullOrEmpty(guanlifl))
                {
                    if (guanliflequal.Equals("="))
                    {
                        if (guanlifland.Equals("and"))
                            where = where.And(base_qixiemulu => base_qixiemulu.GuanliFL == int.Parse(guanlifl));
                        else
                            where = where.Or(base_qixiemulu => base_qixiemulu.GuanliFL == int.Parse(guanlifl));
                    }
                }
                if (!string.IsNullOrEmpty(guanlifl))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guanlifl", guanlifl, guanliflequal, guanlifland);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "guanlifl", "", guanliflequal, guanlifland);

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
                            where = where.And(base_qixiemulu => base_qixiemulu.Bianhao == bianhao);
                        else
                            where = where.Or(base_qixiemulu => base_qixiemulu.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(base_qixiemulu => base_qixiemulu.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(base_qixiemulu => base_qixiemulu.Bianhao.Contains(bianhao));
                    }
                }
                if (!string.IsNullOrEmpty(bianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", bianhao, bianhaoequal, bianhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", "", bianhaoequal, bianhaoand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_qixiemulu => base_qixiemulu.IsDelete == false);

            var tempData = ob_base_qixiemuluservice.LoadSortEntities(where.Compile(), false, base_qixiemulu => base_qixiemulu.ID).ToPagedList<base_qixiemulu>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_qixiemulu = tempData;
            return View(tempData);
        }
        public JsonResult GetQixiemulu()
        {
            var _qxtemp = ob_base_qixiemuluservice.LoadSortEntities(p => p.IsDelete == false, true, s => s.Bianhao);
            List<QiXieMuLu> _qxlist = new List<QiXieMuLu>();
            QiXieMuLu _qx;
            foreach (var qx in _qxtemp)
            {
                _qx = new QiXieMuLu();
                _qx.ID = qx.ID;
                _qx.Bianhao = qx.Bianhao;
                _qx.Mingcheng = qx.Mingcheng;
                _qx.Miaoshu = qx.Miaoshu;
                _qx.GuanliFL = qx.GuanliFL;
                _qxlist.Add(_qx);
            }
            return Json(_qxlist);
        }
        public JsonResult GetQixiemulu1()
        {
            var _qxtemp = ob_base_qixiemuluservice.LoadSortEntities(p => p.Bianhao.Length < 5 && p.IsDelete == false, true, s => s.Bianhao);
            List<QiXieMuLu> _qxlist = new List<QiXieMuLu>();
            QiXieMuLu _qx;
            foreach (var qx in _qxtemp)
            {
                _qx = new QiXieMuLu();
                _qx.ID = qx.ID;
                _qx.Bianhao = qx.Bianhao;
                _qx.Mingcheng = qx.Mingcheng;
                _qx.Miaoshu = qx.Miaoshu;
                _qx.GuanliFL = qx.GuanliFL;
                _qxlist.Add(_qx);
            }
            return Json(_qxlist);
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
            string mingcheng = Request["mingcheng"] ?? "";
            string miaoshu = Request["miaoshu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string guanlifl = Request["guanlifl"] ?? "";
            try
            {
                base_qixiemulu ob_base_qixiemulu = new base_qixiemulu();
                ob_base_qixiemulu.Bianhao = bianhao.Trim();
                ob_base_qixiemulu.Mingcheng = mingcheng.Trim();
                ob_base_qixiemulu.Miaoshu = miaoshu.Trim();
                ob_base_qixiemulu.Col1 = col1.Trim();
                ob_base_qixiemulu.Col2 = col2.Trim();
                ob_base_qixiemulu.Col3 = col3.Trim();
                ob_base_qixiemulu.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_qixiemulu.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_qixiemulu.GuanliFL = guanlifl == "" ? 0 : int.Parse(guanlifl);
                ob_base_qixiemulu = ob_base_qixiemuluservice.AddEntity(ob_base_qixiemulu);
                ViewBag.base_qixiemulu = ob_base_qixiemulu;
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
            base_qixiemulu tempData = ob_base_qixiemuluservice.GetEntityById(base_qixiemulu => base_qixiemulu.ID == id && base_qixiemulu.IsDelete == false);
            ViewBag.base_qixiemulu = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_qixiemuluViewModel base_qixiemuluviewmodel = new base_qixiemuluViewModel();
                base_qixiemuluviewmodel.ID = tempData.ID;
                base_qixiemuluviewmodel.Bianhao = tempData.Bianhao;
                base_qixiemuluviewmodel.Mingcheng = tempData.Mingcheng;
                base_qixiemuluviewmodel.Miaoshu = tempData.Miaoshu;
                base_qixiemuluviewmodel.Col1 = tempData.Col1;
                base_qixiemuluviewmodel.Col2 = tempData.Col2;
                base_qixiemuluviewmodel.Col3 = tempData.Col3;
                base_qixiemuluviewmodel.MakeDate = tempData.MakeDate;
                base_qixiemuluviewmodel.MakeMan = tempData.MakeMan;
                base_qixiemuluviewmodel.GuanliFL = tempData.GuanliFL;
                return View(base_qixiemuluviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string bianhao = Request["bianhao"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string miaoshu = Request["miaoshu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string guanlifl = Request["guanlifl"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_qixiemulu p = ob_base_qixiemuluservice.GetEntityById(base_qixiemulu => base_qixiemulu.ID == uid);
                p.Bianhao = bianhao.Trim();
                p.Mingcheng = mingcheng.Trim();
                p.Miaoshu = miaoshu.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.GuanliFL = guanlifl == "" ? 0 : int.Parse(guanlifl);
                ob_base_qixiemuluservice.UpdateEntity(p);
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
            base_qixiemulu ob_base_qixiemulu;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_qixiemulu = ob_base_qixiemuluservice.GetEntityById(base_qixiemulu => base_qixiemulu.ID == id && base_qixiemulu.IsDelete == false);
                    ob_base_qixiemulu.IsDelete = true;
                    ob_base_qixiemuluservice.UpdateEntity(ob_base_qixiemulu);
                }
            }
            return RedirectToAction("Index");
        }


        public JsonResult GetDetail()
        {
            int _userid = (int)Session["user_id"];
            string _username = (string)Session["user_name"];
            string _qxmlid = Request["qxml"] ?? "";
            IList<QiXieMuLu> _jingyingfanweis;
            if (_qxmlid == "")
                return Json("");
            else
            {
                _jingyingfanweis = new List<QiXieMuLu>();
                var tempdata = ob_base_qixiemuluservice.LoadSortEntities(p => p.IsDelete == false && p.Bianhao == _qxmlid, false, p => p.Mingcheng);
                //_guiges = tempdata.ToList<ob_base_qixiemulu>();
                foreach (base_qixiemulu _qxmlfw in tempdata)
                {
                    if (_qxmlfw.ID > 0)
                    {
                        QiXieMuLu _jingyingfanwei = new QiXieMuLu();
                        _jingyingfanwei.ID = _qxmlfw.ID;
                        _jingyingfanwei.Bianhao = _qxmlfw.Bianhao;
                        _jingyingfanwei.Mingcheng = _qxmlfw.Mingcheng;
                        _jingyingfanwei.Miaoshu = _qxmlfw.Miaoshu;
                        _jingyingfanwei.GuanliFL = _qxmlfw.GuanliFL;
                        //userinfo _user = ServiceFactory.userinfoservice.GetEntityById(p => p.ID == _zczgg.MakeMan);
                        //_guige.Makeman = _user.FullName;
                        _jingyingfanweis.Add(_jingyingfanwei);
                    }
                }
            }
            return Json(_jingyingfanweis);
        }
    }
}

