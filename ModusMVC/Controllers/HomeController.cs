using CSDropdownListMVC4.Models;
using ModusMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using Ionic.Zip;

namespace ModusMVC.Controllers
{
    public class HomeController : Controller
    {
        StreamWriter writer = null;
        ModusDNAEntities db = new ModusDNAEntities();
        Applicant tempApp = new Applicant();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult JoinUs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult JoinUs(Applicant applicant)
        {
            tempApp = applicant;
            string name = applicant.ApplicantName + " " + applicant.ApplicantSurname;
            //  RegistrationSave = applicant;
            // string strMappath = "~/CV/ " + name;

            Session["PersonInfo"] = applicant;
            applicant.ApplicantCV = "null";
            db.Applicants.Add(applicant);
            db.SaveChanges();
            Session["Foldername"] = name;

        //    SendEmail("Test", "Welcome to modus" ,applicant.ApplicantEmail);

            return RedirectToAction("Upload", applicant); 
        }
        [HttpGet]
        public ActionResult Upload()
        {

            IDictionary<string, string> makes = GetSampleMakes();
            CascadingDropDownSampleModel viewModel = new CascadingDropDownSampleModel()
            {
                Makes = makes
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Upload( HttpPostedFileBase upload, HttpPostedFileBase idDoc, HttpPostedFileBase AccResults, FormCollection formCollect)
        {
            string roleValue1 = formCollect.Get("make");
            string specialization = formCollect.Get("model");
            string applicantName = Session["Foldername"].ToString();
            var applicant = (Applicant)Session["PersonInfo"];
            switch (roleValue1)
            {
                case "1":
                    {
                        roleValue1 = "Internship";
                        break;
                    }
                case "2":
                    {
                        roleValue1 = "Learnership";
                        break;
                    }
                case "3":
                    {
                        roleValue1 = "Graduate";
                        break;
                    }

            }

            switch (specialization)
            {
                case "1":
                    {
                        specialization = "Software Development";
                        break;
                    }
                case "2":
                    {
                        specialization = "Software Quality Assuarence";
                        break;
                    }
                case "3":
                    {
                        specialization = "Software Development";
                        break;
                    }
                case "4":
                    {
                        specialization = "Software Development";
                        break;
                    }
                case "5":
                    {
                        specialization = "Software Development";
                        break;
                    }
                case "6":
                    {
                        specialization = "Software Development";
                        break;
                    }
                case "7":
                    {
                        specialization = "Software Development";
                        break;
                    }

            }
            string readmePageURL = "~/App_Data/tempFiles/Readme(" + applicantName + ").txt";
            writer = new StreamWriter(Server.MapPath("~/App_Data/tempFiles/Readme("+applicantName+").txt"), true);

            string parth = roleValue1 + "/" + specialization + "/";
   
          
            writer.WriteLine("Position applying for: " + roleValue1+" "+specialization);
            writer.Close();

            if (roleValue1 == "Learnership")
            {
                Session["Foldername"] = "~/CV/" + parth + Session["Foldername"].ToString();
            }
            else if (roleValue1 == "Internship")
            {
                Session["Foldername"] = "~/CV/" + parth + Session["Foldername"].ToString();
            }
            else if (roleValue1 == "Graduate")
            {
                Session["Foldername"] = "~/CV/" + parth + Session["Foldername"].ToString();
            }

            if (!Directory.Exists(Session["Foldername"].ToString()))
            {
                DirectoryInfo di = Directory.CreateDirectory(Server.MapPath(Session["Foldername"].ToString()));
            }

            using (ZipFile zip = new ZipFile())
            {

                if (upload.ContentLength > 0)
            {
                var fileName = Path.GetFileName(upload.FileName);
                var path = Path.Combine(Server.MapPath(Session["Foldername"].ToString() + "/"), fileName);
                upload.SaveAs(path);
                 zip.AddFile(path, "Documents");
                }

            if (idDoc.ContentLength > 0)
            {

                var fileName = Path.GetFileName(idDoc.FileName);
                var path = Path.Combine(Server.MapPath(Session["Foldername"].ToString() + "/"), fileName);
                idDoc.SaveAs(path);
                    zip.AddFile(path, "Documents");
                }

            if (AccResults.ContentLength > 0)
            {
                var fileName = Path.GetFileName(AccResults.FileName);
                var path = Path.Combine(Server.MapPath(Session["Foldername"].ToString() + "/"), fileName);
                AccResults.SaveAs(path);
                    zip.AddFile(path, "Documents");
                }
                if (applicantName !=null)
                {
                    writer = new StreamWriter(Server.MapPath(Session["Foldername"].ToString() + " /"+applicant.ApplicantName+".txt"),false);
                    writer.WriteLine("Name: " + applicant.ApplicantName);
                    writer.WriteLine("Surname: " + applicant.ApplicantSurname);
                    writer.WriteLine("ID: " + applicant.ApplicantIdentity);
                    writer.WriteLine("Email: " + applicant.ApplicantEmail);
                    writer.WriteLine("Cell: " + applicant.ApplicantCell);
                    writer.Close();

                    var fileName = Path.GetFileName(readmePageURL);
                    var path = Path.Combine(Server.MapPath(Session["Foldername"].ToString() + "/"), fileName);
                    AccResults.SaveAs(path);
                    zip.AddFile(path, "Documents");
                }
               

                
            //////////////////////////////////////
            //adding zip


                zip.Save(Server.MapPath("~/CV/" + parth + applicantName+ ".zip"));
            }
            return View("Success");
    }

    public ActionResult Success()
    {
        return View();
    }
        public ActionResult admin()
        {
            return View();
        }
    //////////////////////////////////////////////////////////
    /// <summary>
    /// AJAX Action to send sample Models in JSON format based on the selected make
    /// </summary>
    /// <param name="selectedMake"></param>
    /// <returns></returns>
    public ActionResult GetSampleModels(string selectedMake)
    {
        Session["make"] = selectedMake;

        IDictionary<string, string> models = GetSampleModelsFromSelectedMake(selectedMake);
        Session["specialization"] = models;
        return Json(models);
    }

    #region "Private Methods"

    /// <summary>
    /// Method to generate sample makes
    /// </summary>
    /// <returns></returns>
    private IDictionary<string, string> GetSampleMakes()
    {
        IDictionary<string, string> makes = new Dictionary<string, string>();

        makes.Add("1", "Internship");
        makes.Add("2", "Learnership");
        makes.Add("3", "Graduate");

        return makes;
    }

    /// <summary>
    /// Method to generate sample models based on selected make
    /// </summary>
    /// <param name="selectedMake"></param>
    /// <returns></returns>
    private IDictionary<string, string> GetSampleModelsFromSelectedMake(string selectedMake)
    {
        IDictionary<string, string> models = new Dictionary<string, string>();

        switch (selectedMake)
        {
            case "1":
                models.Add("1", "Software Development");
                models.Add("2", "Software Quality Assuarence");

                break;
            case "2":
                models.Add("4", "Software Delvelopment");
                break;
            case "3":
                models.Add("7", "Software Development");
                break;
            default:
                throw new NotImplementedException();

        }

        return models;
    }

        #endregion

        #region Send Email
        public static void SendEmail(string subj, string body, string email)
        {

            string fromEmail = "tithingsystem@gmail.com";
            string emailPass = "DSO34BT2015";

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(fromEmail);
            msg.To.Add(new MailAddress(email));
            msg.Subject =subj;
            msg.Body =body;
            msg.IsBodyHtml = true;

       
            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new NetworkCredential(fromEmail, emailPass);
            smtp.EnableSsl = true;
            smtp.Port = 587;

            try
            {
                 smtp.Send(msg);
       
            }

//#pragma warning disable CS0168 // The variable 'error' is declared but never used
            catch (Exception error)
//#pragma warning restore CS0168 // The variable 'error' is declared but never used
            {
               
            }
        }
        #endregion

    }
}