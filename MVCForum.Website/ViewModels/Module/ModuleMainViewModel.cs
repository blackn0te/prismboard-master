namespace MvcForum.Web.ViewModels.Module
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using MvcForum.Core.Models.Entities;

    public class ModuleMainViewModel
    {
        public List<Module> currentModule { get; set; }
        public List<Module> achievedModule { get; set; }
        public List<Module> futureModule { get; set; }

        public ModuleMainViewModel() {
            currentModule = new List<Module>();
            achievedModule = new List<Module>();
            futureModule = new List<Module>();
        }
    }
}