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
    public class ord_fahuodanController : Controller
    {
        private Iord_fahuodanService ob_ord_fahuodanservice = ServiceFactory.ord_fahuodanservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_fahuodan_index";
            Expression<Func<ord_fahuodan, bool>> where = PredicateExtensionses.True<ord_fahuodan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "chukudanbh":
                            string chukudanbh = scld[1];
                            string chukudanbhequal = scld[2];
                            string chukudanbhand = scld[3];
                            if (!string.IsNullOrEmpty(chukudanbh))
                            {
                                if (chukudanbhequal.Equals("="))
                                {
                                    if (chukudanbhand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                                }
                                if (chukudanbhequal.Equals("like"))
                                {
                                    if (chukudanbhand.Equals("and"))
                                        where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                                    else
                                        where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_fahuodan => ord_fahuodan.IsDelete == false);

            var tempData = ob_ord_fahuodanservice.LoadSortEntities(where.Compile(), false, ord_fahuodan => ord_fahuodan.ID).ToPagedList<ord_fahuodan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_fahuodan = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_fahuodan_index";
            string page = "1";
            string chukudanbh = Request["chukudanbh"] ?? "";
            string chukudanbhequal = Request["chukudanbhequal"] ?? "";
            string chukudanbhand = Request["chukudanbhand"] ?? "";
            Expression<Func<ord_fahuodan, bool>> where = PredicateExtensionses.True<ord_fahuodan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(chukudanbh))
                {
                    if (chukudanbhequal.Equals("="))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                    }
                    if (chukudanbhequal.Equals("like"))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(chukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", chukudanbh, chukudanbhequal, chukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", "", chukudanbhequal, chukudanbhand);
                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(chukudanbh))
                {
                    if (chukudanbhequal.Equals("="))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH == chukudanbh);
                    }
                    if (chukudanbhequal.Equals("like"))
                    {
                        if (chukudanbhand.Equals("and"))
                            where = where.And(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                        else
                            where = where.Or(ord_fahuodan => ord_fahuodan.ChukudanBH.Contains(chukudanbh));
                    }
                }
                if (!string.IsNullOrEmpty(chukudanbh))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", chukudanbh, chukudanbhequal, chukudanbhand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "chukudanbh", "", chukudanbhequal, chukudanbhand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_fahuodan => ord_fahuodan.IsDelete == false);

            var tempData = ob_ord_fahuodanservice.LoadSortEntities(where.Compile(), false, ord_fahuodan => ord_fahuodan.ID).ToPagedList<ord_fahuodan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_fahuodan = tempData;
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
            string chukudanbh = Request["chukudanbh"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string ddid = Request["ddid"] ?? "";
            string ddbh = Request["ddbh"] ?? "";
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string chukurq = Request["chukurq"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string ckcode = Request["ckcode"] ?? "";
            string yunsongfs = Request["yunsongfs"] ?? "";
            string kddanhao = Request["kddanhao"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                ord_fahuodan ob_ord_fahuodan = new ord_fahuodan();
                ob_ord_fahuodan.ChukudanBH = chukudanbh.Trim();
                ob_ord_fahuodan.KehuDH = kehudh.Trim();
                ob_ord_fahuodan.DDID = ddid == "" ? 0 : int.Parse(ddid);
                ob_ord_fahuodan.DDBH = ddbh.Trim();
                ob_ord_fahuodan.Yunsongdizhi = yunsongdizhi.Trim();
                ob_ord_fahuodan.ChukuRQ = chukurq == "" ? DateTime.Now : DateTime.Parse(chukurq);
                ob_ord_fahuodan.Lianxiren = lianxiren.Trim();
                ob_ord_fahuodan.LianxiDH = lianxidh.Trim();
                ob_ord_fahuodan.Beizhu = beizhu.Trim();
                ob_ord_fahuodan.CKCode = ckcode.Trim();
                ob_ord_fahuodan.YunsongFS = yunsongfs.Trim();
                ob_ord_fahuodan.Kddanhao = kddanhao.Trim();
                ob_ord_fahuodan.Col1 = col1.Trim();
                ob_ord_fahuodan.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_fahuodan.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_fahuodan = ob_ord_fahuodanservice.AddEntity(ob_ord_fahuodan);
                ViewBag.ord_fahuodan = ob_ord_fahuodan;
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
            ord_fahuodan tempData = ob_ord_fahuodanservice.GetEntityById(ord_fahuodan => ord_fahuodan.ID == id && ord_fahuodan.IsDelete == false);
            ViewBag.ord_fahuodan = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_fahuodanViewModel ord_fahuodanviewmodel = new ord_fahuodanViewModel();
                ord_fahuodanviewmodel.ID = tempData.ID;
                ord_fahuodanviewmodel.ChukudanBH = tempData.ChukudanBH;
                ord_fahuodanviewmodel.KehuDH = tempData.KehuDH;
                ord_fahuodanviewmodel.DDID = tempData.DDID;
                ord_fahuodanviewmodel.DDBH = tempData.DDBH;
                ord_fahuodanviewmodel.Yunsongdizhi = tempData.Yunsongdizhi;
                ord_fahuodanviewmodel.ChukuRQ = tempData.ChukuRQ;
                ord_fahuodanviewmodel.Lianxiren = tempData.Lianxiren;
                ord_fahuodanviewmodel.LianxiDH = tempData.LianxiDH;
                ord_fahuodanviewmodel.Beizhu = tempData.Beizhu;
                ord_fahuodanviewmodel.CKCode = tempData.CKCode;
                ord_fahuodanviewmodel.YunsongFS = tempData.YunsongFS;
                ord_fahuodanviewmodel.Kddanhao = tempData.Kddanhao;
                ord_fahuodanviewmodel.Col1 = tempData.Col1;
                ord_fahuodanviewmodel.MakeDate = tempData.MakeDate;
                ord_fahuodanviewmodel.MakeMan = tempData.MakeMan;
                return View(ord_fahuodanviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string chukudanbh = Request["chukudanbh"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string ddid = Request["ddid"] ?? "";
            string ddbh = Request["ddbh"] ?? "";
            string yunsongdizhi = Request["yunsongdizhi"] ?? "";
            string chukurq = Request["chukurq"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string ckcode = Request["ckcode"] ?? "";
            string yunsongfs = Request["yunsongfs"] ?? "";
            string kddanhao = Request["kddanhao"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                ord_fahuodan p = ob_ord_fahuodanservice.GetEntityById(ord_fahuodan => ord_fahuodan.ID == uid);
                p.ChukudanBH = chukudanbh.Trim();
                p.KehuDH = kehudh.Trim();
                p.DDID = ddid == "" ? 0 : int.Parse(ddid);
                p.DDBH = ddbh.Trim();
                p.Yunsongdizhi = yunsongdizhi.Trim();
                p.ChukuRQ = chukurq == "" ? DateTime.Now : DateTime.Parse(chukurq);
                p.Lianxiren = lianxiren.Trim();
                p.LianxiDH = lianxidh.Trim();
                p.Beizhu = beizhu.Trim();
                p.CKCode = ckcode.Trim();
                p.YunsongFS = yunsongfs.Trim();
                p.Kddanhao = kddanhao.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_fahuodanservice.UpdateEntity(p);
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
            ord_fahuodan ob_ord_fahuodan;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_fahuodan = ob_ord_fahuodanservice.GetEntityById(ord_fahuodan => ord_fahuodan.ID == id && ord_fahuodan.IsDelete == false);
                    ob_ord_fahuodan.IsDelete = true;
                    ob_ord_fahuodanservice.UpdateEntity(ob_ord_fahuodan);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

