namespace MvcForum.Core.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Models.Entities;

    public class LecturerMapping : EntityTypeConfiguration<Lecturer>
    {
        public LecturerMapping()
        {
            HasKey(x => x.LecturerId);
            Property(x => x.LecturerId).IsRequired();
            Property(x => x.LectName).IsRequired();
            Property(x => x.LectEmail).IsRequired();
            Property(x => x.LectNo).IsRequired();
            Property(x => x.ModuleGrp).IsOptional();

        }

    }
}
