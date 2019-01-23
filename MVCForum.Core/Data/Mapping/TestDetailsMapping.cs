namespace MvcForum.Core.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Models.Entities;

    public class TestDetailsMapping : EntityTypeConfiguration<TestDetails>
    {
        public TestDetailsMapping()
        {
            HasKey(x => x.TestDetId);
            Property(x => x.ModueleId).IsRequired();
            Property(x => x.MatId).IsRequired();
            Property(x => x.Marks).IsRequired();
            Property(x => x.Percentage).IsRequired();
            Property(x => x.JsonLink).IsRequired();
            Property(x => x.Attempts).IsRequired();
            Property(x => x.TimeLimit).IsRequired();

        }

    }
}