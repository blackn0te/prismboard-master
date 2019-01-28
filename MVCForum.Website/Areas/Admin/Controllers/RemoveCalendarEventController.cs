using MvcForum.Core.Data.Context;
using MvcForum.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcForum.Web.Areas.Admin.Controllers
{
    public class RemoveCalendarEventController : Controller
    {
        // GET: Admin/RemoveCalendarEvent
        public ActionResult RemoveCalendarEvent()
        {

            MvcForumContext db = new MvcForumContext();

            List<Event> EventList = db.Event.ToList();

            return View(EventList);
        }


        public ActionResult EditEvent(string id)
        {
            MvcForumContext db = new MvcForumContext();

            if (id != null)
            {
                Event checker = db.Event.First(a => a.Id.ToString() == id);

                if (checker != null)
                {
                    List<Event> unsortList = db.Event.ToList();
                    List<string> examList = new List<string>();
                    List<string> assignmentList = new List<string>();
                    List<string> othersList = new List<string>();
                    List<string> comTestList = new List<string>();

                    foreach (Event s in unsortList)
                    {
                        if (s.Id.ToString().Equals(id))
                        {
                            if (s.EventType.Equals("EX"))
                                examList.Add(s.EventName);
                            if (s.EventType.Equals("AS"))
                                assignmentList.Add(s.EventName);
                            if (s.EventType.Equals("OT"))
                                othersList.Add(s.EventName);
                            if (s.EventType.Equals("CT"))
                                comTestList.Add(s.EventName);
                        }
                        else
                        continue;
                    }

                    dynamic model = new ExpandoObject();
                    model.Event = checker;
                    model.exlist = examList;
                    model.aslist = assignmentList;
                    model.otlist = othersList;
                    model.ctlist = comTestList;
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

        

        [HttpGet]
        public ActionResult DeleteModule(string name)
        {
            MvcForumContext db = new MvcForumContext();

            if (name != null)
            {
                Module checker = db.Module.First(a => a.ModId == name);

                if (checker != null)
                {
                    return View(checker);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEvent(string id, string confirm)
        {

            MvcForumContext db = new MvcForumContext();

            if (id != null)
            {
                Event checker = db.Event.First(a => a.Id.ToString() == id);

                if (checker != null)
                {
                    db.Event.Remove(checker);
                    db.StudentEvent.RemoveRange(db.StudentEvent.Where(c => c.EventId.ToString() == id));
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