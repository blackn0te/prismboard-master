using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcForum.Core.Models.Entities
{
    public partial class Module
    {

        public string ModId { get; set; }
        public string ModName { get; set; }
        public DateTime ModStart { get; set; }
        public DateTime ModEnd { get; set; }

    }
}
