using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcForum.Core.Models.Entities
{
    public partial class ModDetail
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public string PersonType { get; set; }
        public string ModuleId { get; set; }

    }
}