namespace MvcForum.Core.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Models.Entities;

    public class EventMapping : EntityTypeConfiguration<Event>
    {
        public EventMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Date).IsRequired().HasMaxLength(10);
            Property(x => x.Description).IsRequired().HasMaxLength(100);
            Property(x => x.TimeStart).IsRequired();
            Property(x => x.TimeEnd).IsRequired();
            Property(x => x.Module).IsOptional().HasMaxLength(20);
            Property(x => x.EventType).IsRequired();
            
        }
    }
}
