using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ModusMVC.Controllers
{
    public class ApplicantsFilesController : Controller
    {
        // GET: ApplicantsFiles
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Graduates()
        {
            try
            {
                var dir = new System.IO.DirectoryInfo(Server.MapPath("~/CV/Graduate/Software Development"));
                List<string> items = new List<string>();

                System.IO.FileInfo[] fileNames = dir.GetFiles("*.zip");

                foreach (var file in fileNames)
                {
                    items.Add(file.Name);
                }

                return View(items);
            }
            catch (Exception)
            {

                return View("NotFound");
            }
        }
        public ActionResult NotFound()
        {
            return View();
        }

        public FileResult Download(string ImageName)
        {
            //      return  File("~/CV/Graduate/7/" + filename, System.Net.Mime.MediaTypeNames.Application.Octet);


            var filepath = System.IO.Path.Combine(Server.MapPath("~/CV/Graduate/Software Development"), ImageName);
            return File(filepath, MimeMapping.GetMimeMapping(filepath), ImageName);



        }

    }
}