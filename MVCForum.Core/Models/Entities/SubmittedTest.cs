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
    public partial class SubmittedTest
    {

        public SubmittedTest()
        {
            SubmitId = GuidComb.GenerateComb();
        }

        public Guid SubmitId { get; set; }
        public Guid TestDetId { get; set; }
        public int Score { get; set; }
        public string StudId { get; set; }
        public int AttemptNum { get; set; }
    }
}
