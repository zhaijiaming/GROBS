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
    public class base_xiaoshouController : Controller
    {
        private Ibase_xiaoshouService ob_base_xiaoshouservice = ServiceFactory.base_xiaoshouservice;
        //private List<SearchConditionModel> _searchconditions;
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "base_xiaoshou_index";
            PageMenu.Set("Index", "base_xiaoshou", "基础数据");
            Expression<Func<base_xiaoshou, bool>> where = PredicateExtensionses.True<base_xiaoshou>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "shouquanid":
                            string shouquanid = scld[1];
                            string shouquanidequal = scld[2];
                            string shouquanidand = scld[3];
                            if (!string.IsNullOrEmpty(shouquanid))
                            {
                                if (shouquanidequal.Equals("="))
                                {
                                    if (shouquanidand.Equals("and"))
                                        where = where.And(base_xiaoshou => base_xiaoshou.ShouquanID == int.Parse(shouquanid));
                                    else
                                        where = where.Or(base_xiaoshou => base_xiaoshou.ShouquanID == int.Parse(shouquanid));
                                }
                            }
                            break;
                        case "xingming":
                            string xingming = scld[1];
                            string xingmingequal = scld[2];
                            string xingmingand = scld[3];
                            if (!string.IsNullOrEmpty(xingming))
                            {
                                if (xingmingequal.Equals("="))
                                {
                                    if (xingmingand.Equals("and"))
                                        where = where.And(base_xiaoshou => base_xiaoshou.Xingming == xingming);
                                    else
                                        where = where.Or(base_xiaoshou => base_xiaoshou.Xingming == xingming);
                                }
                                if (xingmingequal.Equals("like"))
                                {
                                    if (xingmingand.Equals("and"))
                                        where = where.And(base_xiaoshou => base_xiaoshou.Xingming.Contains(xingming));
                                    else
                                        where = where.Or(base_xiaoshou => base_xiaoshou.Xingming.Contains(xingming));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(base_xiaoshou => base_xiaoshou.IsDelete == false);

            var tempData = ob_base_xiaoshouservice.LoadSortEntities(where.Compile(), false, base_xiaoshou => base_xiaoshou.ID).ToPagedList<base_xiaoshou>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_xiaoshou = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "base_xiaoshou_index";
            string page = "1";
            string shouquanid = Request["shouquanid"] ?? "";
            string shouquanidequal = Request["shouquanidequal"] ?? "";
            string shouquanidand = Request["shouquanidand"] ?? "";
            string xingming = Request["xingming"] ?? "";
            string xingmingequal = Request["xingmingequal"] ?? "";
            string xingmingand = Request["xingmingand"] ?? "";
            PageMenu.Set("Index", "base_xiaoshou", "基础数据");
            Expression<Func<base_xiaoshou, bool>> where = PredicateExtensionses.True<base_xiaoshou>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                //if (!string.IsNullOrEmpty(leibie))
                //{
                //    if (leibieequal.Equals("="))
                //    {
                //        if (leibieand.Equals("and"))
                //            where = where.And(base_xiaoshou => base_xiaoshou.Leibie == int.Parse(leibie));
                //        else
                //            where = where.Or(base_xiaoshou => base_xiaoshou.Leibie == int.Parse(leibie));
                //    }
                //    if (leibieequal.Equals(">"))
                //    {
                //        if (leibieand.Equals("and"))
                //            where = where.And(base_xiaoshou => base_xiaoshou.Leibie > int.Parse(leibie));
                //        else
                //            where = where.Or(base_xiaoshou => base_xiaoshou.Leibie > int.Parse(leibie));
                //    }
                //    if (leibieequal.Equals("<"))
                //    {
                //        if (leibieand.Equals("and"))
                //            where = where.And(base_xiaoshou => base_xiaoshou.Leibie < int.Parse(leibie));
                //        else
                //            where = where.Or(base_xiaoshou => base_xiaoshou.Leibie < int.Parse(leibie));
                //    }
                //}
                //if (!string.IsNullOrEmpty(leibie))
                //    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leibie", leibie, leibieequal, leibieand);
                //else
                //    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "leibie", "", leibieequal, leibieand);
                if (!string.IsNullOrEmpty(shouquanid))
                {
                    if (shouquanidequal.Equals("="))
                    {
                        if (shouquanidand.Equals("and"))
                            where = where.And(base_xiaoshou => base_xiaoshou.ShouquanID == int.Parse(shouquanid));
                        else
                            where = where.Or(base_xiaoshou => base_xiaoshou.ShouquanID == int.Parse(shouquanid));
                    }
                }
                if (!string.IsNullOrEmpty(shouquanid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouquanid", shouquanid, shouquanidequal, shouquanidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouquanid", "", shouquanidequal, shouquanidand);

                if (!string.IsNullOrEmpty(xingming))
                {
                    if (xingmingequal.Equals("="))
                    {
                        if (xingmingand.Equals("and"))
                            where = where.And(base_xiaoshou => base_xiaoshou.Xingming == xingming);
                        else
                            where = where.Or(base_xiaoshou => base_xiaoshou.Xingming == xingming);
                    }
                    if (xingmingequal.Equals("like"))
                    {
                        if (xingmingand.Equals("and"))
                            where = where.And(base_xiaoshou => base_xiaoshou.Xingming.Contains(xingming));
                        else
                            where = where.Or(base_xiaoshou => base_xiaoshou.Xingming.Contains(xingming));
                    }
                }
                if (!string.IsNullOrEmpty(xingming))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xingming", xingming, xingmingequal, xingmingand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xingming", "", xingmingequal, xingmingand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(shouquanid))
                {
                    if (shouquanidequal.Equals("="))
                    {
                        if (shouquanidand.Equals("and"))
                            where = where.And(base_xiaoshou => base_xiaoshou.ShouquanID == int.Parse(shouquanid));
                        else
                            where = where.Or(base_xiaoshou => base_xiaoshou.ShouquanID == int.Parse(shouquanid));
                    }
                }
                if (!string.IsNullOrEmpty(shouquanid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouquanid", shouquanid, shouquanidequal, shouquanidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "shouquanid", shouquanid, shouquanidequal, shouquanidand);


                if (!string.IsNullOrEmpty(xingming))
                {
                    if (xingmingequal.Equals("="))
                    {
                        if (xingmingand.Equals("and"))
                            where = where.And(base_xiaoshou => base_xiaoshou.Xingming == xingming);
                        else
                            where = where.Or(base_xiaoshou => base_xiaoshou.Xingming == xingming);
                    }
                    if (xingmingequal.Equals("like"))
                    {
                        if (xingmingand.Equals("and"))
                            where = where.And(base_xiaoshou => base_xiaoshou.Xingming.Contains(xingming));
                        else
                            where = where.Or(base_xiaoshou => base_xiaoshou.Xingming.Contains(xingming));
                    }
                }
                if (!string.IsNullOrEmpty(xingming))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xingming", xingming, xingmingequal, xingmingand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "xingming", "", xingmingequal, xingmingand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(base_xiaoshou => base_xiaoshou.IsDelete == false);

            var tempData = ob_base_xiaoshouservice.LoadSortEntities(where.Compile(), false, base_xiaoshou => base_xiaoshou.ID).ToPagedList<base_xiaoshou>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_xiaoshou = tempData;
            return View(tempData);
        }

        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            return View();
        }
        public JsonResult GetSalesman()
        {
            string _hzid = Request["huozhu"] ?? "0";
            if (_hzid == "0")
                return Json(-1);
            var tempdata = ob_base_xiaoshouservice.LoadSortEntities(p => p.IsDelete == false && p.ShouquanID == int.Parse(_hzid), true, s => s.Xingming);
            if (tempdata == null)
                return Json(-1);
            return Json(tempdata.ToList<base_xiaoshou>());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string leibie = Request["leibie"] ?? "";
            string shouquanid = Request["shouquanid"] ?? "";
            string shouquanmingcheng = Request["shouquanmingcheng"] ?? "";
            string xingming = Request["xingming"] ?? "";
            string dianhua = Request["dianhua"] ?? "";
            string shenfenzhengbh = Request["shenfenzhengbh"] ?? "";
            string shenfenzheng0tp = Request["shenfenzheng0tp"] ?? "";
            string shenfenzheng1tp = Request["shenfenzheng1tp"] ?? "";
            string shouquanshuyxq = Request["shouquanshuyxq"] ?? "";
            string shouquanshutp = Request["shouquanshutp"] ?? "";
            string shouying = Request["shouying"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                base_xiaoshou ob_base_xiaoshou = new base_xiaoshou();
                ob_base_xiaoshou.Leibie = leibie == "" ? 0 : int.Parse(leibie);
                ob_base_xiaoshou.ShouquanID = shouquanid == "" ? 0 : int.Parse(shouquanid);
                ob_base_xiaoshou.Shouquanmingcheng = shouquanmingcheng.Trim();
                ob_base_xiaoshou.Xingming = xingming.Trim();
                ob_base_xiaoshou.Dianhua = dianhua.Trim();
                ob_base_xiaoshou.ShenfenzhengBH = shenfenzhengbh.Trim();
                ob_base_xiaoshou.Shenfenzheng0TP = shenfenzheng0tp.Trim();
                ob_base_xiaoshou.Shenfenzheng1TP = shenfenzheng1tp.Trim();
                ob_base_xiaoshou.ShouquanshuYXQ = shouquanshuyxq == "" ? DateTime.Now : DateTime.Parse(shouquanshuyxq);
                ob_base_xiaoshou.ShouquanshuTP = shouquanshutp.Trim();
                ob_base_xiaoshou.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                ob_base_xiaoshou.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_xiaoshou.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_xiaoshou = ob_base_xiaoshouservice.AddEntity(ob_base_xiaoshou);
                ViewBag.base_xiaoshou = ob_base_xiaoshou;
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
            base_xiaoshou tempData = ob_base_xiaoshouservice.GetEntityById(base_xiaoshou => base_xiaoshou.ID == id && base_xiaoshou.IsDelete == false);
            ViewBag.base_xiaoshou = tempData;
            if (tempData == null)
                return View();
            else
            {
                base_xiaoshouViewModel base_xiaoshouviewmodel = new base_xiaoshouViewModel();
                base_xiaoshouviewmodel.ID = tempData.ID;
                base_xiaoshouviewmodel.Leibie = tempData.Leibie;
                base_xiaoshouviewmodel.ShouquanID = tempData.ShouquanID;
                base_xiaoshouviewmodel.Shouquanmingcheng = tempData.Shouquanmingcheng;
                base_xiaoshouviewmodel.Xingming = tempData.Xingming;
                base_xiaoshouviewmodel.Dianhua = tempData.Dianhua;
                base_xiaoshouviewmodel.ShenfenzhengBH = tempData.ShenfenzhengBH;
                base_xiaoshouviewmodel.Shenfenzheng0TP = tempData.Shenfenzheng0TP;
                base_xiaoshouviewmodel.Shenfenzheng1TP = tempData.Shenfenzheng1TP;
                base_xiaoshouviewmodel.ShouquanshuYXQ = tempData.ShouquanshuYXQ;
                base_xiaoshouviewmodel.ShouquanshuTP = tempData.ShouquanshuTP;
                base_xiaoshouviewmodel.Shouying = tempData.Shouying;
                base_xiaoshouviewmodel.MakeDate = tempData.MakeDate;
                base_xiaoshouviewmodel.MakeMan = tempData.MakeMan;
                return View(base_xiaoshouviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string leibie = Request["leibie"] ?? "";
            string shouquanid = Request["shouquanid"] ?? "";
            string shouquanmingcheng = Request["shouquanmingcheng"] ?? "";
            string xingming = Request["xingming"] ?? "";
            string dianhua = Request["dianhua"] ?? "";
            string shenfenzhengbh = Request["shenfenzhengbh"] ?? "";
            string shenfenzheng0tp = Request["shenfenzheng0tp"] ?? "";
            string shenfenzheng1tp = Request["shenfenzheng1tp"] ?? "";
            string shouquanshuyxq = Request["shouquanshuyxq"] ?? "";
            string shouquanshutp = Request["shouquanshutp"] ?? "";
            string shouying = Request["shouying"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_xiaoshou p = ob_base_xiaoshouservice.GetEntityById(base_xiaoshou => base_xiaoshou.ID == uid);
                p.Leibie = leibie == "" ? 0 : int.Parse(leibie);
                p.ShouquanID = shouquanid == "" ? 0 : int.Parse(shouquanid);
                p.Shouquanmingcheng = shouquanmingcheng.Trim();
                p.Xingming = xingming.Trim();
                p.Dianhua = dianhua.Trim();
                p.ShenfenzhengBH = shenfenzhengbh.Trim();
                p.Shenfenzheng0TP = shenfenzheng0tp.Trim();
                p.Shenfenzheng1TP = shenfenzheng1tp.Trim();
                p.ShouquanshuYXQ = shouquanshuyxq == "" ? DateTime.Now : DateTime.Parse(shouquanshuyxq);
                p.ShouquanshuTP = shouquanshutp.Trim();
                p.Shouying = shouying == "" ? 0 : int.Parse(shouying);
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_xiaoshouservice.UpdateEntity(p);
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
            base_xiaoshou ob_base_xiaoshou;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_xiaoshou = ob_base_xiaoshouservice.GetEntityById(base_xiaoshou => base_xiaoshou.ID == id && base_xiaoshou.IsDelete == false);
                    ob_base_xiaoshou.IsDelete = true;
                    ob_base_xiaoshouservice.UpdateEntity(ob_base_xiaoshou);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

