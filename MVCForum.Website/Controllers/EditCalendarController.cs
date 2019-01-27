namespace MvcForum.Web.Controllers
{
    using MvcForum.Web.ViewModels.Calendar;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Core.Interfaces;
    using Core.Interfaces.Services;
    using Core.Models.Entities;
    using MvcForum.Core.Data.Context;
    using Module = Core.Models.Entities.Module;

    public class EditCalendarController : BaseController
    {
        public EditCalendarController(ILoggingService loggingService, IMembershipService membershipService,
            ILocalizationService localizationService, IRoleService roleService, ISettingsService settingsService,
            IPostService postService, IUploadedFileService uploadedFileService, ICacheService cacheService,
            IMvcForumContext context)
            : base(loggingService, membershipService, localizationService, roleService,
                settingsService, cacheService, context)
        {

        }

        //GET: Calendar/EditCalendarEvent
        public ActionResult EditCalendarEvent()
        {
            return View();
        }

        //POST: AddEvents
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCalendarEvent(CalendarEdit CalendarIn)
        {
            //add events
            using (MvcForumContext db = new MvcForumContext())
            {
                var query = from c in db.Module
                            select new
                            {
                                c.ModId,
                                c.ModName,
                                c.ModEnd
                            };
                bool checkVal = false; // checking if module is in the modList
                //retrieve the list of modules
                List<Module> modList = new List<Module>();
                if (query != null)
                {
                    foreach (var item in query)
                    {
                        Module addable = new Module
                        {
                            ModId = item.ModId,
                            ModName = item.ModName,
                            ModEnd = item.ModEnd
                        };
                        modList.Add(addable);
                    }
                    foreach (var modVals in modList)
                    {
                        if (modVals.ModId == CalendarIn.Module || CalendarIn.Module == null)
                        {
                            checkVal = true;
                            break;
                        }
                    }
                    if (checkVal == true)
                    {
                        //add in of events

                        Event calEvent = new Event
                        {
                            EventType = CalendarIn.EventType,
                            Date = CalendarIn.Date,
                            Module = CalendarIn.Module,
                            Description = CalendarIn.Description,
                            TimeStart = CalendarIn.TimeStart,
                            TimeEnd = CalendarIn.TimeEnd,
                            EventName = CalendarIn.EventName
                        };

                        bool checker = true;
                        try
                        {
                            db.Event.Add(calEvent);
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            checker = false;
                        }

                        if (checker)
                        {
                            return RedirectToAction("StudentCalendar", "Calendar");
                        }
                        else
                        {
                            //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                            return RedirectToAction("Shared", "Error");
                        }
                    }
                    else
                    {
                        //module list do not match with input
                        return RedirectToAction("Shared", "Error");
                    }
                }
                return RedirectToAction("Shared", "Error");

            }


            //return RedirectToAction("EditCalendarEvent", "Calendar");

            //manual mapping

            //cs.Open();
            //cs.Close();
        }
    }
}