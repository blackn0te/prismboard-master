namespace MvcForum.Core.Models.Entities
{
    using System;
    using Interfaces;
    using Utilities;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SubmittedContent
    {
        public SubmittedContent()
        {
            SubmittedId = GuidComb.GenerateComb();
        }


        public Guid SubmittedId { get; set; }
        public int SubmitableId { get; set; }
        public string StudId { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string FileLink { get; set; }

    }

}
