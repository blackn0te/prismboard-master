namespace MvcForum.Core.Data.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Models.Entities;

    public class StudentMapping : EntityTypeConfiguration<Student>
    {
    public StudentMapping()
        {
            HasKey(x => x.AdminNo);
            Property(x => x.Name).IsRequired();
            Property(x => x.Email).IsRequired();
            Property(x => x.AdminNo).IsRequired();
            Property(x => x.PemGroup).IsRequired();
            Property(x => x.Pem).IsRequired();
            Property(x => x.StudentCourse).IsRequired();
            Property(x => x.StudentSchool).IsRequired();
            Property(x => x.AltEmail).IsOptional();
            Property(x => x.Pem).IsRequired();
            Property(x => x.PemGroup).IsRequired();

        }

    }
}