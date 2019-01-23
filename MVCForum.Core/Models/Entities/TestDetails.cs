namespace MvcForum.Core.Models.Entities
{
    using System;
    using Interfaces;
    using Utilities;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TestDetails
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TestDetId { get; set; }
        public string ModueleId { get; set; }
        public int MatId { get; set; }
        public int Marks { get; set; }
        public int Percentage { get; set; }
        public string JsonLink { get; set; }
        public int Attempts { get; set; }
        public int TimeLimit { get; set; }

    }

}