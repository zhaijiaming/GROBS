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
    public class base_yonghuController : Controller
    {
        private Ibase_yonghuService ob_base_yonghuservice = ServiceFactory.base_yonghuservice;
        public ActionResult Index(string page)
        {
            if (string.IsNullOrEmpty(page))
                page = "1";

            string zhanghao = Request["zhanghao"] ?? "";
            string zhanghaoequal = Request["zhanghaoequal"] ?? "";
            string zhanghaoand = Request["zhanghaoand"] ?? "";

            Expression<Func<base_yonghu, bool>> where = PredicateExtensionses.True<base_yonghu>();
            if (!string.IsNullOrEmpty(zhanghao))
            {
                if (zhanghaoequal.Equals("="))
                {
                    if (zhanghaoand.Equals("and"))
                        where = where.And(base_yonghu => base_yonghu.Zhanghao == zhanghao);
                    else
                        where = where.Or(base_yonghu => base_yonghu.Zhanghao == zhanghao);
                }
                if (zhanghaoequal.Equals("like"))
                {
                    if (zhanghaoand.Equals("and"))
                        where = where.And(base_yonghu => base_yonghu.Zhanghao.Contains(zhanghao));
                    else
                        where = where.Or(base_yonghu => base_yonghu.Zhanghao.Contains(zhanghao));
                }
            }

            where = where.And(base_yonghu => base_yonghu.IsDelete == false);

            var tempData = ob_base_yonghuservice.LoadSortEntities(where.Compile(), false, base_yonghu => base_yonghu.ID).ToPagedList<base_yonghu>(int.Parse(page), int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowPerPage"]));
            ViewBag.base_yonghu = tempData;
            return View(tempData);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Save()
        {
            string id = Request["ob_base_yonghu_id"] ?? "";
            string zhanghao = Request["ob_base_yonghu_zhanghao"] ?? "";
            string mima = Request["ob_base_yonghu_mima"] ?? "";
            string miaoshu = Request["ob_base_yonghu_miaoshu"] ?? "";
            string zilixuhao = Request["ob_base_yonghu_zilixuhao"] ?? "";
            string makedate = Request["ob_base_yonghu_makedate"] ?? "";
            string makeman = Request["ob_base_yonghu_makeman"] ?? "";
            try
            {
                base_yonghu ob_base_yonghu = new base_yonghu();
                ob_base_yonghu.Zhanghao = zhanghao.Trim();
                ob_base_yonghu.Mima = mima.Trim();
                ob_base_yonghu.Miaoshu = miaoshu.Trim();
                ob_base_yonghu.zilixuhao = zilixuhao == "" ? 0 : int.Parse(zilixuhao);
                ob_base_yonghu.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                ob_base_yonghu.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_yonghu = ob_base_yonghuservice.AddEntity(ob_base_yonghu);
                ViewBag.base_yonghu = ob_base_yonghu;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            base_yonghu tempData = ob_base_yonghuservice.GetEntityById(base_yonghu => base_yonghu.ID == id && base_yonghu.IsDelete == false);
            ViewBag.base_yonghu = tempData;
            return View();
        }

        public ActionResult Update()
        {
            string id = Request["ob_base_yonghu_id"] ?? "";
            string zhanghao = Request["ob_base_yonghu_zhanghao"] ?? "";
            string mima = Request["ob_base_yonghu_mima"] ?? "";
            string miaoshu = Request["ob_base_yonghu_miaoshu"] ?? "";
            string zilixuhao = Request["ob_base_yonghu_zilixuhao"] ?? "";
            string makedate = Request["ob_base_yonghu_makedate"] ?? "";
            string makeman = Request["ob_base_yonghu_makeman"] ?? "";
            int uid = int.Parse(id);
            try
            {
                base_yonghu p = ob_base_yonghuservice.GetEntityById(base_yonghu => base_yonghu.ID == uid);
                p.Zhanghao = zhanghao.Trim();
                p.Mima = mima.Trim();
                p.Miaoshu = miaoshu.Trim();
                p.zilixuhao = zilixuhao == "" ? 0 : int.Parse(zilixuhao);
                p.MakeDate = makedate == "" ? DateTime.Now : DateTime.Parse(makedate);
                p.MakeMan = makeman == "" ? 0 : int.Parse(makeman);
                ob_base_yonghuservice.UpdateEntity(p);
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
            base_yonghu ob_base_yonghu;
            foreach (string sD in sdel.Split(','))
            {
                if (sD.Length > 0)
                {
                    id = int.Parse(sD);
                    ob_base_yonghu = ob_base_yonghuservice.GetEntityById(base_yonghu => base_yonghu.ID == id && base_yonghu.IsDelete == false);
                    ob_base_yonghu.IsDelete = true;
                    ob_base_yonghuservice.UpdateEntity(ob_base_yonghu);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

