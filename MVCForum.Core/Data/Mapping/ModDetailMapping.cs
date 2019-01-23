namespace MvcForum.Core.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Models.Entities;

    public class ModDetailMapping : EntityTypeConfiguration<ModDetail>
    {
        public ModDetailMapping()
        {
            HasKey(x => x.PersonId);
            Property(x => x.PersonName).IsRequired();
            Property(x => x.PersonType).IsRequired();
            Property(x => x.ModuleId).IsRequired();

        }

    }
}
