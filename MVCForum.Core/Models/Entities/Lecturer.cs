using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MvcForum.Core.Utilities;

namespace MvcForum.Core.Models.Entities
{
    public partial class Lecturer
    {

        public Lecturer()
        {
            LecturerId = GuidComb.GenerateComb();
        }

        public Guid LecturerId { get; set; }
        public string LectName { get; set; }
        public string LectEmail { get; set; }
        public int LectNo { get; set; }
        public string ModuleGrp { get; set; }

    }
}
