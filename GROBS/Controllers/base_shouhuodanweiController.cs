using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Expressions;
using X.PagedList;
using GROBS.EFModels;
using GROBS.IBSL;
using GROBS.BSL;
using GROBS.Common;

namespace GROBS.Controllers
{
    public class base_shouhuodanweiController : Controller
    {
        private Ibase_shouhuodanweiService ob_base_shouhuodanweiservice = ServiceFactory.base_shouhuodanweiservice;

        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_shouhuodanwei_index";
            PageMenu.Set("Index", "base_shouhuodanwei", "基础数据");
            Expression<Func<base_shouhuodanwei, bool>> where = PredicateExtensionses.True<base_shouhuodanwei>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "huozhuid":
                            string huozhuid = scld[1];
                            string huozhuidequal = scld[2];
                            string huozhuidand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuid))
                            {
                                if (huozhuidequal.Equals("="))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(base_shouhuodanwei => base_shouhuodanwei.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(base_shouhuodanwei => base_shouhuodanwei.HuozhuID == int.Parse(huozhuid));
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
                                        where = where.And(base_shouhuodanwei => base_shouhuodanwei.Mingcheng == mingcheng);
                                    else
                                        where = where.Or(base_shouhuodanwei => base_shouhuodanwei.Mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_shouhuodanwei => base_shouhuodanwei.Mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(base_shouhuodanwei => base_shouhuodanwei.Mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_shouhuodanwei => base_shouhuodanwei.IsDelete == false);
            var tempData = ob_base_shouhuodanweiservice.LoadSortEntities(where.Compile(), false, base_shouhuodanwei => base_shouhuodanwei.ID).ToPagedList<base_shouhuodanwei>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shouhuodanwei = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_shouhuodanwei_index";
            string page = "1";
            //货主
            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";
            //单位名称
            string mingcheng = Request["mingcheng"] ?? "";
            string mingchengequal = Request["mingchengequal"] ?? "";
            string mingchengand = Request["mingchengand"] ?? "";
            PageMenu.Set("Index", "base_shouhuodanwei", "基础数据");
            Expression<Func<base_shouhuodanwei, bool>> where = PredicateExtensionses.True<base_shouhuodanwei>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(base_shouhuodanwei => base_shouhuodanwei.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(base_shouhuodanwei => base_shouhuodanwei.HuozhuID == int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);
                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_shouhuodanwei => base_shouhuodanwei.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_shouhuodanwei => base_shouhuodanwei.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_shouhuodanwei => base_shouhuodanwei.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_shouhuodanwei => base_shouhuodanwei.Mingcheng.Contains(mingcheng));
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
                //huozhuid
                if (!string.IsNullOrEmpty(huozhuid))
                {
                    if (huozhuidequal.Equals("="))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(base_shouhuodanwei => base_shouhuodanwei.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(base_shouhuodanwei => base_shouhuodanwei.HuozhuID == int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_shouhuodanwei => base_shouhuodanwei.Mingcheng == mingcheng);
                        else
                            where = where.Or(base_shouhuodanwei => base_shouhuodanwei.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(base_shouhuodanwei => base_shouhuodanwei.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(base_shouhuodanwei => base_shouhuodanwei.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_shouhuodanwei => base_shouhuodanwei.IsDelete == false);

            var tempData = ob_base_shouhuodanweiservice.LoadSortEntities(where.Compile(), false, base_shouhuodanwei => base_shouhuodanwei.ID).ToPagedList<base_shouhuodanwei>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_shouhuodanwei = tempData;
            return View(tempData);

        }

        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            return View();
        }
        public JsonResult CheckCode()
        {
            int _userid = (int)Session["user_id"];
            var _sh = Request["sh"] ?? "";
            var _hz = Request["hz"] ?? "";
            if (string.IsNullOrEmpty(_sh))
                return Json(1);
            if (string.IsNullOrEmpty(_hz))
                return Json(-2);
            var _shdw = ob_base_shouhuodanweiservice.GetEntityById(p => p.Mingcheng == _sh && p.HuozhuID == int.Parse(_hz) && p.IsDelete == false);
            if (_shdw == null)
                return Json(1);
            return Json(-1);
        }
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string huozhuid = Request["huozhuid"] ?? "";
            string kehudm = Request["kehudm"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string yingyezhizhaobh = Request["yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["yingyezhizhaotp"] ?? "";
            string jingyingxukebh = Request["jingyingxukebh"] ?? "";
            string jingyingxukeyxq = Request["jingyingxukeyxq"] ?? "";
            string jingyingxuketp = Request["jingyingxuketp"] ?? "";
            string shouying = Request["shouying"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string jingyingfanwei = Request["jingyingfanwei"] ?? "";
            string jingyingfanweidm = Request["jingyingfanweidm"] ?? "";
            string quyu = Request["quyu"] ?? "";
            string beianbh = Request["beianbh"] ?? "";
            string beianyxq = Request["beianyxq"] ?? "";
            string beianpzrq = Request["beianpzrq"] ?? "";
            string beianfzjg = Request["beianfzjg"] ?? "";
            string beiantp = Request["beiantp"] ?? "";
            string xukepzrq = Request["xukepzrq"] ?? "";
            string xukefzjg = Request["xukefzjg"] ?? "";
            string qiyedz = Request["qiyedz"] ?? "";
            string songhuodz = Request["songhuodz"] ?? "";
            string shenchasf = Request["shenchasf"] ?? "";
            string hezuosf = Request["hezuosf"] ?? "";
            if (shenchasf.IndexOf("true") > -1)
                shenchasf = "true";
            if (hezuosf.IndexOf("true") > -1)
                hezuosf = "true";
            try
            {
                base_shouhuodanwei ob_base_shouhuodanwei = new base_shouhuodanwei();
                ob_base_shouhuodanwei.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                ob_base_shouhuodanwei.KehuDM = kehudm.Trim();
                ob_base_shouhuodanwei.Mingcheng = mingcheng.Trim();
                ob_base_shouhuodanwei.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                ob_base_shouhuodanwei.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                ob_base_shouhuodanwei.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                ob_base_shouhuodanwei.JingyingxukeBH = jingyingxukebh.Trim();
                ob_base_shouhuodanwei.JingyingxukeYXQ = jingyingxukeyxq == "" ? DateTime.Now : DateTime.Parse(jingyingxukeyxq);
                ob_base_shouhuodanwei.JingyingxukeTP = jingyingxuketp.Trim();
                //ob_base_shouhuodanwei.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                ob_base_shouhuodanwei.Shouying = 1;
                ob_base_shouhuodanwei.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_shouhuodanwei.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_shouhuodanwei.Jingyinfanwei = jingyingfanwei.Trim();
                ob_base_shouhuodanwei.JingyinfanweiDM = jingyingfanweidm.Trim();
                ob_base_shouhuodanwei.Quyu = quyu.Trim();
                ob_base_shouhuodanwei.BeianBH = beianbh.Trim();
                ob_base_shouhuodanwei.BeianYXQ = beianyxq == "" ? DateTime.Now : DateTime.Parse(beianyxq);
                ob_base_shouhuodanwei.BeianPZRQ = beianpzrq == "" ? DateTime.Now : DateTime.Parse(beianpzrq);
                ob_base_shouhuodanwei.BeianFZJG = beianfzjg.Trim();
                ob_base_shouhuodanwei.BeianTP = beiantp.Trim();
                ob_base_shouhuodanwei.XukePZRQ = xukepzrq == "" ? DateTime.Now : DateTime.Parse(xukepzrq);
                ob_base_shouhuodanwei.XukeFZJG = xukefzjg.Trim();
                ob_base_shouhuodanwei.ShenchaSF = shenchasf == "" ? false : Boolean.Parse(shenchasf);
                ob_base_shouhuodanwei.HezuoSF = hezuosf == "" ? false : Boolean.Parse(hezuosf);
                ob_base_shouhuodanwei.QiyeDZ = qiyedz.Trim();
                ob_base_shouhuodanwei.SonghuoDZ = songhuodz.Trim();

                ob_base_shouhuodanwei = ob_base_shouhuodanweiservice.AddEntity(ob_base_shouhuodanwei);
                id = ob_base_shouhuodanwei.ID.ToString();
                ViewBag.base_shouhuodanwei = ob_base_shouhuodanwei;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Edit", new { id = int.Parse(id) });
        }

        public ActionResult Edit(int id)
        {
            base_shouhuodanwei tempData = ob_base_shouhuodanweiservice.GetEntityById(base_shouhuodanwei => base_shouhuodanwei.ID == id && base_shouhuodanwei.IsDelete == false);
            ViewBag.base_shouhuodanwei = tempData;
            return View();
        }
        public JsonResult GetCust()
        {
            var _hzid = Request["hz"] ?? "";
            if (_hzid.Length == 0)
                _hzid = "0";
            if (int.Parse(_hzid) == 0)
                return Json(-1);
            var tempData = ob_base_shouhuodanweiservice.LoadSortEntities(p => p.IsDelete == false && p.HuozhuID == int.Parse(_hzid) && p.HezuoSF == true, true, s => s.Mingcheng);
            if (tempData == null)
                return Json(-1);
            return Json(tempData.ToList<base_shouhuodanwei>());
        }
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string huozhuid = Request["huozhuid"] ?? "";
            string kehudm = Request["kehudm"] ?? "";
            string mingcheng = Request["mingcheng"] ?? "";
            string yingyezhizhaobh = Request["yingyezhizhaobh"] ?? "";
            string yingyezhizhaoyxq = Request["yingyezhizhaoyxq"] ?? "";
            string yingyezhizhaotp = Request["yingyezhizhaotp"] ?? "";
            string jingyingxukebh = Request["jingyingxukebh"] ?? "";
            string jingyingxukeyxq = Request["jingyingxukeyxq"] ?? "";
            string jingyingxuketp = Request["jingyingxuketp"] ?? "";
            string shouying = Request["shouying"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            string jingyingfanwei = Request["jingyingfanwei"] ?? "";
            string jingyingfanweidm = Request["jingyingfanweidm"] ?? "";
            string quyu = Request["quyu"] ?? "";
            string beianbh = Request["beianbh"] ?? "";
            string beianyxq = Request["beianyxq"] ?? "";
            string beianpzrq = Request["beianpzrq"] ?? "";
            string beianfzjg = Request["beianfzjg"] ?? "";
            string beiantp = Request["beiantp"] ?? "";
            string xukepzrq = Request["xukepzrq"] ?? "";
            string xukefzjg = Request["xukefzjg"] ?? "";
            string qiyedz = Request["qiyedz"] ?? "";
            string songhuodz = Request["songhuodz"] ?? "";
            string shenchasf = Request["shenchasf"] ?? "";
            string hezuosf = Request["hezuosf"] ?? "";
            if (shenchasf.IndexOf("true") > -1)
                shenchasf = "true";
            if (hezuosf.IndexOf("true") > -1)
                hezuosf = "true";
            string Lianxiren = Request["Lianxiren"] ?? "";
            string LianxiDH = Request["LianxiDH"] ?? "";

            int uid = int.Parse(id);
            try
            {
                base_shouhuodanwei p = ob_base_shouhuodanweiservice.GetEntityById(base_shouhuodanwei => base_shouhuodanwei.ID == uid);
                p.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                p.Mingcheng = mingcheng.Trim();
                p.KehuDM = kehudm.Trim();
                p.YingyezhizhaoBH = yingyezhizhaobh.Trim();
                p.YingyezhizhaoYXQ = yingyezhizhaoyxq == "" ? DateTime.Now : DateTime.Parse(yingyezhizhaoyxq);
                p.YingyezhizhaoTP = yingyezhizhaotp.Trim();
                p.JingyingxukeBH = jingyingxukebh.Trim();
                p.JingyingxukeYXQ = jingyingxukeyxq == "" ? DateTime.Now : DateTime.Parse(jingyingxukeyxq);
                p.JingyingxukeTP = jingyingxuketp.Trim();
                p.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                p.Jingyinfanwei = jingyingfanwei.Trim();
                p.JingyinfanweiDM = jingyingfanweidm.Trim();
                p.Quyu = quyu.Trim();
                p.BeianBH = beianbh.Trim();
                p.BeianYXQ = beianyxq == "" ? DateTime.Now : DateTime.Parse(beianyxq);
                p.BeianPZRQ = beianpzrq == "" ? DateTime.Now : DateTime.Parse(beianpzrq);
                p.BeianFZJG = beianfzjg.Trim();
                p.BeianTP = beiantp.Trim();
                p.XukePZRQ = xukepzrq == "" ? DateTime.Now : DateTime.Parse(xukepzrq);
                p.XukeFZJG = xukefzjg.Trim();
                p.QiyeDZ = qiyedz.Trim();
                p.SonghuoDZ = songhuodz.Trim();
                p.ShenchaSF = shenchasf == "" ? false : Boolean.Parse(shenchasf);
                p.HezuoSF = hezuosf == "" ? false : Boolean.Parse(hezuosf);
                p.Lianxiren = Lianxiren.Trim();
                p.LianxiDH = LianxiDH.Trim();

                ob_base_shouhuodanweiservice.UpdateEntity(p);
                ViewBag.saveok = ViewAddTag.ModifyOk;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.saveok = ViewAddTag.ModifyNo;
            }
            return RedirectToAction("Edit", new { id = int.Parse(id) });
        }

        public JsonResult UpdateDz()
        {
            string id = Request["id"] ?? "";
            string songhuodz = Request["songhuodz"] ?? "";
            string Lianxiren = Request["Lianxiren"] ?? "";
            string Lianxidianhua = Request["Lianxidianhua"] ?? "";

            int uid = int.Parse(id);
            try
            {
                base_shouhuodanwei p = ob_base_shouhuodanweiservice.GetEntityById(base_shouhuodanwei => base_shouhuodanwei.ID == uid);

                p.SonghuoDZ = songhuodz.Trim();
                p.Lianxiren = Lianxiren.Trim();
                p.LianxiDH = Lianxidianhua.Trim();
                ob_base_shouhuodanweiservice.UpdateEntity(p);
                ViewBag.saveok = ViewAddTag.ModifyOk;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.saveok = ViewAddTag.ModifyNo;
            }

            return Json(1);
        }

        public ActionResult Delete()
        {
            string sdel = Request["del"] ?? "";
            int id;
            base_shouhuodanwei ob_base_shouhuodanwei;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_shouhuodanwei = ob_base_shouhuodanweiservice.GetEntityById(base_shouhuodanwei => base_shouhuodanwei.ID == id && base_shouhuodanwei.IsDelete == false);
                    ob_base_shouhuodanwei.IsDelete = true;
                    ob_base_shouhuodanweiservice.UpdateEntity(ob_base_shouhuodanwei);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult RecipientEntityExport()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_shouhuodanwei_index";
            Expression<Func<base_shouhuodanwei, bool>> where = PredicateExtensionses.True<base_shouhuodanwei>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "huozhuid":
                            string huozhuid = scld[1];
                            string huozhuidequal = scld[2];
                            string huozhuidand = scld[3];
                            if (!string.IsNullOrEmpty(huozhuid))
                            {
                                if (huozhuidequal.Equals("="))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(base_shouhuodanwei => base_shouhuodanwei.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(base_shouhuodanwei => base_shouhuodanwei.HuozhuID == int.Parse(huozhuid));
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
                                        where = where.And(base_shouhuodanwei => base_shouhuodanwei.Mingcheng == mingcheng);
                                    else
                                        where = where.Or(base_shouhuodanwei => base_shouhuodanwei.Mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(base_shouhuodanwei => base_shouhuodanwei.Mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(base_shouhuodanwei => base_shouhuodanwei.Mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_shouhuodanwei => base_shouhuodanwei.IsDelete == false);
            var tempData = ob_base_shouhuodanweiservice.LoadSortEntities(where.Compile(), false, base_shouhuodanwei => base_shouhuodanwei.ID);
            ViewBag.RecipientEntity = tempData;
            ViewData.Model = tempData;
            string viewHtml = ExportNow.RenderPartialViewToString(this, "RecipientEntityExport");
            return File(System.Text.Encoding.UTF8.GetBytes(viewHtml), "application/ms-excel", string.Format("RecipientEntity_{0}.xls", DateTime.Now.ToShortDateString()));
        }
    }
}

