
namespace MvcForum.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using MvcForum.Core.Data.Context;
    using MvcForum.Core.Models.Entities;
    using MvcForum.Core.Interfaces;
    using MvcForum.Core.Interfaces.Services;
    using MvcForum.Web.ViewModels.Module;
    using System.Web;
    using MvcForum.Core.VirusTotal;
    using System.IO;
    using MvcForum.Core.VirusTotal.Results;
    using System.Threading.Tasks;
    using MvcForum.Core.VirusTotal.ResponseCodes;
    using MvcForum.Core.VirusTotal.Objects;

    public class ModuleController : BaseController
    {

        private int numberThingy = 0;
        private string submittableid;


        public ModuleController(ILoggingService loggingService, IMembershipService membershipService, ILocalizationService localizationService, 
            IRoleService roleService, ISettingsService settingsService, ICacheService cacheService, IMvcForumContext context, IPostService postService) 
            : base(loggingService, membershipService, localizationService, roleService, settingsService, cacheService, context)
        {
            
        }
        public ActionResult Error()
        {
            return View();
        }

        // GET: Module/Index
        [Authorize]
        public ActionResult Index()
        {
            MvcForumContext db = new MvcForumContext();

            //Get Username(slug)
            string user = User.Identity.Name;
            //Gather all modules id under name
            List<ModDetail> listofModDetail = db.ModDetails.Where(s => s.PersonName == user).ToList();
            if (listofModDetail == null)
            {
                return View();
            }
            else {

                List<Module> listOfModule = new List<Module>();

                foreach (ModDetail s in listofModDetail)
                {
                    listOfModule.Add(db.Module.Where(a => a.ModId == s.ModuleId).FirstOrDefault());
                }

                ModuleMainViewModel view = new ModuleMainViewModel();

                foreach (Module s in listOfModule)
                {
                    if (s.ModStart.Date > DateTime.Now.Date)
                        view.futureModule.Add(s);
                    else if (s.ModEnd.Date < DateTime.Now.Date)
                        view.achievedModule.Add(s);
                    else
                        view.currentModule.Add(s);
                }

                

                return View(view); 
            }

        }

        // GET: Module/SpecificMod/<ModuleID>
        [Authorize]
        public ActionResult SpecificMod(string moduleID)
        {
          
                if (moduleID != null)
                {
                    MvcForumContext db = new MvcForumContext();
                    if (db.Module.Any(s => s.ModId == moduleID))
                    {
                        ModDetail test = db.ModDetails.Where(s => s.ModuleId == moduleID).Where(s => s.PersonName == User.Identity.Name).First();
                        if (test != null)
                        {
                            List<Materials> MatList = db.Materials.Where(s => s.ModId == moduleID).ToList();
                            List<Materials> SortedMatList = MatList.OrderBy(o => o.Week).ToList();

                            //Retrieve Lecturer List
                            List<ModDetail> LectList = db.ModDetails.Where(s => s.ModuleId == moduleID).Where(s => s.PersonType == "Lecturer").ToList();
                            List<Lecturer> lectList = new List<Lecturer>();
                            foreach (ModDetail a in LectList)
                            {
                                Lecturer finder = db.Lecturers.Where(s => s.LectName == a.PersonName).First();
                                lectList.Add(finder);
                            }
                        ModuleSpecificViewModel view = new ModuleSpecificViewModel();

                        int prefix = 0;
                        Week weekCheck = new Week();

                        foreach (Materials s in SortedMatList)
                        {
                            if (prefix == 0)
                            {
                                prefix = s.Week;
                                weekCheck.matlist.Add(s);

                            }
                            else if (prefix != s.Week)
                            {
                                view.weekList.Add(weekCheck);
                                weekCheck = new Week();
                                prefix = s.Week;
                                weekCheck.matlist.Add(s);
                            }
                            else
                            {
                                weekCheck.matlist.Add(s);
                            }
                        }

                        view.weekList.Add(weekCheck);
                        Module module = db.Module.Where(s => s.ModId == moduleID).First();

                            
                        view.lectList = lectList;
                        view.module = module;

                        return View(view);
                        }
                        else
                        {
                            Console.WriteLine("oof");
                            LoggingService.Error(User.Identity.Name + " trying to enter " + moduleID);
                            return RedirectToAction("Error");
                        }
                    }
                    else
                    {
                        Console.WriteLine("NOT Found");
                        LoggingService.Error("Viewing Specific Module Error, ID Supplied is " + moduleID);
                        return RedirectToAction("Error");
                    }
                }
                else
                {
                    Console.WriteLine("NO INPUT OOF");
                    LoggingService.Error("Viewing Specific Module Error, ID Supplied is " + moduleID);
                    return RedirectToAction("Error");
                }
            
            
        }

        [Authorize]
        public ActionResult FileDownload(string matID)
        {
            MvcForumContext db = new MvcForumContext();

            if (matID != null)
            {
                if (db.Materials.Any(s => s.MatId.ToString() == matID))
                {
                    Materials material = db.Materials.Where(s => s.MatId.ToString() == matID).First();

                    byte[] fileBytes = System.IO.File.ReadAllBytes(material.FileLink);
                    string fileName = material.fileName;
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }
                else {
                    return RedirectToAction("Error");
                }
            }
            else {
                return RedirectToAction("Error");
            }

            
        }

        [Authorize]
        public ActionResult SubmitDialog(string matID)
        {
            if (matID != null)
            {
                MvcForumContext db = new MvcForumContext();
                if (db.SubmittableFiles.Any(s => s.MatId.ToString() == matID))
                {
                    SubmittableFile file = db.SubmittableFiles.Where(s => s.MatId.ToString() == matID).First();
                    Materials mat = db.Materials.Where(s => s.MatId.ToString() == matID).First() ;
                    List<SubmittedContent> submittedList = db.SubmittedContents.Where(s => s.SubmitableId.ToString() == file.SubmittableId.ToString()).ToList();

                    ModuleSubmittedContentsViewModel view = new ModuleSubmittedContentsViewModel();
                    view.file = file;
                    view.mat = mat;
                    view.submitedList = submittedList;

                    return View(view);

                }
                else
                {
                    Console.WriteLine("NO INPUT OOF");
                    LoggingService.Error("User trying to enter mod detail is wrong");
                    return RedirectToAction("Error");
                }
            }
            else
            {
                Console.WriteLine("NO INPUT OOF");
                LoggingService.Error("View SubmitDialog Page error, matId is not supplied");
                return RedirectToAction("Error");
            }
        }

        [Authorize]
        public ActionResult SubmitFile(string submittableId)
        {
            if (submittableId != null)
            {
                MvcForumContext db = new MvcForumContext();
                if (db.SubmittableFiles.Any(s => s.SubmittableId.ToString() == submittableId))
                {
                    SubmittableFile test = db.SubmittableFiles.Where(s => s.SubmittableId.ToString() == submittableId).First();
                    Materials testi = db.Materials.Where(s => s.MatId == test.MatId).First();
                    string user = User.Identity.Name;
                    if (db.ModDetails.Any(s => s.ModuleId == testi.ModId && s.PersonName == user))
                    {
                        SubmitFileViewModel view = new SubmitFileViewModel();
                        view.Mat = testi;
                        view.File = test;
                        submittableid = submittableId;

                        return View(view);
                    }
                    else
                    {
                        LoggingService.Error("View SubmitFile Page error, " + user + " trying to submit item to " + submittableId);
                        return RedirectToAction("Error");
                    }
                }
                else
                {
                    LoggingService.Error("View SubmitFile Page error, submittableId supplied wrong is " + submittableId);
                    return RedirectToAction("Error");
                }
            }
            else
            {
                Console.WriteLine("NO INPUT OOF");
                LoggingService.Error("View SubmitFile Page error, submittableId is not supplied");
                return RedirectToAction("Error");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitFile(HttpPostedFileBase file) {
            if (file != null && file.ContentLength > 0)
            {
                VirusTotal virusTotal = new VirusTotal("cada6fd29a10fc09effba471d6c4286ae0d39110c47a3f30911522a7bed9bd5c");
                virusTotal.UseTLS = true;

                byte[] fileToScan;
                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                fileToScan = target.ToArray();

                var filename = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), filename);
                file.SaveAs(path);

                SubmittedContent test = new SubmittedContent();
                

                return View();
            }
            else
            {
                LoggingService.Error("POST DATA SubmitFile Page error, File is empty when transfered ");
                return RedirectToAction("Error");
            }
        }

        private static async Task RunExample(byte[] file, string filename)
        {
            VirusTotal virusTotal = new VirusTotal("cada6fd29a10fc09effba471d6c4286ae0d39110c47a3f30911522a7bed9bd5c");
            virusTotal.UseTLS = true;

            FileReport fileReport = await virusTotal.GetFileReportAsync(file);
            bool hasfilebeenscanned = fileReport.ResponseCode == FileReportResponseCode.Present;

            if (hasfilebeenscanned)
            {
                PrintScan(fileReport);
            }
            else
            {
                ScanResult fileResult = await virusTotal.ScanFileAsync(file, filename);
                PrintScan(fileResult);  
            }
        }

        private static void PrintScan(FileReport fileReport)
        {
            Console.WriteLine("Scan ID: " + fileReport.ScanId);
            Console.WriteLine("Message: " + fileReport.VerboseMsg);

            if (fileReport.ResponseCode == FileReportResponseCode.Present)
            {
                foreach (KeyValuePair<string, ScanEngine> scan in fileReport.Scans)
                {
                    Console.WriteLine("{0,-25} Detected: {1}", scan.Key, scan.Value.Detected);
                }
            }

            Console.WriteLine();
        }

        private static void PrintScan(ScanResult scanResult)
        {
            Console.WriteLine("Scan ID: " + scanResult.ScanId);
            Console.WriteLine("Message: " + scanResult.VerboseMsg);
            Console.WriteLine();
        }
    }

}