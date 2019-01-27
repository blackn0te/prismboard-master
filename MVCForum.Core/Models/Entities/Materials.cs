namespace MvcForum.Core.Models.Entities
{
    using System;
    using Interfaces;
    using Utilities;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Materials
    {
        public Materials()
        {
            MatId = GuidComb.GenerateComb();
        }


        public Guid MatId { get; set; }
        public string Name { get; set; }
        public string ModId { get; set; }
        public int Week { get; set; }
        public string Type { get; set; }
        public Boolean IsSubmittable { get; set; }
        public Boolean IsTest { get; set; }
        public string FileLink { get; set; }

    }
}
