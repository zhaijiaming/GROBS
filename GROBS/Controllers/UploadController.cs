using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GROBS.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        // GET: Upload/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Upload/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Upload/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Upload/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Upload/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Upload/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Upload/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult UploadNow(FormCollection form)
        {
            if (Request.Files.Count == 0)
            {
                //Request.Files.Count 文件数为0上传不成功
                return View();
            }

            var file = Request.Files[0];
            if (file.ContentLength == 0)
            {
                //文件大小大（以字节为单位）为0时，做一些操作
                return View();
            }
            else
            {
                //文件大小不为0
                HttpPostedFileBase nfile = Request.Files[0];
                nfile.SaveAs(Server.MapPath(@"\\files\\newfile"));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public string UploadOK(FormCollection form)
        {
            //file.FileName.Substring(file.FileName.LastIndexOf('.'))
            string newfile = "0";
            try
            {
                if (Request.Files.Count == 0)
                {
                    return "0";
                }

                var file = Request.Files[0];
                if (file.ContentLength == 0)
                {
                    return "1";
                }
                else if (file.ContentLength > 4 * 1024 * 1024)
                {
                    return "3";
                }
                else
                {
                    HttpPostedFileBase nfile = Request.Files[0];
                    string xxxmark = nfile.FileName.Substring(nfile.FileName.LastIndexOf('.'));
                    //上传文件的类别
                    var lt = Request["uploadtype"];
                    var itemName = Request["uploaditemname"];
                    var lp = @"\\files\\";
                    switch (lt)
                    {
                        case "z"://证照
                            lp = lp + System.Web.Configuration.WebConfigurationManager.AppSettings["UpDirectoryZhengZhao"] + "\\";
                            break;
                        case "y"://业务
                            lp = lp + System.Web.Configuration.WebConfigurationManager.AppSettings["UpDirectoryBus"] + "\\";
                            break;
                        default://其它
                            lp = lp + System.Web.Configuration.WebConfigurationManager.AppSettings["other"] + "\\";
                            break;
                    }
                    newfile = itemName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssff") + xxxmark;
                    nfile.SaveAs(Server.MapPath(lp + newfile));
                }
            }
            catch
            {
                return "2";
            }
            return newfile;
        }

    }
}
