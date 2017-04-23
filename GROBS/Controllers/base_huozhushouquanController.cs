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

namespace GROBS.Controllers
{
    public class base_huozhushouquanController : Controller
    {
        private Ibase_huozhushouquanService ob_base_huozhushouquanservice = ServiceFactory.base_huozhushouquanservice;
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_huozhushouquan_index";
            PageMenu.Set("Index", "base_huozhushouquan", "基础数据");
            Expression<Func<base_huozhushouquan, bool>> where = PredicateExtensionses.True<base_huozhushouquan>();
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
                                        where = where.And(base_huozhushouquan => base_huozhushouquan.HuozhuID == int.Parse(huozhuid));
                                    else
                                        where = where.Or(base_huozhushouquan => base_huozhushouquan.HuozhuID == int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals(">"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(base_huozhushouquan => base_huozhushouquan.HuozhuID > int.Parse(huozhuid));
                                    else
                                        where = where.Or(base_huozhushouquan => base_huozhushouquan.HuozhuID > int.Parse(huozhuid));
                                }
                                if (huozhuidequal.Equals("<"))
                                {
                                    if (huozhuidand.Equals("and"))
                                        where = where.And(base_huozhushouquan => base_huozhushouquan.HuozhuID < int.Parse(huozhuid));
                                    else
                                        where = where.Or(base_huozhushouquan => base_huozhushouquan.HuozhuID < int.Parse(huozhuid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_huozhushouquan => base_huozhushouquan.IsDelete == false);
            var tempData = ob_base_huozhushouquanservice.LoadSortEntities(where.Compile(), false, base_huozhushouquan => base_huozhushouquan.ID).ToPagedList<base_huozhushouquan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_huozhushouquan = tempData;
            return View(tempData);
        }
        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_huozhushouquan_index";
            string page = "1";

            string huozhuid = Request["huozhuid"] ?? "";
            string huozhuidequal = Request["huozhuidequal"] ?? "";
            string huozhuidand = Request["huozhuidand"] ?? "";

            PageMenu.Set("Index", "base_huozhushouquan", "基础数据");
            Expression<Func<base_huozhushouquan, bool>> where = PredicateExtensionses.True<base_huozhushouquan>();
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
                            where = where.And(base_huozhushouquan => base_huozhushouquan.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(base_huozhushouquan => base_huozhushouquan.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(base_huozhushouquan => base_huozhushouquan.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(base_huozhushouquan => base_huozhushouquan.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(base_huozhushouquan => base_huozhushouquan.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(base_huozhushouquan => base_huozhushouquan.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);

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
                            where = where.And(base_huozhushouquan => base_huozhushouquan.HuozhuID == int.Parse(huozhuid));
                        else
                            where = where.Or(base_huozhushouquan => base_huozhushouquan.HuozhuID == int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals(">"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(base_huozhushouquan => base_huozhushouquan.HuozhuID > int.Parse(huozhuid));
                        else
                            where = where.Or(base_huozhushouquan => base_huozhushouquan.HuozhuID > int.Parse(huozhuid));
                    }
                    if (huozhuidequal.Equals("<"))
                    {
                        if (huozhuidand.Equals("and"))
                            where = where.And(base_huozhushouquan => base_huozhushouquan.HuozhuID < int.Parse(huozhuid));
                        else
                            where = where.Or(base_huozhushouquan => base_huozhushouquan.HuozhuID < int.Parse(huozhuid));
                    }
                }
                if (!string.IsNullOrEmpty(huozhuid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", huozhuid, huozhuidequal, huozhuidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "huozhuid", "", huozhuidequal, huozhuidand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_huozhushouquan => base_huozhushouquan.IsDelete == false);

            var tempData = ob_base_huozhushouquanservice.LoadSortEntities(where.Compile(), false, base_huozhushouquan => base_huozhushouquan.ID).ToPagedList<base_huozhushouquan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_huozhushouquan = tempData;
            return View(tempData);

        }

        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            return View();
        }

        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string huozhuid = Request["huozhuid"] ?? "";
            string leibie = Request["leibie"] ?? "";
            string shouquanid = Request["shouquanid"] ?? "";
            string shouquanshuyxq = Request["shouquanshuyxq"] ?? "";
            string shouquanshutp = Request["shouquanshutp"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                base_huozhushouquan ob_base_huozhushouquan = new base_huozhushouquan();
                ob_base_huozhushouquan.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                ob_base_huozhushouquan.Leibie = leibie == "" ? 0 : int.Parse(leibie);
                ob_base_huozhushouquan.ShouquanID = shouquanid == "" ? 0 : int.Parse(shouquanid);
                ob_base_huozhushouquan.ShouquanshuYXQ = shouquanshuyxq == "" ? DateTime.Now : DateTime.Parse(shouquanshuyxq);
                ob_base_huozhushouquan.ShouquanshuTP = shouquanshutp.Trim();
                ob_base_huozhushouquan.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_huozhushouquan.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_huozhushouquan = ob_base_huozhushouquanservice.AddEntity(ob_base_huozhushouquan);
                ViewBag.base_huozhushouquan = ob_base_huozhushouquan;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            base_huozhushouquan tempData = ob_base_huozhushouquanservice.GetEntityById(base_huozhushouquan => base_huozhushouquan.ID == id && base_huozhushouquan.IsDelete == false);
            ViewBag.base_huozhushouquan = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_huozhushouquanViewModel base_huozhushouquanviewmodel = new base_huozhushouquanViewModel();
                base_huozhushouquanviewmodel.ID = tempData.ID;
                base_huozhushouquanviewmodel.HuozhuID = tempData.HuozhuID;
                base_huozhushouquanviewmodel.Leibie = (int)tempData.Leibie;
                base_huozhushouquanviewmodel.ShouquanID = (int)tempData.ShouquanID;
                base_huozhushouquanviewmodel.ShouquanshuYXQ = (DateTime)tempData.ShouquanshuYXQ;
                base_huozhushouquanviewmodel.ShouquanshuTP = tempData.ShouquanshuTP;
                base_huozhushouquanviewmodel.MakeDate = tempData.MakeDate;
                base_huozhushouquanviewmodel.MakeMan = tempData.MakeMan;

                return View(base_huozhushouquanviewmodel);
            }
        }

        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string huozhuid = Request["huozhuid"] ?? "";
            string leibie = Request["leibie"] ?? "";
            string shouquanid = Request["shouquanid"] ?? "";
            string shouquanshuyxq = Request["shouquanshuyxq"] ?? "";
            string shouquanshutp = Request["shouquanshutp"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_huozhushouquan p = ob_base_huozhushouquanservice.GetEntityById(base_huozhushouquan => base_huozhushouquan.ID == uid);
                p.HuozhuID = huozhuid == "" ? 0 : int.Parse(huozhuid);
                p.Leibie = leibie == "" ? 0 : int.Parse(leibie);
                p.ShouquanID = shouquanid == "" ? 0 : int.Parse(shouquanid);
                p.ShouquanshuYXQ = shouquanshuyxq == "" ? DateTime.Now : DateTime.Parse(shouquanshuyxq);
                p.ShouquanshuTP = shouquanshutp.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_huozhushouquanservice.UpdateEntity(p);
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
            base_huozhushouquan ob_base_huozhushouquan;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_huozhushouquan = ob_base_huozhushouquanservice.GetEntityById(base_huozhushouquan => base_huozhushouquan.ID == id && base_huozhushouquan.IsDelete == false);
                    ob_base_huozhushouquan.IsDelete = true;
                    ob_base_huozhushouquanservice.UpdateEntity(ob_base_huozhushouquan);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Overdue(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            var period = Request["period"] ?? "";

            if (string.IsNullOrEmpty(period))
                period = "0";
            var tempData = ob_base_huozhushouquanservice.LoadEntities(p => p.IsDelete == false && p.ShouquanshuYXQ <= DateTime.Now.AddDays(int.Parse(period))).ToPagedList(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_huozhushouquan = tempData;
            ViewBag.period = period;
            return View(tempData);
        }
    }
}

