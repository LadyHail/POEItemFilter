using System.Data.Entity.ModelConfiguration;
using POEItemFilter.Models.ItemsDB;

namespace POEItemFilter.EntityConfiguration
{
    public class ItemDBConfiguration : EntityTypeConfiguration<ItemDB>
    {
        public ItemDBConfiguration()
        {
            ToTable("ItemsDB");

            Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(i => i.Level)
                .IsOptional();


        }
    }
}