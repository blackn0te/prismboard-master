namespace MvcForum.Web.ViewModels.Module
{
    using MvcForum.Core.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class ModuleSubmittedContentsViewModel
    {
        public SubmittableFile file { get; set; }
        public Materials mat { get; set; }
        public List<SubmittedContent> submitedList {get; set;}

        public ModuleSubmittedContentsViewModel() {
            submitedList = new List<SubmittedContent>();
        }
    }
}