using System.Data.Entity.ModelConfiguration;
using POEItemFilter.Models.ItemsDB;

namespace POEItemFilter.EntityConfiguration
{
    public class ItemDBConfiguration : EntityTypeConfiguration<ItemDB>
    {
        public ItemDBConfiguration()
        {
            Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(i => i.Level)
                .IsOptional();

            Property(i => i.BaseType)
                .IsRequired();

            Property(i => i.Type)
                .IsRequired();

            Property(i => i.Attribute1)
                .IsOptional();

            Property(i => i.Attribute2)
                .IsOptional();
        }
    }
}