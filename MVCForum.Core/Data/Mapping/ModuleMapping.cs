namespace MvcForum.Core.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Models.Entities;

    public class ModuleMapping : EntityTypeConfiguration<Module>
    {
        public ModuleMapping()
        {
            HasKey(x => x.ModId);
            Property(x => x.ModName).IsRequired();
            Property(x => x.ModStart).IsRequired();
            Property(x => x.ModEnd).IsRequired();

        }

    }
}
