using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcForum.Core.Models
{
    public class SY_ModuleViewModel
    {
        [Required]
        public string ModId { get; set; }
        [Required]
        public string ModuleName { get; set; }
        [Required]
        public DateTime ModStart { get; set; }
        [Required]
        public DateTime ModEnd { get; set; }
        [Required]
        public string[] studList { get; set; }
        [Required]
        public string[] lectList { get; set; }
    }


}