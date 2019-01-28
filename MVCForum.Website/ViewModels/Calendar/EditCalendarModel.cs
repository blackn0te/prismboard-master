using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcForum.Web.ViewModels.Calendar
{
    public class CalendarEdit
    {
        public string EventType { get; set; }

        public IEnumerable<SelectListItem> EventCodeList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Text = "Select an event type", Value = null},
                    new SelectListItem { Text = "Others", Value = "OT"},
                    new SelectListItem { Text = "Examination", Value = "EX"},
                    new SelectListItem { Text = "Assignment", Value = "AS"},
                    new SelectListItem { Text = "Common Test", Value = "CT"}
                };
            }
        }

        public string Module { get; set; }
        public string Date { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string Description { get; set; }
        public string EventName { get; set; }
    }
    
}