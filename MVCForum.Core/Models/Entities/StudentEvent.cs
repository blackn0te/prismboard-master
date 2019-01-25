namespace MvcForum.Core.Models.Entities
{
    using System;
    using Interfaces;
    using Utilities;

    public partial class StudentEvent 
    {
        public StudentEvent()
        {
            EventId = GuidComb.GenerateComb();
        }
        public Guid EventId { get; set; }
        public string AdminNo { get; set; }
        public string Id { get; set; }

    }
}
