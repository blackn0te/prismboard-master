namespace MvcForum.Web.ViewModels.Module
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using MvcForum.Core.Models.Entities;

    public class ModuleSpecificViewModel
    {
        public List<Lecturer> lectList;
        public List<Week> weekList;
        public Module module;

        public ModuleSpecificViewModel()
        {
            lectList = new List<Lecturer>();
            weekList = new List<Week>();
        }
    }

    public class Week
    {
        public List<Materials> matlist;
        public Week()
        {
            matlist = new List<Materials>();
        }

    }
}