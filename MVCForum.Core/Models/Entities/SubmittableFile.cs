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
    public partial class SubmittableFile
    {
        public SubmittableFile()
        {
            SubmittableId = GuidComb.GenerateComb();
        }


        public Guid SubmittableId { get; set; }
        public Guid MatId { get; set; }
        public DateTime SubDate { get; set; }
        public string Note { get; set; }

    }
}
