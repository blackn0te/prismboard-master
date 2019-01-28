namespace MvcForum.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Dynamic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Core.Constants;
    using Core.Interfaces;
    using Core.Interfaces.Services;
    using Core.Models.Entities;
    using Core.Data.Context;
    using Web.ViewModels;

    [Authorize(Roles = Constants.AdminRoleName)]
    public class ModuleManageController : BaseAdminController
    {

        public ModuleManageController(ILoggingService loggingService,
            IMembershipService membershipService,
            ILocalizationService localizationService,
            ISettingsService settingsService, IMvcForumContext context)
            : base(loggingService, membershipService, localizationService, settingsService, context)
        {
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult DetailModule(string id)
        {
            MvcForumContext db = new MvcForumContext();
            Module query = db.Module.Where(s => s.ModId == id).First();

            return View(query);
        }

        /// <summary>
        ///     Return New Module Page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NewModule()
        {
            MvcForumContext db = new MvcForumContext();
            dynamic model = new ExpandoObject();
            model.Module = db.Module.ToList();
            model.Lecture = db.Lecturers.ToList();
            model.Student = db.Students.ToList();
            return View(model);
        }

        /// <summary>
        ///     Request to delete a language (e.g. confirming on prompt)
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewModule(SY_ModuleViewModel module)
        {

            if (module.ModuleName != null && module.ModStart != null && module.ModEnd != null && module.ModId != null && module.lectList != null && module.studList != null)
            {
                Module modAdd = new Module
                {
                    ModId = module.ModId,
                    ModName = module.ModuleName,
                    ModStart = module.ModStart,
                    ModEnd = module.ModEnd
                };

                MvcForumContext db = new MvcForumContext();
                Module tester = db.Module.Find(modAdd.ModId);

                if (tester == null)
                {
                    db.Module.Add(modAdd);

                    for (int i = 0; i < module.studList.Length; i++)
                    {
                        ModDetail studDetail = new ModDetail
                        {
                            PersonName = module.studList[i],
                            ModuleId = module.ModId,
                            PersonType = "Student"
                        };

                        db.ModDetails.Add(studDetail);
                    }
                    for (int i = 0; i < module.lectList.Length; i++)
                    {
                        ModDetail lectDetail = new ModDetail
                        {
                            PersonName = module.lectList[i],
                            ModuleId = module.ModId,
                            PersonType = "Lecturer"
                        };

                        db.ModDetails.Add(lectDetail);
                    }

                    db.SaveChanges();
                    var redirectUrl = new UrlHelper(Request.RequestContext).Action("Module", "ModuleManage");
                    return Json(new { Url = redirectUrl });
                }
                else
                {
                    return JavaScript("location.reload(true)");
                }

            }
            else
            {
                return JavaScript("location.reload(true)");
            }
        }

        public ActionResult EditModule(string id)
        {
            MvcForumContext db = new MvcForumContext();

            if (id != null)
            {
                Module checker = db.Module.First(a => a.ModId == id);

                if (checker != null)
                {
                    List<ModDetail> unsortList = db.ModDetails.ToList();
                    List<string> studList = new List<string>();
                    List<string> lectList = new List<string>();

                    foreach (ModDetail s in unsortList)
                    {
                        if (s.ModuleId.Equals(id))
                        {
                            if (s.PersonType.Equals("Student"))
                                studList.Add(s.PersonName);
                            if (s.PersonType.Equals("Lecturer"))
                                lectList.Add(s.PersonName);
                        }
                        else
                            continue;
                    }

                    dynamic model = new ExpandoObject();
                    model.Module = checker;
                    model.studLid = studList;
                    model.lectList = lectList;
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: AdminDashBoard/Module  
        public ActionResult Module()
        {

            MvcForumContext db = new MvcForumContext();

            List<Module> mList = db.Module.ToList();

            return View(mList);
        }

        [HttpGet]
        public ActionResult DeleteModule(string id)
        {
            MvcForumContext db = new MvcForumContext();

            if (id != null)
            {
                Module checker = db.Module.First(a => a.ModId == id);

                if (checker != null)
                {
                    return View(checker);
                }
                else {
                    return RedirectToAction("Error");
                }
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteModule(string id, string confirm)
        {

            MvcForumContext db = new MvcForumContext();

            if (id != null)
            {
                Module checker = db.Module.First(a => a.ModId == id);

                if (checker != null)
                {
                    db.Module.Remove(checker);
                    db.ModDetails.RemoveRange(db.ModDetails.Where(c => c.ModuleId == id));
                    db.SaveChanges();
                    return RedirectToAction("Module");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        

        
    }
}
