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
    public class ord_dingdanController : Controller
    {
        private Iord_dingdanService ob_ord_dingdanservice = ServiceFactory.ord_dingdanservice;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "ord_dingdan_index";
            Expression<Func<ord_dingdan, bool>> where = PredicateExtensionses.True<ord_dingdan>();
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
                                        where = where.And(ord_dingdan => ord_dingdan.Bianhao == bianhao);
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.Bianhao == bianhao);
                                }
                                if (bianhaoequal.Equals("like"))
                                {
                                    if (bianhaoand.Equals("and"))
                                        where = where.And(ord_dingdan => ord_dingdan.Bianhao.Contains(bianhao));
                                    else
                                        where = where.Or(ord_dingdan => ord_dingdan.Bianhao.Contains(bianhao));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(ord_dingdan => ord_dingdan.IsDelete == false);

            var tempData = ob_ord_dingdanservice.LoadSortEntities(where.Compile(), false, ord_dingdan => ord_dingdan.ID).ToPagedList<ord_dingdan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_dingdan = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "ord_dingdan_index";
            string page = "1";
            string bianhao = Request["bianhao"] ?? "";
            string bianhaoequal = Request["bianhaoequal"] ?? "";
            string bianhaoand = Request["bianhaoand"] ?? "";
            Expression<Func<ord_dingdan, bool>> where = PredicateExtensionses.True<ord_dingdan>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(bianhao))
                {
                    if (bianhaoequal.Equals("="))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.Bianhao == bianhao);
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.Bianhao.Contains(bianhao));
                    }
                }
                if (!string.IsNullOrEmpty(bianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", bianhao, bianhaoequal, bianhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", "", bianhaoequal, bianhaoand);
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
                            where = where.And(ord_dingdan => ord_dingdan.Bianhao == bianhao);
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.Bianhao == bianhao);
                    }
                    if (bianhaoequal.Equals("like"))
                    {
                        if (bianhaoand.Equals("and"))
                            where = where.And(ord_dingdan => ord_dingdan.Bianhao.Contains(bianhao));
                        else
                            where = where.Or(ord_dingdan => ord_dingdan.Bianhao.Contains(bianhao));
                    }
                }
                if (!string.IsNullOrEmpty(bianhao))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", bianhao, bianhaoequal, bianhaoand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "bianhao", "", bianhaoequal, bianhaoand);
                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(ord_dingdan => ord_dingdan.IsDelete == false);

            var tempData = ob_ord_dingdanservice.LoadSortEntities(where.Compile(), false, ord_dingdan => ord_dingdan.ID).ToPagedList<ord_dingdan>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.ord_dingdan = tempData;
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
            string bianhao = Request["bianhao"] ?? "";
            string khid = Request["khid"] ?? "";
            string cpxid = Request["cpxid"] ?? "";
            string cglx = Request["cglx"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string xiadanrq = Request["xiadanrq"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string songhuodz = Request["songhuodz"] ?? "";
            string opid = Request["opid"] ?? "";
            string accid = Request["accid"] ?? "";
            string shenhetg = Request["shenhetg"] ?? "";
            string zongshucg = Request["zongshucg"] ?? "";
            string zongjine = Request["zongjine"] ?? "";
            string jieshusf = Request["jieshusf"] ?? "";
            string zhuangtai = Request["zhuangtai"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                ord_dingdan ob_ord_dingdan = new ord_dingdan();
                ob_ord_dingdan.Bianhao = bianhao.Trim();
                ob_ord_dingdan.KHID = khid == "" ? 0 : int.Parse(khid);
                ob_ord_dingdan.CPXID = cpxid == "" ? 0 : int.Parse(cpxid);
                ob_ord_dingdan.CGLX = cglx == "" ? 0 : int.Parse(cglx);
                ob_ord_dingdan.KehuDH = kehudh.Trim();
                ob_ord_dingdan.XiadanRQ = xiadanrq == "" ? DateTime.Now : DateTime.Parse(xiadanrq);
                ob_ord_dingdan.Lianxiren = lianxiren.Trim();
                ob_ord_dingdan.LianxiDH = lianxidh.Trim();
                ob_ord_dingdan.SonghuoDZ = songhuodz.Trim();
                ob_ord_dingdan.OPID = opid == "" ? 0 : int.Parse(opid);
                ob_ord_dingdan.ACCID = accid == "" ? 0 : int.Parse(accid);
                ob_ord_dingdan.ShenheTG = shenhetg == "" ? false : Boolean.Parse(shenhetg);
                ob_ord_dingdan.ZongshuCG = zongshucg == "" ? 0 : float.Parse(zongshucg);
                ob_ord_dingdan.Zongjine = zongjine == "" ? 0 : float.Parse(zongjine);
                ob_ord_dingdan.JieshuSF = jieshusf == "" ? false : Boolean.Parse(jieshusf);
                ob_ord_dingdan.Zhuangtai = zhuangtai == "" ? 0 : int.Parse(zhuangtai);
                ob_ord_dingdan.Beizhu = beizhu.Trim();
                ob_ord_dingdan.Col1 = col1.Trim();
                ob_ord_dingdan.Col2 = col2.Trim();
                ob_ord_dingdan.Col3 = col3.Trim();
                ob_ord_dingdan.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_ord_dingdan.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_dingdan = ob_ord_dingdanservice.AddEntity(ob_ord_dingdan);
                ViewBag.ord_dingdan = ob_ord_dingdan;
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
            ord_dingdan tempData = ob_ord_dingdanservice.GetEntityById(ord_dingdan => ord_dingdan.ID == id && ord_dingdan.IsDelete == false);
            ViewBag.ord_dingdan = tempData;
            if (tempData == null)
                return View();
            else
            {
                ord_dingdanViewModel ord_dingdanviewmodel = new ord_dingdanViewModel();
                ord_dingdanviewmodel.ID = tempData.ID;
                ord_dingdanviewmodel.Bianhao = tempData.Bianhao;
                ord_dingdanviewmodel.KHID = tempData.KHID;
                ord_dingdanviewmodel.CPXID = tempData.CPXID;
                ord_dingdanviewmodel.CGLX = tempData.CGLX;
                ord_dingdanviewmodel.KehuDH = tempData.KehuDH;
                ord_dingdanviewmodel.XiadanRQ = tempData.XiadanRQ;
                ord_dingdanviewmodel.Lianxiren = tempData.Lianxiren;
                ord_dingdanviewmodel.LianxiDH = tempData.LianxiDH;
                ord_dingdanviewmodel.SonghuoDZ = tempData.SonghuoDZ;
                ord_dingdanviewmodel.OPID = tempData.OPID;
                ord_dingdanviewmodel.ACCID = tempData.ACCID;
                ord_dingdanviewmodel.ShenheTG = tempData.ShenheTG;
                ord_dingdanviewmodel.ZongshuCG = tempData.ZongshuCG;
                ord_dingdanviewmodel.Zongjine = tempData.Zongjine;
                ord_dingdanviewmodel.JieshuSF = tempData.JieshuSF;
                ord_dingdanviewmodel.Zhuangtai = tempData.Zhuangtai;
                ord_dingdanviewmodel.Beizhu = tempData.Beizhu;
                ord_dingdanviewmodel.Col1 = tempData.Col1;
                ord_dingdanviewmodel.Col2 = tempData.Col2;
                ord_dingdanviewmodel.Col3 = tempData.Col3;
                ord_dingdanviewmodel.MakeDate = tempData.MakeDate;
                ord_dingdanviewmodel.MakeMan = tempData.MakeMan;
                return View(ord_dingdanviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string bianhao = Request["bianhao"] ?? "";
            string khid = Request["khid"] ?? "";
            string cpxid = Request["cpxid"] ?? "";
            string cglx = Request["cglx"] ?? "";
            string kehudh = Request["kehudh"] ?? "";
            string xiadanrq = Request["xiadanrq"] ?? "";
            string lianxiren = Request["lianxiren"] ?? "";
            string lianxidh = Request["lianxidh"] ?? "";
            string songhuodz = Request["songhuodz"] ?? "";
            string opid = Request["opid"] ?? "";
            string accid = Request["accid"] ?? "";
            string shenhetg = Request["shenhetg"] ?? "";
            string zongshucg = Request["zongshucg"] ?? "";
            string zongjine = Request["zongjine"] ?? "";
            string jieshusf = Request["jieshusf"] ?? "";
            string zhuangtai = Request["zhuangtai"] ?? "";
            string beizhu = Request["beizhu"] ?? "";
            string col1 = Request["col1"] ?? "";
            string col2 = Request["col2"] ?? "";
            string col3 = Request["col3"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                ord_dingdan p = ob_ord_dingdanservice.GetEntityById(ord_dingdan => ord_dingdan.ID == uid);
                p.Bianhao = bianhao.Trim();
                p.KHID = khid == "" ? 0 : int.Parse(khid);
                p.CPXID = cpxid == "" ? 0 : int.Parse(cpxid);
                p.CGLX = cglx == "" ? 0 : int.Parse(cglx);
                p.KehuDH = kehudh.Trim();
                p.XiadanRQ = xiadanrq == "" ? DateTime.Now : DateTime.Parse(xiadanrq);
                p.Lianxiren = lianxiren.Trim();
                p.LianxiDH = lianxidh.Trim();
                p.SonghuoDZ = songhuodz.Trim();
                p.OPID = opid == "" ? 0 : int.Parse(opid);
                p.ACCID = accid == "" ? 0 : int.Parse(accid);
                p.ShenheTG = shenhetg == "" ? false : Boolean.Parse(shenhetg);
                p.ZongshuCG = zongshucg == "" ? 0 : float.Parse(zongshucg);
                p.Zongjine = zongjine == "" ? 0 : float.Parse(zongjine);
                p.JieshuSF = jieshusf == "" ? false : Boolean.Parse(jieshusf);
                p.Zhuangtai = zhuangtai == "" ? 0 : int.Parse(zhuangtai);
                p.Beizhu = beizhu.Trim();
                p.Col1 = col1.Trim();
                p.Col2 = col2.Trim();
                p.Col3 = col3.Trim();
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_ord_dingdanservice.UpdateEntity(p);
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
            ord_dingdan ob_ord_dingdan;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_ord_dingdan = ob_ord_dingdanservice.GetEntityById(ord_dingdan => ord_dingdan.ID == id && ord_dingdan.IsDelete == false);
                    ob_ord_dingdan.IsDelete = true;
                    ob_ord_dingdanservice.UpdateEntity(ob_ord_dingdan);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

