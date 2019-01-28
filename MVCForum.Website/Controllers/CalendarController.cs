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

    public class CalendarController : BaseController
    {

        public CalendarController(ILoggingService loggingService, IMembershipService membershipService,
            ILocalizationService localizationService, IRoleService roleService, ISettingsService settingsService,
            IPostService postService, IUploadedFileService uploadedFileService, ICacheService cacheService,
            IMvcForumContext context)
            : base(loggingService, membershipService, localizationService, roleService,
                settingsService, cacheService, context)
        {

        }

        // GET: Calendar/StudentCalendar
        public ActionResult StudentCalendar()
        {
            //first get userid through method ontop, string userid
            //Second, mapping/sql query of events where studentid = userid ^
            //return list of events created by sql in view() method
            //populate cshtml with model returned

            using (MvcForumContext db = new MvcForumContext())
            {
                //add in link between user and the event
                //db.Students
                try
                {
                    //Get one object from Identiy
                    string Username = User.Identity.Name;
                    Student student = db.Students.Where(s => s.Name == Username).First();
                    List<StudentEvent> StudEventList = db.StudentEvent.Where(s => s.AdminNo == student.AdminNo).ToList();

                    List<Event> eventList = new List<Event>();
                    foreach (StudentEvent potatoe in StudEventList)
                    {
                        Event test = db.Event.Where(a => a.Id == potatoe.EventId).First();
                        eventList.Add(test);
                    }

                    List<CalendarModel> eList = new List<CalendarModel>();

                    return View(eventList);
                }
                catch
                {
                    return View();
                }
                
            }
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
        }
    }
}