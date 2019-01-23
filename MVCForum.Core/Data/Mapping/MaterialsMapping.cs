namespace MvcForum.Core.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Models.Entities;

    public class MaterialsMapping : EntityTypeConfiguration<Materials>
    {
        public MaterialsMapping()
        {
            HasKey(x => x.MatId);
            Property(x => x.Name).IsRequired();
            Property(x => x.ModId).IsRequired();
            Property(x => x.Week).IsRequired();
            Property(x => x.Type).IsRequired();
            Property(x => x.IsSubmittable).IsRequired();
            Property(x => x.IsTest).IsRequired();
            Property(x => x.FileLink).IsOptional();


        }

    }
}
