namespace MvcForum.Core.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Models.Entities;

    public class SubmittableFileMapping : EntityTypeConfiguration<SubmittableFile>
    {
        public SubmittableFileMapping()
        {
            HasKey(x => x.SubmittableId);
            Property(x => x.MatId).IsRequired();
            Property(x => x.SubDate).IsRequired();
            Property(x => x.Note).IsOptional();

        }

    }
}
