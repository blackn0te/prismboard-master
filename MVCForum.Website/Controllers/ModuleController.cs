
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

    public class ModuleController : BaseController
    {

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


    }
}