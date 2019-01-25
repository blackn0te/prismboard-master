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
    using System.Net;

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

            Event eventList = new Event();
            //db.Events.All
            
            using (MvcForumContext db = new MvcForumContext())
            {
                var query = from c in db.Event
                            join s in db.StudentEvent 
                            on c.Id equals s.EventId
                            where c.Id == s.EventId
                            select new
                             {
                                 c.EventName,
                                 c.Description,
                                 c.Date,
                                 c.TimeStart,
                                 c.TimeEnd
                             };
               
                List<CalendarModel> eList = new List<CalendarModel>();

                if (query != null)
                {
                    foreach (var item in query)
                    {
                        CalendarModel addable = new CalendarModel
                        {
                            Date = item.Date,
                            EventName = item.EventName,
                            EventCode = item.Date,
                            TimeStart = item.TimeStart,
                            TimeEnd = item.TimeEnd,
                            Description = item.Description
                        };

                        eList.Add(addable);
                    }
                }
                else
                {

                }
                return View(eList);

            }

            //string query = "SELECT * FROM event WHERE 
            //string connection = ConfigurationManager.ConnectionStrings["PrismBoardUserDBEntities1"].ConnectionString;


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
            MvcForumContext db = new MvcForumContext();
            Event calEvent = new Event
            {
                EventType = CalendarIn.EventType,
                Date = CalendarIn.Date,
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            //return RedirectToAction("EditCalendarEvent", "Calendar");
            
            //manual mapping
            
            //cs.Open();
            //cs.Close();
        }
    }
}