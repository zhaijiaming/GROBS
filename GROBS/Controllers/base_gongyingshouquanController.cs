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
    public class base_gongyingshouquanController : Controller
    {
        private Ibase_gongyingshouquanService ob_base_gongyingshouquanservice = ServiceFactory.base_gongyingshouquanservice;
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_gongyingshouquan_index";
            Expression<Func<base_gongyingshouquan, bool>> where = PredicateExtensionses.True<base_gongyingshouquan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "gongyingshangid":
                            string gongyingshangid = scld[1];
                            string gongyingshangidequal = scld[2];
                            string gongyingshangidand = scld[3];
                            if (!string.IsNullOrEmpty(gongyingshangid))
                            {
                                if (gongyingshangidequal.Equals("="))
                                {
                                    if (gongyingshangidand.Equals("and"))
                                        where = where.And(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID == int.Parse(gongyingshangid));
                                    else
                                        where = where.Or(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID == int.Parse(gongyingshangid));
                                }
                                if (gongyingshangidequal.Equals(">"))
                                {
                                    if (gongyingshangidand.Equals("and"))
                                        where = where.And(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID > int.Parse(gongyingshangid));
                                    else
                                        where = where.Or(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID > int.Parse(gongyingshangid));
                                }
                                if (gongyingshangidequal.Equals("<"))
                                {
                                    if (gongyingshangidand.Equals("and"))
                                        where = where.And(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID < int.Parse(gongyingshangid));
                                    else
                                        where = where.Or(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID < int.Parse(gongyingshangid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_gongyingshouquan => base_gongyingshouquan.IsDelete == false);
            var tempData = ob_base_gongyingshouquanservice.LoadSortEntities(where.Compile(), false, base_gongyingshouquan => base_gongyingshouquan.ID).ToPagedList<base_gongyingshouquan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_gongyingshouquan = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_gongyingshouquan_index";
            string page = "1";

            string gongyingshangid = Request["gongyingshangid"] ?? "";
            string gongyingshangidequal = Request["gongyingshangidequal"] ?? "";
            string gongyingshangidand = Request["gongyingshangidand"] ?? "";

            Expression<Func<base_gongyingshouquan, bool>> where = PredicateExtensionses.True<base_gongyingshouquan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //gongyingshangid
                if (!string.IsNullOrEmpty(gongyingshangid))
                {
                    if (gongyingshangidequal.Equals("="))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID == int.Parse(gongyingshangid));
                        else
                            where = where.Or(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID == int.Parse(gongyingshangid));
                    }
                    if (gongyingshangidequal.Equals(">"))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID > int.Parse(gongyingshangid));
                        else
                            where = where.Or(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID > int.Parse(gongyingshangid));
                    }
                    if (gongyingshangidequal.Equals("<"))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID < int.Parse(gongyingshangid));
                        else
                            where = where.Or(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID < int.Parse(gongyingshangid));
                    }
                }
                if (!string.IsNullOrEmpty(gongyingshangid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingshangid", gongyingshangid, gongyingshangidequal, gongyingshangidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingshangid", "", gongyingshangidequal, gongyingshangidand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                //gongyingshangid
                if (!string.IsNullOrEmpty(gongyingshangid))
                {
                    if (gongyingshangidequal.Equals("="))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID == int.Parse(gongyingshangid));
                        else
                            where = where.Or(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID == int.Parse(gongyingshangid));
                    }
                    if (gongyingshangidequal.Equals(">"))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID > int.Parse(gongyingshangid));
                        else
                            where = where.Or(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID > int.Parse(gongyingshangid));
                    }
                    if (gongyingshangidequal.Equals("<"))
                    {
                        if (gongyingshangidand.Equals("and"))
                            where = where.And(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID < int.Parse(gongyingshangid));
                        else
                            where = where.Or(base_gongyingshouquan => base_gongyingshouquan.GongyingshangID < int.Parse(gongyingshangid));
                    }
                }
                if (!string.IsNullOrEmpty(gongyingshangid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingshangid", gongyingshangid, gongyingshangidequal, gongyingshangidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "gongyingshangid", "", gongyingshangidequal, gongyingshangidand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_gongyingshouquan => base_gongyingshouquan.IsDelete == false);

            var tempData = ob_base_gongyingshouquanservice.LoadSortEntities(where.Compile(), false, base_gongyingshouquan => base_gongyingshouquan.ID).ToPagedList<base_gongyingshouquan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_gongyingshouquan = tempData;
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
            string gongyingshangid = Request["gongyingshangid"] ?? "";
            string shouquanid = Request["shouquanid"] ?? "";
            string shouquanmingcheng = Request["shouquanmingcheng"] ?? "";
            string shouquanshuyxq = Request["shouquanshuyxq"] ?? "";
            string shouquanshutp = Request["shouquanshutp"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                base_gongyingshouquan ob_base_gongyingshouquan = new base_gongyingshouquan();
                ob_base_gongyingshouquan.GongyingshangID = gongyingshangid == "" ? 0 : int.Parse(gongyingshangid);
                ob_base_gongyingshouquan.ShouquanID = shouquanid == "" ? 0 : int.Parse(shouquanid);
                ob_base_gongyingshouquan.Shouquanmingcheng = shouquanmingcheng.Trim();
                ob_base_gongyingshouquan.ShouquanshuYXQ = shouquanshuyxq == "" ? DateTime.Now : DateTime.Parse(shouquanshuyxq);
                ob_base_gongyingshouquan.ShouquanshuTP = shouquanshutp.Trim();
                ob_base_gongyingshouquan.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_gongyingshouquan.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_gongyingshouquan = ob_base_gongyingshouquanservice.AddEntity(ob_base_gongyingshouquan);
                ViewBag.base_gongyingshouquan = ob_base_gongyingshouquan;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            base_gongyingshouquan tempData = ob_base_gongyingshouquanservice.GetEntityById(base_gongyingshouquan => base_gongyingshouquan.ID == id && base_gongyingshouquan.IsDelete == false);
            ViewBag.base_gongyingshouquan = tempData;
            return View();
        }

        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string gongyingshangid = Request["gongyingshangid"] ?? "";
            string shouquanid = Request["shouquanid"] ?? "";
            string shouquanmingcheng = Request["shouquanmingcheng"] ?? "";
            string shouquanshuyxq = Request["shouquanshuyxq"] ?? "";
            string shouquanshutp = Request["shouquanshutp"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_gongyingshouquan p = ob_base_gongyingshouquanservice.GetEntityById(base_gongyingshouquan => base_gongyingshouquan.ID == uid);
                p.GongyingshangID = gongyingshangid == "" ? 0 : int.Parse(gongyingshangid);
                p.ShouquanID = shouquanid == "" ? 0 : int.Parse(shouquanid);
                p.Shouquanmingcheng = shouquanmingcheng.Trim();
                p.ShouquanshuYXQ = shouquanshuyxq == "" ? DateTime.Now : DateTime.Parse(shouquanshuyxq);
                p.ShouquanshuTP = shouquanshutp.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_gongyingshouquanservice.UpdateEntity(p);
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
            base_gongyingshouquan ob_base_gongyingshouquan;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_gongyingshouquan = ob_base_gongyingshouquanservice.GetEntityById(base_gongyingshouquan => base_gongyingshouquan.ID == id && base_gongyingshouquan.IsDelete == false);
                    ob_base_gongyingshouquan.IsDelete = true;
                    ob_base_gongyingshouquanservice.UpdateEntity(ob_base_gongyingshouquan);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

