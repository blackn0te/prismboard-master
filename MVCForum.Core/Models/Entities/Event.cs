namespace MvcForum.Core.Models.Entities
{
    using System;
    using Interfaces;
    using Utilities;

    public partial class Event : IBaseEntity
    {
        public Event()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string TimeStart {get; set;}
        public string TimeEnd { get; set; }
        public string Module { get; set; }
        public string EventType { get; set; }

    }
}
