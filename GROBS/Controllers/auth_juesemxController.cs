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
    public class auth_juesemxController : Controller
    {
        private Iauth_juesemxService ob_auth_juesemxservice = ServiceFactory.auth_juesemxservice;
        [OutputCache(Duration = 10)]
        public ActionResult RoleDetail(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string jsid = Request["jsid"] ?? "";
            if (jsid == "")
                jsid = "0";
            string gnstring = "";
            string modstr = "";
            Iauth_gongnengService gnservice = ServiceFactory.auth_gongnengservice;
            IList<auth_gongneng> gnlist = gnservice.LoadSortEntities(auth_gongneng => auth_gongneng.IsDelete == false, true, auth_gongneng => auth_gongneng.Module).ToList<auth_gongneng>();
            foreach (auth_gongneng gn in gnlist)
            {
                if (modstr.Equals(gn.Module))
                {
                    gnstring = gnstring + gn.Name + "(" + gn.ID.ToString() + "),";
                }
                else
                {
                    modstr = gn.Module;
                    if (gnstring.Length > 0)
                    {
                        gnstring = gnstring.Substring(0, gnstring.Length - 1);
                        gnstring = gnstring + ";";
                    }
                    gnstring = gnstring + gn.Module + ":" + gn.Name + "(" + gn.ID.ToString() + "),";
                }
            }
            var tmpdata = ob_auth_juesemxservice.GetRoleDetailsByRole(int.Parse(jsid));
            string gnstring1 = "";
            string modstr1 = "";
            IList<auth_roledetail> rdlist = tmpdata.ToList<auth_roledetail>();
            foreach (auth_roledetail rd in rdlist)
            {
                if (modstr1.Equals(rd.module))
                {
                    gnstring1 = gnstring1 + rd.name + "(" + rd.funid.ToString() + ")" + ",";
                }
                else
                {
                    modstr1 = rd.module;
                    if (gnstring1.Length > 0)
                    {
                        gnstring1 = gnstring1.Substring(0, gnstring1.Length - 1);
                        gnstring1 = gnstring1 + ";";
                    }
                    gnstring1 = gnstring1 + rd.module + ":" + rd.name + "(" + rd.funid.ToString() + ")" + ",";
                }
            }
            ViewBag.fundata = gnstring;
            ViewBag.funs = gnstring1;
            ViewBag.jsid = jsid;
            ViewBag.roledetails = tmpdata;
            return View();
        }
        [OutputCache(Duration = 30)]
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";
            int userid = (int)Session["user_id"];
            string pagetag = "auth_juesemx_index";
            Expression<Func<auth_juesemx, bool>> where = PredicateExtensionses.True<auth_juesemx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc != null)
            {
                string[] sclist = sc.ConditionInfo.Split(';');
                foreach (string scl in sclist)
                {
                    string[] scld = scl.Split(',');
                    switch (scld[0])
                    {
                        case "roleid":
                            string roleid = scld[1];
                            string roleidequal = scld[2];
                            string roleidand = scld[3];
                            if (!string.IsNullOrEmpty(roleid))
                            {
                                if (roleidequal.Equals("="))
                                {
                                    if (roleidand.Equals("and"))
                                        where = where.And(auth_juesemx => auth_juesemx.RoleID == int.Parse(roleid));
                                    else
                                        where = where.Or(auth_juesemx => auth_juesemx.RoleID == int.Parse(roleid));
                                }
                                if (roleidequal.Equals(">"))
                                {
                                    if (roleidand.Equals("and"))
                                        where = where.And(auth_juesemx => auth_juesemx.RoleID > int.Parse(roleid));
                                    else
                                        where = where.Or(auth_juesemx => auth_juesemx.RoleID > int.Parse(roleid));
                                }
                                if (roleidequal.Equals("<"))
                                {
                                    if (roleidand.Equals("and"))
                                        where = where.And(auth_juesemx => auth_juesemx.RoleID < int.Parse(roleid));
                                    else
                                        where = where.Or(auth_juesemx => auth_juesemx.RoleID < int.Parse(roleid));
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                ViewBag.SearchCondition = sc.ConditionInfo;
            }

            where = where.And(auth_juesemx => auth_juesemx.IsDelete == false);

            var tempData = ob_auth_juesemxservice.LoadSortEntities(where.Compile(), false, auth_juesemx => auth_juesemx.ID).ToPagedList<auth_juesemx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.auth_juesemx = tempData;
            return View(tempData);
        }

        [HttpPost]
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            int userid = (int)Session["user_id"];
            string pagetag = "auth_juesemx_index";
            string page = "1";
            string roleid = Request["roleid"] ?? "";
            string roleidequal = Request["roleidequal"] ?? "";
            string roleidand = Request["roleidand"] ?? "";
            Expression<Func<auth_juesemx, bool>> where = PredicateExtensionses.True<auth_juesemx>();
            searchcondition sc = searchconditionService.GetInstance().GetEntityById(searchcondition => searchcondition.UserID == userid && searchcondition.PageBrief == pagetag);
            if (sc == null)
            {
                sc = new searchcondition();
                sc.UserID = userid;
                sc.PageBrief = pagetag;
                if (!string.IsNullOrEmpty(roleid))
                {
                    if (roleidequal.Equals("="))
                    {
                        if (roleidand.Equals("and"))
                            where = where.And(auth_juesemx => auth_juesemx.RoleID == int.Parse(roleid));
                        else
                            where = where.Or(auth_juesemx => auth_juesemx.RoleID == int.Parse(roleid));
                    }
                    if (roleidequal.Equals(">"))
                    {
                        if (roleidand.Equals("and"))
                            where = where.And(auth_juesemx => auth_juesemx.RoleID > int.Parse(roleid));
                        else
                            where = where.Or(auth_juesemx => auth_juesemx.RoleID > int.Parse(roleid));
                    }
                    if (roleidequal.Equals("<"))
                    {
                        if (roleidand.Equals("and"))
                            where = where.And(auth_juesemx => auth_juesemx.RoleID < int.Parse(roleid));
                        else
                            where = where.Or(auth_juesemx => auth_juesemx.RoleID < int.Parse(roleid));
                    }
                }
                if (!string.IsNullOrEmpty(roleid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "roleid", roleid, roleidequal, roleidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "roleid", "", roleidequal, roleidand);

                searchconditionService.GetInstance().AddEntity(sc);
            }
            else
            {
                sc.ConditionInfo = "";
                if (!string.IsNullOrEmpty(roleid))
                {
                    if (roleidequal.Equals("="))
                    {
                        if (roleidand.Equals("and"))
                            where = where.And(auth_juesemx => auth_juesemx.RoleID == int.Parse(roleid));
                        else
                            where = where.Or(auth_juesemx => auth_juesemx.RoleID == int.Parse(roleid));
                    }
                    if (roleidequal.Equals(">"))
                    {
                        if (roleidand.Equals("and"))
                            where = where.And(auth_juesemx => auth_juesemx.RoleID > int.Parse(roleid));
                        else
                            where = where.Or(auth_juesemx => auth_juesemx.RoleID > int.Parse(roleid));
                    }
                    if (roleidequal.Equals("<"))
                    {
                        if (roleidand.Equals("and"))
                            where = where.And(auth_juesemx => auth_juesemx.RoleID < int.Parse(roleid));
                        else
                            where = where.Or(auth_juesemx => auth_juesemx.RoleID < int.Parse(roleid));
                    }
                }
                if (!string.IsNullOrEmpty(roleid))
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "roleid", roleid, roleidequal, roleidand);
                else
                    sc.ConditionInfo = sc.ConditionInfo + string.Format("{0},{1},{2},{3};", "roleid", "", roleidequal, roleidand);

                searchconditionService.GetInstance().UpdateEntity(sc);
            }
            ViewBag.SearchCondition = sc.ConditionInfo;
            where = where.And(auth_juesemx => auth_juesemx.IsDelete == false);

            var tempData = ob_auth_juesemxservice.LoadSortEntities(where.Compile(), false, auth_juesemx => auth_juesemx.ID).ToPagedList<auth_juesemx>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.auth_juesemx = tempData;
            return View(tempData);
        }

        public ActionResult Add()
        {
            ViewBag.userid = (int)Session["user_id"];
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateMX()
        {
            var jsid = Request["jsid"] ?? "";
            var funs = Request["funids"] ?? "";
            var funsave = Request["funsave"] ?? "";
            int userid = (int)Session["user_id"];
            if (jsid == "")
                jsid = "0";
            else
            {
                string[] originlist = funs.Split(';');
                Dictionary<int, bool> dicfun = new Dictionary<int, bool>();
                string ofunid = "";
                foreach (string originfun in originlist)
                {
                    if (originfun.Length > 0)
                    {
                        string[] ofunlist = funs.Split(',');
                        foreach (string ofun in ofunlist)
                        {
                            if (ofun.Length > 0)
                            {
                                ofunid = ofun.Substring(ofun.IndexOf('(') + 1, ofun.IndexOf(')') - ofun.IndexOf('(') - 1);
                                if (!dicfun.ContainsKey(int.Parse(ofunid)))
                                    dicfun.Add(int.Parse(ofunid), false);
                            }
                        }
                    }
                }
                string[] funlist = funsave.Split(',');
                foreach (string funid in funlist)
                {
                    if (funid.Length > 0)
                    {
                        if (dicfun.ContainsKey(int.Parse(funid)))
                            dicfun[int.Parse(funid)] = true;
                        else
                        {
                            auth_juesemx juesemx = new auth_juesemx();
                            juesemx.RoleID = int.Parse(jsid);
                            juesemx.FuncID = int.Parse(funid);
                            juesemx.MakeMan = userid;
                            ob_auth_juesemxservice.AddEntity(juesemx);
                        }
                    }
                }
                foreach (var b in dicfun)
                {
                    if (!b.Value)
                    {
                        auth_juesemx juesemx = ob_auth_juesemxservice.GetEntityById(auth_juesemx => auth_juesemx.RoleID == int.Parse(jsid) && auth_juesemx.FuncID == b.Key && auth_juesemx.IsDelete == false);
                        if (juesemx != null)
                        {
                            juesemx.IsDelete = true;
                            juesemx.MakeDate = DateTime.Now;
                            juesemx.MakeMan = userid;
                            ob_auth_juesemxservice.UpdateEntity(juesemx);
                        }
                    }
                }
            }

            return RedirectToAction("Index", "auth_juese");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save()
        {
            string id = Request["id"] ?? "";
            string roleid = Request["roleid"] ?? "";
            string funcid = Request["funcid"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            try
            {
                auth_juesemx ob_auth_juesemx = new auth_juesemx();
                ob_auth_juesemx.RoleID = roleid == "" ? 0 : int.Parse(roleid);
                ob_auth_juesemx.FuncID = funcid == "" ? 0 : int.Parse(funcid);
                ob_auth_juesemx.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_auth_juesemx.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_auth_juesemx = ob_auth_juesemxservice.AddEntity(ob_auth_juesemx);
                ViewBag.auth_juesemx = ob_auth_juesemx;
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
            auth_juesemx tempData = ob_auth_juesemxservice.GetEntityById(auth_juesemx => auth_juesemx.ID == id && auth_juesemx.IsDelete == false);
            ViewBag.auth_juesemx = tempData;
            if (tempData == null)
                return View();
            else
            {
                auth_juesemxViewModel auth_juesemxviewmodel = new auth_juesemxViewModel();
                auth_juesemxviewmodel.ID = tempData.ID;
                auth_juesemxviewmodel.RoleID = tempData.RoleID;
                auth_juesemxviewmodel.FuncID = tempData.FuncID;
                auth_juesemxviewmodel.MakeDate = tempData.MakeDate;
                auth_juesemxviewmodel.MakeMan = tempData.MakeMan;
                return View(auth_juesemxviewmodel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update()
        {
            string id = Request["id"] ?? "";
            string roleid = Request["roleid"] ?? "";
            string funcid = Request["funcid"] ?? "";
            string makedate = Request["makedate"] ?? "";
            string makeman = Request["makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                auth_juesemx p = ob_auth_juesemxservice.GetEntityById(auth_juesemx => auth_juesemx.ID == uid);
                p.RoleID = roleid == "" ? 0 : int.Parse(roleid);
                p.FuncID = funcid == "" ? 0 : int.Parse(funcid);
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_auth_juesemxservice.UpdateEntity(p);
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
            auth_juesemx ob_auth_juesemx;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_auth_juesemx = ob_auth_juesemxservice.GetEntityById(auth_juesemx => auth_juesemx.ID == id && auth_juesemx.IsDelete == false);
                    ob_auth_juesemx.IsDelete = true;
                    ob_auth_juesemxservice.UpdateEntity(ob_auth_juesemx);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

