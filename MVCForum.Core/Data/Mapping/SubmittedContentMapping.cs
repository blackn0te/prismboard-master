namespace MvcForum.Core.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Models.Entities;

    public class SubmittedContentMapping : EntityTypeConfiguration<SubmittedContent>
    {
        public SubmittedContentMapping()
        {
            HasKey(x => x.SubmittedId);
            Property(x => x.SubmitableId).IsRequired();
            Property(x => x.StudId).IsRequired();
            Property(x => x.SubmittedDate).IsRequired();
            Property(x => x.FileLink).IsRequired();

        }

    }
}