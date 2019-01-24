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
    public partial class ModDetail
    {
        public ModDetail() {
            PersonId = GuidComb.GenerateComb();
        }

        public Guid PersonId { get; set; }
        public string PersonName { get; set; }
        public string PersonType { get; set; }
        public string ModuleId { get; set; }

    }
}