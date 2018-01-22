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
using System.Data;

namespace GROBS.Controllers
{
    public class ord_fanliczController : Controller
    {
        private Iord_fanliczService ob_ord_fanliczservice = ServiceFactory.ord_fanliczservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_fanlicz_index";
            Expression<Func<ord_commisioncharge_v, bool>> where = PredicateExtensionses.True<ord_commisioncharge_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null && sc.ConditionInfo != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "khid":
                            string khid = scld[1];
                            string khidequal = scld[2];
                            string khidand = scld[3];
                            if (!string.IsNullOrEmpty(khid))
                            {
                                if (khidequal.Equals("="))
                                {
                                    if (khidand.Equals("and"))
                                        where = where.And(ord_fanlicz => ord_fanlicz.KHID == int.Parse(khid));
                                    else
                                        where = where.Or(ord_fanlicz => ord_fanlicz.KHID == int.Parse(khid));
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
                                        where = where.And(ord_fanlicz => ord_fanlicz.Mingcheng == mingcheng);
                                    else
                                        where = where.Or(ord_fanlicz => ord_fanlicz.Mingcheng == mingcheng);
                                }
                                if (mingchengequal.Equals("like"))
                                {
                                    if (mingchengand.Equals("and"))
                                        where = where.And(ord_fanlicz => ord_fanlicz.Mingcheng.Contains(mingcheng));
                                    else
                                        where = where.Or(ord_fanlicz => ord_fanlicz.Mingcheng.Contains(mingcheng));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            //where = where.And(ord_fanlicz => ord_fanlicz.IsDelete == false);

            var tempData = ob_ord_fanliczservice.LoadCommisionCharge(where.Compile()).ToPagedList<ord_commisioncharge_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_fanlicz = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_fanlicz_index";
            string page = "1";
            string khid = Request["khid"] ?? "";
            string khidequal = Request["khidequal"] ?? "";
            string khidand = Request["khidand"] ?? "";
            //mingcheng
            string mingcheng = Request["mingcheng"] ?? "";
            string mingchengequal = Request["mingchengequal"] ?? "";
            string mingchengand = Request["mingchengand"] ?? "";
            Expression<Func<ord_commisioncharge_v, bool>> where = PredicateExtensionses.True<ord_commisioncharge_v>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(khid))
                {
                    if (khidequal.Equals("="))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_fanlicz => ord_fanlicz.KHID == int.Parse(khid));
                        else
                            where = where.Or(ord_fanlicz => ord_fanlicz.KHID == int.Parse(khid));
                    }
                }
                if (!string.IsNullOrEmpty(khid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", khid, khidequal, khidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", "", khidequal, khidand);

                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(ord_fanlicz => ord_fanlicz.Mingcheng == mingcheng);
                        else
                            where = where.Or(ord_fanlicz => ord_fanlicz.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(ord_fanlicz => ord_fanlicz.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(ord_fanlicz => ord_fanlicz.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(khid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", khid, khidequal, khidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", "", khidequal, khidand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(khid))
                {
                    if (khidequal.Equals("="))
                    {
                        if (khidand.Equals("and"))
                            where = where.And(ord_fanlicz => ord_fanlicz.KHID == int.Parse(khid));
                        else
                            where = where.Or(ord_fanlicz => ord_fanlicz.KHID == int.Parse(khid));
                    }
                    if (!string.IsNullOrEmpty(khid))
                        sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", khid, khidequal, khidand);
                    else
                        sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", "", khidequal, khidand);
                }
                if (!string.IsNullOrEmpty(khid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", khid, khidequal, khidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "khid", "", khidequal, khidand);


                //mingcheng
                if (!string.IsNullOrEmpty(mingcheng))
                {
                    if (mingchengequal.Equals("="))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(ord_fanlicz => ord_fanlicz.Mingcheng == mingcheng);
                        else
                            where = where.Or(ord_fanlicz => ord_fanlicz.Mingcheng == mingcheng);
                    }
                    if (mingchengequal.Equals("like"))
                    {
                        if (mingchengand.Equals("and"))
                            where = where.And(ord_fanlicz => ord_fanlicz.Mingcheng.Contains(mingcheng));
                        else
                            where = where.Or(ord_fanlicz => ord_fanlicz.Mingcheng.Contains(mingcheng));
                    }
                }
                if (!string.IsNullOrEmpty(mingcheng))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", mingcheng, mingchengequal, mingchengand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "mingcheng", "", mingchengequal, mingchengand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            //where = where.And(ord_fanlicz => ord_fanlicz.IsDelete == false);

            var tempData = ob_ord_fanliczservice.LoadCommisionCharge(where.Compile()).ToPagedList<ord_commisioncharge_v>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_fanlicz = tempData;
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
            string khid = Request["khid"] ?? "";
            string czje = Request["czje"] ?? "";
            string ffyf = Request["ffyf"] ?? "";
            string kysf = Request["kysf"] ?? "";
            string bz = Request["bz"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                ord_fanlicz ob_ord_fanlicz = new ord_fanlicz();
                ob_ord_fanlicz.KHID = khid == "" ? 0 : int.Parse(khid);
                ob_ord_fanlicz.CZJE = czje == "" ? 0 : decimal.Parse(czje);
                ob_ord_fanlicz.FFYF = ffyf.Trim();
                ob_ord_fanlicz.KYSF = kysf == "" ? false : Boolean.Parse(kysf);
                ob_ord_fanlicz.BZ = bz.Trim();
                ob_ord_fanlicz.Col1 = col1.Trim();
                ob_ord_fanlicz.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_fanlicz.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_fanlicz = ob_ord_fanliczservice.AddEntity(ob_ord_fanlicz);
                ViewBag.ord_fanlicz = ob_ord_fanlicz;
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
            ord_fanlicz tempData = ob_ord_fanliczservice.GetEntityById(ord_fanlicz => ord_fanlicz.ID == id && ord_fanlicz.IsDelete == false);
            ViewBag.ord_fanlicz = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_fanliczViewModel ord_fanliczviewmodel = new ord_fanliczViewModel();
                ord_fanliczviewmodel.ID = tempData.ID;
                ord_fanliczviewmodel.KHID = tempData.KHID;
                ord_fanliczviewmodel.CZJE = tempData.CZJE;
                ord_fanliczviewmodel.FFYF = tempData.FFYF;
                ord_fanliczviewmodel.KYSF = tempData.KYSF;
                ord_fanliczviewmodel.BZ = tempData.BZ;
                ord_fanliczviewmodel.Col1 = tempData.Col1;
                ord_fanliczviewmodel.MakeDate = tempData.MakeDate;
                ord_fanliczviewmodel.MakeMan = tempData.MakeMan;
                return View(ord_fanliczviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string khid = Request["khid"] ?? "";
            string czje = Request["czje"] ?? "";
            string ffyf = Request["ffyf"] ?? "";
            string kysf = Request["kysf"] ?? "";
            string bz = Request["bz"] ?? "";
            string col1 = Request["col1"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                ord_fanlicz p = ob_ord_fanliczservice.GetEntityById(ord_fanlicz => ord_fanlicz.ID == uid);
                p.KHID = khid == "" ? 0 : int.Parse(khid);
                p.CZJE = czje == "" ? 0 : decimal.Parse(czje);
                p.FFYF = ffyf.Trim();
                p.KYSF = kysf == "" ? false : Boolean.Parse(kysf);
                p.BZ = bz.Trim();
                p.Col1 = col1.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_fanliczservice.UpdateEntity(p);
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
            ord_fanlicz ob_ord_fanlicz;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_fanlicz = ob_ord_fanliczservice.GetEntityById(ord_fanlicz => ord_fanlicz.ID == id && ord_fanlicz.IsDelete == false);
                    ob_ord_fanlicz.IsDelete = true;
                    ob_ord_fanliczservice.UpdateEntity(ob_ord_fanlicz);
                }
            }
            return RedirectToAction("Index");
        }

        public JsonResult getFanliChongZhiWithQuery()
        {
            string khid = Request["khid"] ?? "";
            string req_ffyf = Request["req_ffyf"] ?? "";
            string req_kysf = Request["req_kysf"] ?? "";
            string req_fl_date_s = Request["req_fl_date_s"] ?? "";
            string req_fl_date_e = Request["req_fl_date_e"] ?? "";
            string sortOrder = Request["sortOrder"] ?? "";

            Expression<Func<ord_fanlicz, bool>> where = PredicateExtensionses.True<ord_fanlicz>();
            if (!string.IsNullOrEmpty(khid))
                where = where.And(p => p.KHID == int.Parse(khid));
            if (!string.IsNullOrEmpty(req_ffyf))
                where = where.And(p => p.FFYF == req_ffyf);
            if (!string.IsNullOrEmpty(req_kysf))
                where = where.And(p => p.KYSF == Boolean.Parse(req_kysf));
            if (!string.IsNullOrEmpty(req_fl_date_s))
                where = where.And(p => p.MakeDate >= DateTime.Parse(req_fl_date_s));
            if (!string.IsNullOrEmpty(req_fl_date_e))
                where = where.And(p => p.MakeDate <= DateTime.Parse(req_fl_date_e).AddDays(1));

            where = where.And(p => p.IsDelete == false);

            var tempData = ServiceFactory.ord_fanliczservice.LoadSortEntities(where.Compile(), true, p => p.KHID).ToList<ord_fanlicz>();
            #region ÅÅÐò

            @ViewBag.CZJEParm = sortOrder == "CZJE" ? "CZJE_desc" : "CZJE";
            @ViewBag.FFYFParm = sortOrder == "FFYF" ? "FFYF_desc" : "FFYF";
            @ViewBag.KYSFParm = sortOrder == "KYSF" ? "KYSF_desc" : "KYSF";
            @ViewBag.flcz_MakeDateParm = sortOrder == "flcz_MakeDate" ? "flcz_MakeDate_desc" : "flcz_MakeDate";

            switch (sortOrder)
            {
                case "flcz_MakeDate_desc":
                    tempData = tempData.OrderByDescending(p => p.MakeDate).ToList<ord_fanlicz>();
                    break;
                case "flcz_MakeDate":
                    tempData = tempData.OrderBy(p => p.MakeDate).ToList<ord_fanlicz>();
                    break;
                case "KYSF_desc":
                    tempData = tempData.OrderByDescending(p => p.KYSF).ToList<ord_fanlicz>();
                    break;
                case "KYSF":
                    tempData = tempData.OrderBy(p => p.KYSF).ToList<ord_fanlicz>();
                    break;
                case "FFYF_desc":
                    tempData = tempData.OrderByDescending(p => p.FFYF).ToList<ord_fanlicz>();
                    break;
                case "FFYF":
                    tempData = tempData.OrderBy(p => p.FFYF).ToList<ord_fanlicz>();
                    break;
                case "CZJE":
                    tempData = tempData.OrderBy(p => p.CZJE).ToList<ord_fanlicz>();
                    break;
                default:
                    tempData = tempData.OrderByDescending(p => p.CZJE).ToList<ord_fanlicz>();
                    break;
            }

            #endregion

            if (tempData == null)
                return Json(-1);
            return Json(tempData);
        }

        public ActionResult exportFanliChongZhiWith()
        {
            string khid = Request["khid"] ?? "";
            string req_ffyf = Request["req_ffyf"] ?? "";
            string req_kysf = Request["req_kysf"] ?? "";
            string req_fl_date_s = Request["req_fl_date_s"] ?? "";
            string req_fl_date_e = Request["req_fl_date_e"] ?? "";

            Expression<Func<ord_fanlicz, bool>> where = PredicateExtensionses.True<ord_fanlicz>();
            if (!string.IsNullOrEmpty(khid))
                where = where.And(p => p.KHID == int.Parse(khid));
            if (!string.IsNullOrEmpty(req_ffyf))
                where = where.And(p => p.FFYF == req_ffyf);
            if (!string.IsNullOrEmpty(req_kysf))
                where = where.And(p => p.KYSF == Boolean.Parse(req_kysf));
            if (!string.IsNullOrEmpty(req_fl_date_s))
                where = where.And(p => p.MakeDate >= DateTime.Parse(req_fl_date_s));
            if (!string.IsNullOrEmpty(req_fl_date_e))
                where = where.And(p => p.MakeDate <= DateTime.Parse(req_fl_date_e).AddDays(1));
            where = where.And(p => p.IsDelete == false);

            var tempData = ServiceFactory.ord_fanliczservice.LoadSortEntities(where.Compile(), true, p => p.KHID).ToList<ord_fanlicz>();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(Int32));
            dt.Columns.Add("CZJE", typeof(string));
            dt.Columns.Add("FFYF", typeof(string));
            dt.Columns.Add("KYSF", typeof(string));
            dt.Columns.Add("MakeDate", typeof(string));
            var _xh = 0;
            foreach (var item in tempData)
            {
                _xh++;
                DataRow row = dt.NewRow();
                row["ID"] = _xh;
                row["CZJE"] = item.CZJE;
                row["FFYF"] = item.FFYF;
                row["KYSF"] = item.KYSF;
                row["MakeDate"] = item.MakeDate == null ? "" : item.MakeDate.ToString("yyyy-MM-dd");
                dt.Rows.Add(row);
            }
            DataSet ds = new DataSet();
            dt.TableName = "FanliChongZhi";
            ds.Tables.Add(dt);
            ExcelHelper.ExportExcel(ds, "FanliChongZhi");
            return new EmptyResult();
        }

    }
}

