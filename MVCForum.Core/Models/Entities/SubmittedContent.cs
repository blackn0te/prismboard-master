namespace MvcForum.Core.Models.Entities
{
    using System;
    using Interfaces;
    using Utilities;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SubmittedContent
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubmittedId { get; set; }
        public int SubmitableId { get; set; }
        public string StudId { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string FileLink { get; set; }

    }

}
