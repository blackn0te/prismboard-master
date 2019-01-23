using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcForum.Core.Models
{
    public class SY_ShowModules
    {
    }

    public class Materials
    {
        public int MatId { get; set; }
        public string Name { get; set; }
        public string ModId { get; set; }
        public int Week { get; set; }
        public string Type { get; set; }
        public Boolean isSubmitable { get; set; }
        public Boolean isTest { get; set; }
        public string FieLink { get; set; }
    }

    public class SubmitableFile
    {
        public int SubmitableId { get; set; }
        public int MatId { get; set; }
        public DateTime SubDate { get; set; }
        public string Note { get; set; }
    }

    public class SubmitedContent
    {
        public int SubmitedId { get; set; }
        public int SubmitableId { get; set; }
        public string StudId { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string FileLink { get; set; }
    }

    public class SubmitedTest
    {
        public int SubmitId { get; set; }
        public int TestDetId { get; set; }
        public int Score { get; set; }
        public string StudId { get; set; }
        public int AttemptNum { get; set; }
    }

    
    //Data Access Objects

}