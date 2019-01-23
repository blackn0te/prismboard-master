namespace MvcForum.Core.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Models.Entities;

    public class SubmittedTestMapping : EntityTypeConfiguration<SubmittedTest>
    {
        public SubmittedTestMapping()
        {
            HasKey(x => x.SubmitId);
            Property(x => x.TestDetId).IsRequired();
            Property(x => x.Score).IsRequired();
            Property(x => x.StudId).IsRequired();
            Property(x => x.AttemptNum).IsRequired();

        }

    }
}
