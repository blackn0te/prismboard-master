
namespace MvcForum.Web.ViewModels.Calendar
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

    public class RemoveCalendarEvent
    {
        public string EventIdVal { get; set; }
        public string EventNameVal { get; set; }
    }

    public class DeleteEvent
    {
        public string Id { get; set; }
        public string EventType { get; set; }
        public string Module { get; set; }
        public string Date { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string Description { get; set; }
        public string EventName { get; set; }
    }
}