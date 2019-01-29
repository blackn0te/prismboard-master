namespace MvcForum.Web.ViewModels.Module
{
    using MvcForum.Core.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class SubmitFileViewModel
    {
        public SubmittableFile File { get; set; }
        public Materials Mat { get; set; }
    }
}