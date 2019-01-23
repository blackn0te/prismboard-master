using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcForum.Core.Models.Entities
{
    public partial class Lecturer
    {

//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int LecturerId { get; set; }
        public string LectName { get; set; }
        public string LectEmail { get; set; }
        public int LectNo { get; set; }
        public string ModuleGrp { get; set; }
        public string ModuleId { get; set; }

    }
}
