using ModusMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModusMVC.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index(Applicant applicant)
        {
            return View(applicant);
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase upload)
        {
            if (upload.ContentLength>0)
            {
                var filename = Path.GetFileName(upload.FileName);
                var path = Path.Combine(Server.MapPath("~/CV"), filename);
                upload.SaveAs(path);
            }
          
            return RedirectToAction("Index");
        }
    }
}