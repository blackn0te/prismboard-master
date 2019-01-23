using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcForum.Core.Models.Entities
{
    public partial class SubmittedTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubmitId { get; set; }
        public int TestDetId { get; set; }
        public int Score { get; set; }
        public string StudId { get; set; }
        public int AttemptNum { get; set; }
    }
}
