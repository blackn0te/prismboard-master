namespace MvcForum.Core.Models.Entities
{
    using System;
    using Interfaces;
    using Utilities;

    public partial class StudentEvent : IBaseEntity
    {
        public StudentEvent()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public string AdminNo { get; set; }
        public string EventId { get; set; }

    }
}
