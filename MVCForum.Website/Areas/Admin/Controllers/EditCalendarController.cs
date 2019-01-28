namespace MvcForum.Web.Areas.Admin.Controllers
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
    using MvcForum.Core.Constants;
    using System.Net;

    [Authorize(Roles = Constants.AdminRoleName)]
    public class EditCalendarController : AdminController
    {
        public EditCalendarController(ILoggingService loggingService,
            IMembershipService membershipService,
            ILocalizationService localizationService,
            ISettingsService settingsService, IMvcForumContext context)
            : base(loggingService, membershipService, localizationService, settingsService, context)
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
                bool checkNull = true;
                //retrieve the list of modules
                List<StudentEvent> studEv = new List<StudentEvent>();
                List<string> adminList = new List<string>();
                List<Module> modList = new List<Module>();
                if (query != null)
                {
                    //add values into modList
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
                    //take all modlist items and compare to module
                    foreach (var modVals in modList)
                    {
                        if (modVals.ModId == CalendarIn.Module)
                        {
                            checkVal = true;
                            List<ModDetail> moduleList =  db.ModDetails.Where(m => m.ModuleId == modVals.ModId).ToList();
                            List<Student> StudList = new List<Student>();
                            foreach(var MLI in moduleList)
                            {
                                try
                                {
                                    Student testStud = db.Students.Where(s => s.Name == MLI.PersonName).First();
                                    StudList.Add(testStud);
                                }
                                catch
                                {
                                    //not the person
                                }
                            }
                            foreach(var SLI in StudList)
                            {
                                adminList.Add(SLI.AdminNo);
                            }

                            break;
                        }
                        else if (CalendarIn.Module == null)
                        {
                            //not module related
                            checkVal = true;
                            checkNull = false;
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
                        db.Event.Add(calEvent);
                        db.SaveChanges();
                        try
                        {
                           if (checkNull == true)
                            {
                                
                                Event test = db.Event.Where(a => a.EventName.ToString() == CalendarIn.EventName.ToString()).First();//errors

                                foreach (var adminNoVal in adminList)
                                {
                                    var strin = test.Id.ToString();
                                    StudentEvent studEvent = new StudentEvent
                                    {
                                        AdminNo = adminNoVal,
                                        Id = strin
                                    };
                                    db.StudentEvent.Add(studEvent);
                                    db.SaveChanges();
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            checker = false;
                            Console.WriteLine(e);
                        }

                        if (checker == true)
                        {
                            //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                            return RedirectToAction("AddEventSucces", "EditCalendar");
                            //return RedirectToAction("StudentCalendar", "Calendar");
                        }
                        else
                        {
                            //error should be here
                            //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                            return RedirectToAction("Shared", "Error");
                        }
                    }
                    else
                    {
                        //module list do not match with input
                        return RedirectToAction("Shared", "Error");
                        //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                }
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Shared", "Error");

            }


            //return RedirectToAction("EditCalendarEvent", "Calendar");

            //manual mapping

            //cs.Open();
            //cs.Close();
        }

        public ActionResult AddEventSuccess()
        {
            return View();
        }

        //GET: Calendar/EditCalendarEvent
        public ActionResult RemoveCalendarEvent()
        {
            return View();
        }

        //POST: AddEvents
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveCalendarEvent(CalendarEdit CalendarIn)
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