namespace MvcForum.Core.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Models.Entities;

    public class StudentEventMapping : EntityTypeConfiguration<StudentEvent>
    {
        public StudentEventMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.AdminNo).IsRequired();
            Property(x => x.EventId).IsRequired();

        }
    }
}